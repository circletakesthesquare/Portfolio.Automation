using System.Text.Json;
using Serilog;

namespace API.Tests.Utilities
{
    public class LoggingHttpHandler : DelegatingHandler
    {

        public LoggingHttpHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        /// <summary>
        /// Sends an HTTP request and logs the request and response details.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns the HTTP response message after logging request and response details.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // --- Log Request ---
            var requestBody = await ReadAndFormatContentAsync(request.Content, cancellationToken);

            Log.Information(
                "REQUEST:\nMethod: {Method}\nURL: {Url}\nRequest Body: {Body}",
                request.Method,
                request.RequestUri,
                requestBody
            );

            // --- Send Request ---
            var response = await base.SendAsync(request, cancellationToken);

            // --- Log Response ---
            var responseBody = await ReadAndFormatContentAsync(response.Content, cancellationToken);

            Log.Information(
                "RESPONSE:\nStatus: {StatusCode}\nResponse Body: {Body}",
                response.StatusCode,
                responseBody
            );

            return response;
        }

        /// <summary>
        /// Helper method that reads and formats HTTP content for logging.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns formatted JSON or raw string if content is not JSON.</returns>
        private static async Task<string> ReadAndFormatContentAsync(HttpContent? content, CancellationToken cancellationToken)
        {
            if (content == null) return string.Empty;

            var contentString = await content.ReadAsStringAsync(cancellationToken);
            if (string.IsNullOrWhiteSpace(contentString)) return string.Empty;

            try
            {
                using var jsonDoc = JsonDocument.Parse(contentString);
                return JsonSerializer.Serialize(jsonDoc, new JsonSerializerOptions { WriteIndented = true });
            }
            catch
            {
                // Not JSON — return raw string
                return contentString;
            }
        }
    }
}
