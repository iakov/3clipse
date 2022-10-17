//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/3ClipseGame/Steam/Core/GameSource/Accessors/InputAccessor/Inputs/PlayerInput/PlayerInputMap.inputactions
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

public partial class @PlayerInputMap : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputMap"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""3e905f28-2b14-4713-96f6-82f4ad9a547b"",
            ""actions"": [
                {
                    ""name"": ""EnvironmentInteraction"",
                    ""type"": ""Button"",
                    ""id"": ""82217cc1-17c1-4273-b37f-ad6444c2b751"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""20a8ef7a-5a33-46f2-815f-a88350f92ba6"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnvironmentInteraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_EnvironmentInteraction = m_PlayerInput.FindAction("EnvironmentInteraction", throwIfNotFound: true);
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

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_EnvironmentInteraction;
    public struct PlayerInputActions
    {
        private @PlayerInputMap m_Wrapper;
        public PlayerInputActions(@PlayerInputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @EnvironmentInteraction => m_Wrapper.m_PlayerInput_EnvironmentInteraction;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @EnvironmentInteraction.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnEnvironmentInteraction;
                @EnvironmentInteraction.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnEnvironmentInteraction;
                @EnvironmentInteraction.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnEnvironmentInteraction;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EnvironmentInteraction.started += instance.OnEnvironmentInteraction;
                @EnvironmentInteraction.performed += instance.OnEnvironmentInteraction;
                @EnvironmentInteraction.canceled += instance.OnEnvironmentInteraction;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    public interface IPlayerInputActions
    {
        void OnEnvironmentInteraction(InputAction.CallbackContext context);
    }
}
