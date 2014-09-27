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

        [Test]
        public void TestHandIsFullHouse()
        {
            var hand = TestHandGenerator.GetHand_FullHouse();
            Assert.IsTrue(Poker.IsFullHouse(hand));
        }

        [Test]
        public void TestHandIsNotFullHouse()
        {
            var hand = TestHandGenerator.GetHand_FourOfAKind();
            Assert.IsFalse(Poker.IsFullHouse(hand));
        }

        [Test]
        public void TestHandIsThreeOfAKind()
        {
            var hand = TestHandGenerator.GetHand_ThreeOfAKind();
            Assert.IsTrue(Poker.IsThreeOfAKind(hand));
        }
        [Test]
        public void TestHandIsNotThreeOfAKind()
        {
            var hand = TestHandGenerator.GetHand_TwoPair();
            Assert.IsFalse(Poker.IsThreeOfAKind(hand));
        }

        [Test]
        public void TestHandIsTwoPair()
        {
            var hand = TestHandGenerator.GetHand_TwoPair();
            Assert.IsTrue(Poker.IsTwoPair(hand));
        }

        [Test]
        public void TestHandIsNotTwoPair()
        {
            var hand = TestHandGenerator.GetHand_ThreeOfAKind();
            Assert.IsFalse(Poker.IsTwoPair(hand));
        }

        [Test]
        public void TestHandIsPair()
        {
            var hand = TestHandGenerator.GetHand_Pair();
            Assert.IsTrue(Poker.IsPair(hand));
        }

        [Test]
        public void TestHandIsNotPair()
        {
            var hand = TestHandGenerator.GetHand_TwoPair();
            Assert.IsFalse(Poker.IsPair(hand));
        }

        [Test]
        public void TestHandIsHighCard()
        {
            var hand = TestHandGenerator.GetHand_UnsortedRank();
            Assert.IsTrue(Poker.IsHighCard(hand));
        }
    }
}
