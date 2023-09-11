using ReactFridgeTests.TestBase;

namespace ReactFridgeTests.Tests.UITests
{
    [TestClass]
    public class InventoryPageTests : UITestBase
    {

        [TestMethod]
        public void PageLoadingAllStaticElements()
        {
            string JIRA = jira(4567);
            var inventoryPage = new Pages.InventoryPage(driver);
            inventoryPage.NavigateTo();
            Assert.IsTrue(inventoryPage.IsAt());
            Assert.IsTrue(inventoryPage.staticElementsArePresent(), JIRA);
        }

        [TestMethod]
        public void AddItemToInventory()
        {
            string JIRA = jira(4568);
            var inventoryPage = new Pages.InventoryPage(driver);
            inventoryPage.NavigateTo();
            int initialQuantityButter = inventoryPage.getQuantityOfItem("Butter");
            inventoryPage.AddItem("5", "Butter", "4");
            Assert.IsTrue(inventoryPage.ingredientListContains("Butter"), JIRA);
            int finalQuantityButter = inventoryPage.getQuantityOfItem("Butter");
            Assert.AreEqual(initialQuantityButter + 4, finalQuantityButter, JIRA);
        }

        [TestMethod]
        public void UsingItemFromInventory()
        {
            string JIRA = jira(4569);
            var inventoryPage = new Pages.InventoryPage(driver);
            inventoryPage.NavigateTo();
            int initialQuantityEgg = inventoryPage.getQuantityOfItem("Egg");
            int initialQuantityBread = inventoryPage.getQuantityOfItem("Bread Slice");
            inventoryPage.selectRecipe("Egg Sandwich");
            inventoryPage.clickUseItemButton();
            int finalQuantityEgg = inventoryPage.getQuantityOfItem("Egg");
            int finalQuantityBread = inventoryPage.getQuantityOfItem("Bread Slice");
            Assert.AreEqual(initialQuantityEgg - 1, finalQuantityEgg, JIRA);
            Assert.AreEqual(initialQuantityBread - 2, finalQuantityBread, JIRA);
        }
    }
}
