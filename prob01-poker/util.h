#ifndef UTIL_H
#define UTIL_H

typedef enum {
	card_2  = 2,
	card_3, card_4, card_5, card_6,
	card_7, card_8, card_9, card_10,
	card_J, card_Q, card_K, card_A,
} rank_t;

#define RANK_MIN (card_2)
#define RANK_MAX (card_A)

static const char* rank_t_string[] = {
	"", "",
	"2",
	"3", "4", "5", "6",
	"7", "8", "9", "10",
	"J", "Q", "K", "A" };

typedef struct card {
	rank_t rank;
	char suit;
} card_t;

rank_t string_to_rank(const char* rank_string);
int cmpcardp(const void* c1, const void* c2);
int hand_is_sequential(card_t* hand, size_t nmemb );

#endif /* UTIL_H */
