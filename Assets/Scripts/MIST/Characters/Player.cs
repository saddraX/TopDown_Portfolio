namespace MIST.Characters
{
    public class Player : Character
    {
        /// <summary>
        /// Handle death and invoke PlayerDeath event
        /// </summary>
        protected override void Death()
        {
            base.Death();
            MIST.Events.EventManager.PlayerDeath(this);
        }
    }
}