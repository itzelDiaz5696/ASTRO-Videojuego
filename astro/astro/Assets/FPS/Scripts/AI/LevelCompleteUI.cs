using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.FPS.AI
{
    public class LevelCompleteUI : MonoBehaviour
    {
        public GameObject panel;
        public string NextLevelName = "SecondaryScene";
        public float DelayBeforeLoad = 3f;

        void Start()
        {
            panel.SetActive(false);
        }

        public void ShowMessage()
        {
            Debug.Log("🎉 ¡Nivel completado!");
            if (panel == null)
            {
                Debug.LogError("❌ Panel es NULL o fue destruido");
                return;
            }
            panel.SetActive(true);
            Invoke(nameof(LoadNextLevel), DelayBeforeLoad);
        }

        void LoadNextLevel()
        {
            SceneManager.LoadScene(NextLevelName);
        }
    }
}