using System;
using Experiences;
using Misc;
using UnityEngine;

namespace Managers
{
    public static class EventManager
    {
        // Event triggered when the player's state changes
        public static Action<PlayerState> OnPlayerStateChanged;
        
        // Event triggered when the game state changes
        public static Action<GameState> OnGameStateChanged;
        
        // Event triggered for UI updates
        public static Action<int> OnExperienceChanged;
        public static Action<int> OnMaxExperienceChanged;
        
        // Event triggered for enemy actions
        public static Action<Vector3> OnEnemyDied;
        
        //Event triggered for experience Colledted
        public static Action<Experience> OnExperienceCollected;
    }
}