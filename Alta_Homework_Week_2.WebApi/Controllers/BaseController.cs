using Microsoft.AspNetCore.Mvc;

namespace Alta_Homework_Week_2.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
}
