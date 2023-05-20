using LemonApi;
using LemonApi.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

//Addservicestothecontainer.
builder.Services.AddDatabaseModule(builder.Configuration);
builder.Services.AddEmailModule(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureAuthorization(builder.Configuration);
builder.Services.ConfigureServices();

builder.Services.AddDirectoryBrowser();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
//LearnmoreaboutconfiguringSwagger/OpenAPIathttps://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//ConfiguretheHTTPrequestpipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
