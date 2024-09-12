﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolWep.Core.Basics;

namespace SchoolWepApi.Basics
{
    [ApiController]
    public class AppControllerBasic : ControllerBase
    {

        protected readonly IMediator _Mediator;

        public AppControllerBasic(IMediator mediator)
        {
            _Mediator = mediator;
        }



        public ObjectResult NewResult<T>(Response<T> response)
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case System.Net.HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case System.Net.HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case System.Net.HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                case System.Net.HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case System.Net.HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case System.Net.HttpStatusCode.Forbidden:
                    return new ObjectResult(response) 
                    { StatusCode = (int)System.Net.HttpStatusCode.Forbidden };
                case System.Net.HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                default:
                    return new ObjectResult(response) 
                    { StatusCode = (int)System.Net.HttpStatusCode.InternalServerError };
            }
        }

    }
}
