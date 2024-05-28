using _5by5_ChampionshipController.src.Bank;
using _5by5_ChampionshipController.src.Entity;
using System.IO;

namespace _5by5_ChampionshipController.src
{
    class Program
    {
        static void ClearLine(int line)
        {
            Console.SetCursorPosition(0, line);
            Console.WriteLine("\n                                                       ");
        }

        static void ShowTitle()
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

        static void ShowChampionshipOptions()
        {
            Console.Write(@"
               [1] Assign Team
               [2] Unnassign Team
               [3] List Teams
               [4] Start Championship

               [0] Return");
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
            ChampionshipBankController bChampionship = new();
            TeamBankController bTeam = new();
            GameBankController bGame = new();
            StatsBankController bStats = new();
            #endregion

            int command;

            do
            {
                Console.Clear();

                ShowTitle();
                ShowMenuOptions();

                command = GetCommand(4, 8);   

                switch (command)
                {
                    case 1:
                        ListChampionships(bChampionship.GetAll(), bStats);
                        break;
                    case 2:
                        CreateChampionship(bChampionship, bTeam, bStats, bGame);
                        break;
                    case 3:
                        ShowTeams(bTeam.GetAll());
                        break;
                    case 4:
                        RegisterTeam(bTeam);
                        break;
                }

                Console.WriteLine(@"
               Press any key to continue...");
                Console.ReadKey();

            } while (command != 0);
        }

        private static void CreateChampionship(ChampionshipBankController bChampionship, TeamBankController bTeam, StatsBankController bStats, GameBankController bGame)
        {
            int command = 0;
            bool isUsed = false;
            string cName, season, tName;
            List<Team> assigneds = new();

            DateOnly startDate;

            do
            {
                Console.Write(@"
               Championship name: ");

                cName = Console.ReadLine();
            } while (string.IsNullOrEmpty(cName));


            Console.Clear();
            ShowTitle();

            do
            {
                do
                {
                    Console.Write(@"
               Season: ");

                    season = Console.ReadLine();
                } while (string.IsNullOrEmpty(season));

                if (isUsed = bChampionship.HasChampionship(cName, season))
                        Console.WriteLine(@"
               This Championship already exists! Try a different name, or season.");

            } while (isUsed);

            Console.Clear();
            ShowTitle();

            do
            {
                Console.Write(@"
               Start date (yyyy/mm/dd): ");

            } while (!DateOnly.TryParse(Console.ReadLine(), out startDate));

            bChampionship.Insert(new Championship(cName, season, startDate));

            Console.WriteLine(@"
               Championship created successfully!
                  
               Press any key to continue.");

            Console.ReadKey();

            do
            {
                Console.Clear();
                ShowTitle();
                ShowChampionshipOptions();
                command = GetCommand(4, 8);

                switch (command)
                {
                    case 1:
                        if (assigneds.Count > 4)
                        {
                            Console.WriteLine(@"
                   This championship is full!");
                            break;
                        }

                        do
                        {
                            Console.Write(@"
               Team name: ");
                            tName = Console.ReadLine();
                        } while (string.IsNullOrEmpty(tName));

                        Team? t = bTeam.RetrieveByName(tName);

                        if (t == null)
                            Console.WriteLine(@"
               We couldn't find this team in our registers.");
                        else
                        {
                            assigneds.Add(t);

                            Console.WriteLine(@"
               Team successfully assigned!");
                        }

                        break;
                    case 2:

                        if (assigneds.Count == 0)
                        {
                            Console.WriteLine(@"
               This championship doesn't have teams assigneds yet!");
                            break;
                        }

                        do
                        {
                            Console.Write(@"
               Team name: ");
                            tName = Console.ReadLine();
                        } while (string.IsNullOrEmpty(tName));

                        if (assigneds.Remove(bTeam.RetrieveByName(tName)))
                        {
                            Console.WriteLine(@"
               Team successfully unnassigned!");                      
                        }
                        else
                            Console.WriteLine(@"
               We couldn't unnassign this Team, maybe he already isn't assigned.");

                        break;
                    case 3:
                        ShowTeams(assigneds);
                        break;
                    case 4:
                        
                        if (assigneds.Count < 3)
                        {
                            Console.WriteLine(@"
               Insufficient number of teams.");
                            break;
                        }

                        StartChampionship(bChampionship.GetByNameAndSeason(cName, season), bStats, bGame, bTeam, bChampionship, assigneds);

                        break;
                }

                Console.WriteLine(@"
               Press any key to continue...");
                Console.ReadKey();

            } while (command != 0 && bChampionship.GetByNameAndSeason(cName, season).EndDate == null);

            bChampionship.SetEndDate(cName, season);

            Console.Clear();
            Console.WriteLine(@"
               Returning...");
            Console.ReadKey();
        }

        private static void StartChampionship(Championship championship, StatsBankController bStats, GameBankController bGame, TeamBankController bTeam, ChampionshipBankController bChampionship, List<Team> assigneds)
        {
            ShowTitle();

            Console.WriteLine(@"
               [1] Simulate games

               [0] Return");

            int command = GetCommand(2, Console.CursorTop);

            if (command == 0)
                return;

            else
            {
                List<string> teams = new();

                foreach (var team in assigneds)
                {
                    teams.Add(team.Name);
                }

                Random random = new();
                Game game;

                for (int i = 0; i < teams.Count; i++)
                {
                    for (int j = 0; j < teams.Count; j++)
                    {
                        if (teams[i] != teams[j])
                        {
                            Team a = bTeam.RetrieveByName(teams[i]);
                            Team b = bTeam.RetrieveByName(teams[j]);

                            bGame.Insert(new Game(championship.Name, championship.Season, a.Nickname, b.Nickname, random.Next(0, 7), random.Next(0, 7)));
                        }
                    }

                    bStats.Insert(new Stats(teams[i], championship.Name, championship.Season, 0, 0, 0));
                }

                for (int i = 0; i < teams.Count; i++)
                    bStats.UpdateTeamStats(bTeam.RetrieveByName(teams[i]).Name, bTeam.RetrieveByName(teams[i]).Nickname, championship.Name, championship.Season);

                bChampionship.SetEndDate(championship.Name, championship.Season);

                Console.WriteLine(@"
               Championship Ended!");
            }
        }

        private static void RegisterTeam(TeamBankController bTeam)
        {
            string name, nickname, data;
            DateOnly creationDate;
            bool isUsed = false;

            Console.Clear();
            ShowTitle();

            do
            {
                if (isUsed)
                    Console.WriteLine(@"
               Team name already in use! Try another one.");
                
                do
                {
                    Console.Write(@"
               Team name: ");
                    name = Console.ReadLine();
                } while (string.IsNullOrEmpty(name));

                isUsed = bTeam.nameIsUsed(name);

            } while (isUsed);

            Console.Clear();
            ShowTitle();

            do
            {
                if (isUsed)
                    Console.WriteLine(@"
               Team nickname already in use! Try another one.");

                do
                {
                    Console.Write(@"
               Team nickname: ");
                    nickname = Console.ReadLine();

                } while (string.IsNullOrEmpty(nickname));

                isUsed = bTeam.nicknameIsUsed(nickname);
            } while (isUsed);
            
            Console.Clear();
            ShowTitle();

            do
            {
                Console.Write(@"
               Creation date (yyyy/mm/dd): ");
                data = Console.ReadLine();
            } while (!DateOnly.TryParse(data, out creationDate));

            bTeam.Insert(new Team(name, nickname, creationDate));

            Console.WriteLine(@"
               Team registered successfully!");

            Console.Clear();
            Console.WriteLine(@"
               Returning...");
        }

        private static void ListChampionships(List<Championship> championships, StatsBankController bStats)
        {
            int command = 0;

            do
            {
                Console.Clear();
                ShowTitle();

                if (championships == null || championships.Count == 0)
                    Console.WriteLine(@"
               There are no championships registered!");
                else
                {
                    int index = 0;

                    foreach (var championship in championships)
                        Console.WriteLine($@"
               [{++index}]" + championship.ToString());


                    Console.WriteLine(@"
               [0] Return");

                    command = GetCommand(championships.Count, Console.CursorTop);

                    if (command > 0 && command <= championships.Count)
                    {
                        List<Stats>? s = bStats.RetrieveStatsByChampionship(championships[command - 1].Name, championships[command - 1].Season);

                        if (s == null || s.Count == 0)
                            Console.WriteLine(@"
               There are no stats registered for this championship!");

                        else
                        {
                            s.Sort((a, b) => a.Pontuation == b.Pontuation ? (a.ScoredGoals == b.ScoredGoals ? 0 : a.ScoredGoals > b.ScoredGoals ? -1 : 1) : a.Pontuation > b.Pontuation ? -1 : 1);

                            foreach (var stats in s)
                                Console.WriteLine(stats.ToString());
                        }
                    
                        Console.WriteLine(@"
               Press any key to continue...");
                    Console.ReadKey();
                    }
                }

            } while (command != 0);
        }

        private static void ShowTeams(List<Team> teams)
        {
            if (teams == null || teams.Count == 0)
                Console.WriteLine(@"
               There are no teams registered!");
            else
            {
                foreach (var team in teams)
                    Console.WriteLine(team.ToString());
            }
        }
        
        private static void ShowStats(List<Stats>? stats)
        {
            if (stats == null || stats.Count == 0)
                Console.WriteLine(@"
               There are no teams registered!");
            else
            {
                foreach (var vStats in stats)
                    Console.WriteLine(vStats.ToString());
            }
        }
    }
}