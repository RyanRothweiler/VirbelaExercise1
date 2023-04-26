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
		if (!spawnDefinitions.ContainsKey(type)) {
			Debug.LogError($"Unknown spawn item type {type}");
			return;
		}

		SpawnItemDefinition def = spawnDefinitions[type];
		GameObject newObj = Instantiate(def.objPrefab, pos, Quaternion.identity, parent);

		if (!allSpawns.ContainsKey(type)) {
			allSpawns[type] = new KDTree();
		}
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
