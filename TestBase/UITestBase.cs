using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReactFridgeTests.TestBase
{
    [TestClass]
    public class UITestBase: TestBase
    {

        protected IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            Console.WriteLine("UI TESTS RUNNING");
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:3000/");
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