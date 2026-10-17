using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StarfishManager : MonoBehaviour
{
    public static StarfishManager Instance;

    [Header("UI & Object References")]
    public TextMeshProUGUI starfishText;
    public GameObject keyObject;
    public GameObject arrowGuide;

    [Header("Settings")]
    public int totalStarfishNeeded = 6;
    public int level1StarfishTarget = 3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. Tự tìm lại Text UI
        if (starfishText == null)
        {
            starfishText = FindFirstObjectByType<TextMeshProUGUI>();
        }

        // 2. Tìm Chìa Khóa an toàn trong Scene 2 (Tìm theo tên chứa chữ 'key' - Không sợ lỗi Tag)
        if (keyObject == null && scene.name == "Level2_TinyOcean")
        {
            foreach (GameObject root in scene.GetRootGameObjects())
            {
                Transform[] transforms = root.GetComponentsInChildren<Transform>(true); // true = tìm cả object đang ẩn
                foreach (Transform t in transforms)
                {
                    // Tìm object có tên chứa chữ 'Key' hoặc 'key' (Ví dụ: KeyObject, key, Key_Prefab...)
                    if (t.name.ToLower().Contains("key"))
                    {
                        keyObject = t.gameObject;
                        break;
                    }
                }
                if (keyObject != null) break;
            }
        }

        // 3. Cập nhật UI & Kiểm tra điều kiện mở khóa
        UpdateStarfishUI();
        CheckLevel1Progress();
        CheckKeyUnlock();
    }

    void Start()
    {
#if UNITY_EDITOR
        // Chỉ reset điểm về 0 nếu bắt đầu Play từ Level 1
        if (SceneManager.GetActiveScene().name == "Level1_TinyOcean")
        {
            PlayerPrefs.SetInt("StarfishCount", 0);
            PlayerPrefs.Save();
        }
#endif
        UpdateStarfishUI();
        CheckLevel1Progress();
        CheckKeyUnlock();
    }

    public void AddStarfish()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0) + 1;
        PlayerPrefs.SetInt("StarfishCount", currentCount);
        PlayerPrefs.Save();

        UpdateStarfishUI();
        CheckLevel1Progress();
        CheckKeyUnlock();
    }

    public void UpdateStarfishUI()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
        if (starfishText != null)
        {
            starfishText.text = "Tổng sao biển: " + currentCount + "/" + totalStarfishNeeded;
        }
    }

    void CheckLevel1Progress()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
        if (SceneManager.GetActiveScene().name == "Level1_TinyOcean")
        {
            if (arrowGuide != null)
                arrowGuide.SetActive(currentCount >= level1StarfishTarget);
        }
        else
        {
            if (arrowGuide != null)
                arrowGuide.SetActive(false);
        }
    }

    void CheckKeyUnlock()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);

        // Nếu không phải Level 2 thì ép ẩn chìa khóa
        if (SceneManager.GetActiveScene().name != "Level2_TinyOcean")
        {
            if (keyObject != null) keyObject.SetActive(false);
            return;
        }

        // Kiểm tra số lượng sao biển
        if (currentCount >= totalStarfishNeeded)
        {
            if (keyObject != null)
            {
                keyObject.SetActive(true);

                Camera mainCam = Camera.main;
                if (mainCam != null)
                {
                    // Xuất hiện ngay trước mặt ngang tầm mắt
                    Vector3 spawnPosition = mainCam.transform.position + mainCam.transform.forward * 1.2f;
                    spawnPosition.y = mainCam.transform.position.y - 0.05f;

                    keyObject.transform.position = spawnPosition;
                    keyObject.transform.rotation = mainCam.transform.rotation;
                }
            }
            else
            {
                Debug.LogError("keyObject is NULL! Không tìm thấy GameObject nào có tên chứa chữ 'key' ở Scene 2!");
            }
        }
        else
        {
            // Chưa đủ 6 con -> ẨN CHÌA KHÓA
            if (keyObject != null)
                keyObject.SetActive(false);
        }
    }
}