# Requirements

Currently only Abb B2x and Schneider iEM3xxx devices are supported. Manuals for
these devices were analyzed to determine the interfacing strategy.

The final solution involves using an on-premise Raspberry PI Model 4B to read
measurement data from devices or gateways called a messenger. For now, a single
messenger is planned to be used for each location, but this can be expanded to
multiple devices for a location if needed for redundancy.

The messenger reads data from the devices using Modbus TCP/IP and sends the data
to the server via a simple REST API. The server is a cloud-based server that is
hosted on Azure.

The Modbus registers that are read from each device type were determined from
their manuals as well as registers needed to configure current device tariff.

For Abb B2x devices, the following registers are read:

| Address | Kind                     | Name                                |
| ------- | ------------------------ | ----------------------------------- |
| 0x5B00  | u32 (Multiplied by 0.1)  | Voltage L1 (Volts)                  |
| 0x5B02  | u32 (Multiplied by 0.1)  | Voltage L2 (Volts)                  |
| 0x5B04  | u32 (Multiplied by 0.1)  | Voltage L3 (Volts)                  |
| 0x5B0C  | u32 (Multiplied by 0.01) | Current L1 (Amps)                   |
| 0x5B0E  | u32 (Multiplied by 0.01) | Current L2 (Amps)                   |
| 0x5B10  | u32 (Multiplied by 0.01) | Current L3 (Amps)                   |
| 0x5B16  | s32 (Multiplied by 0.01) | Active Power L1 (Watts)             |
| 0x5B18  | s32 (Multiplied by 0.01) | Active Power L2 (Watts)             |
| 0x5B1A  | s32 (Multiplied by 0.01) | Active Power L3 (Watts)             |
| 0x5B1E  | s32 (Multiplied by 0.01) | Reactive Power L1 (VAr)             |
| 0x5B20  | s32 (Multiplied by 0.01) | Reactive Power L2 (VAr)             |
| 0x5B22  | s32 (Multiplied by 0.01) | Reactive Power L3 (VAr)             |
| 0x5460  | u64 (Multiplied by 10)   | Active Power Import L1 (Wh)         |
| 0x5464  | u64 (Multiplied by 10)   | Active Power Import L2 (Wh)         |
| 0x5468  | u64 (Multiplied by 10)   | Active Power Import L3 (Wh)         |
| 0x546C  | u64 (Multiplied by 10)   | Active Power Export L1 (Wh)         |
| 0x5470  | u64 (Multiplied by 10)   | Active Power Export L2 (Wh)         |
| 0x5474  | u64 (Multiplied by 10)   | Active Power Export L3 (Wh)         |
| 0x5484  | u64 (Multiplied by 10)   | Reactive Power Import L1 (VARh)     |
| 0x5488  | u64 (Multiplied by 10)   | Reactive Power Import L2 (VARh)     |
| 0x548C  | u64 (Multiplied by 10)   | Reactive Power Import L3 (VARh)     |
| 0x5490  | u64 (Multiplied by 10)   | Reactive Power Export L1 (VARh)     |
| 0x5494  | u64 (Multiplied by 10)   | Reactive Power Export L2 (VARh)     |
| 0x5498  | u64 (Multiplied by 10)   | Reactive Power Export L3 (VARh)     |
| 0x5000  | u64 (Multiplied by 10)   | Active Energy Import Total (Wh)     |
| 0x5004  | u64 (Multiplied by 10)   | Active Energy Export Total (Wh)     |
| 0x500C  | u64 (Multiplied by 10)   | Reactive Energy Import Total (VARh) |
| 0x5010  | u64 (Multiplied by 10)   | Reactive Energy Export Total (VARh) |
| 0x5170  | u64 (Multiplied by 10)   | Active Energy Import Total T1 (Wh)  |
| 0x5174  | u64 (Multiplied by 10)   | Active Energy Import Total T2 (Wh)  |

For Schneider iEM3xxx devices, the following registers are used:

| Address | Kind                     | Name                                |
| ------- | ------------------------ | ----------------------------------- |
| 0x0BD3  | f32 (Multiplied by 1)    | Voltage L1 (Volts)                  |
| 0x0BD5  | f32 (Multiplied by 1)    | Voltage L2 (Volts)                  |
| 0x0BD7  | f32 (Multiplied by 1)    | Voltage L3 (Volts)                  |
| 0x0BB7  | f32 (Multiplied by 1)    | Current L1 (Amps)                   |
| 0x0BB9  | f32 (Multiplied by 1)    | Current L2 (Amps)                   |
| 0x0BBB  | f32 (Multiplied by 1)    | Current L3 (Amps)                   |
| 0x0BED  | f32 (Multiplied by 1000) | Active Power L1 (Watts)             |
| 0x0BEF  | f32 (Multiplied by 1000) | Active Power L2 (Watts)             |
| 0x0BF1  | f32 (Multiplied by 1000) | Active Power L3 (Watts)             |
| 0x0BFB  | f32 (Multiplied by 1000) | Reactive Power Total (VAR)          |
| 0x0C03  | f32 (Multiplied by 1000) | Apparent Power Total (VA)           |
| 0x0DBD  | u64 (Multiplied by 1)    | Active Energy Import L1 (Wh)        |
| 0x0DC1  | u64 (Multiplied by 1)    | Active Energy Import L2 (Wh)        |
| 0x0DC5  | u64 (Multiplied by 1)    | Active Energy Import L3 (Wh)        |
| 0x0C83  | u64 (Multiplied by 1)    | Active Energy Import Total (Wh)     |
| 0x0C87  | u64 (Multiplied by 1)    | Active Energy Export Total (Wh)     |
| 0x0C93  | u64 (Multiplied by 1)    | Reactive Energy Import Total (VARh) |
| 0x0C97  | u64 (Multiplied by 1)    | Reactive Energy Export Total (VARh) |
| 0x1063  | u64 (Multiplied by 1)    | Active Energy Import Total T1 (Wh)  |
| 0x1067  | u64 (Multiplied by 1)    | Active Energy Import Total T2 (Wh)  |

Types used in tables:

- `u32` - unsigned 32-bit integer
- `s32` - signed 32-bit integer
- `u64` - unsigned 64-bit integer
- `f32` - 32-bit floating point number

Active and reactive energy registers are used to calculate billing data. Active
power peaks are calculated via active energy. The other registers are measured
for monitoring and diagnostic purposes.
