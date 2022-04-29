//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Inputs/DefaultController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @DefaultController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultController"",
    ""maps"": [
        {
            ""name"": ""PlayerMainActions"",
            ""id"": ""f74d4ef8-1330-4e94-8cba-aa6db1cd7898"",
            ""actions"": [
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""676e0f9d-0ed8-4f02-84b5-52949e5e20c0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Impulse"",
                    ""type"": ""Button"",
                    ""id"": ""c068eeb9-2cc4-4628-b686-e58fd4f3f65d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WeaponRotation"",
                    ""type"": ""Value"",
                    ""id"": ""9e8c6012-0a63-49a7-abb0-1743a3d9b318"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""d3d724a2-1525-411c-aa42-cafae86121a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e252c844-8f1b-4dae-8def-c1129892e421"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControl"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7316ff58-1f05-4449-b87e-1b65ffe562d0"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControl"",
                    ""action"": ""Impulse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""667f375d-7de9-48a9-83b1-af7c79dd153e"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControl"",
                    ""action"": ""WeaponRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d818ff4-b6dc-438b-b821-f7ef192a9244"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerControl"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UIControls"",
            ""id"": ""c724b3ef-e47f-4898-b193-317517a6587a"",
            ""actions"": [
                {
                    ""name"": ""MenuValidate"",
                    ""type"": ""Button"",
                    ""id"": ""ebfd673d-ec90-40e5-b617-adbfaf9a3122"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MenuReturn"",
                    ""type"": ""Button"",
                    ""id"": ""5f77447a-f39c-4344-ad18-501fe8a1fa82"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""b951e6df-497f-40d0-9183-c6adf7848910"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2ed22af0-c057-43ef-ab1e-fbc2ad2ef961"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keybord"",
                    ""action"": ""MenuValidate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2c53065-b780-49a4-91ca-85ba5721d474"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuValidate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34e49e3e-fcd7-474a-bc6d-e8244e85f0eb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuReturn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f5dcdbc-ab3a-4792-8baa-d40cba7767d7"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuReturn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7dbdd390-573b-4d42-8502-b6c54f31579a"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9f95fe3-178c-4617-b946-c9ca4bc421b2"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keybord"",
            ""bindingGroup"": ""Keybord"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""PlayerControl"",
            ""bindingGroup"": ""PlayerControl"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerMainActions
        m_PlayerMainActions = asset.FindActionMap("PlayerMainActions", throwIfNotFound: true);
        m_PlayerMainActions_Rotation = m_PlayerMainActions.FindAction("Rotation", throwIfNotFound: true);
        m_PlayerMainActions_Impulse = m_PlayerMainActions.FindAction("Impulse", throwIfNotFound: true);
        m_PlayerMainActions_WeaponRotation = m_PlayerMainActions.FindAction("WeaponRotation", throwIfNotFound: true);
        m_PlayerMainActions_Fire = m_PlayerMainActions.FindAction("Fire", throwIfNotFound: true);
        // UIControls
        m_UIControls = asset.FindActionMap("UIControls", throwIfNotFound: true);
        m_UIControls_MenuValidate = m_UIControls.FindAction("MenuValidate", throwIfNotFound: true);
        m_UIControls_MenuReturn = m_UIControls.FindAction("MenuReturn", throwIfNotFound: true);
        m_UIControls_Start = m_UIControls.FindAction("Start", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMainActions
    private readonly InputActionMap m_PlayerMainActions;
    private IPlayerMainActionsActions m_PlayerMainActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerMainActions_Rotation;
    private readonly InputAction m_PlayerMainActions_Impulse;
    private readonly InputAction m_PlayerMainActions_WeaponRotation;
    private readonly InputAction m_PlayerMainActions_Fire;
    public struct PlayerMainActionsActions
    {
        private @DefaultController m_Wrapper;
        public PlayerMainActionsActions(@DefaultController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotation => m_Wrapper.m_PlayerMainActions_Rotation;
        public InputAction @Impulse => m_Wrapper.m_PlayerMainActions_Impulse;
        public InputAction @WeaponRotation => m_Wrapper.m_PlayerMainActions_WeaponRotation;
        public InputAction @Fire => m_Wrapper.m_PlayerMainActions_Fire;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMainActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMainActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMainActionsActions instance)
        {
            if (m_Wrapper.m_PlayerMainActionsActionsCallbackInterface != null)
            {
                @Rotation.started -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnRotation;
                @Impulse.started -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnImpulse;
                @Impulse.performed -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnImpulse;
                @Impulse.canceled -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnImpulse;
                @WeaponRotation.started -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnWeaponRotation;
                @WeaponRotation.performed -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnWeaponRotation;
                @WeaponRotation.canceled -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnWeaponRotation;
                @Fire.started -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerMainActionsActionsCallbackInterface.OnFire;
            }
            m_Wrapper.m_PlayerMainActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Impulse.started += instance.OnImpulse;
                @Impulse.performed += instance.OnImpulse;
                @Impulse.canceled += instance.OnImpulse;
                @WeaponRotation.started += instance.OnWeaponRotation;
                @WeaponRotation.performed += instance.OnWeaponRotation;
                @WeaponRotation.canceled += instance.OnWeaponRotation;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
            }
        }
    }
    public PlayerMainActionsActions @PlayerMainActions => new PlayerMainActionsActions(this);

    // UIControls
    private readonly InputActionMap m_UIControls;
    private IUIControlsActions m_UIControlsActionsCallbackInterface;
    private readonly InputAction m_UIControls_MenuValidate;
    private readonly InputAction m_UIControls_MenuReturn;
    private readonly InputAction m_UIControls_Start;
    public struct UIControlsActions
    {
        private @DefaultController m_Wrapper;
        public UIControlsActions(@DefaultController wrapper) { m_Wrapper = wrapper; }
        public InputAction @MenuValidate => m_Wrapper.m_UIControls_MenuValidate;
        public InputAction @MenuReturn => m_Wrapper.m_UIControls_MenuReturn;
        public InputAction @Start => m_Wrapper.m_UIControls_Start;
        public InputActionMap Get() { return m_Wrapper.m_UIControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIControlsActions set) { return set.Get(); }
        public void SetCallbacks(IUIControlsActions instance)
        {
            if (m_Wrapper.m_UIControlsActionsCallbackInterface != null)
            {
                @MenuValidate.started -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnMenuValidate;
                @MenuValidate.performed -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnMenuValidate;
                @MenuValidate.canceled -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnMenuValidate;
                @MenuReturn.started -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnMenuReturn;
                @MenuReturn.performed -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnMenuReturn;
                @MenuReturn.canceled -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnMenuReturn;
                @Start.started -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnStart;
            }
            m_Wrapper.m_UIControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MenuValidate.started += instance.OnMenuValidate;
                @MenuValidate.performed += instance.OnMenuValidate;
                @MenuValidate.canceled += instance.OnMenuValidate;
                @MenuReturn.started += instance.OnMenuReturn;
                @MenuReturn.performed += instance.OnMenuReturn;
                @MenuReturn.canceled += instance.OnMenuReturn;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
            }
        }
    }
    public UIControlsActions @UIControls => new UIControlsActions(this);
    private int m_KeybordSchemeIndex = -1;
    public InputControlScheme KeybordScheme
    {
        get
        {
            if (m_KeybordSchemeIndex == -1) m_KeybordSchemeIndex = asset.FindControlSchemeIndex("Keybord");
            return asset.controlSchemes[m_KeybordSchemeIndex];
        }
    }
    private int m_PlayerControlSchemeIndex = -1;
    public InputControlScheme PlayerControlScheme
    {
        get
        {
            if (m_PlayerControlSchemeIndex == -1) m_PlayerControlSchemeIndex = asset.FindControlSchemeIndex("PlayerControl");
            return asset.controlSchemes[m_PlayerControlSchemeIndex];
        }
    }
    public interface IPlayerMainActionsActions
    {
        void OnRotation(InputAction.CallbackContext context);
        void OnImpulse(InputAction.CallbackContext context);
        void OnWeaponRotation(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
    }
    public interface IUIControlsActions
    {
        void OnMenuValidate(InputAction.CallbackContext context);
        void OnMenuReturn(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
    }
}
