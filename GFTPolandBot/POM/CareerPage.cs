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
    public class CareerPage : PageModel<CareerPage>
    {
        private readonly By JobOfferTabButton = By.XPath("(//a[contains(text(),'Job offers')])[2]");
        public CareerPage(RemoteWebDriver driver) : base(driver)
        {
            Driver = driver;
        }
        public JobOffersPage GoToJobOfferPage()
        {
            CommonWaits.UntilToBeClickable(Driver, JobOfferTabButton);
            ClickOnElement(JobOfferTabButton);

            return new JobOffersPage(Driver);
        }
    }
}
