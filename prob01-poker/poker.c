#include <stdio.h>
#include <stdlib.h>

#include "poker.h"

void usage(int argc, char** argv) {
	fprintf(stderr, "Usage:\n  %s <filename>\n", argv[0]);
}


int main(int argc, char** argv) {
	char* buffer;
	char* filename;
	FILE* fh;
	card_t hand[HAND_SZ];
	size_t len = 0;
	ssize_t read;
	int card_i;
	char card_rank[5][3];
	char card_suit[5];

	if( argc != 2 ) {
		usage(argc, argv);
		exit(1);
	}

	filename = argv[1];
	if( NULL == (fh = fopen(filename, "r")) )
		exit(EXIT_FAILURE);

	while( -1 != (read = getline(&buffer, &len, fh)) ) {
		sscanf(buffer,
			"%[0-9JQKA]%c,%[0-9JQKA]%c,%[0-9JQKA]%c,%[0-9JQKA]%c,%[0-9JQKA]%c",
			&( card_rank[0]), &( card_suit[0]),
			&( card_rank[1]), &( card_suit[1]),
			&( card_rank[2]), &( card_suit[2]),
			&( card_rank[3]), &( card_suit[3]),
			&( card_rank[4]), &( card_suit[4])
			);
		for( card_i = 0; card_i < HAND_SZ; card_i++ ) {
			printf("%s %c\n", card_rank[card_i], card_suit[card_i]);
		}
		printf("----\n");
	}

	if(buffer)
		free(buffer);


	return EXIT_SUCCESS;
}
