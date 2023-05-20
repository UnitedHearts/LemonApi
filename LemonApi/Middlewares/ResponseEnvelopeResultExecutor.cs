using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Contracts.Http;
using System;
using Microsoft.AspNetCore.Http;

namespace LemonApi.Middlewares
{
    internal class ResponseEnvelopeResultExecutor : ObjectResultExecutor
    {
        public ResponseEnvelopeResultExecutor(OutputFormatterSelector formatterSelector, IHttpResponseStreamWriterFactory writerFactory, ILoggerFactory loggerFactory, IOptions<MvcOptions> mvcOptions) : base(formatterSelector, writerFactory, loggerFactory, mvcOptions)
        {
        }

        public override Task ExecuteAsync(ActionContext context, ObjectResult result)
        {
            TypeCode typeCode = Type.GetTypeCode(result.Value?.GetType());
            switch (typeCode)
            {
                case TypeCode.String:
                    return context.HttpContext.Response.WriteAsJsonAsync(new Response<object>(result.Value));
                default:
                    if (result.Value is ContentResult)
                        return base.ExecuteAsync(context, result);
                    result.Value = new Response<object>(result.Value);
                    return base.ExecuteAsync(context, result);
            }
        }
    }
}
