using Api.Elegiblidade;
using Api.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

using Api.DTO;

namespace Api.Business
{
    public class ElegibilidadeBusiness
    {

        public ElegibilidadeDTO verificaElegibilidade(RequisicaoAutorizacaoDTO requisicao)
        {
            var resposta = obterRespostaElegibilidade(requisicao);
            
            if (resposta != null)
            {
                var retorno = new ElegibilidadeDTO();

                retorno.CartaoNacionalSaude = resposta.numeroCNS;
                retorno.NomeBeneficiario = resposta.nomeBeneficiario;
                retorno.NumeroCarteiraBeneficiario = resposta.numeroCarteira;
                retorno.RegistroANS = resposta.registroANS;
                retorno.IndicadorElegibilidade = resposta.respostaSolicitacao == dm_simNao.S ? "S" : "N";
                if (resposta.respostaSolicitacao == dm_simNao.N && resposta.motivosNegativa != null && resposta.motivosNegativa.Length > 0)
                {
                    retorno.CodigoMotivoNegativa = resposta.motivosNegativa.FirstOrDefault().codigoGlosa.ToString();
                    retorno.DescricaoMotivoNegativa = resposta.motivosNegativa.FirstOrDefault().descricaoGlosa;
                }
                retorno.DataValidadeCarteira = resposta.validadeCarteira;

                return retorno;
            }
            else
            {
                return null;
            }
        }

        private ct_elegibilidadeRecibo obterRespostaElegibilidade(RequisicaoAutorizacaoDTO parametrosRequisicao)
        {
            pedidoElegibilidadeWS pedido = new pedidoElegibilidadeWS();

            pedido.cabecalho = new cabecalhoTransacao();
            pedido.cabecalho.identificacaoTransacao = new cabecalhoTransacaoIdentificacaoTransacao();
            pedido.cabecalho.identificacaoTransacao.tipoTransacao = dm_tipoTransacao.VERIFICA_ELEGIBILIDADE;
            pedido.cabecalho.identificacaoTransacao.sequencialTransacao = "1";
            pedido.cabecalho.identificacaoTransacao.dataRegistroTransacao = parametrosRequisicao.DataAtual.HasValue ? parametrosRequisicao.DataAtual.Value : System.DateTime.Today;
            pedido.cabecalho.identificacaoTransacao.horaRegistroTransacao = parametrosRequisicao.HoraAtual.HasValue ? parametrosRequisicao.HoraAtual.Value : System.DateTime.Now;
            pedido.cabecalho.Padrao = dm_versao.Item30301;

            cabecalhoTransacaoOrigemIdentificacaoPrestador prestadorOrigem = new cabecalhoTransacaoOrigemIdentificacaoPrestador();
            prestadorOrigem.Item = parametrosRequisicao.CNPJPrestador;
            prestadorOrigem.ItemElementName = ItemChoiceType.CNPJ;

            ct_prestadorIdentificacao prestadorDestino = new ct_prestadorIdentificacao();
            prestadorDestino.Item = parametrosRequisicao.CNPJPrestador;
            prestadorDestino.ItemElementName = ItemChoiceType.CNPJ;

            pedido.cabecalho.origem = new cabecalhoTransacaoOrigem();
            pedido.cabecalho.origem.identificacaoPrestador = prestadorOrigem;
            //pedido.cabecalho.origem.registroANS = "417505";

            pedido.cabecalho.destino = new cabecalhoTransacaoDestino();
            pedido.cabecalho.destino.identificacaoPrestador = prestadorDestino;
            //pedido.cabecalho.destino.registroANS = "417505";

            pedido.pedidoElegibilidade = new ct_elegibilidadeVerifica();
            pedido.pedidoElegibilidade.dadosPrestador = new ct_contratadoDados();
            pedido.pedidoElegibilidade.dadosPrestador.Item = parametrosRequisicao.CNPJPrestador;
            pedido.pedidoElegibilidade.dadosPrestador.ItemElementName = ItemChoiceType1.cnpjContratado;
            pedido.pedidoElegibilidade.dadosPrestador.nomeContratado = parametrosRequisicao.NomePrestador;

            pedido.pedidoElegibilidade.numeroCarteira = parametrosRequisicao.NumeroCarteiraBeneficiario;
            pedido.pedidoElegibilidade.nomeBeneficiario = parametrosRequisicao.NomeBeneficiario != null ? parametrosRequisicao.NomeBeneficiario : "NOME FICTICIO";
            pedido.pedidoElegibilidade.numeroCNS = parametrosRequisicao.CartaoNacionalSaude.Length > 0 ? parametrosRequisicao.CartaoNacionalSaude : "1"; 
            
            pedido.hash = "8ba95497190d34fc350f18dbdf24bc5e";

            Binding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://187.94.62.176:20807/tissVerificaElegibilidadeV3_03_01.apw");
            tissVerificaElegibilidade_PortTypeClient client = new tissVerificaElegibilidade_PortTypeClient(binding, address);

            client.Endpoint.EndpointBehaviors.Add(new InspectorBehavior());

            var response = client.tissVerificaElegibilidade_Operation(pedido);

            var item = response.respostaElegibilidade != null ? response.respostaElegibilidade.Item : response.respostaElegibilidade;
            
            /*
            if (item != null && typeof(ct_motivoGlosa) == item.GetType())
            {
                var motivoGlosa = (ct_motivoGlosa)item;
                string message = "Glosa " + motivoGlosa.codigoGlosa + ": " + motivoGlosa.descricaoGlosa;
                throw new BusinessException(message);
            }
            */

            return (ct_elegibilidadeRecibo)item;

        }
        
    }
}
