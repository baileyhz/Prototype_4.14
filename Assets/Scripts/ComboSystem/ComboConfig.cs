using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewComboConfig", menuName ="ComboSystem/CreateNewComboSystemConfig")]
public class ComboConfig : ScriptableObject
{
    public string m_animatorStateName;
    public float m_releaseTime;
}
