using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTO
{

    public class SolicitacaoProcedimentoDTO
    {
        public string NumeroGuiaPrestador { get; set; }
        public string NumeroGuiaOperadora { get; set; }
        public DateTime? DataAutorizacao { get; set; }

        public string Senha { get; set; }
        public DateTime? DataValidadeSenha { get; set; }

        public string StatusSolicitacao { get; set; }
        public string CodigoMotivoNegativa { get; set; }
        public string DescricaoMotivoNegativa { get; set; }

        public SolicitacaoProcedimentoBeneficiarioDTO Beneficiario { get; set; }

        public SolicitacaoProcedimentoContratadoDTO Contratado { get; set; }

        public SolicitacaoProcedimentoProcedimentoDTO Procedimento { get; set; }
        
    }

    public class SolicitacaoProcedimentoBeneficiarioDTO
    {
        public string NumeroCarteira { get; set; }
        public string Nome { get; set; }
        public string CartaoNacionalSaude { get; set; }
        public bool? IdentificadorRecemNato { get; set; }
    }

    public class SolicitacaoProcedimentoContratadoDTO
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string CNES { get; set; }
    }

    public class SolicitacaoProcedimentoProcedimentoDTO
    {
        public string CodigoTabela { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string QuantidadeSolicitada { get; set; }
        public string QuantidadeAutorizada { get; set; }
        public decimal ValorMaterialSolicitado { get; set; }
        public decimal ValorMaterialAutorizado { get; set; }
        public string OrdemOpcaoMaterialFabricante { get; set; }
        public string RegistroAnvisaMaterialSolicitado { get; set; }
        public string CodigoFabricanteMaterialSolicitado { get; set; }
        public string NumeroAutorizacaoMaterialEmpresa { get; set; }
    }

}
