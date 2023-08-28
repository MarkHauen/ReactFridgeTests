using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.DevTools.V113.Network;
using ReactFridgeTests.TestBase;
using System.Text;
using ReactFridgeTests.TestData;
using System.ComponentModel;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;

namespace ReactFridgeTests.Tests.APITests
{
    [TestClass]
    public class InventoryAPITests : APITestBase
    {

        private string inventoryEndpoint = $"{baseURL}/inventory";
        
        [DataTestMethod] //// Test that the inventory endpoint returns all known invantory ids with their names and standard quantities ////
        [DataRow(1, "Egg", 12)]
        [DataRow(2, "Bacon", 9)]
        [DataRow(3, "Bread Slice", 10)]
        [DataRow(4, "Cheese Slice", 10)]
        public async Task GetAllInventoryTest(int id, string expectedName, int standardQuantity)
        {
            string JIRA = jira(1337); 
            JObject foodObject = await GetInvantoryItemByIDFromAll(id, JIRA);
            if (foodObject != null)
            {
                Assert.AreEqual(expectedName, foodObject["name"].Value<string>());
                Assert.AreEqual(standardQuantity, foodObject["standardQuantity"].Value<int>());
            }
            else
            {
                Assert.Fail($"Object with id ${id} not found, {JIRA}");
            }
        }

        [DataTestMethod] //// Test that the inventory endpoint returns the correct invantory item by id ////
        [DataRow(1, "Egg", 12)]
        [DataRow(2, "Bacon", 9)]
        [DataRow(3, "Bread Slice", 10)]
        [DataRow(4, "Cheese Slice", 10)]
        public async Task GetInventoryFromIDTest(int id, string expectedName, int standardQuantity)
        {
            string JIRA = jira(1336); 
            JObject foodObject = await GetInvantoryItemByIDFromEndPoint(id, JIRA);
            if (foodObject != null)
            {
                Assert.AreEqual(expectedName, foodObject["name"].Value<string>());
                Assert.AreEqual(standardQuantity, foodObject["standardQuantity"].Value<int>());
            }
            else
            {
                Assert.Fail($"Object with id ${id} not found, {JIRA}");
            }
        }


        [TestMethod] //// Test that the inventory endpoint can add and delete new items ////
        public async Task AddDeleteNewItemTest()
        {
            string JIRA = jira([1339, 1338, 1340]);
            HttpResponseMessage response;
            if (!await ItemExistsInInventory(5, JIRA))
            {
                response = await PostNewIngredient(IngredientData.Butter);
                Assert.IsTrue(response.IsSuccessStatusCode, $"ENPOINT RESPONSE FAILURE. INGREDIENT NOT POSTED. {JIRA}");
                Assert.IsTrue(await GetInvantoryItemByIDFromAll(5, JIRA) != null); 
                response = await DeleteIngredient(5);
                Assert.IsTrue(response.IsSuccessStatusCode, $"ENPOINT RESPONSE FAILURE. INGREDIENT NOT DELETED. {JIRA}");
            } 
            else
            {
                response = await DeleteIngredient(5);
                Assert.IsTrue(response.IsSuccessStatusCode, $"ENPOINT RESPONSE FAILURE. INGREDIENT NOT DELETED. {JIRA}");
                response = await PostNewIngredient(IngredientData.Butter);
                Assert.IsTrue(response.IsSuccessStatusCode, $"ENPOINT RESPONSE FAILURE. INGREDIENT NOT POSTED. {JIRA}");
                Assert.IsTrue(await GetInvantoryItemByIDFromAll(5, JIRA) != null);
                response = await DeleteIngredient(5);
                Assert.IsTrue(response.IsSuccessStatusCode, $"ENPOINT RESPONSE FAILURE. INGREDIENT NOT DELETED. {JIRA}");
            }            
        }

