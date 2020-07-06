using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerGame
{
    public class Hand
    {
        public List<Card> cards { get; set; }
        public int HighCard { get; set; }
        public int LowCard { get; set; }
        public int Score { get; set; }
        public int ScoreCardValue { get; set; }
        public bool IsFlush { get; set; }
        public bool IsStraight { get; set; }
        public bool HasDuplicates { get; set; }


        public Hand()
        {
            cards = new List<Card>();
        }

        public void SetHandValues()
        {
            SetHighCard();
            SetLowCard();
            CheckForDuplicates();
            if (HasDuplicates)
            {
                CalculateDuplicateScore();
            }
            else
            {
                CalculateNonDuplicateScore();
            }

        }

        /// <summary>
        /// Check to see whether the hand is a flush (i.e all suits are the same)(
        /// </summary>
        /// <param name="hand">The current hand, consists of five "card" values</param>
        /// <returns></returns>
        public void CheckForFlush()
        {
            if (cards == null || cards.Count() != 5) return;

            string suit = cards[0].Suit;
            if (cards.Where(flush => flush.Suit.Equals(suit)).Count() == 5)
                IsFlush = true;

            IsFlush = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetHighCard()
        {
            if (cards == null || cards.Count() != 5) return;
            HighCard = cards.Max(m => m.Number);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetLowCard()
        {
            if (cards == null || cards.Count() != 5) return;
            LowCard = cards.Min(m => m.Number);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public void CheckForStraight()
        {
            IsStraight = false;

            if (cards == null || cards.Count() != 5) return;

            if (HasDuplicates) return;

            if (HighCard - LowCard == 4) IsStraight = true;

        }

        public void CheckForDuplicates()
        {
            HasDuplicates = cards.GroupBy(card => card.Number).Where(group => group.Count() > 1).Select(duplicates => duplicates.Key).Any();
        }

        /// <summary>
        /// for any hand containing a duplicate, calculate and set the score for the hand, and the relevant card value
        /// posssible hands here are Pair (2), Two Pair (3), 3 of a Kind (4), Full House (7), 4 of a Kind (8) 
        /// score is based on the rank in the requirements, with 1 for high card and 10 for royal flush
        /// relevant card value would be the duplicate value for 4 of a kind, 3 of a kind or pair, highest pair value for two pair and value of 3 cards in full house
        /// </summary>
        public void CalculateDuplicateScore()
        {
            List<(int Element, int Counter)> duplicateCards = cards.GroupBy(card => card.Number).Where(group => group.Count() > 1).Select(duplicates => (Element: duplicates.Key, Counter: duplicates.Count())).ToList();

            if (duplicateCards.Count == 1)
            {
                // 4 of a kind, 3 of a kind or pair
                ScoreCardValue = duplicateCards[0].Element;
                switch (duplicateCards[0].Counter)
                {
                    case 2:
                        Score = 2; // pair
                        break;
                    case 3:
                        Score = 4; // three of a kind
                        break;
                    case 4:
                        Score = 8; // four of a kind
                        break;
                }
            }
            else
            {
                //two pair or full house
                if (duplicateCards.Max(m => m.Counter) == 2)
                {
                    // two pair
                    ScoreCardValue = duplicateCards.Max(m => m.Element);
                    Score = 3;
                }
                else
                {
                    // fullhouse
                    ScoreCardValue = duplicateCards.Where(c => c.Counter == 3).FirstOrDefault().Element;
                    Score = 7;
                }
            }
        }

        /// <summary>
        /// for any hand NOT containing a duplicate, calculate and set the score for the hand, and the relevant card value
        /// score is based on the rank in the requirements, with 1 for high card and 10 for royal flush
        /// posssible hands here are High Card (1), Straight (5), Flush (6), Straight Flush (9), Royal Flush (10)
        /// relevant card value would be the highest card value in all cases
        /// </summary>
        public void CalculateNonDuplicateScore()
        {
            CheckForStraight();
            CheckForFlush();

            ScoreCardValue = HighCard;

            if (IsStraight && IsFlush) Score = HighCard == 14 ? 10 : 9; // Royal flush or Stright flush
            else if (IsFlush) Score = 6; // flush
            else if (IsStraight) Score = 5; // straight
            else Score = 1; // high card only

        }

    }
}
