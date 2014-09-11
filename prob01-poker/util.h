#ifndef UTIL_H
#define UTIL_H

typedef enum {
	card_2  = 2,
	card_3, card_4, card_5, card_6,
	card_7, card_8, card_9, card_10,
	card_J, card_Q, card_K, card_A,
} rank_t;

typedef struct card {
	rank_t rank;
	char suit;
} card_t;

rank_t string_to_rank(const char* rank_string);

#endif /* UTIL_H */