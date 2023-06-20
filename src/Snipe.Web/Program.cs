using Snipe.App.Core.Serialization;
using Snipe.App.Features.Common.Interfaces;
using Snipe.Infrastructure;
using Snipe.Web;
using Snipe.Web.Configuration;
using Snipe.Web.Filters;
using Snipe.Web.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.OpenApi.Models;
using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ClientConfiguration>(builder.Configuration.GetSection("ClientConfig"));
builder.Services.Configure<AzureAdConfiguration>(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserInfoProvider, UserInfoProvider>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Snipe API", Version = "v1" });
    c.DescribeAllParametersInCamelCase();
});

builder.Services.AddSnipeAuth(builder.Configuration);

builder.Services.AddMvc(opts =>
{
    opts.Filters.Add<ApiExceptionFilterAttribute>();
})
.AddJsonOptions(opts =>
{
    JsonDefaults.Configure(opts.JsonSerializerOptions);
})
.AddFluentValidation(opts =>
{
    opts.AutomaticValidationEnabled = false;
});

builder.Services.AddSnipe(builder.Configuration);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddHostedService<DbMigrationsBootstrap>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.Use(async (context, next) =>
    {
        //throtle
        await Task.Delay(500);
        await next.Invoke();
    });
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Snipe API V1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
}

var pwaProvider = new FileExtensionContentTypeProvider();
pwaProvider.Mappings[".webmanifest"] = "application/manifest+json";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = pwaProvider
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.Run();
