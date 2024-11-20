namespace Frontend.Helper
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetDataFromApiAsync(string apiUrl)
        {
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return data;
            }
            else
            {
                // Hata durumunu yönetmek için
                throw new HttpRequestException($"Error calling API: {response.StatusCode}");
            }
        }
    }
}