        [TestMethod] //// Test that the inventory endpoint can update existing items ////
        public async Task UpdateExistingItemTest()
        {
            string JIRA = jira([1341, 1342]);
            HttpResponseMessage response;
            JObject foodObject = await GetInvantoryItemByIDFromAll(1, JIRA);
            if (foodObject != null)
            {
                int originalQuantity = foodObject["quantity"].Value<int>();
                int newQuantity = originalQuantity + foodObject["standardQuantity"].Value<int>();
                string newFoodObject = foodObject.ToString().Replace(foodObject["quantity"].Value<int>().ToString(), newQuantity.ToString());
                response = await UpdateIngredient(1, newFoodObject);
                Assert.IsTrue(response.IsSuccessStatusCode, $"ENPOINT RESPONSE FAILURE. INGREDIENT NOT UPDATED. {JIRA}");
                foodObject = await GetInvantoryItemByIDFromAll(1, JIRA);
                Assert.AreEqual(newQuantity, foodObject["quantity"].Value<int>());
                newFoodObject = foodObject.ToString().Replace(foodObject["quantity"].Value<int>().ToString(), originalQuantity.ToString());
                response = await UpdateIngredient(1, newFoodObject);
                Assert.IsTrue(response.IsSuccessStatusCode, $"ENPOINT RESPONSE FAILURE. INGREDIENT NOT UPDATED. {JIRA}");
                foodObject = await GetInvantoryItemByIDFromAll(1, JIRA);
                Assert.AreEqual(originalQuantity, foodObject["quantity"].Value<int>());
            }
            else
            {
                Assert.Fail($"Object with id ${1} not found, {JIRA}");
            }
        }



        ////////////// Helper Methods //////////////

        private async Task<JArray?> GetInvantoryItems(string JIRA)
        {
            HttpResponseMessage response = client.GetAsync(inventoryEndpoint).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JArray foodObjects = JArray.Parse(responseContent);
                return foodObjects;
            }
            else
            {
                Assert.Fail($"Inventory not found, {JIRA}");
                return null;
            }
        }

        private async Task<JObject> GetInvantoryItemByIDFromAll(int id, string JIRA)
        {
            JArray foodObjects = await GetInvantoryItems(JIRA);
            JObject foodObject = foodObjects.Children<JObject>()
                .FirstOrDefault(o => o["id"] != null && o["id"].Value<int>() == id);
            if (foodObject != null)
            {
                return foodObject;
            }
            else
            {
                Assert.Fail($"Object with id ${id} not found, {JIRA}");
                return null;
            }
        }

        private async Task<JObject> GetInvantoryItemByIDFromEndPoint(int id, string JIRA)
        {
            HttpResponseMessage response = client.GetAsync(inventoryEndpoint + $"/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JObject foodObject = (JObject)JArray.Parse(responseContent).First();
                return foodObject;
            }
            else
            {
                Assert.Fail($"Object with id ${id} not found, {JIRA}");
                return null;
            }
        }

        private async Task<HttpResponseMessage> UpdateIngredient(int id, string ingredient)
        {
            return await client.PutAsync(inventoryEndpoint + $"/{id}", new StringContent(ingredient, Encoding.UTF8, "application/json"));
        }

        private async Task<HttpResponseMessage> PostNewIngredient(string ingredient)
        {
            return await client.PostAsync(inventoryEndpoint, new StringContent(ingredient, Encoding.UTF8, "application/json"));
        }

        private Task<HttpResponseMessage> DeleteIngredient(int id)
        {
            return client.DeleteAsync(inventoryEndpoint + $"/{id}");
        }

        private async Task<bool> ItemExistsInInventory(int id, string JIRA)
        {
            JArray foodObjects = await GetInvantoryItems(JIRA);
            JObject foodObject = foodObjects.Children<JObject>()
                .FirstOrDefault(o => o["id"] != null && o["id"].Value<int>() == id);
            return (foodObject != null);
        }

    }
}
