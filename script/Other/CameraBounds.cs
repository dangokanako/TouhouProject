using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraBounds", menuName = "ScriptableObjects/CameraBounds", order = 1)]
public class CameraBounds : ScriptableObject
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public string remark;
}