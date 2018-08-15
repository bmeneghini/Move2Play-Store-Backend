using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Prestador
    {
        public Prestador()
        {
            Atendimento = new HashSet<Atendimento>();
            ProfissionalExecutante = new HashSet<ProfissionalExecutante>();
        }

        public int Id { get; set; }
        public string CpfCnpj { get; set; }
        public string Nome { get; set; }
        public string Cnes { get; set; }

        public ICollection<Atendimento> Atendimento { get; set; }
        public ICollection<ProfissionalExecutante> ProfissionalExecutante { get; set; }
    }
}
