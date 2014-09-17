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
    class HandGenerator
    {
        public static List<PokerCard> GetHand_UnsortedRankFlush()
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
    }
}
