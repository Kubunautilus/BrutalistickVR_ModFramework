using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapScriptableObject", menuName = "ScriptableObjects/MapScriptableObject", order = 1)]
public class MapScriptableObject : ScriptableObject
{
    public uint yourSteamId;
    public SkyboxType skyboxType;

    [System.Serializable]
    public struct SkyboxType
    {
        public bool isNight;
        public int skyboxIndex;
    }
}
