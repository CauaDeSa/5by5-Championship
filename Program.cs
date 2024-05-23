using _5by5_ChampionshipController.Bank;
using _5by5_ChampionshipController.Entity;

namespace _5by5_ChampionshipController
{
    class Program
    {
        static void Main(string[] args)
        {
            ChampionshipBankController cBank = new();
            TeamBankController tBank = new();
            GameBankController gBank = new();
            GameBankController hBank = new();
            GameBankController mBank = new();
        }
    }
}