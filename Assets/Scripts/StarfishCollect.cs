using UnityEngine;
using UnityEngine.EventSystems;

public class StarfishCollect : MonoBehaviour, IPointerClickHandler
{
    private bool isCollected;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("STARFISH CLICKED: " + gameObject.name);
        Collect();
    }

    private void OnMouseDown()
    {
        Debug.Log("STARFISH MOUSE DOWN: " + gameObject.name);
        Collect();
    }

    void Collect()
    {
        if (isCollected)
            return;

        Debug.Log("COLLECT STARFISH: " + gameObject.name);

        if (StarfishManager.Instance == null)
        {
            Debug.LogError("Không tìm thấy StarfishManager trong scene.");
            return;
        }

        isCollected = true;
        StarfishManager.Instance.AddStarfish();
        Destroy(gameObject);
    }
}