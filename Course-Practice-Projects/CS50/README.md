# CS50 Introduction to Game Development Course Projects

This repository contains the projects I completed while taking the "CS50 Introduction to Game Development" course, along with the changes I made to them.

## Projects

### 1. Pong
* **Description**: A recreation of the classic *Pong* game.
* **Changes Made**:
  - Implemented an AI-controlled paddle for Player 2 (right paddle).
  - The AI paddle tracks the Y position of the ball and moves up or down accordingly.
  - Added logic to make the AI movement more balanced, preventing it from reacting too quickly (to make the game more challenging).
  - The AI logic was placed in the `love.update` function, where the paddle movement is controlled based on the ball's Y-axis position.
  - Added a difficulty modifier by introducing a reaction delay to the AI, making it more competitive.

---

### 2. Flappy Bird (Clone of Flappy Bird Game)
* **Description**: A Clone of *Flappy Bird* with similar gameplay.
* **Changes Made**:
  - Randomized the gap between pipes, making it vary for each pair of pipes rather than a fixed 90 pixels.
  - Randomized the interval between pipe spawns, so pipes now appear at random intervals instead of always 2 seconds apart.
  - Implemented a medal system in the `ScoreState`. The player earns one of three different medals based on their score:
    - Bronze Medal for scores between 5 and 9.
    - Silver Medal for scores between 10 and 19.
    - Gold Medal for scores of 20 and above.
  - Added a pause feature triggered by pressing the "P" key. When paused:
    - A pause icon is displayed in the center of the screen.
    - Game music pauses and resumes when unpaused.
    - A custom pause sound effect plays when entering and exiting pause mode.

---

### 3. Breakout
* **Description**: A clone of the classic *Breakout* game.
* **Changes Made**:
  - Added a `Powerup` class that spawns powerups randomly, which descend toward the player. Upon collecting a specific powerup, two extra Balls spawn and behave identically to the original.
  - Implemented paddle growth and shrinkage:
    - The Paddle shrinks when the player loses a life (up to a minimum size).
    - The Paddle grows when the player scores a certain number of points (up to a maximum size).
  - Added locked Bricks that can only be broken by collecting a key powerup:
    - The key powerup spawns randomly like the ball powerup and allows the player to unlock and break the locked Bricks.
    - Locked Bricks are worth more points to reward players for unlocking them.
  - Updated the `LevelMaker` class to occasionally spawn locked Bricks during level generation, adding variety and challenge.

---

### 4. Match 3
* **Description**: A game similar to *Candy Crush* with match-3 mechanics.
* **Changes Made**:
  - Implemented a time addition mechanic:
    - Matching tiles adds 1 second per tile in a match to the game timer.
  - Updated the level progression:
    - Level 1 starts with simple flat blocks (the first of each color in the sprite sheet).
    - Later levels generate blocks with patterns (e.g., triangles, crosses) that are worth more points.
  - Added shiny versions of blocks:
    - Random shiny blocks destroy an entire row when matched, awarding points for each block in the row.
  - Enhanced swapping logic:
    - Swaps are only allowed if they result in a match. If no matches are available, the board is reset.
  - **Optional Feature**: Implemented mouse-based matching:
    - Matching can be performed using the mouse, allowing for click-based or drag-based interactions for swapping tiles.

---

### 5. Mario50.
* **Description**: A platformer game inspired by *Super Mario Bros.*
* **Changes Made**:
  - Ensured the player is always dropped above solid ground when entering a level:
    - When generating levels in `LevelMaker.lua`, checked the map column by column to ensure the player starts above a solid tile (using `TILE_ID_GROUND` as reference).
  - Added key and lock blocks:
    - Implemented random key and lock block generation in `LevelMaker.lua` using `keys_and_locks.png`.
    - The key unlocks the lock block when the player collides with it, causing the lock block to disappear.
  - Introduced a goal post that spawns after the lock is unlocked:
    - Spawned the goal post using `flags.png`, with a separate GameObject for the flag and pole, making them appear as one.
    - The flag pole appears at the end of the level once the lock block disappears.
  - Enhanced level progression:
    - Upon touching the goal post, the level regenerates with a longer map and the player is respawned at the beginning.
    - Modified `PlayState:enter` to keep track of the current level and persist the player’s score, ensuring that each new level is slightly longer than the last.

