using LemonApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace api_avia.Extensions
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(LoginModel))
            {
                schema.Example = new OpenApiObject()
                {
                    ["Email"] = new OpenApiString("bondarev.bogdan2013@yandex.ru"),
                    ["Password"] = new OpenApiString("LoneWald13579"),
                };
            }
            else
                schema.Example = new OpenApiObject();
        }
    }
}
