using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace GFTPolandBot.FrameworkTools
{
    
    public abstract class WebEntity
    {
        public RemoteWebDriver Driver;

        public WebEntity(RemoteWebDriver driver)
        {
            Driver = driver;
        }
        public void CliuckOnElementUsingJavaScript(By e)
        {
            IJavaScriptExecutor ex = Driver;
            ex.ExecuteScript("arguments[0].click();", Driver.FindElement(e));
        }
        public void ScrollPageToElement(By e)
        {
            IJavaScriptExecutor ex = Driver;
            ex.ExecuteScript("arguments[0].scollIntoView({block: 'end'});", Driver.FindElement(e));
        }
        public void ClickOnElement(By e)
        {
            Driver.FindElement(e).Click();
        }
        public void FillTextBox(By e, string  text)
        {
            Driver.FindElement(e).SendKeys(text);
        }
        public void HoverOverElement(By e)
        {
            Actions action = new Actions(Driver);
            action.MoveToElement(Driver.FindElement(e)).Perform();
            action.MoveToElement(Driver.FindElement(e)).Perform();
        }
        public void ChangeTabWindow(int tabIndexToClose, int tabIndexToSwitch)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[tabIndexToSwitch]);
        }
    }
}
