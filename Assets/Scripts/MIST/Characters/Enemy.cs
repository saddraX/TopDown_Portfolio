using UnityEngine;

namespace MIST.Characters
{
    public class Enemy : Character
    {
        [Header("Enemy State")]
        public EnemyType type = EnemyType.None; // enemy type (for now it's Melee or Range)

        /// <summary>
        /// Handle death and invoke EnemyDeath event
        /// </summary>
        protected override void Death()
        {
            base.Death();
            MIST.Events.EventManager.EnemyDeath(this);
        }

        /// <summary>
        /// Reset enemy parameters
        /// </summary>
        public override void ResetParameters()
        {
            base.ResetParameters();
            type = equipmentController.IsWeaponMelee() ? EnemyType.Melee : EnemyType.Range; // Set enemy type
        }
    }
}

