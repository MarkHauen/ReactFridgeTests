namespace ReactFridgeTests.TestBase
{
    public class TestBase
    {

        public string jira(int ticketNumber)
        {
            return $"https://jira.com/JiraProjectName-{ticketNumber}";
        }

        public string jira(int[] ticketNumbers) {             
            string tickets = "";
                   foreach (int ticket in ticketNumbers)
            {
                tickets += $"https://jira.com/JiraProjectName-{ticket}, ";
            }
            return tickets;
        }

    }
}
