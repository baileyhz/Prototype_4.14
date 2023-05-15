using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public WeaponManager currentWeapon;
    public float releaseTime;

    private Animator m_animator;
    private ComboInput m_comboInput;

    private float m_releaseTimer;
    private bool m_isOnNeceTime;
    private ComboConfig m_currentComboConfig;

    private int m_lightAttactIdx = 0;
    private int m_heavyAttactIdx = 0;

    public const float m_animationFadeTime = 0.1f;

    private void Awake()
    {
        Inti();
    }

    private void Inti() 
    {
        m_animator = GetComponent<Animator>();
        m_comboInput = GetComponent<ComboInput>();        
    }

    private void Update() 
    {
        if(Time.time > m_releaseTimer)
            StopCombo();

        HandleCombo();
    }

    private void HandleCombo()
    {
        if (m_isOnNeceTime)
            return;

        if (m_comboInput.m_lightAttact)
        {
            NormalAttact(true);
            m_comboInput.m_lightAttact = false;
        }
            
        if(m_comboInput.m_heavyAttact)
            NormalAttact(false);
    }

    IEnumerator PlayCombo(ComboConfig comboConfig)
    {
        m_isOnNeceTime = true;
        m_releaseTimer = Time.time + releaseTime;
        m_currentComboConfig = comboConfig;
        
        m_animator.CrossFade(comboConfig.m_animatorStateName, m_animationFadeTime);

        float timeOrigin = 0f;
        while(true)
        {
            if(timeOrigin >= comboConfig.m_releaseTime)
                break;

            timeOrigin += Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        m_isOnNeceTime = false;
        yield break;
    }

    private void NormalAttact(bool isLight)
    {
        List<ComboConfig> configs = isLight ? currentWeapon.config.m_lightComboConfigs : currentWeapon.config.m_heavyComboConfigs;
        int comboIdx = isLight ? m_lightAttactIdx : m_heavyAttactIdx;

        StartCoroutine(PlayCombo(configs[comboIdx]));

        if (comboIdx >= configs.Count - 1)
            comboIdx = 0;
        else
            comboIdx++;

        if(isLight)
            m_lightAttactIdx = comboIdx;
        else
            m_heavyAttactIdx = comboIdx;
    }

    private void StopCombo()
    {
        m_lightAttactIdx = 0;
        m_heavyAttactIdx = 0;
    }
}
