using UnityEngine;
using System.Collections;

public class ChestLid : MonoBehaviour
{
    [Header("Cài đặt Nắp Rương")]
    public Transform lidTransform; // Kéo đối tượng nắp rương (Lid) vào đây
    public Vector3 openRotation = new Vector3(-90f, 0f, 0f); // Góc xoay khi mở (có thể thay đổi số đo cho khớp với rương của bạn)
    public float rotationSpeed = 2f; // Tốc độ xoay nắp rương

    private Quaternion closedRotation;
    private Quaternion targetRotation;
    private bool shouldOpen = false;

    void Start()
    {
        if (lidTransform != null)
        {
            // Lưu lại góc đóng ban đầu của nắp
            closedRotation = lidTransform.localRotation;
            targetRotation = closedRotation * Quaternion.Euler(openRotation);
        }
    }

    void Update()
    {
        // Khi lệnh mở được kích hoạt, nắp rương sẽ xoay mượt mà từ đóng sang mở
        if (shouldOpen && lidTransform != null)
        {
            lidTransform.localRotation = Quaternion.Slerp(lidTransform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // Hàm này sẽ được gọi từ ChestScript khi bấm nút Yes
    public void OpenLid()
    {
        shouldOpen = true;
    }
}