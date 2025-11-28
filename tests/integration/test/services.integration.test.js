import got from "got";

const endpoints = [
  { name: "csharp ready", url: "http://localhost:5000/health/ready", expect: 200 },
  { name: "java ready",   url: "http://localhost:8081/actuator/health/readiness", expect: 200 },
  { name: "js ready",     url: "http://localhost:8082/health/ready", expect: 200 },
  { name: "js can-update",url: "http://localhost:8082/firmware/can-update?now=03:30&window=22:00-04:00", expect: 200, jsonKey: "canUpdate", jsonVal: true },
  { name: "csharp metrics", url: "http://localhost:5000/metrics", expect: 200, contains: "process_cpu_seconds_total" },
  { name: "java metrics",   url: "http://localhost:8081/actuator/prometheus", expect: 200, contains: "jvm_memory_bytes_used" },
  { name: "js metrics",     url: "http://localhost:8082/metrics", expect: 200, contains: "process_cpu_user_seconds_total" }
];

async function waitForOk(url, attempts=30, delayMs=1000){
  for(let i=0;i<attempts;i++){
    try {
      const res = await got(url, { throwHttpErrors: false, retry: 0 });
      if (res.statusCode === 200) return true;
    } catch {}
    await new Promise(r=>setTimeout(r, delayMs));
  }
  return false;
}

describe("services integration", () => {
  beforeAll(async () => {
    const ok = await Promise.all([
      waitForOk("http://localhost:5000/health/ready"),
      waitForOk("http://localhost:8081/actuator/health/readiness"),
      waitForOk("http://localhost:8082/health/ready")
    ]);
    if (ok.some(v=>!v)) throw new Error("Services not ready. Start: docker compose up -d --build");
  });

  it.each(endpoints)("$name", async ({url, expect, jsonKey, jsonVal, contains}) => {
    const res = await got(url, { throwHttpErrors: false, retry: 0 });
    expect(res.statusCode).toBe(expect);
    if (jsonKey){
      const body = JSON.parse(res.body);
      expect(body[jsonKey]).toBe(jsonVal);
    }
    if (contains){
      expect(res.body).toContain(contains);
    }
  });
});
