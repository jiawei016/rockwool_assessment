using Newtonsoft.Json;
using StackExchange.Redis;
using web_api.Extensions.Process;
using web_api.Models;

namespace web_api.Extensions.Redis
{
    public class RedisHelper
    {
        private readonly IConfiguration _configuration;
        private readonly Serilog.ILogger _logger;
        private readonly ProcessHelper _processHelper;
        public RedisHelper(IServiceProvider serviceProvider)
        {
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
            _logger = serviceProvider.GetRequiredService<Serilog.ILogger>();
            _processHelper = serviceProvider.GetRequiredService<ProcessHelper>();
        }
        public IDatabase GetConnection()
        {
            var _redisConnection = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("RedisConnection"));
            return _redisConnection.GetDatabase();
        }
        public async Task<string> GetValueAsync(string redisKey)
        {
            string redisValue = null;
            try
            {
                string _value = await GetConnection().StringGetAsync(redisKey);
                if (!string.IsNullOrEmpty(_value))
                {
                    redisValue = _value;
                }

                _logger.Information($"ProcessID : {_processHelper.getProcessId()} --- Get from redis on key {redisKey}");
                return redisValue;
            }
            catch (Exception ex)
            {
                _logger.Error($"ProcessID : {_processHelper.getProcessId()} --- redis error {ex.ToString()}");
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                redisValue = null;
            }

            return redisValue;
        }

        public async Task<bool> SaveValueAsync(string redisKey, string redisValue)
        {
            try
            {
                bool _is_set = await GetConnection().StringSetAsync(redisKey, redisValue, new TimeSpan(3, 0, 0));
                _logger.Information($"ProcessID : {_processHelper.getProcessId()} --- Set to redis on key {redisKey}");
                if (_is_set)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"ProcessID : {_processHelper.getProcessId()} --- redis error {ex.ToString()}");
                Console.WriteLine(ex.ToString());
            }

            return false;
        }
    }
}
