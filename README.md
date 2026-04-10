# OpenTelemetry Demo - Devoxx

This repository contains experiments and demos using OpenTelemetry to send **traces, metrics, and logs** to OTEL Viewer 

👉 https://github.com/mesaglio/otel-front

It includes working and non-working demos in **Go, .NET, Python, and PHP**, along with failures and lessons learned.

<img width="1227" height="854" alt="Screenshot 2026-04-08 at 12 17 32" src="https://github.com/user-attachments/assets/c2c49e7f-2235-42a8-a118-daaf7af313c5" />


---

## **Folder structure**

- **Go/** - Working Go demo with traces + metrics.  
- **DotNet/** - Full .NET demo with traces, metrics, and logs.  
- **Python/** - Full Python demo with traces, metrics and logs -> see example code at https://github.com/avillela/otel-errors-talk/
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
<img width="1157" height="376" alt="Screenshot 2026-04-08 at 11 59 56" src="https://github.com/user-attachments/assets/5b6e9ee3-e0d9-41bc-b4c0-faff9658ce54" />


What doesn’t work: logs are not supported in the current Go SDK.
The demo runs for a few seconds and exits; metrics are batched and may show delays.

### .NET
```bash
cd DotNet/OtelDemoDevoxx-net
dotnet restore
dotnet run
```

What works: full support for traces, metrics, and logs.
<img width="1258" height="841" alt="Screenshot 2026-04-08 at 13 45 59" src="https://github.com/user-attachments/assets/b80fe4c9-3ecc-47db-a5ec-d59617054cbc" />


Tips: Make sure NuGet packages match the ones in the csproj file to avoid version conflicts.

Note: Metrics might need a few seconds to appear in OTEL Viewer due to batching.

### Python

I initially attempted to implement Python OpenTelemetry instrumentation in this repository.

However, instead of duplicating a fragile setup, it is best to reference a working and reproducible implementation:

👉 https://github.com/avillela/otel-errors-talk/

Follow the instructions in the repository above.

<img width="1245" height="698" alt="Screenshot 2026-04-10 at 10 39 20" src="https://github.com/user-attachments/assets/9aef7149-4b02-43ba-9744-98e21301b24c" />


It demonstrates:

OpenTelemetry auto-instrumentation
OTLP export to an observability backend
End-to-end traces generation

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
