using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.XR;

[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }

public class putterScript : MonoBehaviour
{

    public PrimaryButtonEvent primaryButtonPress;
    private bool lastButtonState = false;
    private List<UnityEngine.XR.InputDevice> allDevices;
    private List<UnityEngine.XR.InputDevice> devicesWithPrimaryButton;

    private bool isLeftHand;
    public GameObject leftController;
    public GameObject rightController;
    private bool primaryButtonPressed = true;

    // Start is called before the first frame update
    void Start()
    {
        if (primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
        }

        allDevices = new List<UnityEngine.XR.InputDevice>();
        devicesWithPrimaryButton = new List<UnityEngine.XR.InputDevice>();
        InputTracking.nodeAdded += InputTracking_nodeAdded; ;

    }

    // check for new input devices when new XRNode is added
    private void InputTracking_nodeAdded(XRNodeState obj)
    {
        updateInputDevices();
    }
 

    // Update is called once per frame
    void Update()
    {
        bool tempState = false;
        bool invalidDeviceFound = false;
        foreach (var device in devicesWithPrimaryButton)
        {
            bool buttonState = false;
            tempState = device.isValid // the device is still valid
                        && device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonState) // did get a value
                        && buttonState // the value we got
                        || tempState; // cumulative result from other controllers

            if (!device.isValid)
                invalidDeviceFound = true;
        }

        if (tempState != lastButtonState) // Button state changed since last frame
        {
            primaryButtonPress.Invoke(tempState);
            lastButtonState = tempState;
        }

        if (invalidDeviceFound || devicesWithPrimaryButton.Count == 0) // refresh device lists
            updateInputDevices();
    }

    // find any devices supporting the desired feature usage
    void updateInputDevices()
    {
        devicesWithPrimaryButton.Clear();
        UnityEngine.XR.InputDevices.GetDevices(allDevices);
        bool discardedValue;

        foreach (var device in allDevices)
        {
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
            {
                devicesWithPrimaryButton.Add(device); // Add any devices that have a primary button.
            }
        }
    }

    public void switchHands()
    {
        if (primaryButtonPressed)
        {
            if (isLeftHand)
            {
                gameObject.transform.parent = rightController.transform;
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
                isLeftHand = !isLeftHand;
            }
            else
            {
                gameObject.transform.parent = leftController.transform;
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
                isLeftHand = !isLeftHand;
            }
            
        }

        primaryButtonPressed = !primaryButtonPressed;

    }
}
