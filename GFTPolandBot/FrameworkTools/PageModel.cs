using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace GFTPolandBot.FrameworkTools
{
     public class PageModel<T> : WebEntity where T : PageModel<T>
     {
        public PageModel(RemoteWebDriver driver) : base(driver)
        {

        }
     }
}
