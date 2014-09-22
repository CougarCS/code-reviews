package com.zeebo.poker

/**
 * User: Eric
 * Date: 9/22/14
 */

class CardTest extends GroovyTestCase {

	def twoToTen = (2..10)
	def faceCards = Card.Rank.values()*.name().findAll { !it.startsWith('_') }
	def suites = Card.Suit.values()*.name()

	void testRankToString() {
		twoToTen.each {
			assert Card.Rank."_$it".toString() == "$it"
		}
		faceCards.each {
			assert Card.Rank."$it".toString() == it
		}
	}

	void testRankGetAt() {
		twoToTen.each {
			assert Card.Rank[it] == Card.Rank."_$it"
		}
		faceCards.each {
			assert Card.Rank[it[0]] == Card.Rank."$it"
		}
	}

	void testRankCompareTo() {
		(2..9).each { a ->
			(a + 1..10).each { b ->
				if (a < b) {
					assert Card.Rank[a] < Card.Rank[b]
				}
			}
		}

		faceCards.each { // tested against all cards lower than 10 via the transitive property
			assert Card.Rank[it[0]] > Card.Rank[10]
		}
	}

	void testSuitGetAt() {
		suites.each {
			assert Card.Suit[it[0]] == Card.Suit."$it"
		}
	}

	void testSuitCompareTo() {
		assert Card.Suit.Hearts < Card.Suit.Clubs
		assert Card.Suit.Clubs < Card.Suit.Diamonds
		assert Card.Suit.Diamonds < Card.Suit.Spades
	}

	void testCardCompareToSameSuit() {
		assert new Card(suit: Card.Suit['S'], rank: Card.Rank['10']) < new Card(suit: Card.Suit['S'], rank: Card.Rank['A'])
		assertFalse new Card(suit: Card.Suit['S'], rank: Card.Rank['10']) > new Card(suit: Card.Suit['S'], rank: Card.Rank['A'])
	}

	void testCardCompareToDifferentSuit() {
		assert new Card(suit: Card.Suit['H'], rank: Card.Rank['A']) < new Card(suit: Card.Suit['S'], rank: Card.Rank['2'])
		assertFalse new Card(suit: Card.Suit['H'], rank: Card.Rank['A']) > new Card(suit: Card.Suit['S'], rank: Card.Rank['2'])
	}
}
