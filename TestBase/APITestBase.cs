using Newtonsoft.Json.Linq;
using System.Net;

namespace ReactFridgeTests.TestBase
{
    [TestClass]
    public class APITestBase : TestBase
    {
        
        public HttpClient client;

        public static string baseURL = "http://localhost:3000";
        public string inventoryEndpoint = $"{baseURL}/inventory";
        public string recipeEndpoint = $"{baseURL}/recipe";

        [TestInitialize]
        public void Setup()
        {
            Console.WriteLine("API TESTS STARTING");
            client = new HttpClient();
        }


        [TestCleanup]
        public void TestCleanup()
        {
            Console.WriteLine("API TEST CLEANUP COMPLETE");
        }

        

    }
}
