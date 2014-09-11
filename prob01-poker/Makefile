OBJECTS:= util.o
run: poker
	./poker data.txt

poker: poker.c $(OBJECTS)
	$(CC) -o $@ $^

test: t/tests
	prove -v -e '' $^

t/tests: tests.c $(OBJECTS)
	-@mkdir t 2>&1 > /dev/null
	$(CC) -o $@ $^

clean:
	-rm -f *.o
	-rm t/tests poker

.PHONY: run clean
