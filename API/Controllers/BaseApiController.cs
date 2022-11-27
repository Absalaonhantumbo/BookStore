using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]  
    public class BaseApiController :ControllerBase
    {
        
    }
}