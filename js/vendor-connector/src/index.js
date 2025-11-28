import Fastify from "fastify";
import { Registry, collectDefaultMetrics, Histogram } from "prom-client";
import { canUpdate } from "./firmwarePolicy.js";

const app = Fastify({ logger: true });
const registry = new Registry();
collectDefaultMetrics({ register: registry });
const httpLatency = new Histogram({
  name: "http_request_duration_seconds",
  help: "HTTP latency",
  labelNames: ["route","method","status"],
  buckets: [0.01,0.025,0.05,0.1,0.25,0.5,1,2,5],
  registers: [registry]
});

app.addHook("onResponse", async (req, reply) => {
  const ms = reply.getResponseTime?.() ?? 0;
  httpLatency.labels(req.routerPath || req.url, req.method, String(reply.statusCode)).observe(ms/1000);
});

app.get("/health/live", async () => ({ status: "ok" }));
app.get("/health/ready", async () => ({ status: "ok" }));
app.get("/metrics", async (req, reply) => {
  reply.header("Content-Type", registry.contentType);
  reply.send(await registry.metrics());
});
app.get("/firmware/can-update", async (req) => {
  const now = req.query.now || "03:30";
  const window = req.query.window || "22:00-04:00";
  return { canUpdate: canUpdate(now, window) };
});

const port = Number(process.env.PORT || 8082);
app.listen({ port, host: "0.0.0.0" });
