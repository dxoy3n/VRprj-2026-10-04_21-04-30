using UnityEngine;
using System.Collections;

public class CameraDescend : MonoBehaviour
{
    [Header("Điểm bắt đầu (mặt nước)")]
    public float surfaceY = 8f;            // Độ cao mặt nước (script UnderwaterDepth của bạn dùng 8)
    public float startAboveSurface = 0.5f; // Bắt đầu cao hơn mặt nước một chút

    [Header("Chuyển động")]
    public float startDelay = 1f;          // Chờ bao nhiêu giây rồi mới hạ
    public float duration = 10f;           // Thời gian hạ xuống (giây)
    public bool smooth = true;             // Tick = chậm dần lúc đến nơi

    [Header("Khóa di chuyển (tùy chọn)")]
    public MonoBehaviour[] lockMovement;   // Kéo Move Provider / Teleportation Provider vào đây

    void Start()
    {
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
        yield return new WaitForSeconds(startDelay);

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