using Microsoft.AspNetCore.Mvc;

namespace VendorConnector.Controllers;

[ApiController]
[Route("firmware")]
public class HealthController : ControllerBase
{
  [HttpGet("can-update")]
  public IActionResult CanUpdate([FromQuery] string now = "03:30", [FromQuery] string window = "22:00-04:00")
  {
    var ok = Services.FirmwarePolicy.CanUpdate(now, window);
    return Ok(new { canUpdate = ok });
  }
}
