namespace ReactFridgeTests.TestBase
{
    [TestClass]
    public class TestBase
    {


        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Console.WriteLine("ASSEMBLY INIT");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Console.WriteLine("ASSEMBLY CLEANUP");
        }



        public string jira(int ticketNumber)
        {
            return $"\nhttps://jira.com/JiraProjectName-{ticketNumber}";
        }

        public string jira(int[] ticketNumbers) {             
            string tickets = "";
                   foreach (int ticket in ticketNumbers)
            {
                tickets += $"\nhttps://jira.com/JiraProjectName-{ticket}, ";
            }
            return tickets;
        }

    }
}
