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


		printf("----\n");
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
