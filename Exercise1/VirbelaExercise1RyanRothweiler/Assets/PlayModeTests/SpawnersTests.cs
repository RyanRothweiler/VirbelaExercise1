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
		/* Find bot definition.
		Finding by filename here is brittle. Other options each with pros / cons
			- Move to resources and load by type
			- Find all SpawnDefinitions using asset database then pick one at random, or iterate through each
			- Use spawn definition GUID instead of fie name.
		*/
		string[] guids = AssetDatabase.FindAssets("BotSpawnDefinition", null);
		string definitionPath = AssetDatabase.GUIDToAssetPath(guids[0]);
		SpawnItemDefinition definition = AssetDatabase.LoadAssetAtPath<SpawnItemDefinition>(definitionPath);
		SpawnManager.AddSpawnDefinition(definition);

		// Test settings. Move these into its own structure and then run the same test with different settings.
		int count = 100;
		float radius = 5.5f;

		// Create spawner.
		GameObject spawner = new GameObject("Spawner");
		RadialSpawner radial = spawner.AddComponent<RadialSpawner>();
		radial.SetRadius(radius);
		radial.SetType(SpawnType.Bot);
		radial.Spawn(count);

		// Do spawning and verify the results.
		List<GameObject> spawnedList = SpawnManager.GetAllSpawned();
		Debug.Assert(spawnedList.Count == count);
		foreach (GameObject obj in spawnedList) {
			float dist = Vector3.Distance(obj.transform.position, Vector3.zero);
			Debug.Assert(dist <= radius);
		}
	}
}