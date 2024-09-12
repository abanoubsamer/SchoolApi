using AutoMapper.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolWep.Core.Basics;
using FluentValidation;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.SharedResources;

namespace SchoolWep.Core.Middleware
{
    public class ErrorHandlerMiddleware
    {
        
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<SharedResource> _StringLocalizer;

        public ErrorHandlerMiddleware(RequestDelegate next,
                IStringLocalizer<SharedResource> stringLocalizer)
        {
                _next = next;
               _StringLocalizer = stringLocalizer;
        }

            public async Task Invoke(HttpContext context)
            {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string> { Succeeded = false, Message = ex.Message };

                switch (ex)
                {
                    case ValidationException e: 
                        responseModel.Errors = e.Errors.Select(x => _StringLocalizer[x.PropertyName]  + ": " + _StringLocalizer[x.ErrorMessage]).ToList();
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;
                    case DbUpdateException e:
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        responseModel.Message = ex.InnerException == null ? ex.Message : $"{ex.Message}\n{ex.InnerException}";
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
        }
    }

