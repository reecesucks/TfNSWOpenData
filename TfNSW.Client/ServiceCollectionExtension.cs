using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using TfNSWOpenData.API;

namespace TfNSWOpenData.TfNSW.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTransportNsw(
            this IServiceCollection services,
            Action<TfNSWOptions> configure)
        {
            services.Configure(configure);

            services.AddHttpClient<ITfNSWClient, TfNSWClient>(
                    (sp, client) =>
                    {
                        var options = sp
                            .GetRequiredService<IOptions<TfNSWOptions>>()
                            .Value;

                        if (string.IsNullOrWhiteSpace(options.ApiKey))
                        {
                            throw new InvalidOperationException(
                                "TfNSW ApiKey is not configured.");
                        }

                        client.BaseAddress = options.BaseUri;

                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("apikey", options.ApiKey);
                    });

            return services;
        }
    }
}
