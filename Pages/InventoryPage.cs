using OpenQA.Selenium;
using ReactFridgeTests.TestBase;
using OpenQA.Selenium.Support.UI;


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

        private IWebElement recipeDropDown => driver.FindElement(By.Name("Recipe-Select"));

        private IWebElement useItemButton => driver.FindElement(By.Name("UseItem-Button"));


        private IWebElement[] staticElementsToVerify => new IWebElement[] { ingredientIDInput, ingredientNameInput, ingredientQuantityInput, AddItemButton };
        

        public InventoryPage(IWebDriver driver)
        {
            this.driver = driver;
        }
      
        public void NavigateTo()
        {
            driver.Navigate().GoToUrl(baseURL);
        }

        public bool IsAt()
        {
            return driver.Title == "React App";
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

        public bool ingredientListContains(string ingredientName)
        {
            foreach (IWebElement element in inventoryList)
            {
                var text = element.Text;
                if (element.Text.Contains(ingredientName))
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

        //selects a recipe from the recipeDropDown
        public void selectRecipe(string ingredientName)
        {
            var selectElement = new SelectElement(recipeDropDown);
            selectElement.SelectByText(ingredientName);           
        }
        public void clickUseItemButton()
        {
            useItemButton.Click();
        }

        public int getQuantityOfItem(string ingredientName)
        {
            foreach (IWebElement element in inventoryList)
            {
                var text = element.Text;
                var ingredients = text.Split('\n');
                foreach (string ingredient in ingredients)
                {
                    if (ingredient.Contains(ingredientName))
                    {
                        var quantity = ingredient.Split(':')[1].Trim();
                        return Int32.Parse(quantity);
                    }
                }
            }
            return 0;
        }
    }
}
