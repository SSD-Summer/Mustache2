Mustache2
=========

To - Do's

 - Game Menu
  - Worlds/Endless
  - Leaderboards
  - Settings
 - Actual game play
  - World game loop
  - Endless game loop
  - Tile Board creation
  
 - Results
  - Link up with leaderboards
  -






Read-Me from original game

===================================================================================================================================
Mustache-ic
===========
TO DO
- Game Mechanic's
  - Impove randomization of hiding tiles -- Should be tweaked for a move even spread
  - Add in methods to make sure that there is a certian percentage of the correct tile on the board -- Specific random distribution
  - Add event for tile click - When click it should do the following
    - Fix when correct tile is hidden to make sure it only shows a mustache for 1-2 secs then hides and resets
    - When player reaches 0 lives make sure to kick to results screen
  - Improve current method for linking world/sub-world to its associated difficulty and tile-set(dino, fish,...)
- Game Play
  - Add additional worlds
  - Add sub-world buttons
    - Put a locking mechinisim on the sub-worlds until the previous sub-world/world has been completed
  - Work on Endless mode
    - Convey to user which tile is the correct
    - Method for incrementing the time for correct selection
    - Mechanic for raising difficulty over time
      - Raise tile count NxN -> (N+1)x(N+1) either based on total time played or total score
- Game Aesthetic's
 -  Boarder style for form?
 -  Boarder style for menu buttons
 -  background style for menu buttons
 -  Fix game label clipping when in a game
 -  Transparent background for all game tile image
 -  Add a check or some image to show the user which worlds/sub-worlds they have completed -- ????
 -  Game music??

- Leaderboard
  - Contact Joe Healy to possibly recieve an azure account
    - or
  - Configure a dropbox or some other solution to store a text file and be able to retrieved
    - Would maybe? have to implement a security solution
  - Settle on if we are keeping leaders for individual worlds/sub-worlds or just endless
  - Settle on if we are storing leader info in plain text or if we are hashing
================================================================================================================================
