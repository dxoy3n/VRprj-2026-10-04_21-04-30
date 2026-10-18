using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject ChestPanel;

    [Header("Lid Rotator Settings")]
    public ChestLid lid;

    [Header("Reward Settings")]
    public GameObject Reward;

    private bool isPlayerNearby = false;
    private bool isOpened = false;

    void Start()
    {
        if (ChestPanel != null) ChestPanel.SetActive(false);
        if (Reward != null) Reward.SetActive(false);

        // Đã mở rương trước đó: chỉ đánh dấu đã mở, nắp do ChestLid tự đặt ở trạng thái mở
        if (PlayerPrefs.GetInt("ChestOpened", 0) == 1)
        {
            isOpened = true;
        }
    }

    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player") || other.name.Contains("Main Camera") || other.name.Contains("XR");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other) || isOpened) return;

        isPlayerNearby = true;

        int currentStarfish = PlayerPrefs.GetInt("StarfishCount", 0);
        if (currentStarfish >= 6)
        {
            if (ChestPanel != null) ChestPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Rương đang khóa! Bạn cần thu thập đủ 6 sao biển để mở khóa.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsPlayer(other)) return;

        isPlayerNearby = false;
        if (ChestPanel != null) ChestPanel.SetActive(false);
    }

    public void OnClickYes()
    {
        if (!isPlayerNearby || isOpened) return;

        isOpened = true;

        PlayerPrefs.SetInt("ChestOpened", 1);
        PlayerPrefs.Save();

        if (lid != null) lid.OpenLid();
        if (ChestPanel != null) ChestPanel.SetActive(false);
        if (Reward != null) Reward.SetActive(true);

        Debug.Log("Đã mở rương thành công");
    }

    public void OnClickNo()
    {
        if (ChestPanel != null) ChestPanel.SetActive(false);
    }

    public void OnClickCloseReward()
    {
        if (Reward != null) Reward.SetActive(false);
    }
}