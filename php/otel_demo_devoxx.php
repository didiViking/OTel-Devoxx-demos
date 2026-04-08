<?php
require 'vendor/autoload.php';

use OpenTelemetry\SDK\Trace\TracerProvider;
use OpenTelemetry\SDK\Trace\Exporter\OTLPTraceExporter;
use OpenTelemetry\SDK\Metrics\MeterProvider;
use OpenTelemetry\SDK\Metrics\Exporter\OTLPMetricExporter;

// This demo **does not work**: SDK incomplete for PHP
$tracerProvider = new TracerProvider();
$traceExporter = new OTLPTraceExporter(endpoint: 'http://localhost:4317');
$tracerProvider->addSpanProcessor(new \OpenTelemetry\SDK\Trace\SpanProcessor($traceExporter));

$meterProvider = new MeterProvider();
$metricExporter = new OTLPMetricExporter(endpoint: 'http://localhost:4317');

// Logging is not supported
echo "PHP demo cannot send traces, metrics, or logs reliably.\n";
