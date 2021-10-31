namespace Mitekat.RestApi.Filters
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    internal class SwaggerPadlockIndicatorFilter : IOperationFilter
    {
        private readonly OpenApiSecurityRequirement _authenticationRequirement;

        public SwaggerPadlockIndicatorFilter() =>
            _authenticationRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;

            var markedWithAuthorize = actionMetadata.Any(metadataItem => metadataItem is AuthorizeAttribute);
            var markedWithAllowAnonymous = actionMetadata.Any(metadataItem => metadataItem is AllowAnonymousAttribute);

            if (!markedWithAuthorize || markedWithAllowAnonymous)
            {
                return;
            }

            operation.Security.Add(_authenticationRequirement);
        }
    }
}
