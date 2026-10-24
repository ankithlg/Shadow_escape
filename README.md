# Shadow Escape

**Shadow Escape** is a 2D stealth game developed in **Unity using C#**. The player must navigate through each level, avoid different types of enemies, use special abilities strategically, and reach the exit while preserving their limited lives.

The project focuses on enemy behavior, reusable level management, player abilities, scene transitions, and a data-driven level system.


## 🎥 Gameplay Demo [youtube link]

https://youtu.be/sImQa6JkU9I



## 🎮 Gameplay

The objective is simple:

> **Avoid the enemies, use your powers wisely, and reach the exit.**

The player starts with **3 lives** and progresses through multiple levels. Contact with a dangerous enemy can kill the player and reduce the remaining lives.

If all lives are lost, the game ends. Completing all available levels results in a win.



## 🕹️ Controls

| Action | Control |
|---|---|
| Move | Arrow Keys / WASD |
| Invisibility | `X` |
| Freeze Enemies | `C` |

Movement is supported in all four directions.



## ⚡ Player Powers

The player has two special abilities that can be used to escape or avoid enemies.

### 👻 Invisibility

Press **`X`** to temporarily become invisible.

- Duration: **3 seconds**
- Cooldown: **10 seconds**
- Enemies cannot kill the player while invisibility is active
- The player becomes partially transparent while the ability is active

### ❄️ Freeze

Press **`C`** to freeze nearby enemies.

- Freeze radius: **3 units**
- Freeze duration: **3 seconds**
- Cooldown: **12 seconds**
- Enemies inside the freeze radius temporarily stop their behavior
- Frozen enemies are visually highlighted

The UI displays whether each power is **Ready**, **Active**, or currently on **Cooldown**.



## 👾 Enemy Types

Shadow Escape currently supports three enemy types.

### Patrol Enemy

Patrol enemies continuously move horizontally within a configured patrol distance.

Each patrol enemy can have its own patrol range, allowing different movement patterns to be configured for each level.

When frozen, the patrol enemy stops moving until the freeze effect ends.

### Attacker Enemy

The attacker reacts when the player enters its trigger area.

Once activated, it:

1. Detects the player
2. Begins chasing the player
3. Moves toward the player's current position
4. Stops chasing if the player activates invisibility

The attacker also stops moving while frozen.

### Guard Enemy

The guard uses a rotating detection area.

Its detection triangle rotates around the guard, creating an area the player must avoid while navigating the level.

The guard's rotation stops when the enemy is frozen.



## ❤️ Lives and Death System

The player begins the game with:

```text
3 Lives
```

When the player is caught by an enemy:

1. Player movement is disabled
2. The player's velocity is stopped
3. A short death animation is played
4. One life is removed
5. The result screen is displayed

If lives remain, the player can retry.

If no lives remain, the game displays **Game Over** and allows the player to start again.


### Level Loading Flow

```text
LevelData
    ↓
LevelManager
    ↓
Clear Previous Level
    ↓
Spawn Walls
    ↓
Spawn Enemies
    ↓
Spawn Player
    ↓
Spawn Exit
    ↓
Start Level
```

The same `GameScene` can therefore be reused for different levels.


## 🚪 Level Completion

Each level contains an exit zone.

When the player successfully reaches the exit:

```text
Player reaches Exit
        ↓
Level Complete
        ↓
Save current music position
        ↓
Result Screen
        ↓
Next Level
```

After completing the final available level, the player receives the **You Won!** result and can start the game again.


## 🔄 Game Flow

The overall game flow is:

```text
Intro Scene
     ↓
Power Instructions
     ↓
Enemy Instructions
     ↓
Game Scene
     ↓
Load LevelData
     ↓
Play Level
     ↓
 ┌───────────────┐
 │               │
Player Dies   Reaches Exit
 │               │
 ↓               ↓
Lose Life     Level Clear
 │               │
 ↓               ↓
Result Scene  Result Scene
 │               │
 ↓               ↓
Retry         Next Level
 │               │
 └──────→ Game Scene
```

If the player loses all lives:

```text
Game Over
   ↓
Play Again
   ↓
Reset Game
```



## 💡 Design Approach

The project was designed around reusable gameplay components instead of putting all game logic into a single script.

Enemy behaviors share common functionality through the `EnemyAI` base class, while individual enemy scripts implement their own movement behavior.

Levels are represented using `LevelData` ScriptableObjects, allowing the `LevelManager` to construct each level from configuration data.

Global information such as lives and the current level is maintained separately by `GameManager`, while UI and result information are handled by dedicated components.

This keeps player logic, enemy behavior, level management, UI, and persistent game state separated from one another.


## 🔮 Future Improvements

Possible additions to the project include:

- More levels and level environments
- Additional enemy types
- Improved enemy detection behavior
- Sound effects for abilities and enemies
- Improved animations
- Main menu settings
- Pause and resume functionality
- Difficulty modes
- Scoring or completion-time system
