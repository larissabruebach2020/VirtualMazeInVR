//Created by Florian Kern and primary based on https://github.com/dilmerv/XRInputExamples and all the other scripts out there trying to do something similar
//Go to https://docs.unity3d.com/2019.3/Documentation/Manual/xr_input.html which devices support which input mappings

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace uniwue.hci.vilearn
{
    [System.Serializable]
    public class UnityEventValue : UnityEvent<float>
    {
        // Extended Unity Events to provide one float parameter
    }

    [System.Serializable]
    public class UnityEventValues : UnityEvent<float, float>
    {
        // Extended Unity Events to provide two float parameters
    }

    public class XRControllerInput : MonoBehaviour
    {
        #region Reference

        private static List<XRControllerInput> _xrControllerInputList = new List<XRControllerInput>();

        public static bool TryGetXRControllerInput(XRNode xrNode, out XRControllerInput xrControllerInput)
        {
            xrControllerInput = _xrControllerInputList.Find(x => x.Hand == xrNode);

            if (xrControllerInput == null)
                xrControllerInput = Array.Find(GameObject.FindObjectsOfType<XRControllerInput>(),
                    xrControllerInputScript => xrControllerInputScript.Hand == xrNode);

            if (xrControllerInput == null)
                _xrControllerInputList.Add(xrControllerInput);

            if (xrControllerInput == null)
                    Debug.Log("XRControllerInput not found for: " + xrNode);

            return xrControllerInput != null;
        }

        #endregion

        #region Configuration

        [Tooltip("Left or Right Hand. This script does not need to be on the actual controller object.")]
        [SerializeField]
        public XRNode Hand = XRNode.RightHand;

        [Header("Configuration")] [Space(20)] [Tooltip("When enbaled, disables controller input.")]
        public bool keyboardDebug = false;

        [Tooltip("How much should each keyboard press of float values change by?")]
        public float debugAxisValueIncrement = 0.1f;

        [Tooltip(
            "Minimum value that needs to be read of the axes to register. If you're getting input without touching anything, increase this value")]
        public float minAxisValue = 0.15f;

        [Tooltip("Minimum value that needs to be read of the axes to fire a press event")]
        public float minAxisIsPress = 0.9f;

        #endregion

        #region Buttons

        [Header("Trigger Button")] [Space(20)] public bool triggerTouch = false;

        public bool triggerButton = false;

        [Range(0, 1)] public float triggerValue = 0.0f;


        [Header("Grip Button")] [Space(20)] public bool gripButton = false;

        [Range(0, 1)] public float gripValue = 0.0f;


        [Header("Other Buttons")] [Space(20)] [Tooltip("Corresponds to X or A Button.")]
        public bool primaryButton = false;

        [Tooltip("Corresponds to Y or B Button")]
        public bool secondaryButton = false;

        public bool menuButton = false;

        #endregion

        #region Primary Axis

        [Header("Primary Axis")] [Space(20)] public bool primary2DAxisButton = false;

        [HideInInspector] public Vector2 primary2DAxisValue = Vector2.zero;

        [Range(-1, 1)] public float primary2DAxisXValue = 0.0f;

        [Range(-1, 1)] public float primary2DAxisYValue = 0.0f;

        public bool primary2DAxisUp = false;
        public bool primary2DAxisDown = false;
        public bool primary2DAxisLeft = false;
        public bool primary2DAxisRight = false;

        #endregion

        #region Secondary Axis

        [Header("Secondary Axis")] [Space(20)] public bool secondary2DAxisButton = false;

        [HideInInspector] public Vector2 secondary2DAxisValue = Vector2.zero;

        [Range(-1, 1)] public float secondary2DAxisXValue = 0.0f;

        [Range(-1, 1)] public float secondary2DAxisYValue = 0.0f;

        public bool secondary2DAxisUp = false;
        public bool secondary2DAxisDown = false;
        public bool secondary2DAxisLeft = false;
        public bool secondary2DAxisRight = false;

        #endregion

        #region Internal Values

        private List<InputDevice> devices = new List<InputDevice>();

        private InputDevice device;

        // private versions of the controller input variables before passed to public versions
        bool _triggerButton = false;
        bool _triggerTouch = false;
        float _triggerValue = 0.0f;

        bool _gripButton = false;
        float _gripValue = 0.0f;

        bool _primary2DAxisButton = false;
        Vector2 _primary2DAxisValue = Vector2.zero;

        bool _secondary2DAxisButton = false;
        Vector2 _secondary2DAxisValue = Vector2.zero;

        bool _primaryButton = false;
        bool _secondaryButton = false;
        bool _menuButton = false;

        bool _primary2DAxisUp = false;
        bool _primary2DAxisDown = false;
        bool _primary2DAxisLeft = false;
        bool _primary2DAxisRight = false;

        bool _secondary2DAxisUp = false;
        bool _secondary2DAxisDown = false;
        bool _secondary2DAxisLeft = false;
        bool _secondary2DAxisRight = false;

        #endregion

        #region UnityEvents: Accessable from the editor

        [Header("Event Registration")]
        [Space(20)]

        // Trigger
        [Tooltip("Event: Trigger Pressed")]
        [HideInInspector] public UnityEvent OnTriggerPress = new UnityEvent();

        [Tooltip("Event: Trigger Released")]
        [HideInInspector] public UnityEvent OnTriggerRelease = new UnityEvent();
        [Tooltip("Event: Trigger Touched")]
        [HideInInspector] public UnityEvent OnTriggerTouch = new UnityEvent();
        [Tooltip("Event: Trigger Untouched")]
        [HideInInspector] public UnityEvent OnTriggerUntouch = new UnityEvent();
        [Tooltip("Event: Trigger Value")]
        [HideInInspector] public UnityEventValue OnTriggerValue = new UnityEventValue();

        // Grip
        [Tooltip("Event: Grip Pressed")]
        [HideInInspector] public UnityEvent OnGripPress = new UnityEvent();
        [Tooltip("Event: Grip Released")]
        [HideInInspector] public UnityEvent OnGripRelease = new UnityEvent();
        [Tooltip("Event: Grip Value")]
        [HideInInspector] public UnityEventValue OnGripValue = new UnityEventValue();

        //Other Buttons
        [Tooltip("Event: Primary Button Pressed (X/A)")]
        [HideInInspector] public UnityEvent OnPrimaryButtonPress = new UnityEvent();

        [Tooltip("Event: Primary Button Released (X/A)")]
        [HideInInspector] public UnityEvent OnPrimaryButtonRelease = new UnityEvent();

        [Tooltip("Event: Secondary Button Pressed (Y/B)")]
        [HideInInspector] public UnityEvent OnSecondaryButtonPress = new UnityEvent();

        [Tooltip("Event: Secondary Button Released (Y/B)")]
        [HideInInspector] public UnityEvent OnSecondaryButtonRelease = new UnityEvent();

        [Tooltip("Event: Menu Button Pressed (Y/B)")]
        [HideInInspector] public UnityEvent OnMenuButtonPress = new UnityEvent();

        [Tooltip("Event: Menu Button Released (Y/B)")]
        [HideInInspector] public UnityEvent OnMenuButtonRelease = new UnityEvent();

        //Primary Axis
        [Tooltip("Event: Primary 2D Axis Pressed (Thumbstick Pressed)")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisPress = new UnityEvent();

        [Tooltip("Event: Primary 2D Axis Released (Thumbstick Pressed)")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisRelease = new UnityEvent();

        [Tooltip("Event: Primary 2D Axis Value (X and Y Axis)")]
        [HideInInspector] public UnityEventValues OnPrimary2DAxisValue = new UnityEventValues();

        [Tooltip("Event: Primary 2D Axis Up Pressed (Value of Axis > minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisUpPress = new UnityEvent();

        [Tooltip("Event: Primary 2D Axis Up Released (Value of Axis < minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisUpRelease = new UnityEvent();

        [Tooltip("Event: Primary 2D Axis Down Pressed (Value of Axis < -minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisDownPress = new UnityEvent();

        [Tooltip("Event: Primary 2D Axis Down Released (Value of Axis > -minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisDownRelease = new UnityEvent();

        [Tooltip("Event: Primary 2D Axis Right Pressed (Value of Axis > minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisRightPress = new UnityEvent();

        [Tooltip("Event: Primary 2D Axis Right Released (Value of Axis < minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisRightRelease = new UnityEvent();

        [Tooltip("Event: Primary 2D Axis Left Pressed (Value of Axis < -minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisLeftPress = new UnityEvent();

        [Tooltip("Event: Primary 2D Axis Left Released (Value of Axis > -minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnPrimary2DAxisLeftRelease = new UnityEvent();

        // Secondary Axis
        [Tooltip("Event: Secondary 2D Axis Pressed (Thumbstick Pressed)")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisPress = new UnityEvent();

        [Tooltip("Event: Secondary 2D Axis Released (Thumbstick Pressed)")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisRelease = new UnityEvent();

        [Tooltip("Event: Secondary 2D Axis Value (X and Y Axis)")]
        [HideInInspector] public UnityEventValues OnSecondary2DAxisValue = new UnityEventValues();

        [Tooltip("Event: Secondary 2D Axis Up Pressed (Value of Axis > minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisUpPress = new UnityEvent();

        [Tooltip("Event: Secondary 2D Axis Up Released (Value of Axis < minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisUpRelease = new UnityEvent();

        [Tooltip("Event: Secondary 2D Axis Down Pressed (Value of Axis < -minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisDownPress = new UnityEvent();

        [Tooltip("Event: Secondary 2D Axis Down Released (Value of Axis > -minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisDownRelease = new UnityEvent();

        [Tooltip("Event: Secondary 2D Axis Right Pressed (Value of Axis > minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisRightPress = new UnityEvent();

        [Tooltip("Event: Secondary 2D Axis Right Released (Value of Axis < minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisRightRelease = new UnityEvent();

        [Tooltip("Event: Secondary 2D Axis Left Pressed (Value of Axis < -minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisLeftPress = new UnityEvent();

        [Tooltip("Event: Secondary 2D Axis Left Released (Value of Axis > -minAxisIsPress )")]
        [HideInInspector] public UnityEvent OnSecondary2DAxisLeftRelease = new UnityEvent();

        #endregion

        #region General Methods

        void GetDevice()
        {
            InputDevices.GetDevicesAtXRNode(Hand, devices);
            device = devices.FirstOrDefault();
        }

        private void UpdateXRDevice()
        {
            if (!keyboardDebug)
            {
                if (!device.isValid)
                {
                    GetDevice();
                }

                // These ranged, non-boolean inputs invoke the events above that are not targetable from the editor

                // Capture trigger value
                if (device.TryGetFeatureValue(CommonUsages.trigger, out _triggerValue))
                {
                    if (_triggerValue > minAxisValue) TriggerValueAction = _triggerValue;
                    else TriggerValueAction = 0f;
                }

                // Capture grip value
                if (device.TryGetFeatureValue(CommonUsages.grip, out _gripValue))
                {
                    if (_gripValue > minAxisValue) GripValueAction = _gripValue;
                    else GripValueAction = 0f;
                }
                //don't forget to use an absolute value for the axes

                // Capture primary 2D Axis
                if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _primary2DAxisValue))
                {
                    if (Mathf.Abs(_primary2DAxisValue.x) > minAxisValue ||
                        Mathf.Abs(_primary2DAxisValue.y) > minAxisValue) Primary2DAxisValueAction = _primary2DAxisValue;
                    else Primary2DAxisValueAction = Vector2.zero;
                }

                // Capture trigger button      
                _primary2DAxisUp = primary2DAxisYValue > minAxisIsPress;
                if (_primary2DAxisUp) Primary2DAxisUpAction = true;
                else Primary2DAxisUpAction = false;

                _primary2DAxisDown = primary2DAxisYValue < -minAxisIsPress;
                if (_primary2DAxisDown) Primary2DAxisDownAction = true;
                else Primary2DAxisDownAction = false;

                _primary2DAxisRight = primary2DAxisXValue > minAxisIsPress;
                if (_primary2DAxisRight) Primary2DAxisRightAction = true;
                else Primary2DAxisRightAction = false;

                _primary2DAxisLeft = primary2DAxisXValue < -minAxisIsPress;
                if (_primary2DAxisLeft) Primary2DAxisLeftAction = true;
                else Primary2DAxisLeftAction = false;


                // Capture trigger button      
                _secondary2DAxisUp = secondary2DAxisYValue > minAxisIsPress;
                if (_secondary2DAxisUp) Secondary2DAxisUpAction = true;
                else Secondary2DAxisUpAction = false;

                _secondary2DAxisDown = secondary2DAxisYValue < -minAxisIsPress;
                if (_secondary2DAxisDown) Secondary2DAxisDownAction = true;
                else Secondary2DAxisDownAction = false;

                _secondary2DAxisRight = secondary2DAxisXValue > minAxisIsPress;
                if (_secondary2DAxisRight) Secondary2DAxisRightAction = true;
                else Secondary2DAxisRightAction = false;

                _secondary2DAxisLeft = secondary2DAxisXValue < -minAxisIsPress;
                if (_primary2DAxisLeft) Secondary2DAxisLeftAction = true;
                else Primary2DAxisLeftAction = false;

                // Capture secondary 2D Axis
                if (device.TryGetFeatureValue(CommonUsages.secondary2DAxis, out _secondary2DAxisValue))
                {
                    if (Mathf.Abs(_secondary2DAxisValue.x) > minAxisValue ||
                        Mathf.Abs(_secondary2DAxisValue.y) > minAxisValue)
                        Secondary2DAxisValueAction = _secondary2DAxisValue;
                    else Secondary2DAxisValueAction = Vector2.zero;
                }


                // These press/release inputs invoke the public, editor-definable events above

                // Capture trigger button
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out _triggerButton))
                {
                    if (_triggerButton && triggerValue > minAxisIsPress) TriggerButtonAction = true;
                    else TriggerButtonAction = false;
                }

                // Capture trigger button
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out _triggerTouch))
                {
                    if (_triggerTouch) TriggerTouchAction = true;
                    else TriggerTouchAction = false;
                }

                // Capture grip button
                if (device.TryGetFeatureValue(CommonUsages.gripButton, out _gripButton))
                {
                    if (_gripButton) GripButtonAction = true;
                    else GripButtonAction = false;
                }

                // Capture primary 2d axis button
                if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out _primary2DAxisButton))
                {
                    if (_primary2DAxisButton) Primary2DAxisButtonAction = true;
                    else Primary2DAxisButtonAction = false;
                }

                // Capture secondary 2d axis button
                if (device.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out _secondary2DAxisButton))
                {
                    if (_secondary2DAxisButton) Secondary2DAxisButtonAction = true;
                    else Secondary2DAxisButtonAction = false;
                }

                // Capture primary button
                if (device.TryGetFeatureValue(CommonUsages.primaryButton, out _primaryButton))
                {
                    if (_primaryButton) PrimaryButtonAction = true;
                    else PrimaryButtonAction = false;
                }

                // Capture secondary button
                if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out _secondaryButton))
                {
                    if (_secondaryButton) SecondaryButtonAction = true;
                    else SecondaryButtonAction = false;
                }

                // Capture menu button
                if (device.TryGetFeatureValue(CommonUsages.menuButton, out _menuButton))
                {
                    if (_menuButton) MenuButtonAction = true;
                    else MenuButtonAction = false;
                }
            }
        }

        private void UpdateKeyboardDebug()
        {
            // Keyboard Debug. Disables controller input. For clarity, recommend only turning on with left or right hand script at a time.
            if (keyboardDebug)
            {
                // trigger
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //you could also use GetKeyDown and GetKeyUp for Press (true) and Release (false)
                    TriggerButtonAction = !TriggerButtonAction;
                }

                if (Input.GetKeyDown(KeyCode.PageUp) && triggerValue < 1)
                {
                    TriggerValueAction += debugAxisValueIncrement;
                }

                if (Input.GetKeyDown(KeyCode.PageDown) && triggerValue > 0)
                {
                    TriggerValueAction -= debugAxisValueIncrement;
                }

                // grip
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    GripButtonAction = !GripButtonAction;
                }

                if (Input.GetKeyDown(KeyCode.KeypadPlus) && gripValue < 1)
                {
                    GripValueAction += debugAxisValueIncrement;
                }

                if (Input.GetKeyDown(KeyCode.KeypadMinus) && gripValue > 0)
                {
                    GripValueAction -= debugAxisValueIncrement;
                }

                // primary 2D axis
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Primary2DAxisButtonAction = !Primary2DAxisButtonAction;
                }

                if (Input.GetKeyDown(KeyCode.UpArrow) && primary2DAxisValue.y < 1)
                {
                    _primary2DAxisValue += Vector2.up * debugAxisValueIncrement;
                    Primary2DAxisValueAction = _primary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.DownArrow) && primary2DAxisValue.y > -1)
                {
                    _primary2DAxisValue -= Vector2.up * debugAxisValueIncrement;
                    Primary2DAxisValueAction = _primary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow) && primary2DAxisValue.x < 1)
                {
                    _primary2DAxisValue += Vector2.right * debugAxisValueIncrement;
                    Primary2DAxisValueAction = _primary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) && primary2DAxisValue.x > -1)
                {
                    _primary2DAxisValue -= Vector2.right * debugAxisValueIncrement;
                    Primary2DAxisValueAction = _primary2DAxisValue;
                }

                // secondary 2D axis
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    Secondary2DAxisButtonAction = !Secondary2DAxisButtonAction;
                }

                if (Input.GetKeyDown(KeyCode.Keypad8) && secondary2DAxisValue.y < 1)
                {
                    _secondary2DAxisValue += Vector2.up * debugAxisValueIncrement;
                    Secondary2DAxisValueAction = _secondary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.Keypad2) && secondary2DAxisValue.y > -1)
                {
                    _secondary2DAxisValue -= Vector2.up * debugAxisValueIncrement;
                    Secondary2DAxisValueAction = _secondary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.Keypad6) && secondary2DAxisValue.x < 1)
                {
                    _secondary2DAxisValue += Vector2.right * debugAxisValueIncrement;
                    Secondary2DAxisValueAction = _secondary2DAxisValue;
                }

                if (Input.GetKeyDown(KeyCode.Keypad4) && secondary2DAxisValue.x > -1)
                {
                    _secondary2DAxisValue -= Vector2.right * debugAxisValueIncrement;
                    Secondary2DAxisValueAction = _secondary2DAxisValue;
                }

                // primary button
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    PrimaryButtonAction = !PrimaryButtonAction;
                }

                // secondary button
                if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    SecondaryButtonAction = !SecondaryButtonAction;
                }

                // menu button
                if (Input.GetKeyDown(KeyCode.BackQuote))
                {
                    MenuButtonAction = !MenuButtonAction;
                }
            }
        }

        #endregion

        #region Internal Evaluation: Used to Get or Set target value and invoke respective events (like GripPressed or GripReleased)

        private bool TriggerTouchAction
        {
            get { return triggerTouch; }
            set
            {
                if (value == triggerTouch) return;
                triggerTouch = value;

                if (value == true) OnTriggerTouch?.Invoke();
                else OnTriggerUntouch?.Invoke();

            }
        }

        private bool TriggerButtonAction
        {
            get { return triggerButton; }
            set
            {
                if (value == triggerButton) return;
                triggerButton = value;

                if (value == true) OnTriggerPress?.Invoke();
                else OnTriggerRelease?.Invoke();

            }
        }

        private bool GripButtonAction
        {
            get { return gripButton; }
            set
            {
                if (value == gripButton) return;
                gripButton = value;

                if (value == true) OnGripPress?.Invoke();
                else OnGripRelease?.Invoke();

            }
        }

        private bool Primary2DAxisButtonAction
        {
            get { return primary2DAxisButton; }
            set
            {
                if (value == primary2DAxisButton) return;
                primary2DAxisButton = value;

                if (value == true) OnPrimary2DAxisPress?.Invoke();
                else OnPrimary2DAxisRelease?.Invoke();

                //Debug.Log($"Primary 2D Axis Button Press {primary2DAxisButton} on {XRController}");
            }
        }

        private float TriggerValueAction
        {
            get { return triggerValue; }
            set
            {
                if (value == triggerValue) return;
                triggerValue = value;

                OnTriggerValue.Invoke(triggerValue);
            }
        }

        private bool Secondary2DAxisButtonAction
        {
            get { return secondary2DAxisButton; }
            set
            {
                if (value == secondary2DAxisButton) return;
                secondary2DAxisButton = value;

                if (value == true) OnSecondary2DAxisPress?.Invoke();
                else OnSecondary2DAxisRelease?.Invoke();

            }
        }

        private bool PrimaryButtonAction
        {
            get { return primaryButton; }
            set
            {
                if (value == primaryButton) return;
                primaryButton = value;

                if (value == true) OnPrimaryButtonPress?.Invoke();
                else OnPrimaryButtonRelease?.Invoke();

            }
        }

        private bool SecondaryButtonAction
        {
            get { return secondaryButton; }
            set
            {
                if (value == secondaryButton) return;
                secondaryButton = value;

                if (value == true) OnSecondaryButtonPress?.Invoke();
                else OnSecondaryButtonRelease?.Invoke();

            }
        }

        private bool MenuButtonAction
        {
            get { return menuButton; }
            set
            {
                if (value == menuButton) return;
                menuButton = value;

                if (value == true) OnMenuButtonPress?.Invoke();
                else OnMenuButtonRelease?.Invoke();

            }
        }

        private bool Primary2DAxisUpAction
        {
            get { return primary2DAxisUp; }
            set
            {
                if (value == primary2DAxisUp) return;
                primary2DAxisUp = value;

                if (value == true) OnPrimary2DAxisUpPress?.Invoke();
                else OnPrimary2DAxisUpRelease?.Invoke();

            }
        }

        private bool Primary2DAxisDownAction
        {
            get { return primary2DAxisDown; }
            set
            {
                if (value == primary2DAxisDown) return;
                primary2DAxisDown = value;

                if (value == true) OnPrimary2DAxisDownPress?.Invoke();
                else OnPrimary2DAxisDownRelease?.Invoke();
            }
        }

        private bool Primary2DAxisRightAction
        {
            get { return primary2DAxisRight; }
            set
            {
                if (value == primary2DAxisRight) return;
                primary2DAxisRight = value;

                if (value == true) OnPrimary2DAxisRightPress?.Invoke();
                else OnPrimary2DAxisRightRelease?.Invoke();
            }
        }

        private bool Primary2DAxisLeftAction
        {
            get { return primary2DAxisLeft; }
            set
            {
                if (value == primary2DAxisLeft) return;
                primary2DAxisLeft = value;

                if (value == true) OnPrimary2DAxisLeftPress?.Invoke();
                else OnPrimary2DAxisLeftRelease?.Invoke();
            }
        }

        private bool Secondary2DAxisUpAction
        {
            get { return secondary2DAxisUp; }
            set
            {
                if (value == secondary2DAxisUp) return;
                secondary2DAxisUp = value;

                if (value == true) OnSecondary2DAxisUpPress?.Invoke();
                else OnSecondary2DAxisUpRelease?.Invoke();

            }
        }

        private bool Secondary2DAxisDownAction
        {
            get { return secondary2DAxisDown; }
            set
            {
                if (value == secondary2DAxisDown) return;
                secondary2DAxisDown = value;

                if (value == true) OnSecondary2DAxisDownPress?.Invoke();
                else OnSecondary2DAxisDownRelease?.Invoke();
            }
        }

        private bool Secondary2DAxisRightAction
        {
            get { return secondary2DAxisRight; }
            set
            {
                if (value == secondary2DAxisRight) return;
                secondary2DAxisRight = value;

                if (value == true) OnSecondary2DAxisRightPress?.Invoke();
                else OnSecondary2DAxisRightRelease?.Invoke();
            }
        }

        private bool Secondary2DAxisLeftAction
        {
            get { return secondary2DAxisLeft; }
            set
            {
                if (value == secondary2DAxisLeft) return;
                secondary2DAxisLeft = value;

                if (value == true) OnSecondary2DAxisLeftPress?.Invoke();
                else OnSecondary2DAxisLeftRelease?.Invoke();
            }
        }

        private float GripValueAction
        {
            get { return gripValue; }
            set
            {
                if (value == gripValue) return;
                gripValue = value;

                OnGripValue.Invoke(gripValue);

            }
        }

        private Vector2 Primary2DAxisValueAction
        {
            get { return primary2DAxisValue; }
            set
            {
                if (value == primary2DAxisValue) return;
                primary2DAxisValue = value;
                primary2DAxisXValue = primary2DAxisValue.x;
                primary2DAxisYValue = primary2DAxisValue.y;

                OnPrimary2DAxisValue.Invoke(primary2DAxisXValue, primary2DAxisYValue);
            }
        }

        private Vector2 Secondary2DAxisValueAction
        {
            get { return secondary2DAxisValue; }
            set
            {
                if (value == secondary2DAxisValue) return;
                secondary2DAxisValue = value;
                secondary2DAxisXValue = secondary2DAxisValue.x;
                secondary2DAxisYValue = secondary2DAxisValue.y;

                OnSecondary2DAxisValue.Invoke(secondary2DAxisXValue, secondary2DAxisYValue);


            }
        }

        #endregion

        #region Unity MonoBehaviour Functions

        void OnEnable()
        {
            if (!device.isValid)
            {
                GetDevice();
            }
        }

        void Update()
        {
            UpdateXRDevice();
            UpdateKeyboardDebug();
        }

        #endregion

    }
}