using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class SpawnersTests
{
	[Test]
	public void RadialSpawner()
	{
		// Find bot definition. Could move them to resources to keep them more unified.
		string[] guids = AssetDatabase.FindAssets("BotSpawnDefinition", null);
		string definitionPath = AssetDatabase.GUIDToAssetPath(guids[0]);
		SpawnItemDefinition definition = AssetDatabase.LoadAssetAtPath<SpawnItemDefinition>(definitionPath);
		SpawnManager.AddSpawnDefinition(definition);

		int count = 100;
		float radius = 5.5f;

		GameObject spawner = new GameObject("Spawner");
		RadialSpawner radial = spawner.AddComponent<RadialSpawner>();
		radial.SetRadius(radius);
		radial.SetType(SpawnType.Bot);
		radial.Spawn(count);

		List<GameObject> spawnedList = SpawnManager.GetAllSpawned();
		Debug.Assert(spawnedList.Count == count);
		foreach (GameObject obj in spawnedList) {
			float dist = Vector3.Distance(obj.transform.position, Vector3.zero);
			Debug.Assert(dist <= radius);
		}
	}
}