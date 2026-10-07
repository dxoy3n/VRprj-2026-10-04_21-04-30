using UnityEngine;
using UnityEngine.EventSystems;

public class StarfishCollect : MonoBehaviour, IPointerClickHandler
{
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
        Debug.Log("COLLECT STARFISH: " + gameObject.name);

        if (StarfishManager.Instance != null)
        {
            StarfishManager.Instance.AddStarfish();
        }

        Destroy(gameObject);
    }
}