using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers
{
    [Route("/error")]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
