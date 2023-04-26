using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnItemDefinition", order = 1)]
public class SpawnItemDefinition : ScriptableObject
{
	public SpawnType type;
	public GameObject objPrefab;
	public Color highlightColor;
	public Color baseColor;
	public string materialColorProperty;
}