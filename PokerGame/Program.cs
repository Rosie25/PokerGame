using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int playerOneWins = 0;
            int playerTwoWins = 0;

            List<string> handOne;
            List<string> handTwo;
            int handOneScore;
            int handTwoScore;
            // get input stream 

            // for each line (game) in stream
            string line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                GetHands(line, out handOne, out handTwo);
                handOneScore = GetHandScore(handOne);
                handTwoScore = GetHandScore(handTwo);
            }







        // get hand one result
        // get hand two result
        // compare result
        // if different, record result
        // if the same, sort by tie breaker then record result

        // output results
    }


        private static void GetHands(string line, out List<string> handOne, out List<string> handTwo)
        {
            handOne = new List<string>();
            handTwo = new List<string>();


        }

        private static int GetHandScore(List<string> hand)
        {
            int score = 0;


            return score;
        }
    }
}
