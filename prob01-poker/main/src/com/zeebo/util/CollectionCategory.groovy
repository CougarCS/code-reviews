package com.zeebo.util

/**
 * User: Eric
 * Date: 9/23/14
 */
@Category(Collection)
class CollectionCategory {

	def rotateLeft() {
		[this[1..-1], this[0]].flatten()
	}

	def rotateRight() {
		[this[-1], this[0..<-1]].flatten()
	}
}
