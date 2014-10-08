# Word Chains

Write a program that solves word-chain puzzles.

## Description

There's a type of puzzle where the challenge is to build a chain of words,
starting with one particular word and ending with another. Successive entries
in the chain must all be real words, and each can differ from the previous word
by just one letter. For example, you can get from "cat" to "dog" using the
following chain.

1. cat
2. cot
3. cog
4. dog

The objective of this problem is to write a program that accepts start and end
words and, using words from the dictionary, builds a word chain between them.
For added programming fun, return the shortest word chain that solves each
puzzle. For example, you can turn "lead" into "gold" in four steps (lead, load,
goad, gold), and "ruby" into "code" in six steps (ruby, rubs, robs, rods, rode,
code).

Once your code works, try timing it. Does it take less than a second for the
above examples given a decent-sized word list? And is the timing the same
forwards and backwards (so "lead" into "gold" takes the same time as "gold"
into "lead")?

## Wordlist

An example wordlist that you can use is from SIL here:
<http://www-01.sil.org/linguistics/wordlists/english/>.

## Extra credit

Mark the position of the change you made at each step by marking it with square
brackets, e.g. cat:c[o]t:co[g]:[d]og.

Note: Description taken from Code Kata website [^ref 1].

## References

1. Dave Thomas. "Kata19: Word Chains". <http://codekata.com/kata/kata19-word-chains/>.
2. "Word ladder". <http://en.wikipedia.org/wiki/Word_ladder>. *Wikipedia*.
3. Martin Gardner. "Word Ladders: Lewis Carroll's Doublets".
   <http://www.jstor.org/stable/3620349>. *The Mathematical Gazette*.

