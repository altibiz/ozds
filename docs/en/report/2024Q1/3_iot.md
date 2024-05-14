# IOT

<div style="display: none;">
  \page report-2024-q1-iot IOT
</div>

The Raspberry PI 4 messenger devices host a Rust program that continuously reads
measurements from meters, batches them every specified interval and sends them
to the cloud server.

The Rust program is ran via a Systemd service which is running on NixOS. The
service is configured to start on boot and restart on failure. Measurements are
buffered in a locally installed Postgresql database with the TimescaleDB
extension.
