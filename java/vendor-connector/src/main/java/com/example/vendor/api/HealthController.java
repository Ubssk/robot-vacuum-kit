package com.example.vendor.api;
import com.example.vendor.service.FirmwarePolicy;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/firmware")
public class HealthController {
  @GetMapping("/can-update")
  public Object canUpdate(@RequestParam String now, @RequestParam String window) {
    boolean ok = FirmwarePolicy.canUpdate(now, window);
    return java.util.Map.of("canUpdate", ok);
  }
}
