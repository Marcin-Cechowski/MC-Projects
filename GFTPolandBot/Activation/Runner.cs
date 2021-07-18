using GFTPolandBot.FrameworkTools;
using GFTPolandBot.POM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFTPolandBot.Activation
{
    public class Runner : AutomationBase
    {
        private HomePage homePage;

        private JobOffersPage jobOffersPage;

        public string LookForADotNetJob()
        {
            SetUp();

            homePage = new HomePage(Driver);
            jobOffersPage = new JobOffersPage(Driver);


            homePage
                .Navigate()
                .GoToCareerPage()
                .GoToJobOfferPage()
                .LookForDotNetOfferts(".net", "Warszawa");
                 var number = jobOffersPage.GetNumberOfOfferts();


            return number;
        }
        public void CloseApplication()
        {
            TearDown();
        }
    }
}
