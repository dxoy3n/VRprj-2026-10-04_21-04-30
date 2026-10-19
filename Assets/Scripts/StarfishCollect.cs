using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class StarfishCollect : MonoBehaviour, IPointerClickHandler
{
    public string starId; // Đặt trong Inspector: L1_1, L1_2, L2_1...

    private bool isCollected;
    private XRSimpleInteractable xrInteractable;

    private void Awake()
    {
        xrInteractable = GetComponent<XRSimpleInteractable>();
        xrInteractable.selectEntered.AddListener(OnXRSelected);
    }

    private void Start()
    {
        // Sao đã nhặt trước đó thì ẩn ngay khi load scene
        if (PlayerPrefs.GetInt("Star_" + starId, 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (xrInteractable != null)
            xrInteractable.selectEntered.RemoveListener(OnXRSelected);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Collect();
    }

    private void OnMouseDown()
    {
        Collect();
    }

    private void OnXRSelected(SelectEnterEventArgs args)
    {
        Collect();
    }

    private void Collect()
    {
        if (isCollected) return;

        if (StarfishManager.Instance == null)
        {
            Debug.LogError("Không tìm thấy StarfishManager trong scene.");
            return;
        }

        isCollected = true;
        StarfishManager.Instance.AddStarfish(starId);
        Destroy(gameObject);
    }
}