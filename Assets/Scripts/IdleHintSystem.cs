using UnityEngine;
using TMPro;

public class IdleHintSystem : MonoBehaviour
{
    [Header("Tham chiếu")]
    [SerializeField] Transform xrCamera;        // Main Camera trong XR Origin
    [SerializeField] Transform fishTarget;      // Cá
    [SerializeField] GameObject hintPanel;      // HintCanvas
    [SerializeField] TMP_Text hintText;         // HintText
    [SerializeField] GameObject beaconPrefab;   // Beacon

    [Header("Cài đặt")]
    [SerializeField] string starfishTag = "Starfish";
    [SerializeField] float idleTime = 30f;
    [SerializeField] float moveThreshold = 0.05f;   // mét, tăng nhẹ để bỏ rung đầu khi kính
    [SerializeField] float rotateThreshold = 5f;    // độ

    float idleTimer;
    Vector3 lastPos;
    Quaternion lastRot;
    bool hintShowing;
    GameObject fishBeacon, starBeacon;

    void Start()
    {
        lastPos = xrCamera.position;
        lastRot = xrCamera.rotation;
        hintPanel.SetActive(false);
    }

    void Update()
    {
        bool moved = Vector3.Distance(xrCamera.position, lastPos) > moveThreshold
                  || Quaternion.Angle(xrCamera.rotation, lastRot) > rotateThreshold;

        lastPos = xrCamera.position;
        lastRot = xrCamera.rotation;

        if (moved)
        {
            NotifyPlayerActivity();
            return;
        }

        if (hintShowing) return;

        idleTimer += Time.deltaTime;
        if (idleTimer >= idleTime)
            ShowHint();
    }

    // Gọi khi người chơi bấm cá, nhặt sao, di chuyển, hoặc đang xem cá
    public void NotifyPlayerActivity()
    {
        idleTimer = 0f;
        if (hintShowing) HideHint();
    }

    void ShowHint()
    {
        hintShowing = true;
        hintPanel.SetActive(true);

        Transform star = FindNearestStarfish();

        string msg = "Hãy bấm vào con cá để tiếp tục.";
        if (star != null)
            msg += "\nSao biển gần nhất nằm theo vật đánh dấu.";
        hintText.text = msg;

        if (fishTarget != null && beaconPrefab != null)
            fishBeacon = Instantiate(beaconPrefab, fishTarget.position + Vector3.up * 0.5f, Quaternion.identity);

        if (star != null && beaconPrefab != null)
            starBeacon = Instantiate(beaconPrefab, star.position + Vector3.up * 0.5f, Quaternion.identity);
    }

    void HideHint()
    {
        hintShowing = false;
        idleTimer = 0f;
        hintPanel.SetActive(false);

        if (fishBeacon) Destroy(fishBeacon);
        if (starBeacon) Destroy(starBeacon);
    }

    Transform FindNearestStarfish()
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag(starfishTag);
        Transform nearest = null;
        float best = float.MaxValue;

        foreach (var s in stars)
        {
            if (!s.activeInHierarchy) continue;
            float d = Vector3.Distance(xrCamera.position, s.transform.position);
            if (d < best)
            {
                best = d;
                nearest = s.transform;
            }
        }
        return nearest;
    }
}