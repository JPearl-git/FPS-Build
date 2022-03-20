// GENERATED AUTOMATICALLY FROM 'Assets/Player/Input/InputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""1040da58-5ec0-47c3-b903-cfd88e6eea80"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""11a234eb-3fe8-4131-96cc-16d38d9d6d29"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""291f4a47-8934-47fd-a0ee-fe2333e18c99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""4195663c-2951-4229-813e-0459a792f054"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""251caeaa-9668-4150-a6c9-465d910cd8ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookHorizontal"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b142784d-5efa-4d53-91f1-c07ec6b328fd"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookVertical"",
                    ""type"": ""PassThrough"",
                    ""id"": ""41df31ae-1e91-41d9-be01-968613b7e0c8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""417bf0ad-2ce8-416c-a5aa-1c9b221cb60a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Equip Weapon 1"",
                    ""type"": ""Button"",
                    ""id"": ""a1a236c2-71c5-423b-9646-59be83269c69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Equip Weapon 2"",
                    ""type"": ""Button"",
                    ""id"": ""81e1227b-0426-4682-b43b-4e226ddac5f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Equip Weapon 3"",
                    ""type"": ""Button"",
                    ""id"": ""17b0a4ce-4d35-4a5b-9666-174ea04b4d37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Equip Weapon 4"",
                    ""type"": ""Button"",
                    ""id"": ""f34cfe45-30dc-4c2f-a3c7-f48237de393d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""10ce31b6-f262-4c0e-a98d-8c000ef87cff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quick Swap"",
                    ""type"": ""Button"",
                    ""id"": ""4b91b542-fc24-40df-a102-a4c88b610ac8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop Weapon"",
                    ""type"": ""Button"",
                    ""id"": ""c626bbb4-22f1-4b02-810d-c8b59e5a1094"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""105d1b17-7abe-4c1a-ac5c-4a0a2a26f95b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""171c935d-b28f-483f-90df-87f226a84fe6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d31fabb1-ca79-49fe-a5cc-577c6adc32aa"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4baaa65a-39cb-41e8-825a-171bc395ee63"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c9c3a275-2602-43a1-bffd-0254953f68e5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""377a54b9-d18a-4dad-b414-6990ce3d2669"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""GamePad"",
                    ""id"": ""2e5782d1-fd61-4239-a282-929030f43f0b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0e705024-a1ce-4258-bd12-d9cd0e68fa32"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""333c70bc-40db-427a-bbce-17f171fc2f32"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e91e74ed-5a38-466e-b462-b1dcdecec489"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""90138ed7-1daa-44f3-a5f9-d387d2189354"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d26502b1-252f-44ac-90c9-fe4950b83de2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de6ac43b-10a1-482a-a84c-f6c3d32690a2"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e7dc440-226d-4e27-93e2-b707ac230727"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c66bb309-2b06-4eb1-851b-43a412dc7189"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1d6fa0f-d244-4762-868d-0da25a739574"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09335646-3dd5-4f20-967d-b53b82662603"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bb02233-5930-41ce-84c3-101fa8437657"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a133363-bc98-44c7-8f0f-906692e39f4c"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d25889d-0fd3-4a20-94fe-33b50b70e8bd"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5036f181-59f0-4644-b4fc-2e730e64bb3f"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd3f8557-2338-413a-9a03-5e437e627a5f"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e398778-7148-42e9-9e13-fa4efb4f50a5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2fc3660a-8271-4e05-9f58-bc6e4ce58c2b"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Weapon 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b98ff580-06e2-4bdd-b1f0-d8088cbb1003"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Weapon 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8309300d-6b9b-443a-8000-c8b53a83e9c5"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Weapon 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f37619a-b4a9-4604-ac11-feab14018e93"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Weapon 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""067400fd-0aae-45a0-97b8-2fea2832a204"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Weapon 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2abe1738-5f28-4815-b647-9e41f68f0d1c"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Weapon 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe6c2690-8c41-43a4-9368-3b38434aebfe"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Weapon 4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d27602b8-c01b-42a4-8654-8963ebf881e1"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Weapon 4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f168eec6-bc43-4e60-8da3-31e9e0260e56"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ebaca72-89a7-41e8-bd2c-733f833813b7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a802cfd6-269c-45d7-94c2-5f10da0be466"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quick Swap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab05dba6-4794-45d5-a7fc-42f25659c1c4"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quick Swap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fcc6ab12-579a-4b7b-9b06-64840dc7d52f"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a3bad44-e2b9-43e5-b772-32b5f425e154"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60177eb9-a7ac-4f2e-96d5-940b0bd4a85f"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_LookHorizontal = m_Player.FindAction("LookHorizontal", throwIfNotFound: true);
        m_Player_LookVertical = m_Player.FindAction("LookVertical", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_EquipWeapon1 = m_Player.FindAction("Equip Weapon 1", throwIfNotFound: true);
        m_Player_EquipWeapon2 = m_Player.FindAction("Equip Weapon 2", throwIfNotFound: true);
        m_Player_EquipWeapon3 = m_Player.FindAction("Equip Weapon 3", throwIfNotFound: true);
        m_Player_EquipWeapon4 = m_Player.FindAction("Equip Weapon 4", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_QuickSwap = m_Player.FindAction("Quick Swap", throwIfNotFound: true);
        m_Player_DropWeapon = m_Player.FindAction("Drop Weapon", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Pause;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_LookHorizontal;
    private readonly InputAction m_Player_LookVertical;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_EquipWeapon1;
    private readonly InputAction m_Player_EquipWeapon2;
    private readonly InputAction m_Player_EquipWeapon3;
    private readonly InputAction m_Player_EquipWeapon4;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_QuickSwap;
    private readonly InputAction m_Player_DropWeapon;
    private readonly InputAction m_Player_Dash;
    public struct PlayerActions
    {
        private @InputControls m_Wrapper;
        public PlayerActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @LookHorizontal => m_Wrapper.m_Player_LookHorizontal;
        public InputAction @LookVertical => m_Wrapper.m_Player_LookVertical;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @EquipWeapon1 => m_Wrapper.m_Player_EquipWeapon1;
        public InputAction @EquipWeapon2 => m_Wrapper.m_Player_EquipWeapon2;
        public InputAction @EquipWeapon3 => m_Wrapper.m_Player_EquipWeapon3;
        public InputAction @EquipWeapon4 => m_Wrapper.m_Player_EquipWeapon4;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @QuickSwap => m_Wrapper.m_Player_QuickSwap;
        public InputAction @DropWeapon => m_Wrapper.m_Player_DropWeapon;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @LookHorizontal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookHorizontal;
                @LookHorizontal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookHorizontal;
                @LookHorizontal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookHorizontal;
                @LookVertical.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookVertical;
                @LookVertical.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookVertical;
                @LookVertical.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookVertical;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @EquipWeapon1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon1;
                @EquipWeapon1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon1;
                @EquipWeapon1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon1;
                @EquipWeapon2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon2;
                @EquipWeapon2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon2;
                @EquipWeapon2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon2;
                @EquipWeapon3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon3;
                @EquipWeapon3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon3;
                @EquipWeapon3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon3;
                @EquipWeapon4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon4;
                @EquipWeapon4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon4;
                @EquipWeapon4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipWeapon4;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @QuickSwap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSwap;
                @QuickSwap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSwap;
                @QuickSwap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnQuickSwap;
                @DropWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDropWeapon;
                @DropWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDropWeapon;
                @DropWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDropWeapon;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LookHorizontal.started += instance.OnLookHorizontal;
                @LookHorizontal.performed += instance.OnLookHorizontal;
                @LookHorizontal.canceled += instance.OnLookHorizontal;
                @LookVertical.started += instance.OnLookVertical;
                @LookVertical.performed += instance.OnLookVertical;
                @LookVertical.canceled += instance.OnLookVertical;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @EquipWeapon1.started += instance.OnEquipWeapon1;
                @EquipWeapon1.performed += instance.OnEquipWeapon1;
                @EquipWeapon1.canceled += instance.OnEquipWeapon1;
                @EquipWeapon2.started += instance.OnEquipWeapon2;
                @EquipWeapon2.performed += instance.OnEquipWeapon2;
                @EquipWeapon2.canceled += instance.OnEquipWeapon2;
                @EquipWeapon3.started += instance.OnEquipWeapon3;
                @EquipWeapon3.performed += instance.OnEquipWeapon3;
                @EquipWeapon3.canceled += instance.OnEquipWeapon3;
                @EquipWeapon4.started += instance.OnEquipWeapon4;
                @EquipWeapon4.performed += instance.OnEquipWeapon4;
                @EquipWeapon4.canceled += instance.OnEquipWeapon4;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @QuickSwap.started += instance.OnQuickSwap;
                @QuickSwap.performed += instance.OnQuickSwap;
                @QuickSwap.canceled += instance.OnQuickSwap;
                @DropWeapon.started += instance.OnDropWeapon;
                @DropWeapon.performed += instance.OnDropWeapon;
                @DropWeapon.canceled += instance.OnDropWeapon;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLookHorizontal(InputAction.CallbackContext context);
        void OnLookVertical(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnEquipWeapon1(InputAction.CallbackContext context);
        void OnEquipWeapon2(InputAction.CallbackContext context);
        void OnEquipWeapon3(InputAction.CallbackContext context);
        void OnEquipWeapon4(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnQuickSwap(InputAction.CallbackContext context);
        void OnDropWeapon(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
}
