using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame.Tests
{
    [TestClass()]
    public class HandTests
    {
        [TestMethod()]
        public void CheckForDuplicatesTest_True()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("AS"),
                new Card("AC"),
                new Card("10S"),
                new Card("5H"),
                new Card("2D")
            };

            hand.CheckForDuplicates();

            Assert.IsTrue(hand.HasDuplicates);
        }

        [TestMethod()]
        public void CheckForDuplicatesTest_False()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("AS"),
                new Card("KC"),
                new Card("10S"),
                new Card("5H"),
                new Card("2D")
            };
            hand.CheckForDuplicates();
            Assert.IsFalse(hand.HasDuplicates);
        }

        [TestMethod()]
        public void CalculateDuplicateScore_Pair_Test()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("AS"),
                new Card("AC"),
                new Card("10S"),
                new Card("5H"),
                new Card("2D")
            };

            hand.CalculateDuplicateScore();

            Assert.AreEqual(2, hand.Score);
            Assert.AreEqual(14, hand.ScoreCardValue);
        }

        [TestMethod()]
        public void CalculateDuplicateScore_TwoPair_Test()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("AS"),
                new Card("AC"),
                new Card("3S"),
                new Card("3H"),
                new Card("2D")
            };

            hand.CalculateDuplicateScore();

            Assert.AreEqual(3, hand.Score);
            Assert.AreEqual(14, hand.ScoreCardValue);
        }

        [TestMethod()]
        public void CalculateDuplicateScore_TOAK_Test()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("JS"),
                new Card("JC"),
                new Card("JH"),
                new Card("5H"),
                new Card("2D")
            };

            hand.CalculateDuplicateScore();

            Assert.AreEqual(4, hand.Score);
            Assert.AreEqual(11, hand.ScoreCardValue);
        }

        [TestMethod()]
        public void CalculateDuplicateScore_FOAK_Test()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("6S"),
                new Card("6C"),
                new Card("6D"),
                new Card("6H"),
                new Card("2D")
            };

            hand.CalculateDuplicateScore();

            Assert.AreEqual(8, hand.Score);
            Assert.AreEqual(6, hand.ScoreCardValue);
        }

        [TestMethod()]
        public void CalculateDuplicateScore_FullHouse_Test()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("KS"),
                new Card("KC"),
                new Card("3S"),
                new Card("3H"),
                new Card("3D")
            };

            hand.CalculateDuplicateScore();

            Assert.AreEqual(7, hand.Score);
            Assert.AreEqual(3, hand.ScoreCardValue);
        }

        [TestMethod]
        public void CheckForFlushTest_True()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("KS"),
                new Card("JS"),
                new Card("3S"),
                new Card("7S"),
                new Card("2S")
            };

            bool expectedValue = true;

            hand.CheckForFlush();

            Assert.AreEqual(expectedValue, hand.IsFlush);
        }

        [TestMethod]
        public void CheckForFlushTest_False()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("KS"),
                new Card("KC"),
                new Card("3S"),
                new Card("3H"),
                new Card("3D")
            };
            bool expectedValue = false;
            hand.CheckForFlush();

            Assert.AreEqual(expectedValue, hand.IsFlush);
        }

        [TestMethod]
        public void CheckForStraightTest_Picture_True()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("AS"),
                new Card("KC"),
                new Card("QS"),
                new Card("JH"),
                new Card("TD")
            };
            bool expectedValue = true;

            hand.SetHighCard();
            hand.SetLowCard();
            hand.CheckForStraight();

            Assert.AreEqual(expectedValue, hand.IsStraight);
        }

        [TestMethod]
        public void CheckForStraightTest_Number_True()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("2S"),
                new Card("5C"),
                new Card("6S"),
                new Card("4H"),
                new Card("3D")
            };
            bool expectedValue = true;
            hand.SetHighCard();
            hand.SetLowCard();
            hand.CheckForStraight();

            Assert.AreEqual(expectedValue, hand.IsStraight);
        }
        [TestMethod]
        public void CheckForStraightTest_Mix_True()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("8S"),
                new Card("9C"),
                new Card("7S"),
                new Card("JH"),
                new Card("TD")
            };
            bool expectedValue = true;
            hand.SetHighCard();
            hand.SetLowCard();
            hand.CheckForStraight();

            Assert.AreEqual(expectedValue, hand.IsStraight);
        }

        [TestMethod]
        public void CheckForStraightTest_False()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("KS"),
                new Card("KC"),
                new Card("3S"),
                new Card("3H"),
                new Card("3D")
            };
            bool expectedValue = false;
            hand.SetHighCard();
            hand.SetLowCard();
            hand.CheckForStraight();

            Assert.AreEqual(expectedValue, hand.IsStraight);
        }

        [TestMethod()]
        public void SetLowCardTest()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("9S"),
                new Card("9C"),
                new Card("3S"),
                new Card("3H"),
                new Card("3D")
            };
            int expectedValue = 3;
            hand.SetLowCard();
            Assert.AreEqual(expectedValue, hand.LowCard);
        }

        [TestMethod()]
        public void SetHighCardTest()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("9S"),
                new Card("9C"),
                new Card("3S"),
                new Card("3H"),
                new Card("3D")
            };
            int expectedValue = 9;
            hand.SetHighCard();
            Assert.AreEqual(expectedValue, hand.HighCard);
        }

        [TestMethod()]
        public void CalculateNonDuplicateScoreTest_Flush()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("9S"),
                new Card("6S"),
                new Card("3S"),
                new Card("4S"),
                new Card("5S")
            };
            int expectedScore = 6;
            int expectedScoreCardValue = 9;
            hand.SetLowCard();
            hand.SetHighCard();
            hand.CalculateNonDuplicateScore();
            Assert.AreEqual(expectedScore, hand.Score);
            Assert.AreEqual(expectedScoreCardValue, hand.ScoreCardValue);
        }

        [TestMethod()]
        public void CalculateNonDuplicateScoreTest_Straight()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("9C"),
                new Card("8C"),
                new Card("7H"),
                new Card("6S"),
                new Card("5D")
            };
            int expectedScore = 5;
            int expectedScoreCardValue = 9;
            hand.SetLowCard();
            hand.SetHighCard();
            hand.CalculateNonDuplicateScore();
            Assert.AreEqual(expectedScore, hand.Score);
            Assert.AreEqual(expectedScoreCardValue, hand.ScoreCardValue);
        }

        [TestMethod()]
        public void CalculateNonDuplicateScoreTest_StraightFlush()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("KS"),
                new Card("QS"),
                new Card("JS"),
                new Card("TS"),
                new Card("9S")
            };
            int expectedScore = 9;
            int expectedScoreCardValue = 13;
            hand.SetLowCard();
            hand.SetHighCard();
            hand.CalculateNonDuplicateScore();
            Assert.AreEqual(expectedScore, hand.Score);
            Assert.AreEqual(expectedScoreCardValue, hand.ScoreCardValue);
        }

        [TestMethod()]
        public void CalculateNonDuplicateScoreTest_RoyalFlush()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("AH"),
                new Card("KH"),
                new Card("QH"),
                new Card("JH"),
                new Card("TH")
            };
            int expectedScore = 10;
            int expectedScoreCardValue = 14;
            hand.SetLowCard();
            hand.SetHighCard();
            hand.CalculateNonDuplicateScore();
            Assert.AreEqual(expectedScore, hand.Score);
            Assert.AreEqual(expectedScoreCardValue, hand.ScoreCardValue);
        }

        [TestMethod()]
        public void CalculateNonDuplicateScoreTest_High()
        {
            Hand hand = new Hand();
            hand.Cards = new List<Card>
            {
                new Card("KS"),
                new Card("6H"),
                new Card("9S"),
                new Card("JD"),
                new Card("2S")
            };
            int expectedScore = 1;
            int expectedScoreCardValue = 13;
            hand.SetLowCard();
            hand.SetHighCard();
            hand.CalculateNonDuplicateScore();
            Assert.AreEqual(expectedScore, hand.Score);
            Assert.AreEqual(expectedScoreCardValue, hand.ScoreCardValue);
        }

    }
}