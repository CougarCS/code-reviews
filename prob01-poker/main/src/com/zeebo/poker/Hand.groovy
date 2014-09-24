package com.zeebo.poker

import com.zeebo.util.CollectionCategory
import groovy.transform.Memoized

/**
 * User: Eric
 * Date: 9/23/14
 */
class Hand {

	@Delegate
	List cards

	@Memoized
	def getRoyalFlush() {
		straightFlush?.straight[4]?.rank == Card.Rank['A'] ? [royalFlush: straight] : null
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
			cards*.rank*.value == cards[0].rank.value..cards[4].rank.value ? cards : cards[4].rank == Card.Rank['A'] && cards[0].rank == Card.Rank[2] &&
				cards*.rank*.value.rotateRight() == [cards[4].rank.value, cards[0].rank.value..cards[3].rank.value].flatten() ? cards.rotateRight() : null
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
}
