using UnityEngine;
using TMPro;

using MIST.Characters;
using MIST.Events;
using MIST.Managers;

namespace MIST.UI
{
    public class UIGameStatsController : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI enemyCounterText;
        [SerializeField] private TextMeshProUGUI lastEnemyKilledText;

        private void Awake() => EventManager.OnEnemyDeath += UpdateEnemyStatsAndTexts;

        private void OnDestroy() => EventManager.OnEnemyDeath -= UpdateEnemyStatsAndTexts;

        private void UpdateEnemyStatsAndTexts(Enemy enemy)
        {
            GameManager.Instance.EnemiesKilled++; // Increase EnemiesKilled value

            // Update texts
            enemyCounterText.text = $"Enemies killed: {GameManager.Instance.EnemiesKilled}";
            lastEnemyKilledText.text = $"Last killed: {enemy.name}";
        }
    }
}