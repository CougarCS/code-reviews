#include <ctype.h>
#include <stdio.h>

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
		return atoi(rank_string);
	} else {
		switch( rank_string[0] ) {
			case 'J': return card_J;
			case 'Q': return card_Q;
			case 'K': return card_K;
			case 'A': return card_A;
		}
	}

#ifdef DEBUG
	fprintf(stderr, "%s: invalid rank argument: %s\n", __FUNCTION__, rank_string);
#endif /* DEBUG */
	return -1; /* error */
}

int isflush( card_t* hand ) {
	/* TODO */
}
