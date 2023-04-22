using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewWeaponConfig",menuName ="ComboSystem/CreateNewWeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public List<ComboConfig> m_lightComboConfigs = new List<ComboConfig>();
    public List<ComboConfig> m_heavyComboConfigs = new List<ComboConfig>();
}
