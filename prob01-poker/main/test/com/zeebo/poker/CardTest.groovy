package com.zeebo.poker

/**
 * User: Eric
 * Date: 9/22/14
 */

class CardTest extends GroovyTestCase {

	def twoToTen = (2..10)
	def faceCards = ['Jack', 'Queen', 'King', 'Ace']

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
}
