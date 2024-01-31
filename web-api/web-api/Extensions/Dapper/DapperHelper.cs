using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace web_api.Extensions.Dapper
{
    public class DapperHelper
    {
        public async Task ExecuteAsync(string storedProcedure, UnitOfWork unitOfWork, Dictionary<string, string> param)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                foreach (var dict in param)
                {
                    parameters.Add(dict.Key, dict.Value);
                }

                await unitOfWork.GetConnection().ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: unitOfWork.GetTransaction());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
