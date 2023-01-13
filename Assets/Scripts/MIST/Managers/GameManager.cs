using UnityEngine;

namespace MIST.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public bool debugMode = false; // Enable/disable debug mode.

        // TODO: Create a class thet contains current gameplay stats. EnemiesKilled should be in this class.
        public int EnemiesKilled { get; set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}