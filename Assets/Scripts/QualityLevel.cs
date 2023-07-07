using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityLevel : MonoBehaviour
{
    void OnGUI()
    {
        string[] names = QualitySettings.names;
        GUILayout.BeginVertical();
        for (int i = 0; i < names.Length; i++)
        {
            if (GUILayout.Button(names[i]))
            {
                QualitySettings.SetQualityLevel(i, true);
            }
        }
        GUILayout.EndVertical();
    }
}
