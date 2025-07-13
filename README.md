DROP IT, KITTY!
1.STATEMENT OF PURPOSE
The purpose of creating this Project is to learn how to use Unity game engine and making a fun 2D game from scratch.
2.TOOLS

Game Engine: Unity,

Programming Language: C#,

Design: Canva (used for creating UI elements like text and boxes)

Game Assets: itch.com, opengameart.org, craftpix.net, freesound.org, pinterest

Resources – Tutorials - YouTube:
o Brackeys - How to make AWESOME Scene Transitions in Unity! https://www.youtube.com/watch?v=CE9VOZivb3I&t=528s
o Dani Krossing - CHANGE SCENE WITH BUTTON IN UNITY https://www.youtube.com/watch?v=jrPTpD2eAMw
o Mr.Adhikari - Mario in Unity 6 (2025) | Part 2: Adding Player Movement https://www.youtube.com/watch?v=GJSVGFiEtX8
o bendux - Smooth Camera Follow In Unity https://www.youtube.com/watch?v=ZBj3LBA2vUY
o Projects done in class
3.ALGORITHM
The game starts in the PlayingScene with a cat, a bird, and various obstacles. The player controls the cat using arrow keys and tries to catch the bird.
As the player moves to the right side, obstacles such as tables and shelves appear, with object like glasses and bottles placed on them. The player earns points by knocking these objects to the ground. However, if the cat drops a candle object, the score decreases, the cat loses a heart, and the speed of the cat decreases temporarily. This makes catching the bird even more challenging. But the good side is a heart can be regained by collecting health-potion bottle.
The game ends under one of the two conditions:

The cat catches the bird

The cat loses all its hearts
To increase difficulty of the game, the bird dynamically reacts by escaping when the cat comes too close.
4.IMPLEMENTATION
The game has five scenes as requested:

StartScene

PlayingScene

WinningScene

LosingScene

InstructionsScene
Kübra Özalp
When the game is first opened StartScene appears. It contains Play and Instructions buttons. Player can display the instructions to learn how to play the game or press Play button to begin the game.
In PlayingScene, the cat, the bird, and obstacles appear. UI elements such as score and lives are displayed at the top of the screen. The player quit anytime using the Quit button located on the upper right corner.
If the cat successfully catches the bird, the game transitions to the WinningScene, where the final score is shown, along with Play Again and Quit buttons.
If the cat runs out of hearts, the game transitions to the LosingScene, which also provides Play Again and Quit buttons.
Scene transitions are managed by scripts such as UIStartingScene, UIInstructionsScene, and UIQuitScene, coordinated via a central ScenesManager Game Object.
Falling objects are controlled by using the FallDetector script. Based on their tags, different outcomes -like score change or heart loss- are triggered upon impact with the ground.
The ObstacleSpawnerController dynamically spawns objects based on the camera’s position and destroys them once they exit the scene to optimize performance.
The CameraFollowController ensures that the camera follows the cat, allowing background and obstacles to move only when the cat moves. This prevent unwanted motion when the cat is idle.
For showing the score in the WinningScene, a script called ScoreData is created for storing the final score.
5.REFINEMENT
Initially, the bird only moved in a straight path and was moving when the cat moves, which make the game too easy and boring. To improve the game, an escape mechanism was added. When the cat gets too close to the bird, it detects the movement of the cat and escapes with a slight delay. This improvement made the game more difficult and longer to play.
Originally, the cat move left indefinitely. This was later restircted to a fixed boundary to maintain the scene layout.
At first, hearts were displayed using a sprite with four hearts, but it was inconsistently updated using animation. This was replaced with a reliable Image + Text UI solution.
There were also animation delays when controlling the cat. These were resolved by setting the transition suration to 0.05 and disabling Has Exit Time in the Animator transitions.
Kübra Özalp
6. SCREENSHOTS OF GAME
Kübra Özalp