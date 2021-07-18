using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;

namespace GFTPolandBot.FrameworkTools
{
    public abstract class AutomationBase
    {
        public RemoteWebDriver Driver;

        public virtual void SetUp()
        {
            var chromOpt = new ChromeOptions();
            chromOpt.AddAdditionalCapability("useAutomationExtension", false);

            Driver = new ChromeDriver(chromOpt);
            
            Driver.Manage().Window.Maximize();
            Driver.Manage().Cookies.DeleteAllCookies();
        }
        public void TearDown()
        {
            if(Driver !=null)
            {
                Driver.Close();
                Driver.Quit();
            }
        }
    }
}
