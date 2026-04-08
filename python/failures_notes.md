# Python OTEL Demo - Failures

- Cannot import logs from `opentelemetry.sdk.logs`.
- Metrics export API changed frequently between versions.
- Traces work, metrics partially work, logs not supported.
- OTLP HTTP/Protobuf and gRPC configurations differ; easy to misconfigure.
