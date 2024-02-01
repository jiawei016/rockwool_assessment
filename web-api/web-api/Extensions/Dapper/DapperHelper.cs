using Dapper;
using System.Data;
using web_api.Extensions.Process;

namespace web_api.Extensions.Dapper
{
    public class DapperHelper
    {
        private readonly Serilog.ILogger _logger;
        private readonly ProcessHelper _processHelper;
        public DapperHelper(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<Serilog.ILogger>();
            _processHelper = serviceProvider.GetRequiredService<ProcessHelper>();
        }
        public async Task<bool> ExecuteAsync(string storedProcedure, UnitOfWork unitOfWork, Dictionary<string, string> param)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                foreach (var dict in param)
                {
                    parameters.Add(dict.Key, dict.Value);
                }

                await unitOfWork.GetConnection().ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: unitOfWork.GetTransaction());

                _logger.Information($"ProcessID : {_processHelper.getProcessId()} --- Write Into {storedProcedure}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"ProcessID : {_processHelper.getProcessId()} --- SQL Error {ex.ToString()}");
                Console.WriteLine(ex.ToString());

                return false;
            }
        }
    }
}
