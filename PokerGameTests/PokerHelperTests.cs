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
    public class PokerHelperTests
    {
        [TestMethod()]
        public void GetDuplicatesTest()
        {
            PokerHelper poker = new PokerHelper();
            List<Card> cards = new List<Card>
            {
                new Card("KS"),
                new Card("KH"),
                new Card("9S"),
                new Card("9D"),
                new Card("2S")
            };
            int expectedPairMax = 13;
            int expectedPairMin = 9;

            List<int> duplicates = poker.GetDuplicates(cards);

            Assert.AreEqual(expectedPairMax, duplicates.Max());
            Assert.AreEqual(expectedPairMin, duplicates.Min());

        }

        [TestMethod()]
        public void GetNonDuplicatesTest()
        {
            PokerHelper poker = new PokerHelper();
            List<Card> cards = new List<Card>
            {
                new Card("KS"),
                new Card("KH"),
                new Card("9S"),
                new Card("9D"),
                new Card("2S")
            };
            List<int> duplicates = new List<int>
            {
                13,
                9
            };
            string expectedCardValue = "2S";
            int expectedCount = 1;
            List<Card> nonDuplicates = poker.GetNonDuplicates(cards, duplicates);

            Assert.AreEqual(expectedCount, nonDuplicates.Count());
            Assert.AreEqual(expectedCardValue, nonDuplicates[0].Value);
        }

        [TestMethod()]
        public void HighCardTieBreak_HandOneWinsTest()
        {

            PokerHelper poker = new PokerHelper();
            List<Card> hand1 = new List<Card>
            {
                new Card("KC"),
                new Card("KD"),
                new Card("9S"),
                new Card("9D"),
                new Card("2S")
            };

            List<Card> hand2 = new List<Card>
            {
                new Card("KS"),
                new Card("KH"),
                new Card("8S"),
                new Card("8D"),
                new Card("2C")
            };

            bool winner = poker.HighCardTieBreak_HandOneWins(hand1, hand2);

            Assert.IsTrue(winner);
        }

    }
}