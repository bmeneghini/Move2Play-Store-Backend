using Api.SolicitacaoProcedimento;
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
    public class SolicitacaoProcedimentoBusiness
    {

        public SolicitacaoProcedimentoDTO solicitarProcedimento(RequisicaoAutorizacaoDTO requisicao)
        {
            var resposta = obterRespostaSolicitacaoProcedimento(requisicao);

            if (resposta != null && resposta.dadosAutorizacao != null)
            {
                var retorno = new SolicitacaoProcedimentoDTO() {
                    Senha = resposta.dadosAutorizacao.senha,
                    DataValidadeSenha = resposta.dadosAutorizacao.dataValidadeSenha,
                    DataAutorizacao = resposta.dadosAutorizacao.dataAutorizacao,
                    NumeroGuiaOperadora = resposta.dadosAutorizacao.numeroGuiaOperadora,
                    NumeroGuiaPrestador = resposta.dadosAutorizacao.numeroGuiaPrestador,
                    StatusSolicitacao = resposta.statusSolicitacao.ToString(),
                    Beneficiario = new SolicitacaoProcedimentoBeneficiarioDTO(),
                    Contratado = new SolicitacaoProcedimentoContratadoDTO(),
                    Procedimento = new SolicitacaoProcedimentoProcedimentoDTO(),
                };

                if (resposta.motivosNegativa != null && resposta.motivosNegativa.Length > 0)
                {
                    retorno.CodigoMotivoNegativa = resposta.motivosNegativa.FirstOrDefault().codigoGlosa.ToString();
                    retorno.DescricaoMotivoNegativa = resposta.motivosNegativa.FirstOrDefault().descricaoGlosa;
                }

                if (resposta.dadosBeneficiario != null)
                {
                    retorno.Beneficiario = new SolicitacaoProcedimentoBeneficiarioDTO(){
                        Nome = resposta.dadosBeneficiario.nomeBeneficiario,
                        NumeroCarteira = resposta.dadosBeneficiario.numeroCarteira,
                        CartaoNacionalSaude = resposta.dadosBeneficiario.numeroCNS != null ? resposta.dadosBeneficiario.numeroCNS : "",
                        IdentificadorRecemNato = resposta.dadosBeneficiario.atendimentoRN == dm_simNao.S
                    };

                }

                if (resposta.prestadorAutorizado != null && resposta.prestadorAutorizado.dadosContratado != null)
                {
                    retorno.Contratado = new SolicitacaoProcedimentoContratadoDTO()
                    {
                        CNES = resposta.prestadorAutorizado.cnesContratado,
                        Nome = resposta.prestadorAutorizado.dadosContratado.nomeContratado,
                        Codigo = resposta.prestadorAutorizado.dadosContratado.Item
                    };
                }

                if (resposta.servicosAutorizados != null && resposta.servicosAutorizados.Length > 0 && resposta.servicosAutorizados.FirstOrDefault().procedimento != null)
                {
                    retorno.Procedimento = new SolicitacaoProcedimentoProcedimentoDTO()
                    {
                        Codigo = resposta.servicosAutorizados.FirstOrDefault().procedimento.codigoProcedimento,
                        CodigoTabela = resposta.servicosAutorizados.FirstOrDefault().procedimento.codigoTabela.ToString(),
                        Descricao = resposta.servicosAutorizados.FirstOrDefault().procedimento.descricaoProcedimento,
                        QuantidadeAutorizada = resposta.servicosAutorizados.FirstOrDefault().quantidadeAutorizada,
                        QuantidadeSolicitada = resposta.servicosAutorizados.FirstOrDefault().quantidadeSolicitada,
                        CodigoFabricanteMaterialSolicitado = resposta.servicosAutorizados.FirstOrDefault().codigoRefFabricante,
                        NumeroAutorizacaoMaterialEmpresa = resposta.servicosAutorizados.FirstOrDefault().autorizacaoFuncionamento,
                        OrdemOpcaoMaterialFabricante = resposta.servicosAutorizados.FirstOrDefault().opcaoFabricante.ToString(),
                        RegistroAnvisaMaterialSolicitado = resposta.servicosAutorizados.FirstOrDefault().registroANVISA,
                        ValorMaterialAutorizado = resposta.servicosAutorizados.FirstOrDefault().valorAutorizado,
                        ValorMaterialSolicitado = resposta.servicosAutorizados.FirstOrDefault().valorSolicitado,
                    };
                }

                return retorno;
            }
            else
            {
                return null;
            }
        }

        private ctm_autorizacaoServico obterRespostaSolicitacaoProcedimento(RequisicaoAutorizacaoDTO parametrosRequisicao)
        {
            solicitacaoProcedimentoWS solicitacao = new solicitacaoProcedimentoWS();

            solicitacao.cabecalho = new cabecalhoTransacao();

            solicitacao.cabecalho = new cabecalhoTransacao();
            solicitacao.cabecalho.identificacaoTransacao = new cabecalhoTransacaoIdentificacaoTransacao();
            solicitacao.cabecalho.identificacaoTransacao.tipoTransacao = dm_tipoTransacao.SOLICITACAO_PROCEDIMENTOS;
            solicitacao.cabecalho.identificacaoTransacao.sequencialTransacao = "333";
            solicitacao.cabecalho.identificacaoTransacao.dataRegistroTransacao = parametrosRequisicao.DataAtual.HasValue ? parametrosRequisicao.DataAtual.Value : System.DateTime.Today;
            solicitacao.cabecalho.identificacaoTransacao.horaRegistroTransacao = parametrosRequisicao.HoraAtual.HasValue ? parametrosRequisicao.HoraAtual.Value : System.DateTime.Now;
            solicitacao.cabecalho.Padrao = dm_versao.Item30301;

            cabecalhoTransacaoOrigemIdentificacaoPrestador prestadorOrigem = new cabecalhoTransacaoOrigemIdentificacaoPrestador();
            prestadorOrigem.Item = parametrosRequisicao.CNPJPrestador;
            prestadorOrigem.ItemElementName = ItemChoiceType.CNPJ;

            ct_prestadorIdentificacao prestadorDestino = new ct_prestadorIdentificacao();
            prestadorDestino.Item = parametrosRequisicao.CNPJPrestador;
            prestadorDestino.ItemElementName = ItemChoiceType.CNPJ;

            solicitacao.cabecalho.origem = new cabecalhoTransacaoOrigem();
            solicitacao.cabecalho.origem.Item = prestadorOrigem;

            solicitacao.cabecalho.destino = new cabecalhoTransacaoDestino();
            solicitacao.cabecalho.destino.Item = prestadorDestino;

            solicitacao.solicitacaoProcedimento = new ct_solicitacaoProcedimento();

            ctm_spsadtSolicitacaoGuia guia = new ctm_spsadtSolicitacaoGuia();

            guia.cabecalhoSolicitacao = new ct_guiaCabecalho();
            guia.cabecalhoSolicitacao.registroANS = parametrosRequisicao.RegistroANS;
            guia.cabecalhoSolicitacao.numeroGuiaPrestador = "1";

            guia.dadosBeneficiario = new ct_beneficiarioDados();
            guia.dadosBeneficiario.atendimentoRN = parametrosRequisicao.RecemNascido ? dm_simNao.S : dm_simNao.N;
            guia.dadosBeneficiario.numeroCarteira = parametrosRequisicao.NumeroCarteiraBeneficiario;
            guia.dadosBeneficiario.nomeBeneficiario = parametrosRequisicao.NomeBeneficiario;
            guia.dadosBeneficiario.numeroCNS = parametrosRequisicao.CartaoNacionalSaude.Length > 0 ? parametrosRequisicao.CartaoNacionalSaude : "1";

            guia.dadosSolicitante = new ctm_spsadtSolicitacaoGuiaDadosSolicitante();
            guia.dadosSolicitante.contratadoSolicitante = new ct_contratadoDados();
            guia.dadosSolicitante.contratadoSolicitante.Item = parametrosRequisicao.CNPJPrestador;
            guia.dadosSolicitante.contratadoSolicitante.ItemElementName = ItemChoiceType1.cnpjContratado;
            guia.dadosSolicitante.contratadoSolicitante.nomeContratado = parametrosRequisicao.NomePrestador;
            guia.dadosSolicitante.profissionalSolicitante = new ct_contratadoProfissionalDados();
            guia.dadosSolicitante.profissionalSolicitante.nomeProfissional = "PLANTONISTA";
            guia.dadosSolicitante.profissionalSolicitante.numeroConselhoProfissional = "999999";
            guia.dadosSolicitante.profissionalSolicitante.UF = dm_UF.Item31;
            guia.dadosSolicitante.profissionalSolicitante.conselhoProfissional = dm_conselhoProfissional.Item06;
            guia.dadosSolicitante.profissionalSolicitante.CBOS = dm_CBOS.Item999999;

            guia.dataSolicitacao = parametrosRequisicao.DataAtual.HasValue ? parametrosRequisicao.DataAtual.Value : DateTime.Today;
            guia.caraterAtendimento = dm_caraterAtendimento.Item2;
            

            List<ctm_spsadtSolicitacaoGuiaProcedimentosSolicitados> listaProcedimentos = new List<ctm_spsadtSolicitacaoGuiaProcedimentosSolicitados>();
            ctm_spsadtSolicitacaoGuiaProcedimentosSolicitados procedimentoSolicitado = new ctm_spsadtSolicitacaoGuiaProcedimentosSolicitados();
            procedimentoSolicitado.procedimento = new ct_procedimentoDados();
            procedimentoSolicitado.procedimento.codigoTabela = dm_tabela.Item22;
            procedimentoSolicitado.procedimento.codigoProcedimento = "10101039";
            procedimentoSolicitado.procedimento.descricaoProcedimento = "Consulta em Pronto Socorro";
            procedimentoSolicitado.quantidadeSolicitada = "1";
            listaProcedimentos.Add(procedimentoSolicitado);
            guia.procedimentosSolicitados = listaProcedimentos.ToArray();

            //guia.dadosExecutante = new ctm_spsadtSolicitacaoGuiaDadosExecutante();
            //guia.dadosExecutante.codigonaOperadora = parametrosRequisicao.CNPJPrestador;
            //guia.dadosExecutante.nomeContratado = parametrosRequisicao.NomePrestador;
            //guia.dadosExecutante.CNES = "9999999";

            solicitacao.solicitacaoProcedimento.Item = guia;

            solicitacao.hash = "89f40990bc9cf7541e239dad0ca907fe";

            Binding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress("http://187.94.62.176:20807/tissSolicitacaoProcedimentoV3_03_01.apw");
            tissSolicitacaoProcedimento_PortTypeClient client = new tissSolicitacaoProcedimento_PortTypeClient(binding, address);

            client.Endpoint.EndpointBehaviors.Add(new InspectorBehavior());

            var response = client.tissSolicitacaoProcedimento_Operation(solicitacao);

            var item = response.autorizacaoProcedimento != null ? response.autorizacaoProcedimento.Item : response.autorizacaoProcedimento;

            /*
            if(item != null && typeof(ct_motivoGlosa) == item.GetType())
            {
                var motivoGlosa = (ct_motivoGlosa)item;
                string message = "Glosa " + motivoGlosa.codigoGlosa + ": " + motivoGlosa.descricaoGlosa;
                throw new BusinessException(message);
            }
            */

            return (ctm_autorizacaoServico)item;

        }

    }
}
