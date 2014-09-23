package com.zeebo.poker

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
}
