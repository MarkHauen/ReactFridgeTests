using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReactFridgeTests.TestBase
{
    [TestClass]
    public class UITestBase: TestBase
    {

        protected IWebDriver driver;

        protected string baseURL = "http://localhost:3001/";

        [TestInitialize]
        public void Setup()
        {
            Console.WriteLine("UI TESTS RUNNING");
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl(baseURL);
        }


        [TestCleanup]
        public void TestCleanup()
        {
            // Clean up resources after each test
            driver.Quit();
            driver.Dispose();
            Console.WriteLine("UI TEST CLEANUP COMPLETE");
        }
    }
}