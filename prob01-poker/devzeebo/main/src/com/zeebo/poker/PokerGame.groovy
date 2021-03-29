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
		new Hand(cards: reader.readLine().split(',').collect {
			(it =~ /([2-9JQKA]|10)([HCDS])/).with {
				new Card(suit: Card.Suit[it[0][2]], rank: Card.Rank[it[0][1]])
			}
		}.sort())
	}

	public static void main(String[] args) {
		PokerGame game = new PokerGame()

		game.file = 'testFile'

		println game.nextHand.bestHand
		println game.nextHand.bestHand
		println game.nextHand.bestHand
		println game.nextHand.bestHand
		println game.nextHand.bestHand
		println game.nextHand.bestHand
		println game.nextHand.bestHand
		println game.nextHand.bestHand
		println game.nextHand.bestHand
		println game.nextHand.bestHand
		println game.nextHand.bestHand
	}
}
