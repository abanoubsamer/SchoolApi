using Microsoft.Extensions.Localization;
using SchoolWep.Core.SharedResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Core.Basics
{
    public class ResponseHandler
    {

        private readonly IStringLocalizer<SharedResource> _StringLocalizer;


        public ResponseHandler(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _StringLocalizer= stringLocalizer;
        }
        public Response<T> Deleted<T>(string Msg)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = Msg,
            };
        }
        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.OK,
                Succeeded = true,
                Message = _StringLocalizer[SharedResourcesKey.Operations.Successed],
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>(string Massege=null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = Massege ?? "UnAuthorized"
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message ?? "Bad Request" 
            };
        }
        public Response<T> UnprocessableEntity<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message ?? "Unprocessable Entity" 
            };
        }
        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? _StringLocalizer[SharedResourcesKey.Valdiation.NotFound] : message
            };
        }

        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = HttpStatusCode.Created,
                Succeeded = true,
                Message = _StringLocalizer[SharedResourcesKey.Operations.Added],
                Meta = Meta
            };
        }
    }
}
