#!/usr/bin/env python3

import argparse
import mmap
import multiprocessing
import os
import logging
from datetime import datetime, timezone
from decimal import Decimal

import pandas as pd
import requests
import sqlalchemy


def main():
  try:
    logging.basicConfig(
      level=logging.INFO,
      format='%(asctime)s - %(levelname)s - %(message)s',
    )

    default_cpu_count = os.cpu_count()
    default_cpu_count = 1 if default_cpu_count is None else default_cpu_count

    parser = argparse.ArgumentParser(
      description='Import/export/push of measurement and aggregate data', )
    parser.add_argument(
      '--batch-size',
      type=int,
      default=10000,
      help='Batch size for manipulating data',
    )
    parser.add_argument(
      '--workers',
      type=int,
      default=default_cpu_count,
      help='Max workers for manipulating data',
    )

    subparsers = parser.add_subparsers(dest='command')
    subparsers.required = True

    push_parser = subparsers.add_parser(
      "push",
      description='Push data to the server',
    )
    push_parser.add_argument(
      'path',
      type=str,
      help='Path to the CSV file',
    )
    push_parser.add_argument(
      '--url',
      type=str,
      default='http://localhost:5000/iot/push/messenger',
      help='URL to push data',
    )

    export_parser = subparsers.add_parser(
      "export",
      description='Export data to CSV',
    )
    export_parser.add_argument(
      'connection_string',
      type=str,
      help='PostgreSQL connection string',
    )
    export_parser.add_argument(
      'table',
      type=str,
      help='Table name to export',
    )
    export_parser.add_argument(
      'path',
      type=str,
      help='Path to the CSV file',
    )
    export_parser.add_argument(
      '--date-from',
      type=datetime.fromisoformat,
      help='Date from to export',
    )
    export_parser.add_argument(
      '--date-to',
      type=datetime.fromisoformat,
      help='Date to to export',
    )

    import_parser = subparsers.add_parser(
      "import",
      description='Import data from CSV',
    )
    import_parser.add_argument(
      'connection_string',
      type=str,
      help='PostgreSQL connection string',
    )
    import_parser.add_argument(
      'table',
      type=str,
      help='Table name to import',
    )
    import_parser.add_argument(
      'path',
      type=str,
      help='Path to the CSV file',
    )

    args = parser.parse_args()

    if args.command == 'push':
      push(
        args.path,
        args.url,
        args.workers,
        args.batch_size,
      )
    elif args.command == 'export':
      export_to_csv(
        args.connection_string,
        args.table,
        args.path,
        args.workers,
        args.batch_size,
        args.date_from,
        args.date_to,
      )
    elif args.command == 'import':
      import_from_csv(
        args.path,
        args.connection_string,
        args.table,
        args.workers,
        args.batch_size,
      )
    else:
      logging.error('Invalid command')
      exit(1)
  except KeyboardInterrupt:
    exit(0)


def process_export_batch(
  connection_string,
  table,
  date_from,
  date_to,
  offset,
  limit,
  path,
):
  """
    Process a batch of rows and write to the CSV file.

    Args:
        connection_string (str): The database connection string.
        table (str): The name of the table to export.
        offset (int): The offset to start reading from.
        limit (int): The max number of rows to read.
        path (str): The path to the CSV file.
  """
  try:
    engine = sqlalchemy.create_engine(connection_string)
    query = (
      f"SELECT * FROM {table} " +
      f"WHERE timestamp >= '{date_from.isoformat()}' AND timestamp < '{date_to.isoformat()}' "
      + f"LIMIT {limit} OFFSET {offset}")
    chunk = pd.read_sql_query(query, engine)
    logging.info(f"Processing batch at {offset} limit {limit} "
                 f"with {len(chunk)} rows...")
    chunk = chunk.map(convert_sci_notation)
    mode = 'w' if offset == 0 else 'a'
    header = offset == 0
    chunk.to_csv(path, mode=mode, header=header, index=False)
  except KeyboardInterrupt:
    pass
  except Exception as e:
    logging.error(f"Error processing batch at {offset} limit {limit}: {e}")


