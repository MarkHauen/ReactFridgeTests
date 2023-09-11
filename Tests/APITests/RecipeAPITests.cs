using Newtonsoft.Json.Linq;
using ReactFridgeTests.TestData;
using ReactFridgeTests.TestBase;

namespace ReactFridgeTests.Tests.APITests
{
    [TestClass]
    public class RecipeAPITests : APITestBase
    {

        [TestMethod] // Tests the recipe base GET endpoint to ensure it returns the expected JSON of recipies
        public async Task GetRecipes()
        {
            string JIRA = jira(4512);
            HttpResponseMessage response = client.GetAsync(recipeEndpoint).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JObject responseObject = JObject.Parse(responseContent);
                JObject expectedObject = JObject.Parse(RecipeData.RecipeJSON);
                Assert.IsTrue(JToken.DeepEquals(responseObject, expectedObject), JIRA);
            }
            else { Assert.Fail($"Response was not successful. {JIRA}"); }
        }
    }
}
