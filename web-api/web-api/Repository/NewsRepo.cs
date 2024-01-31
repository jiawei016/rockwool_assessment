using Dapper;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using thirdparty_api;
using web_api.Extensions.Dapper;
using web_api.Repository.Interfaces;

namespace web_api.Repository
{
    public class NewsRepo : INewsRepo
    {
        private readonly NewsAPI _newsAPI;
        private readonly DapperHelper _dapperHelper;
        private readonly IConfiguration _configuration;
        public NewsRepo(IServiceProvider serviceProvider) 
        {
            _newsAPI = serviceProvider.GetRequiredService<NewsAPI>();
            _dapperHelper = serviceProvider.GetRequiredService<DapperHelper>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }
        public async Task<string> searchNews(string searchTitle)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(_configuration);
                unitOfWork.OpenConnection();

                //string response = await _newsAPI.searchNews(searchTitle);
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("SearchTitle", searchTitle);
                await _dapperHelper.ExecuteAsync("spInsertSearchHistory", unitOfWork, param);

                unitOfWork.Rollback();
            }
            catch (Exception ex)
            {
                // Handle exceptions or rollback the transaction if necessary
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return string.Empty;
        }
    }
}
