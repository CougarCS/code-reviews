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
            hand = SortByRank(hand);

            return hand.ElementAt(hand.Count - 1).Rank - hand.ElementAt(0).Rank == 4 || IsStraightAceLow(hand);
        }

        private static bool IsStraightAceLow(List<PokerCard> hand)
        {
            //sort is already done by IsStraight();
            return hand.ElementAt(0).Rank == CardRanks.Rank_2 &&
                   hand.ElementAt(1).Rank == CardRanks.Rank_3 &&
                   hand.ElementAt(2).Rank == CardRanks.Rank_4 &&
                   hand.ElementAt(3).Rank == CardRanks.Rank_5 &&
                   hand.ElementAt(4).Rank == CardRanks.Rank_A;
        }

        public static bool IsStraightFlush(List<PokerCard> hand)
        {
            hand = SortByRank(hand);
            return IsStraight(hand) && IsFlush(hand) && hand.ElementAt(hand.Count - 1).Rank != CardRanks.Rank_A;
        }

        public static bool IsRoyalFlush(List<PokerCard> hand)
        {
            hand = SortByRank(hand);
            return IsStraight(hand) && IsFlush(hand) && hand.ElementAt(hand.Count - 1).Rank == CardRanks.Rank_A;
        }



        public static bool IsFourOfAKind(List<PokerCard> hand)
        {
            hand = SortByRank(hand);

            return hand.ElementAt(0).Rank == hand.ElementAt(3).Rank || hand.ElementAt(1).Rank == hand.ElementAt(4).Rank;
        }

        public static bool IsFullHouse(List<PokerCard> hand)
        {
            var groups = hand.GroupBy(card => card.Rank);

            return groups.ElementAt(0).Count() == 2 ||
                   groups.ElementAt(0).Count() == 3;
        }

        public static bool IsThreeOfAKind(List<PokerCard> hand)
        {
            var groups = hand.GroupBy(card => card.Rank);


            return (groups.ElementAt(0).Count() == 1 || groups.ElementAt(0).Count() == 3) &&
                    groups.Count() == 3;
        }

        public static bool IsTwoPair(List<PokerCard> hand)
        {
            var groups = hand.GroupBy(card => card.Rank);

            return (groups.ElementAt(0).Count() == 1 || groups.ElementAt(0).Count() == 2) &&
                    groups.Count() == 3;
        }

        public static bool IsPair(List<PokerCard> hand)
        {
            var groups = hand.GroupBy(card => card.Rank);

            return groups.Count() == 4;
        }

        public static bool IsHighCard(List<PokerCard> hand)
        {
            var RankGroups = hand.GroupBy(card => card.Rank);
            var SuitGroups = hand.GroupBy(card => card.Suit);

            return RankGroups.Count() == 5 && SuitGroups.Count() != 1;
        }
    }
}
