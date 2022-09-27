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

    private float _gripSample;
    private float _gripStrength;
    private bool _grabbingActive = false;

    private Vector2 _thumbSample;
    private float _thumbStrength;
    private bool _thumbActive = false;

    private bool _leftTriggerDown;
    private bool _leftGripDown;
    // and other left hand buttons

    private bool _rightTriggerDown;
    private bool _rightGripDown;
    // and other right hand buttons

    private bool isTwoHands = false;
    public GameObject putter;
    public Quaternion originalPutterRotation;
    public float putterLengthSpeed;
    public GameObject putterHead;
    private Vector3 originalPutterHeadPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPutterHeadPos = putterHead.transform.localPosition;
        originalPutterRotation = putter.transform.rotation;

        if (primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
        }

        allDevices = new List<UnityEngine.XR.InputDevice>();
        devicesWithPrimaryButton = new List<UnityEngine.XR.InputDevice>();
        InputTracking.nodeAdded += InputTracking_nodeAdded;

    }

    // check for new input devices when new XRNode is added
    private void InputTracking_nodeAdded(XRNodeState obj)
    {
        updateInputDevices();
    }
 

    // Update is called once per frame
    void Update()
    {
        _gripStrength = 0.0f;
        _gripSample = 0.0f;
        _thumbStrength = 0.0f;
        _thumbSample = Vector2.zero;

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

            device.TryGetFeatureValue(CommonUsages.grip, out _gripSample);
            _gripStrength = Mathf.Max(_gripStrength, _gripSample);

            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _thumbSample);
            
           //     .TryGetFeature Value
           // _gripStrength = Mathf.Max(_gripStrength, _gripSample);
        }

        if (Mathf.Abs(_thumbSample.y) >= 0.2f)
        {
            putterHead.transform.localPosition = putterHead.transform.localPosition + (new Vector3(_thumbSample.y * putterLengthSpeed * Time.deltaTime, 0.0f, 0.0f));
        }

        if (_gripStrength > 0.2f)
        {
            if (!_grabbingActive)
            {
                _grabbingActive = true;
                //press event
                toggleTwoHands();
            }


        }
        else if (_grabbingActive)
        {
            _grabbingActive = false;
            //release event
            toggleTwoHands();
        }

        if (_grabbingActive)
        {
            if (isLeftHand)
            {
                putter.transform.LookAt(rightController.transform);
                //putter.transform.rot
            }
            else
            {
                putter.transform.LookAt(leftController.transform);
            }
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

    void toggleTwoHands()
    {
        if (!isTwoHands)
        {
            if (isLeftHand)
            {
                putter.transform.LookAt(rightController.transform);
               //putter.transform.rot
            } else
            {
                putter.transform.LookAt(leftController.transform);
            }
            isTwoHands = true;
        } else
        {
            putter.transform.localRotation = originalPutterRotation;
            isTwoHands = false;
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
