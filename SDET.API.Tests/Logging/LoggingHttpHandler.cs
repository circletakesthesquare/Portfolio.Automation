using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace SDET.API.Tests.Utilities
{
    public class LoggingHttpHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public LoggingHttpHandler(ILogger logger)
        {
            _logger = logger;
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.Information("REQUEST: {Method} {Uri}", request.Method, request.RequestUri);

            var response = await base.SendAsync(request, cancellationToken);

            _logger.Information("RESPONSE: {StatusCode} {ReasonPhrase}", response.StatusCode, response.ReasonPhrase);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.Error("RESPONSE CONTENT: {Content}", content);
            }

            return response;
        }
    }
}
