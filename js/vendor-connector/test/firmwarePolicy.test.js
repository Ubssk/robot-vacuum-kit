import { canUpdate, semverCmp } from "../src/firmwarePolicy.js";

test("window across midnight", () => {
  expect(canUpdate("03:30","22:00-04:00")).toBe(true);
  expect(canUpdate("21:59","22:00-04:00")).toBe(false);
});
test("semver compare", () => {
  expect(semverCmp("1.2.10","1.2.9")).toBeGreaterThan(0);
  expect(semverCmp("1.2.0","1.2.0")).toBe(0);
  expect(semverCmp("1.2.0","1.3.0")).toBeLessThan(0);
});
