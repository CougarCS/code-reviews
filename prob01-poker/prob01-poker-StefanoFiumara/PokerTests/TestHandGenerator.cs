using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokerLib;
using PokerCard = PokerLib.Poker.PokerCard;
using CardRanks = PokerLib.Poker.CardRanks;
using CardSuits = PokerLib.Poker.CardSuits;

namespace PokerTests
{
    class TestHandGenerator
    {
        public static List<PokerCard> GetHand_UnsortedRank()
        {
            var hand = new List<PokerCard>();
            hand.Add(new PokerCard(CardRanks.Rank_6, CardSuits.DIAMONDS));
            hand.Add(new PokerCard(CardRanks.Rank_3, CardSuits.DIAMONDS));
            hand.Add(new PokerCard(CardRanks.Rank_7, CardSuits.DIAMONDS));
            hand.Add(new PokerCard(CardRanks.Rank_9, CardSuits.DIAMONDS));
            hand.Add(new PokerCard(CardRanks.Rank_2, CardSuits.DIAMONDS));
            return hand;
        }

        public static List<PokerCard> GetHand_UnsortedSuit()
        {
            var hand = new List<PokerCard>();
            hand.Add(new PokerCard(CardRanks.Rank_2, CardSuits.SPADES));
            hand.Add(new PokerCard(CardRanks.Rank_2, CardSuits.CLOVER));
            hand.Add(new PokerCard(CardRanks.Rank_2, CardSuits.HEARTS));
            hand.Add(new PokerCard(CardRanks.Rank_2, CardSuits.DIAMONDS));
            return hand;
        }

        public static List<PokerCard> GetHand_Flush()
        {
            return GetHand_UnsortedRank(); //for legibility in the test class
        }



        public static List<PokerCard> GetHand_Straight()
        {
            var hand = new List<PokerCard>();
            hand.Add(new PokerCard(CardRanks.Rank_2, CardSuits.HEARTS));
            hand.Add(new PokerCard(CardRanks.Rank_6, CardSuits.DIAMONDS));
            hand.Add(new PokerCard(CardRanks.Rank_4, CardSuits.SPADES));
            hand.Add(new PokerCard(CardRanks.Rank_5, CardSuits.DIAMONDS));
            hand.Add(new PokerCard(CardRanks.Rank_3, CardSuits.CLOVER));
            return hand;
        }

        internal static List<PokerCard> GetHand_StraightAceLow()
        {
            var hand = new List<PokerCard>();
            hand.Add(new PokerCard(CardRanks.Rank_2, CardSuits.HEARTS));
            hand.Add(new PokerCard(CardRanks.Rank_A, CardSuits.DIAMONDS));
            hand.Add(new PokerCard(CardRanks.Rank_4, CardSuits.SPADES));
            hand.Add(new PokerCard(CardRanks.Rank_5, CardSuits.DIAMONDS));
            hand.Add(new PokerCard(CardRanks.Rank_3, CardSuits.CLOVER));
            return hand;
        }
    }
}
