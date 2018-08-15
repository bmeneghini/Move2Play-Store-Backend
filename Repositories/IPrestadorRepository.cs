using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repositories
{
   public interface IPrestadorRepository
    {
        Task<IEnumerable<Prestador>> GetPrestadores();

        Task<Prestador> GetPrestador(int PrestadorId);

        Task AddPrestador(Prestador prestador);

        Task UpdatePrestador(Prestador prestador);

        Task DeletePrestador(int PrestadorId);
    }
}
