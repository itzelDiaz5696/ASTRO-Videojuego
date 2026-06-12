using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    public GameObject panel; // el panel o texto TMP que muestra el mensaje

    void Start()
    {
        if (panel != null)
            panel.SetActive(false); // inicia oculto
    }

    public void ShowMessage()
    {
        if (panel == null)
        {
            Debug.LogError("❌ Panel es NULL o fue destruido");
            return;
        }

        Debug.Log("🎉 SHOW Se activó el mensaje");
        panel.SetActive(true);
    }
}