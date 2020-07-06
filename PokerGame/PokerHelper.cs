using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class PokerHelper
    {
        public enum CardOrder
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            J,
            Q,
            K,
            A
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="handOne"></param>
        /// <param name="handTwo"></param>
        public void GetHands(string line, out List<string> handOne, out List<string> handTwo)
        {
            int half = line.Length / 2;
            string one = line.Substring(0, half);
            string two = line.Substring(half + 1, half);

            handOne = line.Substring(0, half).Split(' ').ToList();
            handTwo = line.Substring(half + 1, half).Split(' ').ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public  int GetHandScore(List<string> hand)
        {
            int score = 0;

            //bool isFlush = CheckForFlush(hand);
            //bool isStraight = CheckForStraight(hand);


            return score;
        }





    }
}
