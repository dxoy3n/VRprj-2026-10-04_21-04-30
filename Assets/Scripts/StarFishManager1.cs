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
    public int level2StarfishTarget = 3;

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
        // Đăng ký sự kiện: Mỗi khi load Scene xong sẽ tự động chạy hàm OnSceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Hàm tự động thực thi ngay khi load sang bất kỳ Scene nào
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. Tự tìm lại Text UI nếu tham chiếu bị Missing sau khi chuyển Scene
        if (starfishText == null)
        {
            starfishText = FindFirstObjectByType<TextMeshProUGUI>();
        }

        // 2. Tự động tìm keyObject và arrowGuide trong Scene mới nếu bồ gắn Tag cho chúng
        if (keyObject == null)
        {
            GameObject foundKey = GameObject.FindWithTag("KeyObject");
            if (foundKey != null) keyObject = foundKey;
        }

        if (arrowGuide == null)
        {
            GameObject foundArrow = GameObject.FindWithTag("ArrowGuide");
            if (foundArrow != null) arrowGuide = foundArrow;
        }

        // 3. Cập nhật trạng thái
        UpdateStarfishUI();
        CheckLevel2Progress();
        CheckKeyUnlock();
    }

    void Start()
    {
#if UNITY_EDITOR
        // Mở comment dòng này nếu muốn mỗi lần Play Editor là đếm lại từ 0
        // PlayerPrefs.SetInt("StarfishCount", 0);
        // PlayerPrefs.Save();
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

    public void UpdateStarfishUI()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
        if (starfishText != null)
        {
            starfishText.text = "Tổng sao biển: " + currentCount + "/" + totalStarfishNeeded;
        }
    }

    void CheckLevel2Progress()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
        if (SceneManager.GetActiveScene().name == "Level2_TinyOcean")
        {
            if (arrowGuide != null) 
                arrowGuide.SetActive(currentCount >= level2StarfishTarget);
        }
        else
        {
            if (arrowGuide != null) 
                arrowGuide.SetActive(false);
        }
    }

    void CheckKeyUnlock()
    {
        if (SceneManager.GetActiveScene().name != "Level3_TinyOcean")
        {
            if (keyObject != null) keyObject.SetActive(false);
            return;
        }

        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);

        if (currentCount >= totalStarfishNeeded)
        {
            if (keyObject != null)
            {
                keyObject.SetActive(true);

                Camera mainCam = Camera.main;
                if (mainCam != null)
                {
                    Vector3 spawnPosition = mainCam.transform.position + mainCam.transform.forward * 1.5f;
                    spawnPosition.y -= 0.2f;

                    keyObject.transform.position = spawnPosition;
                    keyObject.transform.rotation = mainCam.transform.rotation;
                }
            }
        }
        else
        {
            if (keyObject != null) 
                keyObject.SetActive(false);
        }
    }
}