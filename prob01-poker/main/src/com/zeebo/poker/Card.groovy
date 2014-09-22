package com.zeebo.poker
/**
 * User: Eric
 * Date: 9/22/14
 */
class Card implements Comparable<Card> {

	enum Suit {
		Hearts, Clubs, Diamonds, Spades

		static Suit getAt(def shortCode) {
			values().find { it.name().startsWith(shortCode) }
		}
	}

	enum Rank {
		_2, _3, _4, _5, _6, _7, _8, _9, _10, Jack(11), Queen(12), King(13), Ace(14)

		int value

		Rank() {
			value = name().substring(1) as int
		}
		Rank(v) {
			value = v
		}

		static Rank getAt(def shortCode) {
			shortCode ==~ /\d+/ ? this."_$shortCode" : values().find { it.name().startsWith(shortCode) }
		}

		String toString() { name().replace('_', '') }
	}

	Suit suit
	Rank rank

	String toString() { "$rank of ${suit.name()}" }

	int compareTo(Card other) {
		suit <=> other.suit ?: rank <=> other.rank
	}
}
