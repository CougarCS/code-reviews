CPPFLAGS:= -DDEBUG
OBJECTS:= util.o
run: poker
	./poker data.txt

poker: poker.c $(OBJECTS)
	$(LINK.c) -o $@ $^

test: t/tests
	prove -v -e '' $^

t/tests: tests.c $(OBJECTS)
	-@mkdir t 2>&1 > /dev/null
	$(LINK.c) -o $@ $^

clean:
	-rm -f *.o
	-rm t/tests poker

.PHONY: run clean
