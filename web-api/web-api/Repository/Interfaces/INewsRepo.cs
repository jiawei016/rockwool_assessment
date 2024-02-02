using web_api.Models;

namespace web_api.Repository.Interfaces
{
    public interface INewsRepo
    {
        public Task<NewsDataAPIModel> searchNews(string searchTitle);
        public Task<NewsDataAPIModel> searchByPagination(string searchTitle, int pageNumber);
    }
}
