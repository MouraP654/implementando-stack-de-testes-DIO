using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Vaquinha.AutomatedUITests
{
	public class DriverFactory
    {
        private IWebDriver _driver;

        // Construtor da classe. 
        public DriverFactory()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService("/usr/share/applications/");
            
            // Faz criação de porta para abrir o browser.
            service.Port = new Random().Next(64000, 64800);

            CodePagesEncodingProvider.Instance.GetEncoding(437);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("-headless");
            options.AddArgument("-safe-mode");
            options.AddArgument("-ignore-certificate-errors");
            FirefoxProfile profile = new FirefoxProfile();
            profile.AcceptUntrustedCertificates = true;
            profile.AssumeUntrustedCertificateIssuer = false;
            options.Profile = profile;
            
            _driver = new FirefoxDriver(service, options);
            
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Manage().Window.Maximize();
        }

        // Navega para determinada URL
        public void NavigateToUrl(String url)
        {            
            _driver.Navigate().GoToUrl(url);
        }

        // Finaliza driver e serviço.
        public void Close()
        {
            _driver.Quit();
        }

        // Disponibiliza driver.
        public IWebDriver GetWebDriver()
        {
            return _driver;
        }
    }
}