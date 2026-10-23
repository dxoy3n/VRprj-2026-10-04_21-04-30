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

    void Start()
    {
        FindSceneReferences(SceneManager.GetActiveScene());
        RefreshAll();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSceneReferences(scene);
        RefreshAll();
    }

    // Tìm lại các object của scene hiện tại (vì scene cũ bị hủy khi chuyển scene)
    void FindSceneReferences(Scene scene)
    {
        starfishText = FindFirstObjectByType<TextMeshProUGUI>();
        keyObject = null;
        arrowGuide = null;

        if (scene.name == "Level2_TinyOcean")
            keyObject = FindObjectWithTag(scene, "keyObject");

        if (scene.name == "Level1_TinyOcean")
            arrowGuide = FindObjectWithTag(scene, "ArrowGuide");
    }

    GameObject FindObjectInScene(Scene scene, string namePart)
    {
        foreach (GameObject root in scene.GetRootGameObjects())
        {
            foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
            {
                if (t.name.ToLower().Contains(namePart.ToLower()))
                    return t.gameObject;
            }
        }
        return null;
    }

    GameObject FindObjectWithTag(Scene scene, string tag)
    {
        foreach (GameObject root in scene.GetRootGameObjects())
        {
            foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
            {
                if (t.CompareTag(tag))
                    return t.gameObject;
            }
        }
        return null;
    }

    void RefreshAll()
    {
        UpdateStarfishUI();
        CheckLevel1Progress();
        CheckKeyUnlock();
    }

    public void AddStarfish(string id)
    {
        if (PlayerPrefs.GetInt("Star_" + id, 0) == 1) return;

        PlayerPrefs.SetInt("Star_" + id, 1);
        PlayerPrefs.SetInt("StarfishCount", PlayerPrefs.GetInt("StarfishCount", 0) + 1);
        PlayerPrefs.Save();

        RefreshAll();
    }

    public void UpdateStarfishUI()
    {
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
        if (starfishText != null)
            starfishText.text = "Tổng sao biển: " + currentCount + "/" + totalStarfishNeeded;
    }

    void CheckLevel1Progress()
    {
        if (arrowGuide == null) return;

        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
        bool isLevel1 = SceneManager.GetActiveScene().name == "Level1_TinyOcean";
        arrowGuide.SetActive(isLevel1 && currentCount >= level1StarfishTarget);
    }

    void CheckKeyUnlock()
    {
        if (keyObject == null) return;

        // Đã mở rương thì chìa khóa không hiện nữa
        if (PlayerPrefs.GetInt("ChestOpened", 0) == 1)
        {
            keyObject.SetActive(false);
            return;
        }

        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
        bool isLevel2 = SceneManager.GetActiveScene().name == "Level2_TinyOcean";

        if (!isLevel2 || currentCount < totalStarfishNeeded)
        {
            keyObject.SetActive(false);
            return;
        }

        keyObject.SetActive(true);

        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            Vector3 spawnPosition = mainCam.transform.position + mainCam.transform.forward * 1.2f;
            spawnPosition.y = mainCam.transform.position.y - 0.05f;

            keyObject.transform.position = spawnPosition;
            keyObject.transform.rotation = mainCam.transform.rotation;
        }
    }
}