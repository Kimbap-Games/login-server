using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
    /// <summary>
    /// 서버가 정상적으로 실행 중인지 확인합니다.
    /// </summary>
    /// <returns>상태 메시지</returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Server is running. 🚀");
    }
}