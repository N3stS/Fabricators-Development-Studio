using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceName", menuName = "ResourceProperties", order = 3)]

public class ResourcePropertiesManager : ScriptableObject
{
    public string _resourceName;
    public string _resourceType;
    public int _heatRequirement;
    public int _heatOutput;
}
