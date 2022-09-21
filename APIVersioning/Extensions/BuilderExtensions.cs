using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace APIVersioning.Extensions
{
    public static class BuilderExtensions
    {
        public enum APIVersioning
        {
            Header = 0,
            QueryString = 1,
            MediaType = 2,
            URISegment = 3
        }

        public static WebApplicationBuilder AddVersioning(this WebApplicationBuilder builder, APIVersioning versioning)
        {
            IApiVersionReader? apiVersionReader = null;
            if (versioning == APIVersioning.Header)
            {
                apiVersionReader = new HeaderApiVersionReader("X-Version");
            }
            else if (versioning == APIVersioning.QueryString)
            {
                apiVersionReader = new QueryStringApiVersionReader("api-version");
            }
            else if (versioning == APIVersioning.MediaType)
            {
                apiVersionReader = new MediaTypeApiVersionReader("ver");
            }

            builder.Services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;

                if (apiVersionReader != null)
                {
                    o.ApiVersionReader = ApiVersionReader.Combine(apiVersionReader);
                }
            });

            builder.Services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    if (versioning == APIVersioning.QueryString)
                    {
                        options.SubstituteApiVersionInUrl = true;
                    }
                });
            return builder;
        }
    }
}