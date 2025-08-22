using System;
using Misc;

namespace Managers
{
    public static class EventManager
    {
        // Event triggered when the player's state changes
        public static Action<PlayerState> OnPlayerStateChange;
    }
}