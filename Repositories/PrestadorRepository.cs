using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Api.Repositories
{
    public class PrestadorRepository : IPrestadorRepository
    {
        public PrestadorRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        private readonly string connectionString = "Server=(local)\\SQLEXPRESS;Database=CemigAutorizador;Trusted_Connection=True;";
        //private readonly string connectionString = Configuration.GetConnectionString("DefaultConnection");

        private SqlConnection sqlConnection;

        public async Task AddPrestador(Prestador prestador)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Id", prestador.Id);
                dynamicParameters.Add("@CpfCnpj", prestador.CpfCnpj);
                dynamicParameters.Add("@Nome", prestador.Nome);
                dynamicParameters.Add("@Cnes", prestador.Cnes);

                await sqlConnection.ExecuteAsync(
                    "spAddPrestador",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeletePrestador(int PrestadorId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Id", PrestadorId);
                await sqlConnection.ExecuteAsync(
                    "spDeletePrestador",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Prestador> GetPrestador(int PrestadorId)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Id", PrestadorId);
                return await sqlConnection.QuerySingleOrDefaultAsync<Prestador>(
                    "spGetPrestador",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Prestador>> GetPrestadores()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                return await sqlConnection.QueryAsync<Prestador>(
                    "spGetPrestadores",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePrestador(Prestador prestador)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Id", prestador.Id);
                dynamicParameters.Add("@CpfCnpj", prestador.CpfCnpj);
                dynamicParameters.Add("@Nome", prestador.Nome);
                dynamicParameters.Add("@Cnes", prestador.Cnes);
                await sqlConnection.ExecuteAsync(
                    "spUpdatePrestador",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
