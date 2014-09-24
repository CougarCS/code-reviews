package com.zeebo.poker

import com.zeebo.util.CollectionCategory

/**
 * User: Eric
 * Date: 9/23/14
 */
class Hand {

	@Delegate
	List cards

	def getRoyalFlush() {

	}

	def getFourOfAKind() {
		cards.groupBy { it.rank }.find { k, v -> v.size() == 4 }
	}

	def getFlush() {
		cards.groupBy { it.suit }.find { k, v -> v.size() == 5 }
	}

	def getStraight() {
		use(CollectionCategory) {
			cards*.rank*.value == cards[0].rank.value..cards[4].rank.value ? cards : cards[4].rank == Card.Rank['A'] && cards[0].rank == Card.Rank[2] &&
				cards*.rank*.value.rotateRight() == [cards[4].rank.value, cards[0].rank.value..cards[3].rank.value].flatten() ? cards.rotateRight() : null
		}
	}

	def getThreeOfAKind() {
		cards.groupBy { it.rank }.find { k, v -> v.size() == 3 }
	}

	def getTwoOfAKind() {
		cards.groupBy { it.rank }.find { k, v -> v.size() == 2 }
	}
}
