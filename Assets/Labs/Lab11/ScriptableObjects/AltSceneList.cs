using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Alt Scene/SceneList", fileName = "SceneList")]
public class AltSceneList : ScriptableObject
{
    [ReorderableList]
    public List<AltScene> scenes;
}
