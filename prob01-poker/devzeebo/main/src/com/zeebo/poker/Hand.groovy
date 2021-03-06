package com.zeebo.poker

import com.zeebo.util.CollectionCategory
import groovy.transform.Memoized

/**
 * User: Eric
 * Date: 9/23/14
 */
class Hand {

	static def handsByBest = ['royalFlush', 'straightFlush', 'fourOfAKind', 'fullHouse', 'flush', 'straight', 'threeOfAKind', 'twoPair', 'pair', 'highCard']

	@Delegate
	List cards

	@Memoized
	def getBestHand() {
		handsByBest.find {
			this."$it"
		}
	}

	@Memoized
	def getRoyalFlush() {
		straightFlush?.straight?.getAt(4)?.rank == Card.Rank['A'] ? [royalFlush: straight] : null
	}

	@Memoized
	def getStraightFlush() {
		straight && flush ? [straight: straight, flush: flush] : null
	}

	@Memoized
	def getFourOfAKind() {
		cards.groupBy { it.rank }.find { k, v -> v.size() == 4 }
	}

	@Memoized
	def getFullHouse() {
		threeOfAKind && pair ? [3: threeOfAKind, 2: pair] : null
	}

	@Memoized
	def getFlush() {
		cards.groupBy { it.suit }.find { k, v -> v.size() == 5 }
	}

	@Memoized
	def getStraight() {
		use(CollectionCategory) {
			cards*.rank*.value.with {
				it == it[0]..it[4] ?
					cards :
				cards[4].rank == Card.Rank['A'] && cards[0].rank == Card.Rank[2] && it.rotateRight() == [it[4], it[0]..it[3]].flatten() ?
					cards.rotateRight() :
					null
			}
		}
	}

	@Memoized
	def getThreeOfAKind() {
		cards.groupBy { it.rank }.find { k, v -> v.size() == 3 }
	}

	@Memoized
	def getTwoPair() {
		cards.groupBy { it.rank }.findAll { k, v -> v.size() == 2 }.with {
			it.size() == 2 ? it : null
		}
	}

	@Memoized
	def getPair() {
		cards.groupBy { it.rank }.find { k, v -> v.size() == 2 }
	}

	@Memoized
	def getHighCard() {
		cards[4]
	}
}
