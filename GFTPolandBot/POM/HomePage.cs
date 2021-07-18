using GFTPolandBot.FrameworkTools;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GFTPolandBot.Utilities;

namespace GFTPolandBot.POM
{
    public class HomePage : PageModel<HomePage>
    {
        private readonly By GoToCareerPageButton = By.XPath("//a[@href='/pl/pl/index/o-nas/kariera/']");
       public HomePage(RemoteWebDriver driver): base(driver)
       {
            Driver = driver;
       }
         
       public HomePage Navigate()
       {
            Driver.Navigate().GoToUrl("https://www.gft.com/pl/pl/index/");

            return this;
       }
       public CareerPage GoToCareerPage()
        {
            CommonWaits.UntilToBeClickable(Driver, GoToCareerPageButton);
            ClickOnElement(GoToCareerPageButton);

            return new CareerPage(Driver);
        }
    }
}
