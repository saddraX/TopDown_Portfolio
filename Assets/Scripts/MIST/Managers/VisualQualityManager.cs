using System.Collections;
using UnityEngine;
using TMPro;

namespace MIST.Managers
{
    public class VisualQualityManager : MonoBehaviour
    {
        [Header("FPS Counter")]
        [SerializeField] private TextMeshProUGUI fpsCountText;

        private void Awake()
        {
            Application.targetFrameRate = 60;   // Set target framerate to 60

            StartCoroutine(UpdateFPSCounter()); // Start FPS counter
        }

        /// <summary>
        /// Update the FPS Counter UI
        /// </summary>
        /// <returns></returns>
        IEnumerator UpdateFPSCounter()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            int currentFps = (int)(1 / Time.deltaTime);
            fpsCountText.text = $"FPS: {currentFps}";

            yield return UpdateFPSCounter();
        }
    }
}