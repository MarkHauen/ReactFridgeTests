namespace ReactFridgeTests.TestBase
{
    public class TestBase
    {

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
