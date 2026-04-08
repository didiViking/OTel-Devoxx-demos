import random
import time
from opentelemetry import trace, metrics
from opentelemetry.sdk.trace import TracerProvider
from opentelemetry.sdk.metrics import MeterProvider
from opentelemetry.sdk.trace.export import BatchSpanProcessor, OTLPSpanExporter
from opentelemetry.sdk.metrics.export import PeriodicExportingMetricReader, OTLPMetricExporter

# Traces
trace.set_tracer_provider(TracerProvider())
tracer = trace.get_tracer(__name__)
span_exporter = OTLPSpanExporter(endpoint="http://localhost:4317", insecure=True)
trace.get_tracer_provider().add_span_processor(BatchSpanProcessor(span_exporter))

# Metrics
meter_provider = MeterProvider(
    metric_readers=[PeriodicExportingMetricReader(OTLPMetricExporter(endpoint="http://localhost:4317", insecure=True))]
)
metrics.set_meter_provider(meter_provider)
meter = metrics.get_meter(__name__)
counter = meter.create_counter("demo_counter")

print("Demo running. Sending traces and metrics to OTEL Viewer at localhost:4317...")

for i in range(10):
    with tracer.start_as_current_span(f"operation-{i}"):
        counter.add(i, {"iteration": i})
        time.sleep(0.5)

print("Demo finished.")
