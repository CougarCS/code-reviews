## Description

Given an input hand, display the highest value of the hand. The
following list of hands is sorted from low to high (i.e., a straight
beats a pair.)

Possible Hands:

1.  High Card: if there are no pairs
2.  Pair: exactly 2 cards with the same card value, e.g., a five of
    hearts and a five of clubs.
3.  Two Pair: two different pairs of cards: e.g. two fives and two tens
4.  Three of a Kind: exactly three cards with the same card value
5.  Straight: a sequence of cards, not of the same suit, e.g.
    5H,6C,7S,8D,9S
6.  Flush: five cards of the same suit, but not in sequence, e.g.
    5H,8H,10H,QH,AH
7.  Full House: three of a kind and a pair
8.  Four of a Kind: four cards of the same card value
9.  Straight Flush: five cards of the same suit, in sequence, e.g. 5D,
    6D, 7D, 8D, 9D.
10. Royal Flush: 10, Jack, Queen, King, Ace of the same suit, e.g.
    10C,JC,QC,KC,AC

Example:

+-------------------+---------------------+
|  2H,3D,KS,JS,JD   | Pair (JS,JD)        |
|  2H,3H,KH,QH,8H   | Flush (Hearts)      |
|  10C,JC,QC,KC,AC  | Royal Flush (Clubs) |
|  5D,6D,7C,9S,QS   | High Card (Queen)   |
+-------------------+---------------------+

## Input

<card value><suit>,<card value><suit>,<card value><suit>,<card value><suit>,<card value><suit>

card value = { 2,3,4,5,6,7,8,9,10,J,Q,K,A } (sorted)

suit = { H,C,D,S } (sorted)

Example: 2H,3D,KS,JS,JD

Assume all hands are valid (no duplicate cards), only 5 cards, etc.

Jack = J, Queen = Q, King = K, Ace = A

Card values are not sorted by card value or suit. Sorting may be
necessary.
