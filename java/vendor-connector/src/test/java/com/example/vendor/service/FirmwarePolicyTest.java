package com.example.vendor.service;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class FirmwarePolicyTest {
  @Test void windowAcrossMidnight() {
    assertTrue(FirmwarePolicy.canUpdate("03:30","22:00-04:00"));
    assertFalse(FirmwarePolicy.canUpdate("21:59","22:00-04:00"));
  }
  @Test void semverCompare() {
    assertTrue(FirmwarePolicy.semverCmp("1.2.10","1.2.9") > 0);
    assertEquals(0, FirmwarePolicy.semverCmp("1.2.0","1.2.0"));
    assertTrue(FirmwarePolicy.semverCmp("1.2.0","1.3.0") < 0);
  }
}
