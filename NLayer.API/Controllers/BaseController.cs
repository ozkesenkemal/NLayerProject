using Microsoft.AspNetCore.Mvc;
using NLayer.Data.Dto;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private const int NoContentResponse = 204;

        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if(response.StatusCode == NoContentResponse) 
            {
                return new ObjectResult(null) { StatusCode = response.StatusCode };
            }

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
