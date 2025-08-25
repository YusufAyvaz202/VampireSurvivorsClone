using System;
using Misc;

namespace Managers
{
    public static class EventManager
    {
        // Event triggered when the player's state changes
        public static Action<PlayerState> OnPlayerStateChanged;
        
        // Event triggered when the game state changes
        public static Action<GameState> OnGameStateChanged;
    }
}