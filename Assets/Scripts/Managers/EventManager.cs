using System;
using Abstract;
using Ammo;
using Experiences;
using Misc;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public static class EventManager
    {
        // Event triggered when the player's state changes
        public static Action<PlayerState> OnPlayerStateChanged;
        
        // Event triggered when the game state changes
        public static Action<GameState> OnGameStateChanged;
        
        // Event triggered for experience updates
        public static Action<Experience> OnExperienceCollected;
        public static Action<int> OnMaxExperienceChanged;
        public static Action<int> OnExperienceChanged;
        
        // Event triggered for enemy actions
        public static Action<Vector3> OnEnemyDiePosition;
        public static Action<BaseEnemy> OnEnemyDied;
        
        // Event triggered for ammo actions
        public static Action<MagicBall> OnMagicBallAchieve;

        // Event triggered for InGameShop and prize actions.
        public static Action OnCurrentLevelChanged;
        public static Action<PrizeDataSO> OnPrizeCollected;
        public static Action<PrizeDataSO> OnPrizeShowed;
    }
}