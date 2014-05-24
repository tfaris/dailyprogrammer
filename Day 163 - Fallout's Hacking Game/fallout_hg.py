"""
A solution to reddit's /r/dailyprogrammer challenge #163 found here:
http://www.reddit.com/r/dailyprogrammer/comments/263dp1/5212014_challenge_163_intermediate_fallouts/

Author: Tom Faris <ta.faris@gmail.com>
"""
import random
# Number of attempts user gets before failing
ATTEMPTS = 4
all_words = [w.rstrip('\n') for w in open('enable1_sorted.txt').readlines()]
# A dict mapping each available word length to the last index of a word
# that length. Assumme all words between an index and the one before it
# are the same length (file is sorted).
length_indices = list({len(all_words[i]):i
                       for i in range(0, len(all_words))}.iteritems())
# Number of words to guess from is based on a chosen difficulty
difficulty_map = {'1': 5, '2': 8, '3': 10, '4': 12, '5': 15}
word_count = difficulty_map.get(raw_input("Difficulty (1-5)? "), 5)
# Get a random word length from the list of available lengths
word_choice_index = random.randint(0, len(length_indices)-1)
# Get all the words we can use for the selected length
word_choices = all_words[(length_indices[word_choice_index-1][1]+1) if word_choice_index > 0 else 0:length_indices[word_choice_index][1]+1]
# And choose a random index into the possible list, making sure we can
# still grab word_count number of words
choice_start = random.randint(0, len(word_choices) - 1 - word_count)
words = word_choices[choice_start : choice_start + word_count]
# Choose a random word as the password
password = random.choice(words)
# Print out all of the words we can guess
for i in range(0, len(words)):
    print "%s: %s" % (i+1, words[i])
# Let the user make their guesses
success = False
for i in range(0, ATTEMPTS):
    if i == ATTEMPTS - 1:
        print "1 ATTEMPT LEFT. LOCKOUT IMMINENT!"
    guess = None
    while not isinstance(guess, int):
        # Use index into the list as input for convenience
        try:
            guess = int(raw_input("Guess [1-%s](%s left)? " % (len(words), ATTEMPTS - i))) - 1
            if guess >= len(words) or guess < 0:
                raise ValueError
        except ValueError:
            guess = None
            print "Invalid choice. Cannot compute."
    guessed_word = words[guess]
    if guessed_word == password:
        # winner        
        success = True
        break
    else:
        # Count the number of characters that are at the same index in
        # either word.
        correct_chars = [guessed_word[i] == password[i] 
                         for i in range(0, len(guessed_word))].count(True)
        print "%s / %s correct" % (correct_chars, len(guessed_word))
        
print 'You win!' if success else 'You lose!'