using System.Net;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction] // this method is not an action method to be defined in Swagger
        public IActionResult CreateActionResult<T>(ServiceResult<T> serviceResult)
        {
            return serviceResult.Status switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.Created => Created(serviceResult.UrlAsCreated, serviceResult.Data),
                _ => new ObjectResult(serviceResult) { StatusCode = serviceResult.Status.GetHashCode() }
            }; 
        }
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult serviceResult)
        {
            return serviceResult.Status switch
            {
                HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = serviceResult.Status.GetHashCode() },
                _ => new ObjectResult(serviceResult) { StatusCode = serviceResult.Status.GetHashCode() }
            };
        }
    }
}
