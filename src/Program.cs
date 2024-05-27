using _5by5_ChampionshipController.src.Bank;
using _5by5_ChampionshipController.src.Entity;

namespace _5by5_ChampionshipController.src
{
    class Program
    {
        static void ClearLine(int line)
        {
            Console.SetCursorPosition(0, line);
            Console.WriteLine("\n                                                       ");
        }

        static void ShowMenu()
        {
            Console.WriteLine(@"
              > CHAMPIONSHIP MANAGER <");
        }

        static void ShowMenuOptions()
        {
            Console.Write(@"
               [1] List Championships
               [2] Create Championship
               [3] List Teams
               [4] Register Team
               [0] Exit");
        }

        static int GetCommand(int max, int pos)
        {
            string command;
            int result;

            do
            {
                do
                {
                    ClearLine(pos);

                    Console.SetCursorPosition(0, pos);
                    Console.Write("\n               Option: ");
                    command = Console.ReadLine();

                } while (!int.TryParse(command, out result));

            } while (result < 0 || result > max);

            return result;
        }

        static void Main(string[] args)
        {
            #region Bank initialize
            TeamBankController bTeam = new();
            GameBankController bGame = new();
            ChampionshipBankController bChampionship = new();
            StatsBankController bStats = new();
            #endregion

            int command;

            do
            {
                Console.Clear();

                ShowMenu();
                ShowMenuOptions();

                command = GetCommand(4, 8);   

                switch (command)
                {
                    case 1:
                        ListChampionships(bChampionship.GetAll(), bStats);
                        break;
                    case 2:
                        // Create Championship
                        break;
                    case 3:
                        ShowTeams(bTeam.GetAll());
                        break;
                    case 4:
                        // Register Team
                        break;
                }

                Console.WriteLine(@"
               Press any key to continue...");
                Console.ReadKey();

            } while (command != 0);
        }

        private static void ListChampionships(List<Championship> championships, StatsBankController bStats)
        {
            int command;

            do
            {
                Console.Clear();
                ShowMenu();

                if (championships.Count == 0)
                    Console.WriteLine("There are no championships registered!");
                else
                {
                    int index = 0;

                    foreach (var championship in championships)
                        Console.WriteLine($@"
               [{++index}]" + championship.ToString());
                }

                Console.WriteLine(@"
               [0] Return");

                command = GetCommand(championships.Count, Console.CursorTop);

                if (command > 0 && command <= championships.Count)
                {
                    List<Stats>? s = bStats.RetrieveByChampionshipAndSeason(championships[command - 1].Name, championships[command - 1].Season);

                    if (s == null || s.Count == 0)
                        Console.WriteLine(@"
               There are no stats registered for this championship!");

                    else
                    {
                        s.Sort((a, b) => a.Pontuation == b.Pontuation ? (a.ScoredGoals == b.ScoredGoals ? 0 : a.ScoredGoals > b.ScoredGoals ? -1 : 1) : a.Pontuation > b.Pontuation ? -1 : 1);

                        foreach (var stats in s)
                            Console.WriteLine(stats.ToString());
                    }
                }

                Console.WriteLine(@"
               Press any key to continue...");
                Console.ReadKey();

            } while (command != 0);

            Console.Clear();
            Console.WriteLine(@"
               Returning...");
        }

        private static void ShowTeams(List<Team> teams)
        {
            if (teams.Count == 0)
                Console.WriteLine("There are no teams registered!");
            else
            {
                foreach (var team in teams)
                    Console.WriteLine(team.ToString());
            }
        }
    }
}