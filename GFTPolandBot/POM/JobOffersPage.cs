using GFTPolandBot.FrameworkTools;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using GFTPolandBot.Utilities;

namespace GFTPolandBot.POM
{
    public class JobOffersPage : PageModel<JobOffersPage>
    {
        private readonly By SearchByKeywordTextBox = By.XPath("(//input[@id='keywordsearch-q'])[2]");

        private readonly By SearchByLocationTextBox = By.XPath("(//input[@id='keywordsearch-locationsearch'])[2]");

        private readonly By SearchJobsButton = By.XPath("(//input[@id='keywordsearch-button'])[2]");

        private readonly By NumberOfOfferts = By.XPath("(//span[@class='paginationLabel']/b)[2]");
        public JobOffersPage(RemoteWebDriver driver) : base(driver)
        {
            Driver = driver;
        }
        public JobOffersPage LookForDotNetOfferts(string searchByKeyword, string searchByLocation)
        {
            Thread.Sleep(2000);
            ChangeTabWindow(0, 1);

            CommonWaits.UntilToBeClickable(Driver,SearchByKeywordTextBox);
            FillTextBox(SearchByKeywordTextBox,searchByKeyword);

            CommonWaits.UntilToBeClickable(Driver, SearchByLocationTextBox);
            FillTextBox(SearchByLocationTextBox, searchByLocation);

            ClickOnElement(SearchJobsButton);


            return this;
        }
        public string GetNumberOfOfferts()
        {
            var numberOfOfferts = Driver.FindElement(NumberOfOfferts).Text;
            return numberOfOfferts;
        }
    }
}
