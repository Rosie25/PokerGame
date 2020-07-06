using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class PokerHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="handOne"></param>
        /// <param name="handTwo"></param>
        public void GetHands(string line, out Hand handOne, out Hand handTwo)
        {
            int half = line.Length / 2;

            List<string> h1 = line.Substring(0, half).Split(' ').ToList();
            handOne = new Hand();
            foreach (string h in h1)
            {
                handOne.cards.Add(new Card(h));
            }
            List<string> h2 = line.Substring(half + 1, half).Split(' ').ToList();
            handTwo = new Hand();
            foreach (string h in h2)
            {
                handTwo.cards.Add(new Card(h));
            }
        }

        /// <summary>
        /// determines whether the two hands have a tie, returns true if so, false otherwise
        /// tie: a tie can occur if both players have  
        /// a royal flush, 
        /// straight flush or straight with same high card, 
        /// same two pairs and same high card value
        /// same pair and same other 3 card values
        /// high card and have same 5 card values/// </summary>
        /// <param name="handOne"></param>
        /// <param name="handTwo"></param>
        /// <returns></returns>
        public bool IsTie(Hand handOne, Hand handTwo)
        {
            if (handOne.Score == 10) return true; // royal flush

            if ((handOne.Score == 9 || handOne.Score == 5) && handOne.ScoreCardValue == handTwo.ScoreCardValue) return true; // straight and same high card

            // var a = handOne.cards.All(handTwo.cards.Contains);

            return false;
        }

        /// <summary>
        /// This determines whether hand one or hand two wins the game via the tie break rules
        /// Returns true if Hand One wins, false if Hand 2 wins
        /// The tie break rules are as follows:
        /// When multiple players have the same ranked hand then the rank made up of the highest value cards wins.
        /// If two ranks tie, for example, if both players have a pair of Jacks, then highest cards in each hand are
        /// compared; if the highest cards tie then the next highest cards are compared, and so on
        /// </summary>
        /// <param name="handOne">Contains the five cards representing hand one</param>
        /// <param name="handTwo">Contains the five cards representing hand two</param>
        /// <returns>True if player one hand wins, false if player two hand wins</returns>
        public bool HandOneWinsTieBreak(Hand handOne, Hand handTwo)
        {
            switch (handOne.Score)
            {
                case 1: // high card
                    if (handOne.HighCard == handTwo.HighCard) { CheckAllCards(); }
                    else return handOne.HighCard > handTwo.HighCard;
                    break;
                case 2: // pair
                    // check for highest pair
                    if (handOne.ScoreCardValue > handTwo.ScoreCardValue) return true;
                    if (handOne.ScoreCardValue < handTwo.ScoreCardValue) return false;
                    // pairs are the same so need to check highest card
                    break;
                case 3: // two pair
                    // check for highest pair
                    if (handOne.ScoreCardValue > handTwo.ScoreCardValue) return true;
                    if (handOne.ScoreCardValue < handTwo.ScoreCardValue) return false;
                    // if high pair are the same, need to check rest of hand
                    break;
                case 4: // 3 of a kind
                    // can only have one three of a kind per value
                    return handOne.ScoreCardValue > handTwo.ScoreCardValue;
                case 5: // straight
                    // check for highest card in straight (can't be the same or would have been a tie)
                    return handOne.ScoreCardValue > handTwo.ScoreCardValue;
                case 6: // flush
                    // check for highest card
                    if (handOne.ScoreCardValue > handTwo.ScoreCardValue) return true;
                    if (handOne.ScoreCardValue < handTwo.ScoreCardValue) return false;
                    // need to check remaining cards if top card matches
                    break;
                case 7: // full house
                    // can only have one three of a kind per value
                    return handOne.ScoreCardValue > handTwo.ScoreCardValue;
                case 8: // four of a kind
                    // can only have one four of a kind per value
                    return handOne.ScoreCardValue > handTwo.ScoreCardValue;
                case 9: // straight flush
                    // check for highest pair (can't be the same or would have been a tie)
                    return handOne.ScoreCardValue > handTwo.ScoreCardValue;
            }

            return false;

        }

        public void CheckAllCards()
        {

        }

    }
}
