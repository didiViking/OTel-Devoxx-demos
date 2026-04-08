package main

import (
    "context"
    "fmt"
    "math/rand"
    "time"

    "go.opentelemetry.io/otel"
    "go.opentelemetry.io/otel/attribute"
    "go.opentelemetry.io/otel/exporters/otlp/otlpmetric/otlpmetricgrpc"
    "go.opentelemetry.io/otel/exporters/otlp/otlptrace/otlptracegrpc"
    "go.opentelemetry.io/otel/sdk/metric"
    sdktrace "go.opentelemetry.io/otel/sdk/trace"
)

func main() {
    ctx := context.Background()

    // Tracer
    traceExporter, _ := otlptracegrpc.New(ctx)
    tp := sdktrace.NewTracerProvider(
        sdktrace.WithBatcher(traceExporter),
    )
    otel.SetTracerProvider(tp)
    tracer := otel.Tracer("demo-tracer")

    // Metrics
    metricExporter, _ := otlpmetricgrpc.New(ctx)
    mp := metric.NewMeterProvider(
        metric.WithReader(metric.NewPeriodicReader(metricExporter, metric.WithInterval(time.Second))),
    )
    meter := mp.Meter("demo-meter")
    counter, _ := meter.Int64Counter("demo_counter")

    fmt.Println("Demo running. Sending traces and metrics to OTEL Viewer at localhost:4317...")

    for i := 0; i < 10; i++ {
        _, span := tracer.Start(ctx, fmt.Sprintf("operation-%d", i))
        counter.Add(ctx, int64(i), attribute.Int("iteration", i))
        span.End()
        time.Sleep(500 * time.Millisecond)
    }

    fmt.Println("Demo finished.")
}
