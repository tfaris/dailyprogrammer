# A solution to reddit's /r/dailyprogrammer challenge #164 found here:
# http://www.reddit.com/r/dailyprogrammer/comments/26ijiu/5262014_challenge_164_easy_assemble_this_scheme/
#
# Author: Tom Faris <ta.faris@gmail.com>

# Task 1
puts "Task #1:"
puts "Hello World"

# Task 2
puts
puts "Task #2:"
i = 1
numbers = []
while numbers.length < 100
    if i % 3 == 0 and i % 5 == 0
        numbers.push(i)
    end
    i += 1
end
print numbers

puts puts
puts "Task #3"
# Task 3
def is_anagram(a, b)
    c = a.downcase.gsub(' ', '')
    d = b.downcase.gsub(' ', '')    
    for ch in c.split("")
        d.gsub!(ch, '')
    end
    return d.empty?
end
a = "Doctor Who"
b = "Torchwood"
puts "Is %s an anagram of %s? %s" % [a, b, is_anagram(a, b)]

puts 
puts "Task #4"
def remove_letter(word, letter)
    return word.gsub(letter, '')
end
puts remove_letter("Word!", 'r')

puts
puts "Task #5"
puts [1, 2, 3, 4, 5].inject(:+)