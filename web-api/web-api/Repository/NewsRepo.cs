using Dapper;
using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using thirdparty_api;
using web_api.Extensions;
using web_api.Extensions.Dapper;
using web_api.Extensions.Redis;
using web_api.Models;
using web_api.Repository.Interfaces;

namespace web_api.Repository
{
    public class NewsRepo : INewsRepo
    {
        private readonly NewsAPI _newsAPI;
        private readonly DapperHelper _dapperHelper;
        private readonly RedisHelper _redisHelper;
        private readonly IConfiguration _configuration;
        public NewsRepo(IServiceProvider serviceProvider) 
        {
            _newsAPI = serviceProvider.GetRequiredService<NewsAPI>();
            _dapperHelper = serviceProvider.GetRequiredService<DapperHelper>();
            _redisHelper = serviceProvider.GetRequiredService<RedisHelper>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        public async Task<NewsDataAPIModel> searchNews(string searchTitle)
        {
            NewsDataAPIModel newsDataAPIModel = new NewsDataAPIModel();
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(_configuration);
                unitOfWork.OpenConnection();

                try
                {
                    bool dapperStatus = await saveToDB(searchTitle, unitOfWork);
                    if (!dapperStatus)
                    {
                        throw new Exception("Write to Db Error");
                    }

                    string cache = await _redisHelper.GetValueAsync(searchTitle);
                    if (cache == null)
                    {
                        string response = await _newsAPI.searchNews(searchTitle);
                        bool redisStatus = await _redisHelper.SaveValueAsync(searchTitle, response);
                        if (!redisStatus)
                        {
                            throw new Exception("Write to redis Error");
                        }
                        newsDataAPIModel = JsonConvert.DeserializeObject<NewsDataAPIModel>(response);
                    }
                    else
                    {
                        newsDataAPIModel = JsonConvert.DeserializeObject<NewsDataAPIModel>(cache);
                    }
                }
                catch
                {
                    unitOfWork.Rollback();

                    throw;
                }

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return newsDataAPIModel;
        }

        private async Task<bool> saveToDB(string searchTitle, UnitOfWork unitOfWork)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("SearchTitle", searchTitle);
            bool dapperStatus = await _dapperHelper.ExecuteAsync("spInsertSearchHistory", unitOfWork, param);
            return dapperStatus;
        }
    }
}
