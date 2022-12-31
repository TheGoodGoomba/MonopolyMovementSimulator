# Monopoly Movement Simulator
A quick and dirty program that simulates a single player going around a Monopoly board to see what spaces are landed on most frequently.

## How to use
After specifying the number of games to simulate and rolls of the dice per game (average is 30 per player apparently), wait for "Press enter..." to appear. The results are presented in order of most common (usually 'In Jail') to least common. For each space, it will show the number of times each were landed on over all the tests and the percentage chance of ending a turn on that space (e.g. you will see you have a 0% chance of ending a turn on 'Go to Jail' for obvious reasons).

## What this takes into account
I tried to make this as accurate as possible. So, landing on a Chance of Community Chest space draws a card and moves you if the card says to do so. There are 16 Chance and 16 Community Chest cards. As of the 2018 standard UK Monopoly board, 10 Chance cards and 2 Community Chest cards move the player:
- Chance
  - Go directly to Jail.
  - Advance to GO.
  - Go back three spaces.
  - Advance to the next station.
  - Advance to the nearest station.
  - Advance to the nearest utility.
  - Take a trip to King's Cross Station.
  - Advance to Pall Mall.
  - Advance to Trafalgar Square.
  - Advance to Mayfair.
- Community Chest
  - Go directly to Jail.
  - Advance to GO.

At the start of each game, the decks are shuffled. After a card has been drawn, it is returned to the bottom of the deck. This means the decks are never shuffled again during the game.

This also takes into account being sent to Jail upon rolling three doubles in a row, and assumes no speed die. I haven't (yet) made it consider holding a Get Out of Jail Free card until used to get out of Jail, which should in theory change the results ever so slightly.

### Nearest v Next
In the 2018 edition of Monopoly there are two Chance cards that direct you to a non-descript station. They are worded exactly the same except one says 'the next station' and the other says 'the nearest station'. I found some people online reasoning that the word 'nearest' implies that if the station behind you is closer you should make a full revolution to it (passing GO in the process) or just move back to it. I initially thought there was meant to be a difference in the cards due to the difference in wording. However, I checked the cards in my 2008 edition of the game and they both say 'nearest' station. Both editions' also have cards that say to advance to the 'nearest' utility. For these reasons, in this simulation both the 'nearest' and 'next' stations refer to the station you next encounter moving in the direction of the arrow on GO.

## My results
![image](https://user-images.githubusercontent.com/67541077/210126620-a88cb6ce-d480-4e11-9a1f-4cd37317f13f.png)

This took about 20 minutes on my poor machine lol.

This was 10 million games with 30 moves each, but I got the exact same rankings with 1 million games and significantly less time.

## Differences with others' findings
I've compared my results with a few others'. Namely, the top-ten landed on properties listed in Gyles Brandreth's book *The Monopoly Omnibus* (p. 109), the results from Hannah Fry's and Thomas Oléron Evans' book *The Indisputable Existence of Santa Claus* and Matt Parker's results in his video [The Mathematics of Winning Monopoly](https://youtu.be/ubQXz5RBBtU). They are each different in slightly different ways, but I have a few theories as to why.

Brandreth's book was published in 1985 and so it was based on an older edition of Monopoly. I know there are different Chance and Community Chest cards in whatever version he has, because he makes reference to cards such as 'Go back to Old Kent Road' and 'Take a trip to Marylebone Station' which don't appear in my edition of the game. I think this is enough to change the results how they were. Interestingly, his results say that two light blue properties are in the top 10 but I found they are all average at best.

Matt Parker published his python code which ran simulations similar to mine, but I noticed a few discrepancies in his code, which may be mistakes or differing Monopoly editions. First, his code shuffles the decks when they deplete, but they should just repeat. I also noticed that his code and results suggest that he had the locations of the third Chance and Community Chest spaces flipped. What I'm fairly sure isn't a mistake, however, is that in the code he only had one Chance card that takes you to the next station, which may be another edition difference.