def wrap_process_export_batch(args):
  process_export_batch(*args)


def export_to_csv(
  connection_string,
  table,
  path,
  workers=1,
  batch_size=10000,
  date_from=datetime.min,
  date_to=datetime.max,
):
  """
    Export data from a database table to a CSV file in parallel batches.

    Args:
        connection_string (str): The database connection string.
        table (str): The name of the table to export.
        path (str): The path to the CSV file.
        workers (int): The number of parallel workers.
        batch_size (int): The number of rows per batch.
  """
  engine = sqlalchemy.create_engine(connection_string)
  query = (
    f"SELECT COUNT(*) FROM {table} " +
    f"WHERE timestamp >= '{date_from.isoformat()}' AND timestamp < '{date_to.isoformat()}'"
  )
  total_rows = pd.read_sql_query(query, engine).iloc[0, 0]
  num_batches = (total_rows + batch_size - 1) // batch_size
  logging.info(f"Exporting {total_rows} rows from {table} to {path} "
               f"in {num_batches} batches with {workers} workers...")

  with multiprocessing.Pool(workers) as executor:
    try:
      executor.map(
        wrap_process_export_batch,
        [(
          connection_string,
          table,
          date_from,
          date_to,
          i * batch_size,
          batch_size,
          path,
        ) for i in range(num_batches)],
      )
    except KeyboardInterrupt:
      executor.terminate()


def process_import_batch(
  path,
  start,
  end,
  connection_string,
  table,
):
  """
    Process a batch of rows from the CSV file and insert into the database.

    Args:
        path (str): The path to the CSV file.
        start (int): The starting row.
        end (int): The ending row.
        connection_string (str): The database connection string.
        table (str): The name of the table to insert into.
  """
  try:
    engine = sqlalchemy.create_engine(connection_string)
    logging.info(f"Processing batch from {start} to {end}...")
    chunk = pd.read_csv(path, skiprows=start, nrows=end - start)
    chunk.columns = pd.read_csv(path, nrows=0).columns
    chunk.to_sql(table, engine, if_exists='append', index=False)
    logging.info(f"Successfully processed batch from {start} to {end}")
  except KeyboardInterrupt:
    pass
  except Exception as e:
    logging.error(f"Error processing batch from {start} to {end}: {e}")


def wrap_process_import_batch(args):
  process_import_batch(*args)


def import_from_csv(
  path,
  connection_string,
  table,
  workers=1,
  batch_size=10000,
):
  """
    Import data from a CSV file to a database table in parallel batches.

    Args:
        path (str): The path to the CSV file.
        connection_string (str): The database connection string.
        table (str): The name of the table to import to.
        workers (int): The number of parallel workers.
        batch_size (int): The number of rows per batch.
  """
  total_rows = mapcount(path) - 1
  num_batches = (total_rows + batch_size - 1) // batch_size
  logging.info(f"Importing {total_rows} rows from {path} to {table} "
               f"in {num_batches} batches with {workers} workers...")

  with multiprocessing.Pool(workers) as executor:
    try:
      executor.map(
        wrap_process_import_batch,
        [(
          path,
          i * batch_size,
          (i + 1) * batch_size,
          connection_string,
          table,
        ) for i in range(num_batches)],
      )
    except KeyboardInterrupt:
      executor.terminate()


