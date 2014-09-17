using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using PokerLib;
using PokerCard = PokerLib.Poker.PokerCard;
using CardRanks = PokerLib.Poker.CardRanks;
using CardSuits = PokerLib.Poker.CardSuits;

namespace PokerTests
{
    [TestFixture]
    class PokerTests
    {
        [Test]
        public void CanaryTest()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestSortByRank()
        {
            var expected = new List<PokerCard>();
            expected.Add(new PokerCard(CardRanks.Rank_2, CardSuits.DIAMONDS));
            expected.Add(new PokerCard(CardRanks.Rank_3, CardSuits.DIAMONDS));
            expected.Add(new PokerCard(CardRanks.Rank_6, CardSuits.DIAMONDS));
            expected.Add(new PokerCard(CardRanks.Rank_7, CardSuits.DIAMONDS));
            expected.Add(new PokerCard(CardRanks.Rank_9, CardSuits.DIAMONDS));

            CollectionAssert.AreEqual(expected, Poker.SortByRank(TestHandGenerator.GetHand_UnsortedRank()));
        }

        [Test]
        public void TestSortBySuit()
        {
            var expected = new List<PokerCard>();
            expected.Add(new PokerCard(CardRanks.Rank_2, CardSuits.HEARTS));
            expected.Add(new PokerCard(CardRanks.Rank_2, CardSuits.DIAMONDS));
            expected.Add(new PokerCard(CardRanks.Rank_2, CardSuits.CLOVER));
            expected.Add(new PokerCard(CardRanks.Rank_2, CardSuits.SPADES));

            CollectionAssert.AreEqual(expected, Poker.SortBySuit(TestHandGenerator.GetHand_UnsortedSuit()));
        }

        [Test]
        public void TestHandIsFlush()
        {
            Assert.IsTrue(Poker.IsFlush(TestHandGenerator.GetHand_Flush()));
        }

        [Test]
        public void TestHandIsNotFlush()
        {
            Assert.IsFalse(Poker.IsFlush(TestHandGenerator.GetHand_UnsortedSuit()));
        }

        [Test]
        public void TestHandIsStraight()
        {
            Assert.IsTrue(Poker.IsStraight(TestHandGenerator.GetHand_Straight()));
        }

        [Test]
        public void TestHandIsStraightWithAceLow()
        {
            Assert.IsTrue(Poker.IsStraight(TestHandGenerator.GetHand_StraightAceLow()));
        }
    }
}
