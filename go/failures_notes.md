# Go OTEL Demo - Observations

- Traces and metrics work, logs not yet supported.
- Demo stops after retries if OTEL Viewer is unavailable.
- OTLP gRPC configuration required for metrics; HTTP works for traces.
- Errors with attribute type casting for metrics if API is misused.
