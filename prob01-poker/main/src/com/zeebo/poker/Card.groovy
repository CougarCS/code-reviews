package com.zeebo.poker

import static com.zeebo.poker.Card.Rank.*
import static com.zeebo.poker.Card.Rank.*
import static com.zeebo.poker.Card.Suit.*

/**
 * User: Eric
 * Date: 9/22/14
 */
class Card {

	enum Suit {
		Hearts, Clubs, Diamonds, Spades
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
	Rank value

	String toString() { "$value of ${suit.name()}" }
}
