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
			def matcher = it =~ /([2-9JQKA]|10)([HCDS])/
			if (matcher.find()) {
				new Card(suit: Card.Suit[matcher.group(2)], rank: Card.Rank[matcher.group(1)])
			}
		}.sort())
	}

	public static void main(String[] args) {
		PokerGame game = new PokerGame()

		game.file = 'testFile'

		println game.nextHand.fourOfAKind
		println game.nextHand.flush
		println game.nextHand.flush
	}
}
