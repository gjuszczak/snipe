using Snipe.App.Core.Serialization;
using Snipe.App.Features.Common.Services;
using Snipe.Infrastructure;
using Snipe.Web;
using Snipe.Web.Configuration;
using Snipe.Web.Filters;
using Snipe.Web.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.OpenApi.Models;
using System.Text;
using Snipe.App.Features.Users.Services;
using Microsoft.Extensions.Options;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.Configure<ClientConfiguration>(builder.Configuration.GetSection("ClientConfig"));
builder.Services.Configure<AzureAdConfiguration>(builder.Configuration.GetSection("AzureAd"));
builder.Services.Configure<UsersFeatureConfiguration>(builder.Configuration.GetSection("UsersFeature"));
builder.Services.AddScoped<IUserInfoProvider, UserInfoProvider>();
builder.Services.AddScoped<IHttpContextDetails, HttpContextDetails>();
builder.Services.AddTransient<IUsersFeatureConfiguration>(provider => provider.GetService<IOptions<UsersFeatureConfiguration>>().Value);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Snipe API", Version = "v1" });
    c.DescribeAllParametersInCamelCase();
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearerAuth"
                    }
                },
                Array.Empty<string>()
        }
    });
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
