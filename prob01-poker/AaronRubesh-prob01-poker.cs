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
                Console.WriteLine("Checking..{0}", c.ToString());
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
           
            bool straight = checkStraight(Hand.ToArray());
            bool flush = checkFlush(Hand.ToArray());
            List<int> cardCount = countCards(Hand);
           
            if (straight)
            {
                if (flush)
                {
                    if (Card.LowestCard(Hand.ToArray()).Value == "10")
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
            else if (cardCount.Contains(4))
            {
                Console.WriteLine("Four of a kind({0})", cardValues[cardCount.IndexOf(4)]);
            }
            else if (cardCount.Contains(3))
            {
                if (cardCount.Contains(2))
                {
                    Console.WriteLine("Full House({0},{1})", cardValues[cardCount.IndexOf(3)], cardValues[cardCount.IndexOf(2)]);
                }
                else{
                Console.WriteLine("Three of a kind({0})", cardValues[cardCount.IndexOf(3)]);
                }
            }
            else if (cardCount.Contains(2))
            {
                if (cardCount.Contains(3))
                {
                    Console.WriteLine("Full House({0},{1})", cardValues[cardCount.IndexOf(3)], cardValues[cardCount.IndexOf(2)]);
                }
                if (cardCount.IndexOf(2) > 0 && (cardCount.IndexOf(2, cardCount.IndexOf(2)) + 1 < cardCount.Count && cardCount.IndexOf(2, cardCount.IndexOf(2) + 1) > 0))
                {
                    Console.WriteLine("Two Pairs({0},{1})", cardValues[cardCount.IndexOf(2)], cardValues[cardCount.IndexOf(2, cardCount.IndexOf(2) + 1)]);
                }
                else
                {
                    Console.WriteLine("Pair({0})", cardValues[cardCount.IndexOf(3)]);
                }
            }
            else
            {
                Console.WriteLine("High Card({0})", Card.HighestCard(Hand.ToArray()));
            }
            
            
        }
        private static List<int> countCards(List<Card> hand)
        {
            int[] cardCount = new int[cardValues.Count];
            foreach (Card card in hand)
            {
                if (cardCount[cardValues.IndexOf(card.Value)] <= 0)
                    cardCount[cardValues.IndexOf(card.Value)] = 1;
                else
                    cardCount[cardValues.IndexOf(card.Value)] += 1;
            }

            List<int> temp = new List<int>();
            temp.AddRange(cardCount);
            return temp;
        }
        private static bool checkFlush(Card[] hand)
        {
            for (int i = 0; i < hand.Length; i++)
            {
                if (hand[i].Suit != hand[0].Suit)
                    return false;
            }
            return true;
        }
        
        private static bool checkStraight(Card[] hand)
        {
            Card lowestCard = Card.Empty;
            foreach (Card card in hand)
            {
                if (lowestCard == Card.Empty || (cardValues.IndexOf(card.Value) < cardValues.IndexOf(lowestCard.Value)))
                {
                    lowestCard = card;
                }
            }
            for (int i = 1; i < 5; i++)
            {
                
                int nextCard = cardValues.IndexOf(lowestCard.Value) + i;
                
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
        private static bool searchHand(Card[] hand, Card card)
        {
            if (hand.Contains(card))
                return true;
            for (int i = 0; i < hand.Length; i++)
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
                    Card lowest = LowestCard(temp.ToArray());
                    sorted.Add(lowest);
                    temp.Remove(lowest);
                }
                return sorted;
            }
            public static Card LowestCard(Card[] cards)
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
            public static Card HighestCard(Card[] cards)
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
