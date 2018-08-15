using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.Extensions.Logging;
using Api.DTO;
using Api.Business;
using System.ServiceModel;
using Api.Shared;
using Api.Repositories;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Controllers
{
    [Authorize]
    [AllowAnonymous]
    [Route("[controller]")]
    public class PrestadorController : Controller
    {
        private IPrestadorRepository prestadorRepository;

        public PrestadorController(IPrestadorRepository prestadorRepository)
        {
            this.prestadorRepository = prestadorRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Prestador>> Get()
        {
            return await this.prestadorRepository.GetPrestadores();
        }

        [HttpGet("{id}")]
        public async Task<Prestador> Get(int PrestadorId)
        {
            return await this.prestadorRepository.GetPrestador(PrestadorId);
        }

        [HttpPost]
        public async Task Post([FromBody]Prestador prestador)
        {
            await this.prestadorRepository.AddPrestador(prestador);
        }

        [HttpPut("{id}")]
        public async Task Put(int PrestadorId, [FromBody]Prestador prestador)
        {
            await this.prestadorRepository.UpdatePrestador(prestador);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int PrestadorId)
        {
            await this.prestadorRepository.DeletePrestador(PrestadorId);
        }
    }
}
