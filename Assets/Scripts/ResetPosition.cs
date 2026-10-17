using UnityEngine;
using UnityEngine.XR;

public class ResetPosition : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    private InputDevice leftController;
    private bool wasPressed;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update()
    {
        if (!leftController.isValid)
            leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressed);

        if (pressed && !wasPressed)
            transform.SetPositionAndRotation(startPos, startRot);

        wasPressed = pressed;
    }
}