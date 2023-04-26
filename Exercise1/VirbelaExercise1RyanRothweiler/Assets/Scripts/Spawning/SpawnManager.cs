using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType {
	Item, Bot
};

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private List<SpawnItemDefinition> spawnspawnDefinitionsInit;

	private static Dictionary<SpawnType, SpawnItemDefinition> spawnDefinitions = new Dictionary<SpawnType, SpawnItemDefinition>();
	private static Dictionary<SpawnType, KDTree> allSpawns = new Dictionary<SpawnType, KDTree>();

	public void Awake()
	{
		for (int i = 0; i < spawnspawnDefinitionsInit.Count; i++) {
			if (spawnDefinitions.ContainsKey(spawnspawnDefinitionsInit[i].type)) {
				Debug.LogError("Duplicate spawn types. That is not allowed.");
			} else {
				spawnDefinitions[spawnspawnDefinitionsInit[i].type] = spawnspawnDefinitionsInit[i];
			}
		}
	}

	public static SpawnItemDefinition GetSpawnItemDefinition(SpawnType type)
	{
		if (!spawnDefinitions.ContainsKey(type)) {
			Debug.LogError($"Unknown spawn item type {type}");
			return null;
		}
		return spawnDefinitions[type];
	}

	public static void Spawn(SpawnType type, Vector3 pos, Transform parent)
	{
		// Does tha type exist
		if (!spawnDefinitions.ContainsKey(type)) {
			Debug.LogError($"Unknown spawn item type {type}.");
			return;
		}

		// get definition
		SpawnItemDefinition def = spawnDefinitions[type];

		// Obj is required
		if (def.objPrefab == null) {
			Debug.LogError($"Spawn definition for type {type} missing object prefab.");
			return;
		}

		// Instantiate the object
		GameObject newObj = Instantiate(def.objPrefab, pos, Quaternion.identity, parent);

		// Create kd tree if there isn't one
		if (!allSpawns.ContainsKey(type)) {
			allSpawns[type] = new KDTree();
		}

		// Add to kd tree
		allSpawns[type].Add(newObj);
	}

	public static GameObject GetNearest(SpawnType type, Vector3 origin)
	{
		if (!allSpawns.ContainsKey(type)) {
			// Not logging erroring here. This will happen if a SpawnType has 0 spawns, which isn't necessarily an error.
			return null;
		}

		return allSpawns[type].FindNearest(origin);
	}
}


/*
void Update()
{
GameObject testNearest = null;
float minDist = 0;
foreach (GameObject o in objs) {
	if (testNearest == null) {
		testNearest = o;
		minDist = Vector3.Distance(player.transform.position, o.transform.position);
	} else if (Vector3.Distance(player.transform.position, o.transform.position) < minDist) {
		testNearest = o;
		minDist = Vector3.Distance(player.transform.position, o.transform.position);
	}
}
sphere.transform.position = testNearest.transform.position;

GameObject nearest = kd.FindNearest(player.transform.position);
if (nearest != prevNearest) {
	if (prevNearest != null) {
		prevNearest.transform.localScale = new Vector3(1, 1, 1);
	}
	nearest.transform.localScale = new Vector3(3, 3, 3);
}
prevNearest = nearest;
}
*/