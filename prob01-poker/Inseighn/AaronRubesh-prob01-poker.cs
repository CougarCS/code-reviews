using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prob01
{
    class Program
    {
        private static List<string> cardValues = new List<string>(){
            "2","3","4","5","6","7","8","9","10","J","Q","K","A"
        };
        private static List<string> cardSuits = new List<string>(){
            "H","C","D","S"
        };
        static void Main(string[] args)
        {
            string hand = string.Empty;
            if (args.Length > 0 && validateHand(args[0]))
                hand = args[0];
            bool stop = false;
            while (!stop)
            {
                do
                {
                    Console.WriteLine("Acceptable Card Values: {0}\r\nAcceptable Card Suits: {1}\r\nEnter your hand, 5 cards total formatted as \"(CARD)(SUIT),\""
                        , string.Join(",", cardValues), string.Join(",", cardSuits));
                    hand = Console.ReadLine().Trim().ToUpper().Replace(" ", "");

                } while (!validateHand(hand));

                Console.WriteLine("Your hand contains: ");
                checkHand(hand);
                Console.WriteLine("Type Stop to stop playing or enter to play again.");
                if (Console.ReadLine().Trim().ToLower() == "stop")
                {
                    stop = true;
                }
            }
        }
        private static bool validateHand(string hand)
        {
            if (hand.Count(i => i == ',') != 4)
                return false;
            foreach (string card in hand.Split(','))
            {
                if (card.Length != 2 && card.Length != 3)
                    return false;
                Card c = getCard(card);
                if (!cardValues.Contains(c.Value) || !cardSuits.Contains(c.Suit))
                    return false;
            }
            return true;
        }
        private static void checkHand(string hand)
        {
            List<Card> Hand = new List<Card>();
            foreach (string cardString in hand.Split(','))
            {
                Hand.Add(getCard(cardString));
            }
            Hand = Card.Sort(Hand);
           
            bool straight = checkStraight(Hand);
            bool flush = checkFlush(Hand);
            List<int> cardCount = new List<int>();
            cardCount.AddRange(countCards(Hand));
            int fOAC = cardCount.IndexOf(4), thrOAC = cardCount.IndexOf(3), pair1 = cardCount.IndexOf(2), pair2 = cardCount.LastIndexOf(2);
           
            if (straight)
            {
                if (flush)
                {
                    if (Card.LowestCard(Hand).Value == "10")
                    {
                        Console.WriteLine("Royal Flush ({0})", Hand[0].Suit);
                    }
                    else
                    {
                        Console.WriteLine("Straight Flush({0})", Hand[0].Suit);
                    }
                }
                else {
                    Console.WriteLine("Straight({0})", string.Join(",", Hand));
                }
            }
            else if (flush && !straight)
            {
                Console.WriteLine("Flush({0})", Hand[0].Suit);
            }
            else if (fOAC > -1)
            {
                Console.WriteLine("Four of a kind({0})", cardValues[fOAC]);
            }
            else if (thrOAC > -1 && pair1 > -1)
            {
                Console.WriteLine("Full House({0},{1})", cardValues[thrOAC], cardValues[pair1]);
            }
            else if (thrOAC > -1)
            {
                Console.WriteLine("Three of a kind({0})", cardValues[thrOAC]);
                
            }
            else if (pair1 > -1)
            {
                if (pair2 > -1 && pair2 != pair1)
                {
                    Console.WriteLine("Two Pairs({0},{1})", cardValues[pair1], cardValues[pair2]);
                }
                else
                {
                    Console.WriteLine("Pair({0})", cardValues[pair1]);
                }
            }
            else
            {
                Console.WriteLine("High Card({0})", Card.HighestCard(Hand));
            }
            
            
        }
        private static int[] countCards(List<Card> hand)
        {
            int[] cardCount = new int[12];
            foreach (Card card in hand)
            {
                cardCount[cardValues.IndexOf(card.Value)]++;
            }
            return cardCount;
        }
        private static bool checkFlush(List<Card> hand)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Suit != hand[0].Suit)
                    return false;
            }
            return true;
        }
        
        private static bool checkStraight(List<Card> hand)
        {
            for (int i = 1; i < 5; i++)
            {
                int nextCard = cardValues.IndexOf(Card.LowestCard(hand).Value) + i;
                
                if (nextCard > cardValues.Count || nextCard < 0)
                    return false;
                if (!searchHand(hand, 
                    new Card() { 
                        Suit = "*", 
                        Value = cardValues[nextCard]
                    }))
                {
                    
                    return false;
                }

            }
            return true;
        }
        private static bool searchHand(List<Card> hand, Card card)
        {
            if (hand.Contains(card))
                return true;
            for (int i = 0; i < hand.Count; i++)
            {
                if (card.Value == "*" && (hand[i].Suit == card.Suit))
                    return true;
                else if (card.Suit == "*" && (hand[i].Value == card.Value))
                    return true;
            }
            return false;
        }
        private static Card getCard(string cardString)
        {
            return new Card()
            {
                Value = (cardString.Length == 2) ? cardString.Substring(0, 1) : cardString.Substring(0, 2),
                Suit = cardString.Substring(cardString.Length - 1, 1)
            };
        }
        class Card
        {
            public string Value;
            public string Suit;
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public static Card Empty
            {
                get { return new Card() { Value = "", Suit = "" }; }
            }
            public override string ToString()
            {
                return Value + Suit;
            }
            public static bool operator ==(Card a, Card b){
                if (a.Value == b.Value && a.Suit == b.Suit)
                    return true;
                return false;
            }
            public override bool Equals(object o)
            {
                Card c = (Card)o;
                return (this.Suit == c.Suit && this.Value == c.Value);
            }
            public static bool operator !=(Card a, Card b)
            {
                if (a.Value == b.Value && a.Suit == b.Suit)
                    return false;
                return true;
            }
            public static List<Card> Sort(List<Card> cards)
            {
                List<Card> temp = new List<Card>();
                temp.AddRange(cards);
                List<Card> sorted = new List<Card>();
                while (temp.Count > 0)
                {
                    Card lowest = LowestCard(temp);
                    sorted.Add(lowest);
                    temp.Remove(lowest);
                }
                return sorted;
            }
            public static Card LowestCard(List<Card> cards)
            {
                Card lowest = Card.Empty;
                foreach (Card c in cards)
                {
                    if (lowest == Card.Empty || (cardValues.IndexOf(c.Value) < cardValues.IndexOf(lowest.Value)))
                    {
                        lowest = c;
                    }
                }
                return lowest;
            }
            public static Card HighestCard(List<Card> cards)
            {
                Card highest = Card.Empty;
                foreach (Card c in cards)
                {
                    if (highest== Card.Empty || (cardValues.IndexOf(c.Value) > cardValues.IndexOf(highest.Value)))
                    {
                        highest = c;
                    }
                }
                return highest;
            }
        }
    }
}
