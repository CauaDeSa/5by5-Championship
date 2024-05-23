namespace _5by5_ChampionshipController.Entity
{
    internal class Game
    {
        public string Championship { get; set; }
        public string Season { get; set; }
        public string Visitor { get; set; }
        public string Home { get; set; }
        public int? VGoals { get; set; }
        public int? HGoals { get; set; }

        public Game(string championship, string season, string visitor, string home)
        {
            Championship = championship;
            Season = season;
            Visitor = visitor;
            Home = home;
            VGoals = null;
            HGoals = null;
        }

        public Game(string championship, string season, string visitor, string home, int vGoals, int hGoals)
        {
            Championship = championship;
            Season = season;
            Visitor = visitor;
            Home = home;
            VGoals = vGoals;
            HGoals = hGoals;
        }
    }
}