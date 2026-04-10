# OpenTelemetry Demo - Devoxx

This repository contains experiments and demos using OpenTelemetry to send **traces, metrics, and logs** to OTEL Viewer.

It includes working and non-working demos in **Go, .NET, Python, and PHP**, along with failures and lessons learned.

---

## **Folder structure**

- **Go/** - Working Go demo with traces + metrics.  
- **DotNet/** - Full .NET demo with traces, metrics, and logs.  
- **Python/** - Attempted Python demos (not fully working).  
- **PHP/** - Attempted PHP demos (not fully working).  
- **notes/** - Conclusions and lessons learned.

---

## How to run

### Prerequisites
- OTEL Viewer running locally (default ports 4317 for gRPC)  
- For Go: Go 1.21+ installed  
- For .NET: .NET 8 SDK installed  
- For Python: Python 3.13+ (virtual environment recommended)  
- For PHP: PHP 8+ (Composer optional)  

---

### Go
```bash
cd Go
go mod tidy
go run otel_demo_devoxx.go
```


What works: traces and metrics are sent to OTEL Viewer.

What doesn’t work: logs are not supported in the current Go SDK.
The demo runs for a few seconds and exits; metrics are batched and may show delays.

### .NET
```bash
cd DotNet/OtelDemoDevoxx-net
dotnet restore
dotnet run
```

What works: full support for traces, metrics, and logs.

Tips: Make sure NuGet packages match the ones in the csproj file to avoid version conflicts.

Note: Metrics might need a few seconds to appear in OTEL Viewer due to batching.

### Python

```bash
cd Python
python3 -m venv venv
source venv/bin/activate

pip install opentelemetry-distro
opentelemetry-bootstrap -a install

opentelemetry-instrument \
  --traces_exporter console,otlp \
  --metrics_exporter console,otlp \
  --logs_exporter console,otlp \
  --service_name python-demo \
  python otel_demo_devoxx.py
```
What works:

Full automatic instrumentation (no manual SDK wiring)
Traces exported via OTLP (visible in OTEL Viewer / otel-front)
Metrics exported (runtime + instrumentation metrics)
Logs exported (when configured correctly)
End-to-end observability with OTEL Viewer

Notes / caveats:

Setup requires correct OpenTelemetry distro packages
Python 3.13 may require additional compatibility fixes for some exporters
Auto-instrumentation is the recommended approach over manual SDK setup

Key learning:

Auto-instrumentation is significantly more reliable than manual SDK setup in Python.

### PHP
```bash
cd PHP
# Composer is optional; scripts included for reference
php otel_demo_devoxx.php
```

What works: mostly just shows attempts; OTLP PHP SDK does not fully support metrics/logs yet.
What doesn’t work: PHP logs and metrics fail; included to show experimentation process.

### Failures & Notes

Check each folder’s failures_notes.md for errors encountered, partial successes, and workarounds tried.

### Conclusions

See notes/conclusions.md for a summary of what worked per language, lessons learned, and recommendations for future demos or research.
