package com.zeebo.poker

/**
 * User: Eric
 * Date: 9/22/14
 */
class PokerGame {

	def reader

	PokerGame() {}

	void setFile(String inputFile) {
		reader = new File(inputFile).newReader()
	}

	def getNextHand() {
		reader.readLine().split(',').collect {
			def matcher = it =~ /([2-9JQKA]|10)([HCDS])/
			new Card(suit: Card.Suit[matcher[1]], rank: Card.Rank[matcher[0]])
		}.sort()
	}

	public static void main(String[] args) {
		PokerGame game = new PokerGame()

		game.file = 'testFile'
	}
}
