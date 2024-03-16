using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Serializer
{
    [TestClass]
    public class Skripta
    {
        IWebDriver driver = new ChromeDriver();
        string user = "";
        string pass = "";

        [TestMethod]
        public void SkriptaBaki()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://ais.usvisa-info.com/en-rs/niv/groups/39910992");
            driver.FindElement(By.ClassName("ui-button")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div[5]/main/div[3]/div/div[1]/div/form/div[3]/label/div")).Click();//Id("policy_confirmed")).Click() ;
            driver.FindElement(By.Id("user_email")).SendKeys(user);
            driver.FindElement(By.Id("user_password")).SendKeys(pass + Keys.Enter);
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("/html/body/div[4]/main/div[2]/div[2]/div[1]/div/div/div[1]/div[2]/ul/li/a")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("/html/body/div[4]/main/div[2]/div[2]/div/section/ul/li[3]/a")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("/html/body/div[4]/main/div[2]/div[2]/div/section/ul/li[3]/div/div/div[2]/p[2]/a")).Click();
            bool foundElement = false;

            while (!foundElement)
            {
                try
                {
                    Thread.Sleep(1000);
                    driver.FindElement(By.Id("appointments_consulate_appointment_date")).Click();
                    IWebElement march = driver.FindElement(By.XPath("/html/body/div[5]/div[1]/table/tbody"));
                    IReadOnlyCollection<IWebElement> datumi = march.FindElements(By.CssSelector("td"));

                    foreach (IWebElement dat in datumi)
                    {
                        string name = dat.GetAttribute("class");
                        if (!name.Contains("disabled"))
                        {
                            IWebElement datum = dat.FindElement(By.CssSelector("span.ui-state-default"));
                            string innerText = dat.Text.Trim();
                            dat.Click();
                            driver.FindElement(By.Id("appointments_consulate_appointment_time")).Click();
                            Actions actions = new Actions(driver);
                            actions.SendKeys(Keys.Down).SendKeys(Keys.Enter).Perform();
                            driver.FindElement(By.Name("commit")).Click();
                            Thread.Sleep(2000);
                            actions.SendKeys(Keys.Enter).Perform();
                            foundElement = true;
                            Console.WriteLine($"Succsesfully scheduled for {innerText}. of March ");
                            break;
                        }
                        if (!foundElement)
                        {
                            driver.Navigate().Refresh();
                            Thread.Sleep(2000);
                            foundElement = false;
                        }
                    }
                }

                catch (Exception)
                {
                    driver.Navigate().Refresh();
                    Thread.Sleep(2000);
                    foundElement = false;
                }
            }
        }
    }
}