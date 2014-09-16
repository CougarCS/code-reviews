#include <stdio.h>
#include "util.h"

int counter = 1;
int fail_count = 0;
int plan_count = 0;

#define plan(_X_COUNT) plan_loc(_X_COUNT, __FILE__, __LINE__)
#define ok(_X_COND, _X_MSG) ok_loc(_X_COND, _X_MSG, __FILE__, __LINE__)

void plan_loc(int tests, const char* file, int line) {
	plan_count = tests;
	printf("1..%d\n", tests);
}

void ok_loc(int cond, const char* msg, const char* file, int line) {
	printf("%s %d - %s\n", ( cond ? "ok" : "not ok"), counter, msg );
	if( !cond ) {
		fail_count++;
		fprintf(stderr, "#  Failed test at %s line %d\n", file, line);
	}
	counter++;
}

void done_testing() {
	if( fail_count ) {
		fprintf(stderr, "#  Looks like you failed %d tests out of %d\n", fail_count, plan_count);
	} else {
		fprintf(stderr, "#  All tests passed\n");
	}
}


int main(int argc, char** argv) {
	plan(2 + 5);
	/* 2 */
	ok( string_to_rank("Z") == -1, "Z is invalid rank");
	ok( string_to_rank("J") == card_J,  "jack parsed correctly");

	card_t c1 = { card_10, 'D' };
	card_t c2 = { card_J , 'D' };
	card_t c3 = { card_J , 'S' };
	/* 5 */
	ok(cmpcardp( &c1, &c2 ) < 0 , "ordering: rank");
	ok(cmpcardp( &c2, &c1 ) > 0 , "ordering: rank");
	ok(cmpcardp( &c2, &c3 ) < 0 , "ordering: suit");
	ok(cmpcardp( &c3, &c2 ) > 0 , "ordering: suit");
	ok(cmpcardp( &c3, &c3 ) == 0, "ordering: equal");

	done_testing();
	return 0;
}


