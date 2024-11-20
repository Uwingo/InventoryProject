using System.Net.Http.Headers;

namespace Frontend
{
    public static class GenericClient
    {
        public static HttpClient Client { get; private set; }

        public static void InitializeClientBaseAddress(this IServiceCollection services, IConfiguration configuration)
        {
            var clientBaseUrl = configuration.GetSection("ClientBaseUrl").Value;
            var apiBaseUrl = configuration.GetSection("ApiBaseUrl").Value;
            Client = new HttpClient();
            Client.BaseAddress = new Uri(apiBaseUrl); // Set the default to API base URL
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void SetBaseUrlToApi()
        {
            var apiBaseUrl = Client.BaseAddress;
            Client.BaseAddress = new Uri(apiBaseUrl.ToString());
        }

        public static void SetBaseUrlToMvc()
        {
            var clientBaseUrl = Client.BaseAddress;
            Client.BaseAddress = new Uri(clientBaseUrl.ToString());
        }
    }
}
