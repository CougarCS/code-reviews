#ifndef POKER_H
#define POKER_H

#define BUFFER_SZ (255)
#define HAND_SZ (5)

#define CARD_FORMAT "%[0-9JQKA]%1[HDSC]"
#define CARD_ITEMS (2)
#define HAND_FORMAT \
		CARD_FORMAT"," /* 0 */ \
		CARD_FORMAT"," /* 1 */ \
		CARD_FORMAT"," /* 2 */ \
		CARD_FORMAT"," /* 3 */ \
		CARD_FORMAT    /* 4 */
#define HAND_ITEMS (CARD_ITEMS * HAND_SZ)

#include "util.h"

#endif /* POKER_H */