---

### 6. Zelda
* **Description**: A game inspired by *The Legend of Zelda* series.
* **Changes Made**:
  - Implemented hearts that drop from defeated enemies:
    - Hearts randomly spawn from vanquished enemies and can be picked up by the player.
    - Picking up a heart heals the player for a full heart (2 health points), with a maximum visual cap of 6 hearts.
    - Defined a GameObject with an `onConsume` callback for the heart pickup mechanism, reusing code from the Super Mario Bros. implementation.
  - Added pots to the game world:
    - Randomly generated pots that the player can pick up, changing the player's animation to reflect carrying the pot (from character sprite sheets).
    - The player cannot swing their sword while carrying a pot.
  - Implemented pot throwing mechanics:
    - When the player throws a pot, it travels in a straight line based on the player's direction.
    - The pot disappears upon colliding with a wall, traveling more than four tiles, or hitting an enemy.
    - If the pot collides with an enemy, it deals 1 point of damage.
    - Incorporated new states for the player to manage carrying and throwing pots, ensuring the pot tracks the player’s position above their head.

---

### 7. Angry 50
* **Description**: A physics-based game inspired by *Angry Birds*.
* **Changes Made**:
  - Implemented Alien splitting mechanics:
    - When the player presses the space bar after launching an Alien (before it collides with anything), the Alien splits into three smaller Aliens.
    - The split Aliens behave identically to the original Alien.
  - Modified `AlienLaunchMarker`:
    - Code for launching the Alien is primarily managed within the `AlienLaunchMarker` class.
    - Introduced a collision flag in the Level to track whether the launched Alien has collided with anything, allowing the split action only if the Alien is still in motion.
  - Spawn logic for split Aliens:
    - Two new Aliens are spawned above and below the original Alien when it splits, maintaining the appropriate angle and velocity.
    - The split uses the original Alien's linear velocity to ensure the new Aliens are launched correctly.
  - Launch marker behavior:
    - The launch marker does not reset until all spawned Aliens have come to a near stop, ensuring consistent gameplay experience.

---

### 8. 50-mon
* **Description**: An RPG-style game inspired by the Pokémon series.
* **Changes Made**:
  - Implemented a Level Up Menu:
    - A menu appears during a Pokémon's level-up process, displaying stat increases in the format: `X + Y = Z`.
      - **X**: Starting stat.
      - **Y**: Amount increased for the current level.
      - **Z**: Resultant sum after the increase.
    - This menu is triggered immediately after the "Level Up" dialogue at the end of a victory where the Pokémon has leveled up.
  - Modified `TakeTurnState`:
    - Updated the `:victory()` function to detect level-ups and push an additional Menu onto the StateStack after the BattleMessageState.
  - Customized the Selection Class:
    - Adjusted the Selection class to allow toggling the cursor visibility via a boolean parameter.
      - Default cursor behavior remains true to preserve the existing functionality in BattleMenuState.
  - Utilized `:levelUp()` Function:
    - The `:levelUp()` function in the Pokémon class is used to retrieve all necessary stat increases for displaying the Level Up Menu accurately.
  - Overall, ensured proper integration with Menu, Selection, and StateStack classes to maintain smooth gameplay experience.

---

