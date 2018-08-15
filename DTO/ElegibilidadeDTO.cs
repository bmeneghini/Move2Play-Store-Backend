using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTO
{
    public class ElegibilidadeDTO
    {
        public string RegistroANS { get; set; }
        public string NumeroCarteiraBeneficiario { get; set; }
        public string NomeBeneficiario { get; set; }
        public string CartaoNacionalSaude { get; set; }
        public bool? IdentificadorBiometricoBeneficiario { get; set; }
        public string IndicadorElegibilidade { get; set; }
        public string CodigoMotivoNegativa { get; set; }
        public string DescricaoMotivoNegativa { get; set; }
        public DateTime? DataValidadeCarteira { get; set; }
    }
}
