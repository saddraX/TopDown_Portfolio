using UnityEngine;

namespace MIST.UI
{
    public class HealthBar : MonoBehaviour
    {
        [Header("Health Bar Transform")]
        public RectTransform healthBarTransform;

        [Header("Position")]
        [SerializeField] Vector3 positionOffset = new Vector3(0, 1, 0);
        [SerializeField] Transform transformToFollow;

        [Header("Behaviour")]
        [SerializeField] bool followEveryFrame = true;

        #region position
        void Start() => UpdatePosition();

        void Update()
        {
            if (followEveryFrame)
                UpdatePosition();
        }

        /// <summary>
        /// Update health bar position
        /// </summary>
        void UpdatePosition() => transform.position = transformToFollow.position + positionOffset;
        #endregion

        #region scale
        /// <summary>
        /// Update health bar scale
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="maxHp"></param>
        public void UpdateBarScale(int hp, int maxHp)
        {
            var hbScale = (float)hp / (float)maxHp;
            healthBarTransform.localScale = new Vector2(hbScale, 1f);
        }

        /// <summary>
        /// Reset health bar scale
        /// </summary>
        public void ResetHealthBarScale() => healthBarTransform.localScale = Vector3.one;
        #endregion
    }
}