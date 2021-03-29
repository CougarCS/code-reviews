#include <ctype.h>
#include <stdio.h>
#include <string.h>

#include "util.h"

/*
 * rank_t string_to_rank(char* rank_string)
 *
 * Returns an integer representing the rank given by the string representation
 * in in argument rank_string.
 *
 * Input:
 *   char* rank_string: A null-terminated C string representing the rank of a card.
 *                      For example, "2", "3", "J", or "A"."
 */
rank_t string_to_rank(const char* rank_string) {
	if( isdigit(rank_string[0]) ) {
		/* card_2, card_3, ..., card_10 */
		int rank_num = atoi(rank_string);
		if( rank_num >= card_2 && rank_num <= card_10 ) {
			return rank_num;
		}
	} else {
		if( strlen(rank_string) == 1 ) {
			switch( rank_string[0] ) {
				case 'J': return card_J;
				case 'Q': return card_Q;
				case 'K': return card_K;
				case 'A': return card_A;
			}
		}
	}

#ifdef DEBUG
	fprintf(stderr, "%s: invalid rank argument: %s\n", __FUNCTION__, rank_string);
#endif /* DEBUG */
	return -1; /* error */
}

int suit_ordering( char suit ) {
	switch( suit ) {
		case 'H': return 1;
		case 'C': return 2;
		case 'D': return 3;
		case 'S': return 4;
	}
	return -1;
}

/*
 * int cmpcardp(const void* c1, const void* c2)
 *
 * comparison function for qsort().
 */
int cmpcardp(const void* c1, const void* c2) {
	int rank_diff = ((card_t*)c1)->rank - ((card_t*)c2)->rank;
	if( rank_diff != 0 ) {
		return rank_diff;
	} else {
		return suit_ordering( ((card_t*)c1)->suit ) - suit_ordering( ((card_t*)c2)->suit );
	}
}

/* hand_is_sequential(card_t* hand, size_t nmemb )
 *
 * Requires: hand is sorted.
 *
 * Returns: True if hand is sequential in rank. False otherwise.
 */
bool hand_is_sequential(card_t* hand, size_t nmemb ) {
	size_t card_i;
	rank_t prev_rank;
	if( nmemb > 0 ) {
		prev_rank = hand[0].rank;
	}
	for (card_i = 1; card_i < nmemb; card_i++) {
		if( prev_rank + 1 != hand[card_i].rank ) {
			return 0;
		}
		prev_rank = hand[card_i].rank; /* update for next iteration */
	}
	return 1;
}
