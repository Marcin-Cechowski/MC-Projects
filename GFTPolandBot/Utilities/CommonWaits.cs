using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using condition = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace GFTPolandBot.Utilities
{
    public static class CommonWaits
    {
        public static void UntilIsVisible(RemoteWebDriver driver, By e, int waitInSecounds = 15)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSecounds))
                .Until(condition.ElementIsVisible(e));
        }
        public static void UntilIsInvisible(RemoteWebDriver driver, By e, int waitInSecounds = 15)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSecounds))
                .Until(condition.InvisibilityOfElementLocated(e));
        }
        public static void UntilToBeClickable(RemoteWebDriver driver, By e, int waitInSecounds = 15)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSecounds))
                .Until(condition.ElementToBeClickable(e));
        }
        public static void UntilExists(RemoteWebDriver driver, By e, int waitInSecounds = 15)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(waitInSecounds))
                .Until(condition.ElementExists(e));
        }
    }
}
