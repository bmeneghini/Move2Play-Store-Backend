using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class ProfissionalExecutante
    {
        public ProfissionalExecutante()
        {
            OcupacaoProfissional = new HashSet<OcupacaoProfissional>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Conselho { get; set; }
        public string Estado { get; set; }
        public string Numeroconselho { get; set; }
        public int Idprestador { get; set; }

        public Prestador IdprestadorNavigation { get; set; }
        public ICollection<OcupacaoProfissional> OcupacaoProfissional { get; set; }
    }
}
