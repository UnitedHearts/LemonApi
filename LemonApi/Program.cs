using LemonApi.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

//Addservicestothecontainer.
builder.Services.AddDatabaseModule(builder.Configuration);
builder.Services.AddEmailModule(builder.Configuration);



builder.Services.AddControllers();
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

app.UseExceptionHandler();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
