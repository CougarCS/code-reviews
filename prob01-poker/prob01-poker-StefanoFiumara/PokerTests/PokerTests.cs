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
            expected.Add(new PokerCard(CardRanks.Rank_2, CardSuits.HEARTS));
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
            var hand = TestHandGenerator.GetHand_Flush();
            Assert.IsTrue(Poker.IsFlush(hand));
        }

        [Test]
        public void TestHandIsNotFlush()
        {
            var hand = TestHandGenerator.GetHand_UnsortedSuit();
            Assert.IsFalse(Poker.IsFlush(hand));
        }

        [Test]
        public void TestHandIsStraight()
        {
            var hand = TestHandGenerator.GetHand_Straight();
            Assert.IsTrue(Poker.IsStraight(hand));
        }

        [Test]
        public void TestHandIsNotStraight()
        {
            var hand = TestHandGenerator.GetHand_UnsortedRank();
            Assert.IsFalse(Poker.IsStraight(hand));
        }

        [Test]
        public void TestHandIsStraightWithAceLow()
        {
            var hand = TestHandGenerator.GetHand_StraightAceLow();
            Assert.IsTrue(Poker.IsStraight(hand));
        }

        [Test]
        public void TestHandIsStraightFlush()
        {
            var hand = TestHandGenerator.GetHand_StraightFlush();
            Assert.IsTrue(Poker.IsStraightFlush(hand));
        }

        [Test]
        public void TestRoyalFlushIsNotStraightFlush()
        {
            var hand = TestHandGenerator.GetHand_RoyalFlush();
            Assert.IsFalse(Poker.IsStraightFlush(hand));
        }

        [Test]
        public void TestHandIsRoyalFlush()
        {
            var hand = TestHandGenerator.GetHand_RoyalFlush();
            Assert.IsTrue(Poker.IsRoyalFlush(hand));
        }

        [Test]
        public void TestHandIsFourOfAKind()
        {
            var hand = TestHandGenerator.GetHand_FourOfAKind();
            Assert.IsTrue(Poker.IsFourOfAKind(hand));
        }
    }
}
