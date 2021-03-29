CPPFLAGS:= -DDEBUG
OBJECTS:= util.o
run: poker
	./poker data.txt

poker: poker.c $(OBJECTS)
	$(LINK.c) -o $@ $^

diff: poker
	bash -c "diff <($(MAKE) run) expect.txt"

test: t/tests
	prove -v -e '' $^

t/tests: tests.c $(OBJECTS)
	-@mkdir t 2>&1 > /dev/null
	$(LINK.c) -o $@ $^

lint:
	splint -unrecog *.c

clean:
	-rm -f *.o
	-rm t/tests poker

.PHONY: run clean diff
