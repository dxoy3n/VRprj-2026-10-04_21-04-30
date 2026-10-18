using UnityEngine;

public class ChestLid : MonoBehaviour
{
    [Header("Cài đặt Nắp Rương")]
    public Transform lidTransform;
    public Vector3 openRotation = new Vector3(-90f, 0f, 0f);
    public float rotationSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion targetRotation;
    private bool shouldOpen = false;

    void Start()
    {
        if (lidTransform == null)
        {
            Debug.LogError("ChestLid: chưa kéo lidTransform vào Inspector!");
            return;
        }

        closedRotation = lidTransform.localRotation;
        targetRotation = closedRotation * Quaternion.Euler(openRotation);

        int opened = PlayerPrefs.GetInt("ChestOpened", 0);
        Debug.Log("ChestLid Start, ChestOpened = " + opened);

        if (opened == 1)
        {
            lidTransform.localRotation = targetRotation;
        }
    }

    void Update()
    {
        if (shouldOpen && lidTransform != null)
        {
            lidTransform.localRotation = Quaternion.Slerp(
                lidTransform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void OpenLid()
    {
        shouldOpen = true;
    }
}