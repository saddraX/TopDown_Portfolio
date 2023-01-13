using System;

using MIST.Characters;

namespace MIST.Events
{
    public static class EventManager
    {
        #region character_events
        public static event Action<Player> OnPlayerDeath;
        public static event Action<Enemy> OnEnemyDeath;

        public static void PlayerDeath(Player player) => OnPlayerDeath?.Invoke(player);
        public static void EnemyDeath(Enemy enemy) => OnEnemyDeath?.Invoke(enemy);
        #endregion
    }
}