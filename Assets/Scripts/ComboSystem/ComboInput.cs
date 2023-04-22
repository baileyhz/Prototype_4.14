using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboInput : MonoBehaviour
{
    public InputActionAsset inputActions;

    public InputAction m_lightAttact;
    public InputAction m_heavyAttact;

    private void OnEnable() => inputActions?.Enable();
    private void OnDisable() => inputActions?.Disable();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_lightAttact = inputActions["LightAttack"];
        m_heavyAttact = inputActions["HeavyAttack"];
    }

    public bool GetLightAttactDown() => m_lightAttact.WasPressedThisFrame();
    public bool GetHeavyAttactDown() => m_heavyAttact.WasPressedThisFrame();

}
