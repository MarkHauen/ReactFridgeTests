using OpenQA.Selenium;
using ReactFridgeTests.TestBase;


namespace ReactFridgeTests.Pages
{
    internal class InventoryPage : UITestBase
    {
        private IWebDriver driver;

        private IWebElement AddItemButton => driver.FindElement(By.Name("add-button"));
        private IWebElement[] inventoryList => driver.FindElements(By.ClassName("InventoryListItem")).ToArray();
        private IWebElement ingredientIDInput => driver.FindElement(By.Name("ID-Input"));
        private IWebElement ingredientNameInput => driver.FindElement(By.Name("Name-Input"));
        private IWebElement ingredientQuantityInput => driver.FindElement(By.Name("Quantity-Input"));


        private IWebElement[] staticElementsToVerify => new IWebElement[] { ingredientIDInput, ingredientNameInput, ingredientQuantityInput, AddItemButton };
        

        public InventoryPage(IWebDriver driver)
        {
            this.driver = driver;
        }
      
        public void NavigateTo()
        {
            driver.Navigate().GoToUrl("http://localhost:4200");
        }

        public bool IsAt()
        {
            return driver.Title == "MeanFridge Root";
        }

        public bool staticElementsArePresent()
        {
            foreach (IWebElement element in staticElementsToVerify)
            {
                if (!element.Displayed)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ingredientListContains(string ingredientID, string ingredientName, string ingredientQuantity)
        {
            foreach (IWebElement element in inventoryList)
            {
                if (element.Text.Contains(ingredientID) && element.Text.Contains(ingredientName) && element.Text.Contains(ingredientQuantity))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddItem(string ingredientID, string ingredientName, string ingredientQuantity)
        {
            ingredientIDInput.SendKeys(ingredientID);
            ingredientNameInput.SendKeys(ingredientName);
            ingredientQuantityInput.SendKeys(ingredientQuantity);
            AddItemButton.Click();
        }
    }
}
