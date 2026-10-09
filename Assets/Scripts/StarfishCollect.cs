using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class StarfishCollect : MonoBehaviour, IPointerClickHandler
{
    private bool isCollected;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable xrInteractable;

    private void Awake()
    {
        xrInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        xrInteractable.selectEntered.AddListener(OnXRSelected);
    }

    private void OnDestroy()
    {
        if (xrInteractable != null)
        {
            xrInteractable.selectEntered.RemoveListener(OnXRSelected);
        }
    }

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

    private void OnXRSelected(SelectEnterEventArgs args)
    {
        Debug.Log("STARFISH XR SELECTED: " + gameObject.name);
        Collect();
    }

    private void Collect()
    {
        if (isCollected)
            return;

        if (StarfishManager.Instance == null)
        {
            Debug.LogError("Không tìm thấy StarfishManager trong scene.");
            return;
        }

        isCollected = true;

        Debug.Log("COLLECT STARFISH: " + gameObject.name);

        StarfishManager.Instance.AddStarfish();
        Destroy(gameObject);
    }
}

