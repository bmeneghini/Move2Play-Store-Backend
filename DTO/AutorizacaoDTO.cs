using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTO
{
    public class RequisicaoAutorizacaoDTO
    {
        public string CNPJPrestador { get; set; }
        public string NomePrestador { get; set; }
        public string NumeroCarteiraBeneficiario { get; set; }
        public string NomeBeneficiario { get; set; }
        public string CartaoNacionalSaude { get; set; }
        public DateTime? DataAtual { get; set; }
        public DateTime? HoraAtual { get; set; }
        public bool RecemNascido { get; set; }
        public string RegistroANS { get; set; }
    }

    public class RetornoAutorizacaoDTO
    {
        public ElegibilidadeDTO Elegibilidade;
        public SolicitacaoProcedimentoDTO SolicitacaoProcedimento;
    }

}
