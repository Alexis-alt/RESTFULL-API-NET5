using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public  abstract class BaseApiController : ControllerBase
    {

        private IMediator _mediator;


        //Es como una propiedad inyectada para todas las subClases
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();




    }
}
