# IoT Device Simulator

This directory contains a basic IoT device simulator for the Cogtive Industrial IoT Platform technical challenge. The simulator is intended for senior-level candidates to demonstrate their understanding of IoT integration and real-time data processing.

## Overview

The simulator generates random production data for various machines and sends it to the Cogtive API. It simulates multiple IoT devices reporting metrics from a factory floor.

## Getting Started

```bash
# Install dependencies
npm install

# Run the simulator
npm start
```

To run with Docker (also defined in the main docker-compose.yml):

```bash
docker build -t cogtive-iot-simulator .
docker run -e API_ENDPOINT=http://localhost:5000/api/production-data cogtive-iot-simulator
```

## Customization Tasks (for Senior Candidates)

As part of the technical challenge, senior candidates are encouraged to extend this simulator with the following improvements:

1. Add more realistic data generation based on machine types
2. Implement error handling and retries for failed API calls
3. Add support for simulating sensor anomalies and equipment failures
4. Create a websocket connection for real-time monitoring
5. Implement a command interface to control the simulator's behavior

## Configuration

The simulator can be configured through environment variables:

- `API_ENDPOINT`: The endpoint to send data to (default: `http://localhost:5000/api/production-data`)
- `SEND_INTERVAL_MS`: The interval in milliseconds at which to send data (default: `10000`)

## Integration with the Main Application

When you run the complete Cogtive Industrial IoT Platform using Docker Compose, this simulator can be integrated by uncommenting the `iot-simulator` service in the `docker-compose.yml` file.
