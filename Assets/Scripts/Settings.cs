using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/SettingsObject", order = 1)]
public class Settings : ScriptableObject
{
    public float AudioValue;
    public int SettingsState;
}
