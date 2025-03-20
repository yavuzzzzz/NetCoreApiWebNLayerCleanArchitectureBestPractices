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
            if (serviceResult.Status == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = serviceResult.Status.GetHashCode() }; // when status is 204, return null in the body
            }

            return new ObjectResult(serviceResult) { StatusCode = serviceResult.Status.GetHashCode() }; // in other status, return the object in the body

        }
        [NonAction]
        public IActionResult CreateActionResult(ServiceResult serviceResult)
        {
            if (serviceResult.Status == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = serviceResult.Status.GetHashCode() }; // when status is 204, return null in the body
            }

            return new ObjectResult(serviceResult) { StatusCode = serviceResult.Status.GetHashCode() }; // in other status, return the object in the body

        }
    }
}
