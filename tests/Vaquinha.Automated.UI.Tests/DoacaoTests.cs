using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
	public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>, 
                                               IClassFixture<EnderecoFixture>, 
                                               IClassFixture<CartaoCreditoFixture>
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DoacaoUI_AcessoTelaHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
		[Fact]
		public void DoacaoUI_CriacaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			IWebElement campoNome = _driver.FindElement(By.Id("DadosPessoais_Nome"));
			campoNome.SendKeys(doacao.DadosPessoais.Nome);

			IWebElement campoEmail = _driver.FindElement(By.Id("DadosPessoais_Email"));
			campoNome.SendKeys(doacao.DadosPessoais.Email);
			
			IWebElement campoMsgApoio = _driver.FindElement(By.Id("DadosPessoais_MensagemApoio"));
			campoNome.SendKeys(doacao.DadosPessoais.MensagemApoio);

			//Dados do Endereço
			IWebElement campoEndereçoCobrança = _driver.FindElement(By.Id("EnderecoCobranca_TextoEndereco"));
			campoNome.SendKeys(doacao.EnderecoCobranca.TextoEndereco);

			IWebElement campoNumeroEndereçoCobrança = _driver.FindElement(By.Id("EnderecoCobranca_Numero"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Numero);

			IWebElement campoCidadeEndereçoCobrança = _driver.FindElement(By.Id("EnderecoCobranca_Cidade"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Cidade);
			
			IWebElement campoEstadoEndereçoCobrança = _driver.FindElement(By.Id("EnderecoCobranca_Estado"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Estado);

			IWebElement campoCepEndereçoCobrança = _driver.FindElement(By.Id("EnderecoCobranca_Cep"));
			campoNome.SendKeys(doacao.EnderecoCobranca.CEP);

			IWebElement campoComplementoEndereçoCobrança = _driver.FindElement(By.Id("EnderecoCobranca_Complemento"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Complemento);

			IWebElement campoTelefoneEndereçoCobrança = _driver.FindElement(By.Id("EnderecoCobranca_Telefone"));
			campoNome.SendKeys(doacao.EnderecoCobranca.Telefone);

			//Dados da forma de pagamento
			IWebElement campoFormaPagamentoNomeTitular = _driver.FindElement(By.Id("FormaPagamento_NomeTitular"));
			campoNome.SendKeys(doacao.FormaPagamento.NomeTitular);

			IWebElement campoNumeroCartaoCredito = _driver.FindElement(By.Id("cardNumber"));
			campoNome.SendKeys(doacao.FormaPagamento.NumeroCartaoCredito);

			IWebElement campoValidadeCartaoCredito = _driver.FindElement(By.Id("validade"));
			campoNome.SendKeys(doacao.FormaPagamento.Validade);

			IWebElement campoCVVCartaoCredito = _driver.FindElement(By.Id("cvv"));
			campoNome.SendKeys(doacao.FormaPagamento.CVV);

			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//Assert
			_driver.Url.Should().Contain("/Doacoes/Create");
			_driver.Url.Should().Contain("/Home/Index");
		}
	}
}