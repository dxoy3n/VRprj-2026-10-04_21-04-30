using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class CameraDescend : MonoBehaviour
{
    // ===== Tự nhớ scene trước đó =====
    static string currentScene = "";
    static string previousScene = "";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        currentScene = "";
        previousScene = "";
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        previousScene = currentScene;
        currentScene = scene.name;
    }

    [Header("Bỏ qua cảnh lặn nếu đi từ scene này tới")]
    public string skipIfComingFrom = "Level2_TinyOcean";

    [Header("Điểm bắt đầu (mặt nước)")]
    public float surfaceY = 8f;            // Độ cao mặt nước (script UnderwaterDepth của bạn dùng 8)
    public float startAboveSurface = 0.5f; // Bắt đầu cao hơn mặt nước một chút

    [Header("Đếm ngược trước khi lặn")]
    public TMP_Text countdownText;         // Kéo dòng chữ TextMeshPro vào đây
    public int countdownSeconds = 3;       // Đếm ngược bao nhiêu giây

    [Header("Chuyển động")]
    public float duration = 10f;           // Thời gian hạ xuống (giây)
    public bool smooth = true;             // Tick = chậm dần lúc đến nơi

    [Header("Khóa di chuyển (tùy chọn)")]
    public MonoBehaviour[] lockMovement;   // Kéo Move Provider / Teleportation Provider vào đây

    void Start()
    {
        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        // Từ Level2 quay về thì đứng yên tại chỗ, không đếm ngược, không lặn
        if (previousScene == skipIfComingFrom) return;

        Vector3 endPos = transform.position; // Vị trí bạn đặt rig trong Scene = vị trí cố định cuối cùng
        Vector3 startPos = new Vector3(endPos.x, surfaceY + startAboveSurface, endPos.z);

        // Nếu vị trí cuối cao hơn mặt nước thì không cần hạ
        if (endPos.y >= startPos.y) return;

        foreach (var m in lockMovement)
            if (m != null) m.enabled = false;

        transform.position = startPos;
        StartCoroutine(Descend(startPos, endPos));
    }

    IEnumerator Descend(Vector3 from, Vector3 to)
    {
        // Đếm ngược: 3... 2... 1...
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            for (int i = countdownSeconds; i > 0; i--)
            {
                countdownText.text = "Bắt đầu lặn xuống trong " + i + "s...";
                yield return new WaitForSeconds(1f);
            }
            countdownText.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(countdownSeconds);
        }

        // Hạ từ từ xuống
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float k = smooth ? Mathf.SmoothStep(0f, 1f, t) : t;
            transform.position = Vector3.Lerp(from, to, k);
            yield return null;
        }
        transform.position = to;
    }
}