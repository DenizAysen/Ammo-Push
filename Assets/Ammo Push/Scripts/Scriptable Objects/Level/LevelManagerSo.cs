using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level/LevelManager", fileName = "Level Manager")]
public class LevelManagerSo : ScriptableObject
{
    public LevelDataSo[] Levels;
}