### 9. Helicopter 3D
* **Description**: A 3D helicopter game where players collect coins and avoid obstacles.
* **Changes Made**:
  - **Added Gems**:
    - Implemented Gems that spawn similarly to Coins but are more rare.
    - Each Gem collected is worth **5 coins**.
    - Gems despawn when they move off the left edge of the screen.
    - Created new `Gem` and `GemSpawner` classes to manage the behavior of Gems.
      - Used a model from the Proto resource pack located in the Assets folder for the Gems, but custom models can also be imported.
    - Configured the Gem prefab to attach to the `GemSpawner` component, ensuring it follows the same movement and despawning behavior as Coins.
  
  - **Bug Fix**:
    - Resolved the issue where the scroll speed of planes, coins, and buildings didn’t reset when the game was restarted via the space bar.
    - Identified that static variables do not reset upon loading a scene, particularly focusing on the `SkyscraperSpawner`.
    - Located the game reset logic and ensured it properly resets the speed for all objects when the game restarts, preventing speed carryover from previous runs.

* **Additional Notes**:
  - Experimented with the Unity interface to learn more about asset management and prefab configurations.

---

### 10. Dreadhalls
* **Description**: A maze exploration game where players navigate through levels and avoid hazards.
* **Changes Made**:
  - **Spawn Holes in the Maze**:
    - Implemented functionality to spawn holes in the floor of the maze.
    - Limited the number of holes to **three or four** per maze, depending on the maze size.
    - Modified the `LevelGenerator` script to instantiate these holes at appropriate locations using existing block instantiation logic.

  - **Game Over Transition**:
    - Created a separate "Game Over" scene that resembles the Title Screen.
    - Implemented logic to transition to the "Game Over" screen when the player falls through a hole.
      - Developed a `DespawnOnHeight` MonoBehaviour to check the player's height relative to the maze ceiling.
      - Utilized Unity's coordinate system to determine if the player has fallen below the acceptable threshold.
    - Added functionality to return to the Title Screen when the player presses **Enter** in the "Game Over" scene.
      - Ensured to handle the audio source (`WhisperSource`) to prevent music overlap between scenes.

  - **Maze Tracking Text**:
    - Added a Text label in the Play scene to display the current maze number.
    - Implemented a static variable to track the maze count, which increments each time the player progresses to a new maze.
    - Ensured that this variable resets to **0** upon reaching a Game Over state.
      - Stored the maze count in a suitable place within the game's state management system.

* **Additional Notes**:
  - Practiced scene management and interactions within Unity.
  - Tested with the transitions and variable resets thoroughly to ensure a smooth gameplay experience.

---


### 11. Portal
* **Description**: A level created using Unity's ProBuilder and ProGrids tools, featuring navigation with an FPSController and an end-level trigger.

* **Changes Made**:
  - **Level Creation**:
    - Designed a new level in a separate scene using **ProBuilder** and **ProGrids**.
    - Leveraged ProBuilder for creating and shaping the environment, ensuring the level is visually appealing and functional.

  - **FPS Controller**:
    - Integrated an **FPSController** from the Standard Assets for player navigation.
    - Ensured the controller allows smooth movement and interaction within the newly created level.

  - **End-Level Trigger**:
    - Created an empty GameObject at the end of the level to serve as a trigger.
    - Added a **BoxCollider** component to the GameObject, ensuring it is set to **Is Trigger** in the inspector for proper functionality.
    - Resized the BoxCollider appropriately to cover the intended end zone.

  - **Victory Display**:
    - Added a Text object to the scene to display “You Won!” upon level completion.
    - Implemented a script utilizing the `OnTriggerEnter` function to detect when the player collides with the trigger zone.
    - Programmed the logic to toggle the visibility of the victory Text object when the player enters the trigger zone, using a structure similar to the **GameOverText** script from the Helicopter Game 3D project.

* **Additional Notes**:
  - Understood the importance of Unity's new tools, which facilitate level design and enhance the overall workflow.
  - Tested the level thoroughly for smooth navigation and proper trigger functionality.

---

Each project demonstrates the key elements learned in game development, including game mechanics, design, and implementation.
