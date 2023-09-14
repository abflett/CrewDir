using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CrewDir.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerGenOptions(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                Scheme = "bearer",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "Enter your valid token",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            options.CustomOperationIds(e =>
            {
                var tag = e.ActionDescriptor.EndpointMetadata.OfType<TagsAttribute>().FirstOrDefault()?.Tags[0];
                if (tag == "Account")
                {
                    var path = char.ToUpper(e.RelativePath![0]) + e.RelativePath![1..]
                    .Replace("/", "")
                    .Replace("{", "By")
                    .Replace("}", "");
                    var method = char.ToUpper(e.HttpMethod![0]) + e.HttpMethod.ToLower()[1..];

                    return $"{method}{path}";
                }
                return $"{e.ActionDescriptor.RouteValues["action"]}";
            });
        }
    }
}
