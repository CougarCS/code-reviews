#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <string.h>

#include "poker.h"

void usage(int argc, char** argv);
void dump_hand( card_t* hand, size_t nmemb );
void dump_rank_count( int* rank_count );

int main(int argc, char** argv) {
	char *buffer, *filename;
	FILE* fh;
	card_t hand[HAND_SZ];
	size_t len = 0;
	ssize_t read;
	int card_i;
	char card_rank[5][3];
	char card_suit[5];

	int rank_count[ RANK_MAX + 1 ];
	int rank_count_i;

	if( argc != 2 ) {
		usage(argc, argv);
		exit(1);
	}

	filename = argv[1];
	if( NULL == (fh = fopen(filename, "r")) )
		exit(EXIT_FAILURE);

	while( -1 != (read = getline(&buffer, &len, fh)) ) {
		/* read the data from the line */
		char* new_line_idx = index( buffer, '\n');
		if( new_line_idx != NULL ) {
			*new_line_idx = '\0'; /* Get rid of the newline. This will be useful for later. */
		}
		sscanf(buffer,
			"%[0-9JQKA]%c,%[0-9JQKA]%c,%[0-9JQKA]%c,%[0-9JQKA]%c,%[0-9JQKA]%c",
			&card_rank[0], &card_suit[0],
			&card_rank[1], &card_suit[1],
			&card_rank[2], &card_suit[2],
			&card_rank[3], &card_suit[3],
			&card_rank[4], &card_suit[4]
			);
		for( card_i = 0; card_i < HAND_SZ; card_i++ ) {
			hand[card_i].rank = string_to_rank(card_rank[card_i]);
			hand[card_i].suit = card_suit[card_i];
		}

		/*dump_hand(hand, HAND_SZ);[>DEBUG<]*/
		qsort(hand, HAND_SZ, sizeof(hand[0]), cmpcardp);
		/*dump_hand(hand, HAND_SZ);[>DEBUG<]*/

		memset( rank_count, 0, sizeof(rank_count) );
		for( card_i = 0; card_i < HAND_SZ; card_i++ ) {
			rank_count[ hand[card_i].rank ]++;
		}
		/*dump_rank_count(rank_count);[> DEBUG <]*/

		int n_of_a_kind[5] = { 0, 0, 0, 0, 0 };
		int three_of_a_kind_rank = -1;
		int two_of_a_kind_rank_0 = -1;
		int two_of_a_kind_rank_1 = -1;
		for( rank_count_i = RANK_MIN; rank_count_i < RANK_MAX; rank_count_i++ ) {
			n_of_a_kind[ rank_count[rank_count_i] ]++;
			if( rank_count[rank_count_i] ==  3 ) {
				three_of_a_kind_rank = rank_count_i;
			}
			if( rank_count[rank_count_i] ==  2 ) {
				if( two_of_a_kind_rank_0 == -1 )
					two_of_a_kind_rank_0 = rank_count_i;
				else
					two_of_a_kind_rank_1 = rank_count_i;
			}
		}

		int is_sequential = hand_is_sequential( hand, HAND_SZ );
		int all_same_suit =    hand[0].suit == hand[1].suit
		                    && hand[0].suit == hand[2].suit
		                    && hand[0].suit == hand[3].suit
		                    && hand[0].suit == hand[4].suit;

		int four_of_a_kind = n_of_a_kind[ 4 ];
		int three_of_a_kind = n_of_a_kind[ 3 ];
		int two_pair = n_of_a_kind[ 2 ] == 2;
		int pair = n_of_a_kind[ 2 ] == 1;
		int full_house = three_of_a_kind && pair;
		int straight = is_sequential && !all_same_suit;
		int flush = !is_sequential && all_same_suit;
		int straight_flush = is_sequential && all_same_suit;
		int royal_flush = straight_flush && hand[0].rank == card_10;

		printf("|%20s | ", buffer); /* print cards in hand */
		int printed_width = 0;
		if( royal_flush ) {
			/*10. Royal Flush: 10, Jack, Queen, King, Ace of the
			 * same suit, e.g.  10C,JC,QC,KC,AC*/
			printed_width = printf("Royal flush (suit = %c)", hand[0].suit);
		} else if( straight_flush ) {
			/*9.  Straight Flush: five cards of the same suit, in
			 * sequence, e.g. 5D, 6D, 7D, 8D, 9D.*/
			printed_width = printf("Straight flush (suit = %c)", hand[0].suit);
		} else if( four_of_a_kind ) {
			/*8.  Four of a Kind: four cards of the same card
			 * value*/
			printed_width = printf("Four of a kind (rank = %s)",
					  hand[0].rank == hand[1].rank
					? rank_t_string[hand[0].rank] /* ranks 0..3 are the same */
					: rank_t_string[hand[1].rank] /* ranks 1..4 are the same */
					);
		} else if( full_house ) {
			/*7.  Full House: three of a kind and a pair*/
			int three_kind_first = hand[1].rank == hand[2].rank; /* 3 (3) (3) 2 2 */
			printed_width = printf("Full house (ranks: 3 of %s and 2 of %s)",
						 three_kind_first ? rank_t_string[ hand[0].rank ] : rank_t_string[ hand[3].rank ],
						!three_kind_first ? rank_t_string[ hand[0].rank ] : rank_t_string[ hand[3].rank ]
					);
		} else if( flush ) {
			/*6.  Flush: five cards of the same suit, but not in
			 * sequence, e.g.  5H,8H,10H,QH,AH*/
			printed_width = printf("Flush (suit = %c)", hand[0].suit);
		} else if( straight ) {
			/*5.  Straight: a sequence of cards, not of the same
			 * suit, e.g.  5H,6C,7S,8D,9S*/
			printed_width = printf("Straight (from %s to %s)", rank_t_string[hand[0].rank], rank_t_string[hand[4].rank]);
		} else if( three_of_a_kind ) {
			/*4.  Three of a Kind: exactly three cards with the
			 * same card value*/
			int three_kind_first = hand[1].rank == hand[2].rank; /* 3 (3) (3) * * */
			printed_width = printf("Three of a kind (rank = %s)",
					three_kind_first ? rank_t_string[ hand[0].rank ] : rank_t_string[ hand[3].rank ]
					);
		} else if( two_pair ) {
			/*3.  Two Pair: two different pairs of cards: e.g. two
			 * fives and two tens*/
			printed_width = printf("Two pair");
		} else if( pair ) {
			/*2.  Pair: exactly 2 cards with the same card value,
			 * e.g., a five of hearts and a five of clubs.*/
			printed_width = printf("Pair");
		} else { /* high card */
			/*1.  High Card: if there are no pairs*/
			printed_width = printf("High card: %s%c", rank_t_string[ hand[4].rank ], hand[4].suit );
		}
		printf("%*s\n", 25 - printed_width, "|");
	}

	if(buffer)
		free(buffer);


	return EXIT_SUCCESS;
}

/*
 * usage(int argc, char** argv)
 *
 * Prints the usage message to stderr. The arguments argc and argv are the same
 * as those passed to the main() function.
 */
void usage(int argc, char** argv) {
	fprintf(stderr, "Usage:\n  %s <filename>\n", argv[0]);
}

void dump_hand( card_t* hand, size_t nmemb ) {
	int card_i;
	for( card_i = 0; card_i < nmemb; card_i++ ) {
		fprintf(stderr, "%d-%c\n",
				hand[card_i].rank,
				hand[card_i].suit);
	}
}

void dump_rank_count( int* rank_count ) {
	int rank_count_i;
	for( rank_count_i = RANK_MIN; rank_count_i < RANK_MAX; rank_count_i++ ) {
		printf("rank_count[%2d] = %d\n", rank_count_i, rank_count[rank_count_i]);
	}
}
