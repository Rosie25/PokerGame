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
        public List<Card> Cards { get; set; }
        public int HighCard { get; set; }
        public int LowCard { get; set; }
        public int Score { get; set; }
        public int ScoreCardValue { get; set; }
        public bool IsFlush { get; set; }
        public bool IsStraight { get; set; }
        public bool HasDuplicates { get; set; }


        public Hand()
        {
            Cards = new List<Card>();
        }

        /// <summary>
        /// perform all the calculations for the current hand (must have 5 cards only)
        /// </summary>
        public void SetHandValues()
        {
            if (Cards == null || Cards.Count() != 5) return;

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
            IsFlush = false;

            if (Cards == null || Cards.Count != 5) return;

            string suit = Cards[0].Suit;
            if (Cards.Where(flush => flush.Suit.Equals(suit)).Count() == 5)
                IsFlush = true;

        }

        /// <summary>
        /// set the high card value in the hand
        /// </summary>
        public void SetHighCard()
        {
            if (Cards == null || Cards.Count != 5) return;
            HighCard = Cards.Max(m => m.Number);
        }

        /// <summary>
        /// set the low card value for the hand
        /// </summary>
        public void SetLowCard()
        {
            if (Cards == null || Cards.Count != 5) return;
            LowCard = Cards.Min(m => m.Number);
        }

        /// <summary>
        /// determines whether the hand is a straight and sets the IsStraight flag accordingly
        /// a straight is 5 cards in a row, so hand cannot conatin duplicates and
        /// the high card will be four larger than the low card
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public void CheckForStraight()
        {
            IsStraight = false;
            if (Cards == null || Cards.Count != 5) return;
            if (HasDuplicates) return;

            if (HighCard - LowCard == 4)
            {
                IsStraight = true;
            }
        }

        /// <summary>
        /// determines whether the hand has any duplicates and sets the HasDuplicates flag accordingly
        /// </summary>
        public void CheckForDuplicates()
        {
            HasDuplicates = Cards.GroupBy(card => card.Number).Where(group => group.Count() > 1).Select(duplicates => duplicates.Key).Any();
        }

        /// <summary>
        /// for any hand containing a duplicate, calculate and set the score for the hand, and the relevant card value
        /// posssible hands here are Pair (2), Two Pair (3), 3 of a Kind (4), Full House (7), 4 of a Kind (8) 
        /// score is based on the rank in the requirements, with 1 for high card and 10 for royal flush
        /// relevant card value would be the duplicate value for 4 of a kind, 3 of a kind or pair, highest pair value for two pair and value of 3 cards in full house
        /// </summary>
        public void CalculateDuplicateScore()
        {
            List<(int Element, int Counter)> duplicateCards = Cards.GroupBy(card => card.Number).Where(group => group.Count() > 1).Select(duplicates => (Element: duplicates.Key, Counter: duplicates.Count())).ToList();

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

            if (IsStraight && IsFlush)
            {
                Score = HighCard == 14 ? 10 : 9; // Royal flush or Stright flush
                return;
            }
            else if (IsFlush) Score = 6; // flush
            else if (IsStraight) Score = 5; // straight
            else Score = 1; // high card only

        }

    }
}
