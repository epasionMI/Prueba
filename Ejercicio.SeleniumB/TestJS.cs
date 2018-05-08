using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Curso.SeleniumB
{
    class JavaScript
    {
        private IWebDriver driver;
        private string baseURL;
       

        [OneTimeSetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
            baseURL = "https://www.anaesthetist.com/mnm/javascript/calc.htm";
        }


        [Test, Property("priority", 0)]
        public void ModifiedWithJs()
        {
            try
            {
                driver.Navigate().GoToUrl(baseURL);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                js.ExecuteScript("document.getElementsByName('seven')[0].click()");
                js.ExecuteScript("document.getElementsByName('add')[0].click()");
                js.ExecuteScript("document.getElementsByName('two')[0].click()");
                js.ExecuteScript("document.getElementsByName('result')[0].click()");

                js.ExecuteScript("document.getElementsByName('seven')[0].style.display='none'");

                var Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                bool result = Wait.Until(x => x.FindElement(By.Name("Display")).GetAttribute("value") == "9");


                if (result == true)
                {
                    Console.WriteLine("El resultado ha sido correcto");
                }

                //este Assert definirá que el Test será PASS o FAIL

                Assert.True(result);


            }
            catch (Exception e)
            {
                Console.WriteLine("El resultado NO ha sido correcto");
                Console.WriteLine("Error capturado: " + e);
            }

        }

        [Test, Property("priority", 1)]
        public void AddingValueWithJS()
        {
            try
            {
                driver.Navigate().GoToUrl(baseURL);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;


                //inserción de multiplicación
                js.ExecuteScript("AddDigit('5')");
                js.ExecuteScript("document.getElementsByName('mul')[0].click()");
                js.ExecuteScript("AddDigit('3')");
                js.ExecuteScript("document.getElementsByName('result')[0].click()");

                Thread.Sleep(1000);

                var total = js.ExecuteScript("return document.Calculator.Display.value");

                Assert.AreEqual("15", total, "The result was not the expected.");
                Console.WriteLine("Result is correct: " + total);

            }
            catch (Exception e)
            {
                Console.WriteLine("Result is NOT correct\nError capturado: " + e);
            }

        }

      

        [OneTimeTearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
