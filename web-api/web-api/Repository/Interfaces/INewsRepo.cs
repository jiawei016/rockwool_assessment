namespace web_api.Repository.Interfaces
{
    public interface INewsRepo
    {
        public Task<string> searchNews(string searchTitle);
    }
}
