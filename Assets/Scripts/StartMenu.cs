using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject instructionsPanel;

    // Danh sách ID sao. Phải khớp với Star Id trong Inspector
    private readonly string[] starIds = { "L1_1", "L1_2", "L1_3", "L2_1", "L2_2", "L2_3" };

    public void OnStartButtonClicked()
    {
        // Bắt đầu game mới: xóa toàn bộ tiến trình cũ
        foreach (string id in starIds)
            PlayerPrefs.DeleteKey("Star_" + id);

        PlayerPrefs.DeleteKey("ChestOpened");
        PlayerPrefs.SetInt("StarfishCount", 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Level1_TinyOcean");
    }

    public void OnInstructionsButtonClicked()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (instructionsPanel != null) instructionsPanel.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        if (instructionsPanel != null) instructionsPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Game is exiting...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}