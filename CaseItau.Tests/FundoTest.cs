using CaseItau.API.Data.Repository;
using CaseItau.API.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaseItau.Tests
{
    [TestClass]
    public class FundoTest
    {
        #region Atributos
        private readonly FundoRepository _fundoRepository;
        private readonly Fundo _fundo;
        #endregion

        [TestMethod]
        public void ObterTodosFundosTest()
        {            
            Assert.AreEqual(_fundoRepository.ObterTodosFundos(), 1);
        }
        [TestMethod]
        public void ObterFundoPorCodigoTest()
        {
            var codigo = "ITAURF123";

            Assert.AreEqual(_fundoRepository.ObterFundoPorCodigo(codigo), "ITAURF123");
        }
        [TestMethod]
        public void CriarFundoTest()
        {
            _fundo.Codigo = "Teste123";
            _fundo.Nome = "Salatiel";
            _fundo.Cnpj = "159753852963147";
            _fundo.CodigoTipo = 3;
            _fundo.Nome = "MULTI MERCARDO";
            _fundo.Patrimonio = 800;
            
            Assert.AreEqual(_fundoRepository.CriarFundo(_fundo), true);
        }
        [TestMethod]
        public void MovimentarPatrimonioTest()
        {
            var codigo = "9";
            decimal? patrimonio = 50;

            Assert.AreEqual(_fundoRepository.MovimentarFundo(codigo, patrimonio), true);
        }
        [TestMethod]
        public void ExcluirFundoTest()
        {
            var codigo = "9";

            Assert.AreEqual(_fundoRepository.ExcluirFundo(codigo), true);
        }
        [TestMethod]
        public void AlterarFundoTest()
        {            
            _fundo.Codigo = "9";
            _fundo.Nome = "Salatiel";
            _fundo.Cnpj = "159753852963147";
            _fundo.CodigoTipo = 3;
            _fundo.Nome = "MULTI MERCARDO";
            _fundo.Patrimonio = 800;

            Assert.AreEqual(_fundoRepository.AlterarFundo(_fundo.Codigo, _fundo), true);
        }
    }
}