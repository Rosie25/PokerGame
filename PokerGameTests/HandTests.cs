using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerGame;
using System;
using System.Collections.Generic;
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
            hand.cards = new List<Card>();
            Card card1 = new Card
            {
                Value = "AS",
                Number = 14,
                Suit = "S"
            };
            Card card2 = new Card
            {
                Value = "AC",
                Number = 14,
                Suit = "C"
            };
            Card card3 = new Card
            {
                Value = "10H",
                Number = 10,
                Suit = "H"
            };
            Card card4 = new Card
            {
                Value = "5D",
                Number = 5,
                Suit = "D"
            };
            Card card5 = new Card
            {
                Value = "4S",
                Number = 4,
                Suit = "S"
            };
            hand.cards.Add(card1);
            hand.cards.Add(card2);
            hand.cards.Add(card3);
            hand.cards.Add(card4);
            hand.cards.Add(card5);

            hand.CheckForDuplicates();

            Assert.IsTrue(hand.HasDuplicates);
        }

        [TestMethod()]
        public void CheckForDuplicatesTest_False()
        {
            Hand hand = new Hand();
            hand.cards = new List<Card>();
            Card card1 = new Card
            {
                Value = "AS",
                Number = 14,
                Suit = "S"
            };
            Card card2 = new Card
            {
                Value = "KC",
                Number = 13,
                Suit = "C"
            };
            Card card3 = new Card
            {
                Value = "10H",
                Number = 10,
                Suit = "H"
            };
            Card card4 = new Card
            {
                Value = "5D",
                Number = 5,
                Suit = "D"
            };
            Card card5 = new Card
            {
                Value = "4S",
                Number = 4,
                Suit = "S"
            };
            hand.cards.Add(card1);
            hand.cards.Add(card2);
            hand.cards.Add(card3);
            hand.cards.Add(card4);
            hand.cards.Add(card5);

            hand.CheckForDuplicates();

            Assert.IsFalse(hand.HasDuplicates);
        }

        [TestMethod()]
        public void CalculateDuplicateScoreTest()
        {
            Hand hand = new Hand();
            hand.cards = new List<Card>();
            Card card1 = new Card
            {
                Value = "AS",
                Number = 14,
                Suit = "S"
            };
            Card card2 = new Card
            {
                Value = "AC",
                Number = 14,
                Suit = "C"
            };
            Card card3 = new Card
            {
                Value = "10H",
                Number = 10,
                Suit = "H"
            };
            Card card4 = new Card
            {
                Value = "10D",
                Number = 10,
                Suit = "D"
            };
            Card card5 = new Card
            {
                Value = "4S",
                Number = 4,
                Suit = "S"
            };
            hand.cards.Add(card1);
            hand.cards.Add(card2);
            hand.cards.Add(card3);
            hand.cards.Add(card4);
            hand.cards.Add(card5);

            hand.CalculateDuplicateScore();

            Assert.IsFalse(hand.HasDuplicates);
        }
    }
}