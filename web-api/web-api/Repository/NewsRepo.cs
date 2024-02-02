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
        private readonly int RowsPerPage = 3;
        public NewsRepo(IServiceProvider serviceProvider) 
        {
            _newsAPI = serviceProvider.GetRequiredService<NewsAPI>();
            _dapperHelper = serviceProvider.GetRequiredService<DapperHelper>();
            _redisHelper = serviceProvider.GetRequiredService<RedisHelper>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        public async Task<NewsDataAPIModel> searchByPagination(string searchTitle, int pageNumber)
        {
            NewsDataAPIModel newsDataAPIModel = new NewsDataAPIModel();
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(_configuration);
                unitOfWork.OpenConnection();

                try
                {
                    string cache = await _redisHelper.GetValueAsync(searchTitle);
                    if (cache == null)
                    {
                        string response = await _newsAPI.searchNews(searchTitle);
                        newsDataAPIModel = JsonConvert.DeserializeObject<NewsDataAPIModel>(response);
                        newsDataAPIModel.totalResults = newsDataAPIModel.results.Count;
                        response = JsonConvert.SerializeObject(newsDataAPIModel);

                        bool dapperStatus = await saveToDB(searchTitle, unitOfWork);
                        if (!dapperStatus)
                        {
                            throw new Exception("Write to Db Error");
                        }

                        bool redisStatus = await _redisHelper.SaveValueAsync(searchTitle, response);
                        if (!redisStatus)
                        {
                            throw new Exception("Write to redis Error");
                        }
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

            if (newsDataAPIModel.totalResults > 0)
            {
                List<NewsDataAPIModel.Results> _results = new List<NewsDataAPIModel.Results>();
                int indexTakeFrom = 0;
                int indexTakeTo = 0;
                if (pageNumber == 1)
                {
                    indexTakeFrom = 0;
                    indexTakeTo = (indexTakeFrom + RowsPerPage) - 1;
                }
                else
                {
                    indexTakeFrom = ((pageNumber * RowsPerPage) - RowsPerPage);
                    indexTakeTo = (indexTakeFrom + RowsPerPage) - 1;
                }

                for (int i = indexTakeFrom; i <= indexTakeTo; i++)
                {
                    if ((newsDataAPIModel.totalResults - 1) < i)
                    {
                        break;
                    }
                    _results.Add(newsDataAPIModel.results[i]);
                }
                newsDataAPIModel.results = _results;
            }

            return newsDataAPIModel;
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
                        newsDataAPIModel = JsonConvert.DeserializeObject<NewsDataAPIModel>(response);
                        newsDataAPIModel.totalResults = newsDataAPIModel.results.Count;
                        response = JsonConvert.SerializeObject(newsDataAPIModel);

                        bool redisStatus = await _redisHelper.SaveValueAsync(searchTitle, response);
                        if (!redisStatus)
                        {
                            throw new Exception("Write to redis Error");
                        }
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

            if (newsDataAPIModel.totalResults > 0)
            {
                List<NewsDataAPIModel.Results> _results = new List<NewsDataAPIModel.Results>();
                for (int i = 0; i < newsDataAPIModel.results.Count(); i++)
                {
                    _results.Add(newsDataAPIModel.results[i]);
                    if (i == (RowsPerPage - 1))
                    {
                        newsDataAPIModel.results = _results;
                        break;
                    }
                }
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
