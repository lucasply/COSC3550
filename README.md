# COSC3550
Repository holding our submission for our final project for COSC3550. For our game we used unity version 2022.3.56f1. 

Features of our game:
- Player movement left and right, player jumping, player climbing up and down non-broken ladders
- Player cannot move through platforms, white border walls
- Barrel movement, including having a random chance of falling down both broken and non-broken ladders
- Sound effects and background music
- A start screen with a level select
- Upon loss, an end screen with the option to quit or try again. Upon victory, an end screen with your score and the option to quit or play again
- PlayerPrefs that retain your best score (in terms of stars)
- When the player runs into a barrel, the player dies and respawns. If all three lives are used then the game is over
- Lives displayed in the top corner (game designed for exclusively Full HD)
- Game is won when the player reaches Iggy's platform at the top of the screen
- Barrels are destroyed upon reaching the fire can at the bottom left of the level

KEY DIFFERENCES between our game and the original Nintendo Donkey Kong arcade game:
- we only created one level instead of four, based on the first level of the original game. In our project outline we said we'd create 4 levels but underestimated the time it took to make the one
- our game is Marquette themed (our player is blue and gold instead of Mario's colors, instead of Pauline we have Iggy)
- we did **not** implement the hammer
- we did **not** implement scoring by jumping over barrels and destorying barrels. Instead, your final score is out of three stars, and based on how many lives you had remaining upon winning the game
- we did **not** implement the fireball from the original game

Something to note: Unity and Github are tempermental (metadata hell), don't merge your work if someone is working off of the main and their work takes longer than yours. Just note on how you can redo what
you've done and accept that you'll have to redo it. 

Steps to get the game on your unity hub:
1: Clone the directory on your github desktop, https://github.com/lucasply/COSC3550.git
2: Make your branch, give it a cool useful name
3: Move to unity hub and click 'add', select from disk, then navigate to your github directory and find this repo and select the 'Kong Donkey' folder
4: Launch the project and verify everything installed correctly
5: Fin
PS: Make sure you launch the scene you want to work on before making any edits, launching the game from the github repo doesn't automatically put you in a scene, this makes it look like there is nothing on the game.
