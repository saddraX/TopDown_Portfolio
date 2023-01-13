using System.Collections;
using UnityEngine;

using MIST.UI;

namespace MIST.IngameObject
{
    public abstract class Hittable : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] protected HealthBar healthBar;         // Health bar above the hittable object
        [SerializeField] protected int healthPoints = 100;      // Current hp
        [SerializeField] protected int maxHealthPoints = 100;   // Max hp

        [Header("Death Parameters")]
        [SerializeField] protected bool destroyOnDeath = false; // true - call Destroy() | false - deactivate gameObject
        [SerializeField] protected float deathDelayTime = 0f;   // Used to finish some death actions before destroy/deactivate
                                                                // (for example displaying death animation)

        #region damage_&_death
        /// <summary>
        /// Handle taking damage
        /// </summary>
        /// <param name="damageTaken"></param>
        public virtual void TakeDamage(int damageTaken)
        {
            healthPoints -= damageTaken; // decrease health points

            UpdateHealthBar(); // Update health bar after health points change

            //Debug.Log($"{name} has taken {damageTaken} damage. Health: {healthPoints}");

            // Checking if the hittable should perform death
            if (healthPoints <= 0)
                StartCoroutine(WaitAndPerformDeath(deathDelayTime));
        }

        /// <summary>
        /// Delay death
        /// </summary>
        /// <param name="deathDelayTime"></param>
        /// <returns></returns>
        protected virtual IEnumerator WaitAndPerformDeath(float deathDelayTime)
        {
            yield return new WaitForSeconds(deathDelayTime);
            Death();
        }

        /// <summary>
        /// Handle death
        /// </summary>
        protected virtual void Death()
        {
            // Checking if hittable should be destroyed or disabled
            if (destroyOnDeath)
                Destroy(transform.parent.gameObject, deathDelayTime);
            else
                gameObject.SetActive(false);

            ResetParameters(); // Reset all parameters after death
        }
        #endregion

        #region healthbar
        /// <summary>
        /// Update the health bar scale
        /// </summary>
        protected void UpdateHealthBar() => healthBar.UpdateBarScale(healthPoints, maxHealthPoints);

        /// <summary>
        /// Reset the health bar scale
        /// </summary>
        public virtual void ResetHealthBar() => healthBar.ResetHealthBarScale();
        #endregion

        #region parameters
        /// <summary>
        /// Reset all parameters
        /// </summary>
        public virtual void ResetParameters() => healthPoints = maxHealthPoints;
        #endregion
    }
}