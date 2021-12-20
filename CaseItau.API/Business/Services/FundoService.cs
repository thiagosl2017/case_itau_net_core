using CaseItau.API.Data.Repository;
using CaseItau.API.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseItau.API.Business.Services
{
    public class FundoService
    {
        #region Atributos
        private readonly FundoRepository _fundoRepository;
        #endregion
       
        #region Services
        public FundoService(FundoRepository fundoRepository)
        {
            _fundoRepository = fundoRepository;
        }
        public async Task<List<Fundo>> ObterTodosFundos()
        {
            try
            {
                var fundos = await _fundoRepository.ObterTodosFundos();
                return fundos;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<List<Fundo>> ObterFundoPorCodigo(string codigo)
        {
            try
            {
                var fundoPorCodigo = await _fundoRepository.ObterFundoPorCodigo(codigo);
                return fundoPorCodigo;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<bool> CriarFundo([FromBody] Fundo value)
        {
            try
            {
                var fundoCriado = await _fundoRepository.CriarFundo(value);
                return fundoCriado;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<bool> MovimentarFundo(string codigo, decimal? patrimonio)
        {
            try
            {
                var fundoMovimentar = await _fundoRepository.MovimentarFundo(codigo, patrimonio);
                return fundoMovimentar;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<bool> ExcluirFundo(string codigo)
        {
            try
            {
                var fundoExcluir = await _fundoRepository.ExcluirFundo(codigo);
                return fundoExcluir;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<bool> AlterarFundo(string codigo, [FromBody] Fundo value)
        {
            try
            {
                var fundoAlterar = await _fundoRepository.AlterarFundo(codigo, value);
                return fundoAlterar;
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
