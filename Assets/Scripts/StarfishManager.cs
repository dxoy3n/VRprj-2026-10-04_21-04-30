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
        if (starfishText == null)
        {
            starfishText = FindFirstObjectByType<TextMeshProUGUI>();
        }

        FindKeyInScene(scene);
        if (scene.name == "Level1_TinyOcean")
        {
            arrowGuide = null;
            foreach (GameObject root in scene.GetRootGameObjects())
            {
                foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
                {
                    if (t.CompareTag("ArrowGuide"))
                    {
                        arrowGuide = t.gameObject;
                        break;
                    }
                }
                if (arrowGuide != null) break;
            }
        }
        UpdateStarfishUI();
        CheckLevel1Progress();
        CheckKeyUnlock();
    }

    void Start()
    {
/*#if UNITY_EDITOR
        if (SceneManager.GetActiveScene().name == "Level1_TinyOcean")
        {
            PlayerPrefs.SetInt("StarfishCount", 0);
            PlayerPrefs.Save();
        }
#endif*/
        FindKeyInScene(SceneManager.GetActiveScene());
        UpdateStarfishUI();
        CheckLevel1Progress();
        CheckKeyUnlock();
    }

    void FindKeyInScene(Scene s)
    {
        if (s.name != "Level2_TinyOcean") return;

        KeyEffects key = FindFirstObjectByType<KeyEffects>(FindObjectsInactive.Include);
        keyObject = key != null ? key.gameObject : null;
    }

    /* public void AddStarfish()
     {
         int currentCount = PlayerPrefs.GetInt("StarfishCount", 0) + 1;
         PlayerPrefs.SetInt("StarfishCount", currentCount);
         PlayerPrefs.Save();

         UpdateStarfishUI();
         CheckLevel1Progress();
         CheckKeyUnlock();
     }*/

    public void AddStarfish(string id)
    {
        if (PlayerPrefs.GetInt("Star_" + id, 0) == 1) return;

        PlayerPrefs.SetInt("Star_" + id, 1);
        PlayerPrefs.SetInt("StarfishCount", PlayerPrefs.GetInt("StarfishCount", 0) + 1);
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
        if (PlayerPrefs.GetInt("ChestOpened", 0) == 1 && keyObject != null)
        {
            keyObject.SetActive(false);
            return;
        }
        int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);

        if (SceneManager.GetActiveScene().name != "Level2_TinyOcean")
        {
            if (keyObject != null) keyObject.SetActive(false);
            return;
        }

        if (currentCount >= totalStarfishNeeded)
        {
            if (keyObject != null)
            {
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
            else
            {
                Debug.LogError("keyObject is NULL! Không tìm thấy KeyEffects nào trong Level2_TinyOcean.");
            }
        }
        else
        {
            if (keyObject != null)
                keyObject.SetActive(false);
        }
    }
}