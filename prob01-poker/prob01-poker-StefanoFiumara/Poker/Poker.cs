using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLib
{
    public class Poker
    {
        public enum CardRanks
        {
            Rank_2 = 2,
            Rank_3,
            Rank_4,
            Rank_5,
            Rank_6,
            Rank_7,
            Rank_8,
            Rank_9,
            Rank_10,
            Rank_J,
            Rank_Q,
            Rank_K,
            Rank_A
        }

        public enum CardSuits
        {
            HEARTS, DIAMONDS, CLOVER, SPADES
        }


        public struct PokerCard
        {
            public CardRanks Rank;
            public CardSuits Suit;

            public PokerCard(CardRanks rank, CardSuits suit)
            {
                Rank = rank;
                Suit = suit;
            }
        }

        public static List<PokerCard> SortByRank(List<PokerCard> hand)
        {
            return hand.OrderBy(x => x.Rank).ToList();
        }

        public static List<PokerCard> SortBySuit(List<PokerCard> hand)
        {
            return hand.OrderBy(x => x.Suit).ToList();
        }

        public static bool IsFlush(List<PokerCard> hand)
        {
            hand = SortBySuit(hand);

            return hand.ElementAt(0).Suit == hand.ElementAt(hand.Count - 1).Suit;
        }

        public static bool IsStraight(List<PokerCard> hand)
        {
            //TODO: Check for straight with Ace Low
            hand = SortByRank(hand);

            var current = hand.ElementAt(0).Rank;
            for (int i = 1; i < hand.Count; i++)
            {
                if (++current != hand.ElementAt(i).Rank)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