def process_push_batch(
  path,
  start,
  end,
  url,
):
  """
    Process a batch of rows from the CSV file and push to the server.

    Args:
        path (str): The path to the CSV file.
        start (int): The starting row.
        end (int): The ending row.
        url (str): The server URL to push data to.
  """
  try:
    logging.info(f"Processing batch from {start} to {end}...")
    df = pd.read_csv(path, skiprows=start, nrows=end - start)
    df.columns = pd.read_csv(path, nrows=0).columns
    df = df.dropna(subset=['timestamp', 'meter_id'])
    format_columns(df, ['timestamp', 'meter_id'])
    batch_data = df.to_dict('records')

    request_data = {
      "Timestamp":
      datetime.now(timezone.utc).isoformat(),
      "Measurements": [{
        "MeterId":
        row['meterid'],
        "Timestamp":
        datetime.fromisoformat(row['timestamp']).isoformat(),
        "Data": {
          key: value
          for key, value in row.items() if key not in ['meterid', 'timestamp']
        }
      } for row in batch_data]
    }

    logging.info(
      f"Pushing {len(batch_data)} measurements at {start} to {end}...")
    while True:
      try:
        response = requests.post(url, json=request_data)
        response.raise_for_status()
        logging.info(f"Successfully pushed {len(batch_data)} measurements "
                     f"at {start} to {end}")
        break
      except requests.RequestException as e:
        logging.error(f"Failed pushing batch at {start} to {end} because {e}. "
                      "Trying again...")
        continue
  except KeyboardInterrupt:
    pass
  except Exception as e:
    logging.error(f"Error processing batch from {start} to {end}: {e}")


def wrap_process_push_batch(args):
  process_push_batch(*args)


def push(
  path,
  url="http://localhost:5000/iot/push/messenger",
  workers=1,
  batch_size=10000,
):
  """
    Push data from a CSV file to a server in parallel batches.

    Args:
        path (str): The path to the CSV file.
        url (str): The server URL to push data to.
        workers (int): The number of parallel workers.
        batch_size (int): The number of rows per batch.
  """
  length = mapcount(path) - 1
  num_batches = (length + batch_size - 1) // batch_size
  logging.info(f"Importing {length} measurements in {num_batches} batches "
               f"with {workers} workers...")

  with multiprocessing.Pool(workers) as executor:
    try:
      executor.map(
        wrap_process_push_batch,
        [(path, i * batch_size, (i + 1) * batch_size, url)
         for i in range(num_batches)],
      )
    except KeyboardInterrupt:
      executor.terminate()


def format_column(snake_str: str, specific_props=None):
  """
    Format a column to match a push request json key.

    `specific_props` will be formatted without any underscores and other props will be
    formatted with the last underscore remaining.
    This is expected because the part after the last underscore is the measurement unit.

    Args:
        snake_str (str): The snake_case column name string to format.
        specific_props (list): List of specific properties to format without underscores.

    Returns:
        str: The formatted string.
  """
  if specific_props is None:
    specific_props = []

  if snake_str in specific_props:
    return snake_str.replace('_', '')

  components = snake_str.rsplit('_', 1)
  if len(components) == 2:
    return f"{components[0].replace('_', '')}_{components[1]}"
  else:
    return components[0].replace('_', '')


def format_columns(df, specific_props=None):
  """
    Format the column names of a DataFrame to match a push request json key.

    Args:
        df (pd.DataFrame): The DataFrame whose columns to format.
        specific_props (list): List of specific properties to format without underscores.

    Returns:
        pd.DataFrame: The DataFrame with formatted columns.
  """
  df.columns = [format_column(col, specific_props) for col in df.columns]
  return df


def convert_sci_notation(value):
  """
    Convert a scientific notation string to a float.

    Pandas has trouble reading scientific notation, so we convert that
    to normal numbers first.

    Args:
        value (str): The value to convert.

    Returns:
        float: The converted value.
  """
  if 'e+' in str(value):
    split_val = str(value).split('e+')
    return float(Decimal(split_val[0]) * (Decimal(10)**int(split_val[1])))
  return value


def mapcount(filename):
  """
    Count the number of lines in a file using memory mapping for efficiency.

    Args:
        filename (str): The path to the file.

    Returns:
        int: The number of lines in the file.
  """
  with open(filename, "r+") as f:
    buf = mmap.mmap(f.fileno(), 0)
    lines = 0
    readline = buf.readline
    while readline():
      lines += 1
  return lines


if __name__ == '__main__':
  main()
