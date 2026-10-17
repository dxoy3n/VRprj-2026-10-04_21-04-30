using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject ChestPanel; // Kéo Panel (Yes/No) vào đây

    [Header("Animation Settings")]
    //public Animator chestAnimator; // Kéo Animator của rương vào đây
    public string openTriggerName = "OpenChest"; // Tên Trigger trong Animator của rương

    private bool isPlayerNearby = false;
    private bool isOpened = false;

    void Start()
    {
        if (ChestPanel != null)
        {
            ChestPanel.SetActive(false); // Ban đầu ẩn panel đi
        }
    }

    // Khi người chơi đi vào vùng Trigger của rương
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra điều kiện: Tag Player, hoặc tên chứa Main Camera / XR trong VR
        if ((other.CompareTag("Player") || other.name.Contains("Main Camera") || other.name.Contains("XR")) && !isOpened)
        {
            isPlayerNearby = true;

            // Kiểm tra xem người chơi đã thu thập đủ 6 sao biển hay chưa
            int currentStarfish = PlayerPrefs.GetInt("StarfishCount", 0);

            if (currentStarfish >= 6)
            {
                if (ChestPanel != null)
                {
                    ChestPanel.SetActive(true); // Hiển thị panel lựa chọn (Yes/No)
                }
            }
            else
            {
                Debug.Log("Rương đang khóa! Bạn cần thu thập đủ 6 sao biển để mở khóa.");
            }
        }
    }

    // Khi người chơi rời khỏi vùng Trigger của rương
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.name.Contains("Main Camera") || other.name.Contains("XR"))
        {
            isPlayerNearby = false;
            if (ChestPanel != null)
            {
                ChestPanel.SetActive(false); // Ẩn panel khi đi ra xa
            }
        }
    }

    // Hàm gọi khi bấm nút Yes trên UI Panel
    [Header("Lid Rotator Settings")]
    public ChestLid lid; // Kéo đối tượng chứa script ChestLid vào đây

    [Header("Reward Settings")]
    public GameObject Reward; // Kéo Panel chứa ảnh voucher mới vào đây

    public void OnClickYes()
    {
        if (isPlayerNearby && !isOpened)
        {
            isOpened = true;

            // 1. MỞ NẮP RƯƠNG
            if (lid != null)
            {
                lid.OpenLid();
            }
            else
            {
                Debug.LogError("Chưa kéo ChestLid vào ô Lid trong ChestScript!");
            }

            // 2. Tắt Panel Yes/No
            if (ChestPanel != null)
            {
                ChestPanel.SetActive(false);
            }

            // 3. Hiện voucher sau 1.5 giây để kịp thấy nắp mở
            Invoke("ShowReward", 1.5f);

            Debug.Log("Đã mở rương thành công");
        }
    }

    void ShowReward()
    {
        if (Reward != null)
        {
            Reward.SetActive(true);
        }
    }

    // Hàm gọi khi bấm nút No trên UI Panel
    public void OnClickNo()
    {
        if (ChestPanel != null)
        {
            ChestPanel.SetActive(false); // Tắt panel theo yêu cầu
        }
    }
    // Hàm gọi khi bấm nút đóng (X / Close / Claim) trên panel voucher
    public void OnClickCloseReward()
    {
        if (Reward != null)
        {
            Reward.SetActive(false);
        }
    }
}
