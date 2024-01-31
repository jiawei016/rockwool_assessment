
namespace thirdparty_api
{
    public class NewsAPI
    {
        private string api_key = "pub_37440293e20c9857f6adcbcc10aabcc3cf262";

        public async Task<string> searchNews(string title)
        {
            using HttpClient client = new HttpClient();
            string apiUrl = $"https://newsdata.io/api/1/news?apikey={api_key}&q={title}&language=en";
            string responseData = "";
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    responseData = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : {ex.ToString()}");
            }

            return responseData;
        }
    }
}
