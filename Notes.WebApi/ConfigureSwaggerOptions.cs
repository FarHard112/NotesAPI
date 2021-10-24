using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;

namespace Notes.WebApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                var apiVersion = description.ApiVersion.ToString();
                options.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Version = apiVersion,
                        Title = $"Notes API{apiVersion}",
                        Description = "Note API but best  practice ",
                        TermsOfService = new Uri("https://github.com/FarHard112"),
                        Contact = new OpenApiContact
                        {
                            Name = "Farhad Mammadov "
                        ,
                            Email = "farhadmdovv@gmail.com"
                        },
                        License = new OpenApiLicense { Name = "Farhad Mammadov" }
                    });
                options.AddSecurityDefinition($"AuthToken{apiVersion}"
                    , new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Name = "Authorization",
                        Description = "Authorization Token",

                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                { {
                new OpenApiSecurityScheme
                {
                    Reference =new OpenApiReference
                    {Type=ReferenceType.SecurityScheme,Id=$"AuthToken{apiVersion}"}
                },
                new string []{}
                } });

                options.CustomOperationIds(apiDescription =>
                apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
                ? methodInfo.Name : null);
            }
        }

    }
}
