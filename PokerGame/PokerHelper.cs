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
                handOne.Cards.Add(new Card(h));
            }
            List<string> h2 = line.Substring(half + 1, half).Split(' ').ToList();
            handTwo = new Hand();
            foreach (string h in h2)
            {
                handTwo.Cards.Add(new Card(h));
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
            // TODO: This is out of scope for these requirements, assuming that every hand has a clear winner
            //if (handOne.Score == 10) return true; // royal flush
            //if ((handOne.Score == 9 || handOne.Score == 5) && handOne.ScoreCardValue == handTwo.ScoreCardValue) return true; // straight and same high card

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
                    return HighCardTieBreak_HandOneWins(handOne.Cards, handTwo.Cards);
                case 2:
                    // check for highest pair
                    List<int> h1Pair = GetDuplicates(handOne.Cards);
                    List<int> h2Pair = GetDuplicates(handTwo.Cards);
                    if (h1Pair.Max(m => m) == h2Pair.Max(m => m))
                    {
                        // same pairs so check remaining cards
                        return HighCardTieBreak_HandOneWins(GetNonDuplicates(handOne.Cards, h1Pair), GetNonDuplicates(handTwo.Cards, h2Pair));
                    }
                    // return highest pair
                    return h1Pair.Max(m => m) > h2Pair.Max(m => m);
                case 3: // two pair
                    // get pairs
                    List<int> h1Pairs = GetDuplicates(handOne.Cards);
                    List<int> h2Pairs = GetDuplicates(handTwo.Cards);
                    // check high pair
                    if (h1Pairs.Max(m => m) == h2Pairs.Max(m => m))
                    {
                        // high pairs equal, so check 2nd pair
                        if (h1Pairs.Min(m => m) == h2Pairs.Min(m => m))
                        {
                            // 2nd pair equal, check last card
                            return HighCardTieBreak_HandOneWins(GetNonDuplicates(handOne.Cards, h1Pairs), GetNonDuplicates(handTwo.Cards, h2Pairs));
                        }
                        else
                        {
                            return h1Pairs.Min(m => m) > h2Pairs.Min(m => m);
                        }
                    }
                    else
                    {
                        return h1Pairs.Max(m => m) > h2Pairs.Max(m => m);
                    }
                case 4: // 3 of a kind
                    // can only have one three of a kind per value
                    return handOne.ScoreCardValue > handTwo.ScoreCardValue;
                case 5: // straight
                    // check for highest card in straight (can't be the same or would have been a tie)
                    return handOne.ScoreCardValue > handTwo.ScoreCardValue;
                case 6: // flush
                    // check for highest card
                    return HighCardTieBreak_HandOneWins(handOne.Cards, handTwo.Cards);
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

        public bool HighCardTieBreak_HandOneWins(List<Card> handOne, List<Card> handTwo)
        {
            if (handOne.Count == 0) return false; // means all cards equal so shouldn;t get here 

            int maxOne = handOne.Max(m => m.Number);
            int maxTwo = handTwo.Max(m => m.Number);

            if (maxOne == maxTwo)
            {
                // check the next card
                List<Card> newHandOne = handOne.Where(card => card.Number != maxOne).ToList();
                List<Card> newHandTwo = handTwo.Where(card => card.Number != maxTwo).ToList();
                return HighCardTieBreak_HandOneWins(newHandOne, newHandTwo);
            }
            else return maxOne > maxTwo;
        }

        /// <summary>
        /// returns a list of duplicate cards in a hand
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public List<int> GetDuplicates(List<Card> cards)
        {
            return cards.GroupBy(card => card.Number).Where(group => group.Count() > 1).Select(duplicates => duplicates.Key).ToList();
        }


        /// <summary>
        /// returns a list of non duplicate cards in a hand
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public List<Card> GetNonDuplicates(List<Card> cards, List<int> duplicates)
        {
            return cards.Where(card => !duplicates.Any(dup => dup == card.Number)).ToList();
            //return cards.GroupBy(card => card.Number).Where(group => !group.Skip(1).Any()).Select(nonDuplicates => nonDuplicates.Key).ToList();
        }

    }
}
