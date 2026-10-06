using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StarfishManager : MonoBehaviour
{
    public static StarfishManager Instance;

    public TextMeshProUGUI starfishText;
    public int totalStarfishNeeded = 6; // Tổng số sao biển cho cả 2 level là 6
    public GameObject keyObject;

    // Biến tham chiếu tới mũi tên gợi ý và điểm A ở Level 2 (nếu có)
    public GameObject arrowGuide;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ Manager này qua các Scene nếu cần, hoặc quản lý theo Scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
#if UNITY_EDITOR
        // Khi ấn Play ở Editor, tự động reset về 0
        PlayerPrefs.SetInt("StarfishCount", 0);
        PlayerPrefs.Save();
#endif

        UpdateStarfishUI();
        CheckLevel2Progress();
        CheckKeyUnlock();
    }

    public void AddStarfish()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0) + 1;
        PlayerPrefs.SetInt("StarfishCount", currentCount);
        PlayerPrefs.Save();

        UpdateStarfishUI();
        CheckLevel2Progress();
        CheckKeyUnlock();
    }

    void UpdateStarfishUI()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
        if (starfishText != null)
        {
            // Hiển thị tiến độ tổng 6 con
            starfishText.text = "Tổng sao biển: " + currentCount + "/" + totalStarfishNeeded;
        }
    }

    // 1. Kiểm tra khi ở Level 2: Đã nhặt đủ 3 con chưa để hiện mũi tên chỉ đường tới điểm A
    void CheckLevel2Progress()
    {
        if (SceneManager.GetActiveScene().name == "Level2_TinyOcean")
        {
            int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
            if (currentCount >= 3)
            {
                if (arrowGuide != null) arrowGuide.SetActive(true); // Hiển thị mũi tên chỉ vào điểm A
            }
            else
            {
                if (arrowGuide != null) arrowGuide.SetActive(false);
            }
        }
    }

    // 2. Kiểm tra mở khóa chìa khóa (Chỉ xuất hiện khi đủ 6 con - thường ở Level 3)
    void CheckKeyUnlock()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
        if (currentCount >= totalStarfishNeeded)
        {
            if (keyObject != null)
            {
                keyObject.SetActive(true);

                if (Camera.main != null)
                {
                    Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
                    spawnPosition.y -= 0.2f;

                    keyObject.transform.position = spawnPosition;
                    keyObject.transform.rotation = Camera.main.transform.rotation;
                }
            }
        }
    }
}