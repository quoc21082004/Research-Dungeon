//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input system.inputactions
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

public partial class @Inputsystem: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputsystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input system"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""942c10db-cc1c-4bdc-8b41-e6fe63d19e4c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6e7a1520-ad86-4cb9-bff3-b425db494957"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Walk Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""d7728098-2cb7-4df3-aede-69aeba2ddcb0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""fdd9c790-add7-44a3-915b-96cac890d8bd"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Clamp(min=-0.1,max=0.1),Invert"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""00ca640b-d935-4593-8157-c05846ea39b3"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e2062cb9-1b15-46a2-838c-2f8d72a0bdd9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""320bffee-a40b-4347-ac70-c210eb8bc73a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d2581a9b-1d11-4566-b27d-b92aff5fabbc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fcfe95b8-67b9-4526-84b5-5d0bc98d6400"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5ec7afe6-a610-404e-af8e-b63afe2d99cf"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8adf2949-73e9-402a-a551-6405af34baf4"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerAbility"",
            ""id"": ""6704a07b-884e-44bb-a661-3d7b23dc31fc"",
            ""actions"": [
                {
                    ""name"": ""Ability0"",
                    ""type"": ""Button"",
                    ""id"": ""760f50ca-9d53-4032-b1c6-89f703f11202"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability1"",
                    ""type"": ""Button"",
                    ""id"": ""9178d928-5aa5-4d93-bd19-4c55f70ac76e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability2"",
                    ""type"": ""Button"",
                    ""id"": ""3d78c891-40e7-4b0e-8c87-e7390d6a1bca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability3"",
                    ""type"": ""Button"",
                    ""id"": ""0ebef329-e1a1-4fca-adb7-ff7d84794b12"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Ability4"",
                    ""type"": ""Button"",
                    ""id"": ""cb568d4f-a3d4-4648-bd5e-61ff2a1d031b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Consume1"",
                    ""type"": ""Button"",
                    ""id"": ""7cf11244-0f88-4d56-90cc-eb12c38aa9be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Consume2"",
                    ""type"": ""Button"",
                    ""id"": ""e915bf68-491c-4be4-b6be-9e79cd5d4e3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ee2fd626-b10b-49fe-8615-1bfafd2dbb46"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0efe7acd-6c98-4ccd-9a54-5d6412484c70"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b00e2f2d-f688-435d-91d0-8d6e4110af6e"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d491869d-f370-4ca7-9f05-20d64ec61a4d"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15c7c9e7-8ff5-43d1-8111-ff18d37815ec"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Consume1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee775cac-4efb-4a48-9026-bf8784375ed4"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Consume2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab5a5bec-4131-43a3-86a1-9c9115b74289"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""58249c31-b2dc-4876-9e85-84adfa7bb69a"",
            ""actions"": [
                {
                    ""name"": ""OpenMenu"",
                    ""type"": ""Button"",
                    ""id"": ""c5be0eab-6335-485c-a522-c42c708b18f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CloseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""018ca469-185b-4870-a0d6-138d6b1a705e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenShop"",
                    ""type"": ""Button"",
                    ""id"": ""aec0060e-3ce9-4022-a085-b489446aea67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenQuest"",
                    ""type"": ""Button"",
                    ""id"": ""7cafaa19-6559-41d2-b938-31716f2d8e5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CloseQuest"",
                    ""type"": ""Button"",
                    ""id"": ""0baf6e57-ef2f-415a-8d4d-bd1dcab78076"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenBag"",
                    ""type"": ""Button"",
                    ""id"": ""7c5cb722-6320-44d6-bac9-1b9944954c20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CloseBag"",
                    ""type"": ""Button"",
                    ""id"": ""d62a1633-6b9f-46ab-961d-92714cfa41d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenInteract"",
                    ""type"": ""Button"",
                    ""id"": ""c27c3c1b-9925-4c8e-a54c-2bcaceb65097"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""82d9850b-153a-49b9-97a1-4d4e90ea1576"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7cd1c6d6-5198-488f-b9a8-b2b654f9c569"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa8572eb-5ae5-46c3-87da-0990d0289030"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenShop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3e7d924-85f6-43a1-a820-80668d833e86"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenQuest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a12d812-004b-4f0b-889e-e7699d9c80d6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseQuest"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c0bfbeb-e588-4565-8774-87aee33bc8a4"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenBag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27155374-1a99-45b3-a12b-1bb9a6189457"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseBag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ccac9f8-014e-47dd-84de-f700ebe79f15"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
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
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_WalkToggle = m_Player.FindAction("Walk Toggle", throwIfNotFound: true);
        m_Player_Zoom = m_Player.FindAction("Zoom", throwIfNotFound: true);
        // PlayerAbility
        m_PlayerAbility = asset.FindActionMap("PlayerAbility", throwIfNotFound: true);
        m_PlayerAbility_Ability0 = m_PlayerAbility.FindAction("Ability0", throwIfNotFound: true);
        m_PlayerAbility_Ability1 = m_PlayerAbility.FindAction("Ability1", throwIfNotFound: true);
        m_PlayerAbility_Ability2 = m_PlayerAbility.FindAction("Ability2", throwIfNotFound: true);
        m_PlayerAbility_Ability3 = m_PlayerAbility.FindAction("Ability3", throwIfNotFound: true);
        m_PlayerAbility_Ability4 = m_PlayerAbility.FindAction("Ability4", throwIfNotFound: true);
        m_PlayerAbility_Consume1 = m_PlayerAbility.FindAction("Consume1", throwIfNotFound: true);
        m_PlayerAbility_Consume2 = m_PlayerAbility.FindAction("Consume2", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_OpenMenu = m_UI.FindAction("OpenMenu", throwIfNotFound: true);
        m_UI_CloseMenu = m_UI.FindAction("CloseMenu", throwIfNotFound: true);
        m_UI_OpenShop = m_UI.FindAction("OpenShop", throwIfNotFound: true);
        m_UI_OpenQuest = m_UI.FindAction("OpenQuest", throwIfNotFound: true);
        m_UI_CloseQuest = m_UI.FindAction("CloseQuest", throwIfNotFound: true);
        m_UI_OpenBag = m_UI.FindAction("OpenBag", throwIfNotFound: true);
        m_UI_CloseBag = m_UI.FindAction("CloseBag", throwIfNotFound: true);
        m_UI_OpenInteract = m_UI.FindAction("OpenInteract", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_WalkToggle;
    private readonly InputAction m_Player_Zoom;
    public struct PlayerActions
    {
        private @Inputsystem m_Wrapper;
        public PlayerActions(@Inputsystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @WalkToggle => m_Wrapper.m_Player_WalkToggle;
        public InputAction @Zoom => m_Wrapper.m_Player_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @WalkToggle.started += instance.OnWalkToggle;
            @WalkToggle.performed += instance.OnWalkToggle;
            @WalkToggle.canceled += instance.OnWalkToggle;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @WalkToggle.started -= instance.OnWalkToggle;
            @WalkToggle.performed -= instance.OnWalkToggle;
            @WalkToggle.canceled -= instance.OnWalkToggle;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // PlayerAbility
    private readonly InputActionMap m_PlayerAbility;
    private List<IPlayerAbilityActions> m_PlayerAbilityActionsCallbackInterfaces = new List<IPlayerAbilityActions>();
    private readonly InputAction m_PlayerAbility_Ability0;
    private readonly InputAction m_PlayerAbility_Ability1;
    private readonly InputAction m_PlayerAbility_Ability2;
    private readonly InputAction m_PlayerAbility_Ability3;
    private readonly InputAction m_PlayerAbility_Ability4;
    private readonly InputAction m_PlayerAbility_Consume1;
    private readonly InputAction m_PlayerAbility_Consume2;
    public struct PlayerAbilityActions
    {
        private @Inputsystem m_Wrapper;
        public PlayerAbilityActions(@Inputsystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Ability0 => m_Wrapper.m_PlayerAbility_Ability0;
        public InputAction @Ability1 => m_Wrapper.m_PlayerAbility_Ability1;
        public InputAction @Ability2 => m_Wrapper.m_PlayerAbility_Ability2;
        public InputAction @Ability3 => m_Wrapper.m_PlayerAbility_Ability3;
        public InputAction @Ability4 => m_Wrapper.m_PlayerAbility_Ability4;
        public InputAction @Consume1 => m_Wrapper.m_PlayerAbility_Consume1;
        public InputAction @Consume2 => m_Wrapper.m_PlayerAbility_Consume2;
        public InputActionMap Get() { return m_Wrapper.m_PlayerAbility; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerAbilityActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerAbilityActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerAbilityActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerAbilityActionsCallbackInterfaces.Add(instance);
            @Ability0.started += instance.OnAbility0;
            @Ability0.performed += instance.OnAbility0;
            @Ability0.canceled += instance.OnAbility0;
            @Ability1.started += instance.OnAbility1;
            @Ability1.performed += instance.OnAbility1;
            @Ability1.canceled += instance.OnAbility1;
            @Ability2.started += instance.OnAbility2;
            @Ability2.performed += instance.OnAbility2;
            @Ability2.canceled += instance.OnAbility2;
            @Ability3.started += instance.OnAbility3;
            @Ability3.performed += instance.OnAbility3;
            @Ability3.canceled += instance.OnAbility3;
            @Ability4.started += instance.OnAbility4;
            @Ability4.performed += instance.OnAbility4;
            @Ability4.canceled += instance.OnAbility4;
            @Consume1.started += instance.OnConsume1;
            @Consume1.performed += instance.OnConsume1;
            @Consume1.canceled += instance.OnConsume1;
            @Consume2.started += instance.OnConsume2;
            @Consume2.performed += instance.OnConsume2;
            @Consume2.canceled += instance.OnConsume2;
        }

        private void UnregisterCallbacks(IPlayerAbilityActions instance)
        {
            @Ability0.started -= instance.OnAbility0;
            @Ability0.performed -= instance.OnAbility0;
            @Ability0.canceled -= instance.OnAbility0;
            @Ability1.started -= instance.OnAbility1;
            @Ability1.performed -= instance.OnAbility1;
            @Ability1.canceled -= instance.OnAbility1;
            @Ability2.started -= instance.OnAbility2;
            @Ability2.performed -= instance.OnAbility2;
            @Ability2.canceled -= instance.OnAbility2;
            @Ability3.started -= instance.OnAbility3;
            @Ability3.performed -= instance.OnAbility3;
            @Ability3.canceled -= instance.OnAbility3;
            @Ability4.started -= instance.OnAbility4;
            @Ability4.performed -= instance.OnAbility4;
            @Ability4.canceled -= instance.OnAbility4;
            @Consume1.started -= instance.OnConsume1;
            @Consume1.performed -= instance.OnConsume1;
            @Consume1.canceled -= instance.OnConsume1;
            @Consume2.started -= instance.OnConsume2;
            @Consume2.performed -= instance.OnConsume2;
            @Consume2.canceled -= instance.OnConsume2;
        }

        public void RemoveCallbacks(IPlayerAbilityActions instance)
        {
            if (m_Wrapper.m_PlayerAbilityActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerAbilityActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerAbilityActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerAbilityActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerAbilityActions @PlayerAbility => new PlayerAbilityActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_OpenMenu;
    private readonly InputAction m_UI_CloseMenu;
    private readonly InputAction m_UI_OpenShop;
    private readonly InputAction m_UI_OpenQuest;
    private readonly InputAction m_UI_CloseQuest;
    private readonly InputAction m_UI_OpenBag;
    private readonly InputAction m_UI_CloseBag;
    private readonly InputAction m_UI_OpenInteract;
    public struct UIActions
    {
        private @Inputsystem m_Wrapper;
        public UIActions(@Inputsystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenMenu => m_Wrapper.m_UI_OpenMenu;
        public InputAction @CloseMenu => m_Wrapper.m_UI_CloseMenu;
        public InputAction @OpenShop => m_Wrapper.m_UI_OpenShop;
        public InputAction @OpenQuest => m_Wrapper.m_UI_OpenQuest;
        public InputAction @CloseQuest => m_Wrapper.m_UI_CloseQuest;
        public InputAction @OpenBag => m_Wrapper.m_UI_OpenBag;
        public InputAction @CloseBag => m_Wrapper.m_UI_CloseBag;
        public InputAction @OpenInteract => m_Wrapper.m_UI_OpenInteract;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @OpenMenu.started += instance.OnOpenMenu;
            @OpenMenu.performed += instance.OnOpenMenu;
            @OpenMenu.canceled += instance.OnOpenMenu;
            @CloseMenu.started += instance.OnCloseMenu;
            @CloseMenu.performed += instance.OnCloseMenu;
            @CloseMenu.canceled += instance.OnCloseMenu;
            @OpenShop.started += instance.OnOpenShop;
            @OpenShop.performed += instance.OnOpenShop;
            @OpenShop.canceled += instance.OnOpenShop;
            @OpenQuest.started += instance.OnOpenQuest;
            @OpenQuest.performed += instance.OnOpenQuest;
            @OpenQuest.canceled += instance.OnOpenQuest;
            @CloseQuest.started += instance.OnCloseQuest;
            @CloseQuest.performed += instance.OnCloseQuest;
            @CloseQuest.canceled += instance.OnCloseQuest;
            @OpenBag.started += instance.OnOpenBag;
            @OpenBag.performed += instance.OnOpenBag;
            @OpenBag.canceled += instance.OnOpenBag;
            @CloseBag.started += instance.OnCloseBag;
            @CloseBag.performed += instance.OnCloseBag;
            @CloseBag.canceled += instance.OnCloseBag;
            @OpenInteract.started += instance.OnOpenInteract;
            @OpenInteract.performed += instance.OnOpenInteract;
            @OpenInteract.canceled += instance.OnOpenInteract;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @OpenMenu.started -= instance.OnOpenMenu;
            @OpenMenu.performed -= instance.OnOpenMenu;
            @OpenMenu.canceled -= instance.OnOpenMenu;
            @CloseMenu.started -= instance.OnCloseMenu;
            @CloseMenu.performed -= instance.OnCloseMenu;
            @CloseMenu.canceled -= instance.OnCloseMenu;
            @OpenShop.started -= instance.OnOpenShop;
            @OpenShop.performed -= instance.OnOpenShop;
            @OpenShop.canceled -= instance.OnOpenShop;
            @OpenQuest.started -= instance.OnOpenQuest;
            @OpenQuest.performed -= instance.OnOpenQuest;
            @OpenQuest.canceled -= instance.OnOpenQuest;
            @CloseQuest.started -= instance.OnCloseQuest;
            @CloseQuest.performed -= instance.OnCloseQuest;
            @CloseQuest.canceled -= instance.OnCloseQuest;
            @OpenBag.started -= instance.OnOpenBag;
            @OpenBag.performed -= instance.OnOpenBag;
            @OpenBag.canceled -= instance.OnOpenBag;
            @CloseBag.started -= instance.OnCloseBag;
            @CloseBag.performed -= instance.OnCloseBag;
            @CloseBag.canceled -= instance.OnCloseBag;
            @OpenInteract.started -= instance.OnOpenInteract;
            @OpenInteract.performed -= instance.OnOpenInteract;
            @OpenInteract.canceled -= instance.OnOpenInteract;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnWalkToggle(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IPlayerAbilityActions
    {
        void OnAbility0(InputAction.CallbackContext context);
        void OnAbility1(InputAction.CallbackContext context);
        void OnAbility2(InputAction.CallbackContext context);
        void OnAbility3(InputAction.CallbackContext context);
        void OnAbility4(InputAction.CallbackContext context);
        void OnConsume1(InputAction.CallbackContext context);
        void OnConsume2(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnOpenMenu(InputAction.CallbackContext context);
        void OnCloseMenu(InputAction.CallbackContext context);
        void OnOpenShop(InputAction.CallbackContext context);
        void OnOpenQuest(InputAction.CallbackContext context);
        void OnCloseQuest(InputAction.CallbackContext context);
        void OnOpenBag(InputAction.CallbackContext context);
        void OnCloseBag(InputAction.CallbackContext context);
        void OnOpenInteract(InputAction.CallbackContext context);
    }
}
