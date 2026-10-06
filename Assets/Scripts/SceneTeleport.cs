using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleport : MonoBehaviour
{
    [Header("Tên Scene muốn chuyển tới")]
    public string targetSceneName = "Level3_TinyOcean";

    void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem người chơi (Player / XR Rig / Camera) bước vào vùng chưa
        // Bạn có thể gắn Tag "Player" cho Camera hoặc nhân vật VR của mình
        if (other.CompareTag("Player") || other.name.Contains("Main Camera") || other.name.Contains("XR"))
        {
            // Kiểm tra điều kiện: Phải nhặt đủ ít nhất 3 sao ở Level 2 mới cho qua màn
            int currentCount = PlayerPrefs.GetInt("StarfishCount", 0);
            if (currentCount >= 3)
            {
                Debug.Log("Đã đủ 3 sao ở Level 2, đang chuyển sang màn tiếp theo...");
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                Debug.Log("Chưa đủ 3 sao biển ở Level 2! Cần nhặt thêm.");
                // Bạn có thể hiển thị UI thông báo nhỏ tại đây nếu muốn
            }
        }
    }
}