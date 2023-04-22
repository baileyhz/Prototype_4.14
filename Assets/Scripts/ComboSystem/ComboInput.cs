using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboInput : MonoBehaviour
{
    //public InputActionAsset inputActions;

    public bool m_lightAttact;
    public bool m_heavyAttact;

    //private void OnEnable() => inputActions?.Enable();
    //private void OnDisable() => inputActions?.Disable();



    private void Awake()
    {
        //Init();
    }

    private void Init()
    {
        //m_lightAttact = inputActions["LightAttack"];
        //m_heavyAttact = inputActions["HeavyAttack"];
    }

    public void GetLightAttack(InputAction.CallbackContext ctx)
    {
        m_lightAttact = ctx.ReadValueAsButton();
    }

    public void GetHeavyAttactHold(InputAction.CallbackContext ctx)
    {
        m_heavyAttact = ctx.ReadValueAsButton();
    }

    //public bool GetLightAttactDown() => m_lightAttact.WasPressedThisFrame();
    //public bool GetHeavyAttactDown() => m_heavyAttact.WasPressedThisFrame();

}
