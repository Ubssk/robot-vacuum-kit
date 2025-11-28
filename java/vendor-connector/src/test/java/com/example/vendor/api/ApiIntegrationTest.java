package com.example.vendor.api;
import com.example.vendor.Application;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.boot.test.web.server.LocalServerPort;
import static org.assertj.core.api.Assertions.assertThat;

@SpringBootTest(classes = Application.class, webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class ApiIntegrationTest {
  @LocalServerPort private int port;
  @Autowired private TestRestTemplate rest;

  @Test
  void readinessOk() {
    var res = rest.getForEntity("http://localhost:"+port+"/actuator/health/readiness", String.class);
    assertThat(res.getStatusCode().value()).isEqualTo(200);
  }

  @Test
  void canUpdateOk() {
    var res = rest.getForEntity("http://localhost:"+port+"/firmware/can-update?now=03:30&window=22:00-04:00", String.class);
    assertThat(res.getStatusCode().value()).isEqualTo(200);
    assertThat(res.getBody()).contains("\"canUpdate\":true");
  }
}
