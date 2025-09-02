# Unity Survival Game

A 2D survival roguelike game built with Unity, featuring enemy waves, weapon collection, experience progression, and an economy system.

***Note:** This project is only an internship project prepared in a few days. It is not a completed game. It may contain errors and performance issues.*

## 🎮 Game Overview

This is a survival-style game where players fight against waves of enemies, collect experience points to level up, gather weapons and upgrades, and manage resources through an integrated economy system. The game features multiple weapon types, collectible items, and a progressive difficulty system.

## ✨ Key Features

### Core Gameplay
- **Wave-based Enemy Combat**: Fight against continuous waves of enemies with AI pathfinding
- **Multiple Weapon Systems**: 
  - Axe (Melee area-of-effect weapon)
  - Magic Gun (Projectile-based ranged weapon)
  - Sword Gun (Throwable weapon system)
- **Experience & Leveling**: Gain experience from defeated enemies and level up to unlock new abilities
- **Roguelike Progression**: Prize system with random weapon and upgrade selection on level up

### Game Systems
- **Object Pooling**: Optimized performance with pooled enemies, projectiles, and experience orbs
- **Economy System**: Collect gold from treasures and spend on permanent upgrades
- **Health & Armor**: Player health system with upgradeable armor protection
- **Collectibles**: Health potions and treasure chests scattered throughout the game world
- **Pause System**: Full game pause functionality with UI management

### Technical Features
- **Event-Driven Architecture**: Comprehensive EventManager for decoupled system communication
- **State Management**: Separate state controllers for player, game, and UI states
- **ScriptableObject Configuration**: Data-driven design for weapons, prizes, and game balance
- **Input System**: Modern Unity Input System integration
- **Animation Integration**: Animator-based character and weapon animations

## 🎯 Core Systems

### Enemy System
- **BaseEnemy**: Abstract base class for all enemy types
- **AI Navigation**: NavMesh-based pathfinding to chase player
- **Health System**: Integrated health with UI feedback
- **Animation Controller**: State-based animation management

### Weapon System
- **BaseGun**: Abstract weapon base with cooldown and damage systems
- **Melee Weapons**: Area-of-effect damage (Axe)
- **Ranged Weapons**: Projectile-based combat (Magic Gun)
- **Special Weapons**: Physics-based throwable weapons (Sword Gun)

### Progression System
- **Experience Collection**: Visual experience orbs with smooth UI animation
- **Level Progression**: Curve-based experience requirements
- **Prize Selection**: Random weapon and upgrade selection on level up
- **Permanent Upgrades**: Economy-based armor and stat improvements

### Economy System
- **Gold Collection**: Treasure-based currency system
- **Persistent Economy**: Save/load total gold across game sessions
- **Upgrade Shop**: Spend gold on permanent character improvements

## 🚀 Getting Started

### Prerequisites
- Unity 2021.3 LTS or higher
- Unity Input System package
- DOTween (for UI animations)
- NavMesh Components

### Setup Instructions
1. Clone or download the project
2. Open in Unity 2021.3 LTS or higher
3. Ensure required packages are installed:
   - Unity Input System
   - DOTween
   - NavMesh Components
4. Open the main scene and press Play

## 🔧 Technical Implementation

### Performance Optimizations
- Object pooling for frequently spawned objects (enemies, projectiles, experience orbs)
- Event-driven architecture to minimize dependencies
- Efficient collision detection with interface-based damage systems

### Design Patterns Used
- **Singleton Pattern**: Core managers (GameManager, EconomyManager, ExperienceManager)
- **Observer Pattern**: EventManager for system communication
- **Object Pool Pattern**: Performance optimization for spawned objects
- **Strategy Pattern**: Different weapon behaviors through inheritance

### Data Management
- ScriptableObjects for weapon stats and prize configurations
- PlayerPrefs for persistent data (gold, armor upgrades)
- Event-based UI updates for real-time feedback

## 🎮 Gameplay Systems

### Combat Mechanics
- Automatic weapon firing with individual cooldowns
- Multiple weapons can be active simultaneously
- Damage scaling and weapon upgrade system
- Enemy AI with collision-based attack triggers

### Progression Mechanics
- Experience curve-based leveling
- Random prize selection system
- Weapon collection and improvement
- Permanent upgrade shop

## 🔄 Future Enhancement Opportunities

- Additional enemy types and behaviors
- More weapon varieties and combinations
- Environmental hazards and interactive elements
- Achievement and progression tracking
- Audio and visual effect improvements
- Mobile platform optimization

---
