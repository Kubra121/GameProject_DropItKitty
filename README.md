# 🎮 DROP IT, KITTY!

## 1. STATEMENT OF PURPOSE

The purpose of creating this project is to learn how to use the Unity game engine and make a fun 2D game from scratch.

## 2. TOOLS

- **Game Engine:** Unity  
- **Programming Language:** C#  
- **Design:** Canva (used for creating UI elements like text and boxes)  
- **Game Assets:**  
  - [itch.io](https://itch.io)  
  - [opengameart.org](https://opengameart.org)  
  - [craftpix.net](https://craftpix.net)  
  - [freesound.org](https://freesound.org)  
  - Pinterest  

- **Tutorial Resources – YouTube:**
  - [Brackeys - Scene Transitions](https://www.youtube.com/watch?v=CE9VOZivb3I&t=528s)
  - [Dani Krossing - Change Scene with Button](https://www.youtube.com/watch?v=jrPTpD2eAMw)
  - [Mr. Adhikari - Mario in Unity 6 (2025)](https://www.youtube.com/watch?v=GJSVGFiEtX8)
  - [bendux - Smooth Camera Follow](https://www.youtube.com/watch?v=ZBj3LBA2vUY)
  - Projects done in class

## 3. ALGORITHM

- The game starts in the **PlayingScene** with a cat, a bird, and various obstacles.
- The player controls the cat using arrow keys and tries to catch the bird.
- The player earns points by knocking objects like glasses and bottles to the ground.
- Dropping a **candle** results in:
  - Score decrease  
  - Heart loss  
  - Temporary speed reduction
- Collecting a **health potion** restores hearts.
- The game ends when:
  - The cat **catches the bird**  
  - The cat **loses all its hearts**
- The bird reacts dynamically to escape when the cat gets too close.

## 4. IMPLEMENTATION

### Game Scenes:
- `StartScene`
- `PlayingScene`
- `WinningScene`
- `LosingScene`
- `InstructionsScene`

### Scene Transitions:
- Handled by scripts like `UIStartingScene`, `UIInstructionsScene`, `UIQuitScene`
- Central controller: `ScenesManager`

### Mechanics:
- Falling objects managed by `FallDetector`
- Objects trigger different effects based on tags
- `ObstacleSpawnerController` dynamically spawns objects
- `CameraFollowController` makes camera follow the cat only when moving
- `ScoreData` script saves the final score for `WinningScene`

## 5. REFINEMENT

- Bird AI improved to escape the cat when too close
- Cat's leftward movement restricted to preserve layout
- Heart display changed from animated sprite to **Image + Text** UI
- Cat animation delays fixed by:
  - Setting transition duration to `0.05`
  - Disabling `Has Exit Time` in transitions

## 6. SCREENSHOTS

> (You can drag screenshots into this file on GitHub to embed them here.)

---

**👩‍💻 Developer:** Kübra Özalp  
**🕹️ Project Year:** 2025
