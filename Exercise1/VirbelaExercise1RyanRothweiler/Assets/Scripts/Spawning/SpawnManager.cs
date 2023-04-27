using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public enum SpawnType {
	Item, Bot,
};

/// <summary>
/// Handles spawning based on spawn types, and organizes them into datastructures
/// </summary>
public class SpawnManager : MonoBehaviour
{
	// This would be injected by a framework
	//private static IGetNearestBuilder nearestBuilder = new NearestBuilderSlow();
	private static IGetNearestBuilder nearestBuilder = new NearestBuilderKD();

	[SerializeField] private List<SpawnItemDefinition> spawnDefinitionsInit;

	private static Dictionary<SpawnType, SpawnItemDefinition> spawnDefinitions = new Dictionary<SpawnType, SpawnItemDefinition>();
	private static Dictionary<SpawnType, IGetNearest> allSpawns = new Dictionary<SpawnType, IGetNearest>();

	// This is somewhat redundant, the kdtree structure could probably be expanded to return a list of all it holds
	private static List<GameObject> allObjects = new List<GameObject>();
	public static List<GameObject> GetAllSpawned() { return allObjects; }

	public void Awake()
	{
		for (int i = 0; i < spawnDefinitionsInit.Count; i++) {
			if (spawnDefinitions.ContainsKey(spawnDefinitionsInit[i].type)) {
				Debug.LogError("Duplicate spawn types. That is not allowed.");
			} else {
				spawnDefinitions[spawnDefinitionsInit[i].type] = spawnDefinitionsInit[i];
			}
		}
	}

	/// <summary>
	/// Adds a new spawn definition to the spawnDefinitions dictionary.
	/// </summary>
	/// <param name="def">The spawn definition to be added.</param>
	public static void AddSpawnDefinition(SpawnItemDefinition def)
	{
		spawnDefinitions[def.type] = def;
	}

	/// <summary>
	/// Returns the SpawnItemDefinition for the given SpawnType.
	/// </summary>
	/// <param name="type">The SpawnType to get the definition for.</param>
	/// <returns>The SpawnItemDefinition for the given SpawnType.</returns>
	/// <remarks>
	/// If the SpawnType is not found in the spawnDefinitions dictionary, a debug error message will be logged and null will be returned.
	/// </remarks>
	public static SpawnItemDefinition GetSpawnItemDefinition(SpawnType type)
	{
		if (!spawnDefinitions.ContainsKey(type)) {
			Debug.LogError($"Unknown spawn item type {type}");
			return null;
		}
		return spawnDefinitions[type];
	}

	/// <summary>
	/// Spawns a new object of the specified type at the specified position, and adds it to the list of all spawned objects of that type.
	/// </summary>
	/// <param name="type">The type of object to spawn.</param>
	/// <param name="pos">The position at which to spawn the object.</param>
	/// <param name="parent">The parent transform under which to place the object.</param>
	public static void Spawn(SpawnType type, Vector3 pos, Transform parent)
	{
		// Does that type exist
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

		// Create storage if there isn't one
		if (!allSpawns.ContainsKey(type)) {
			allSpawns[type] = nearestBuilder.BuildImplementation();
		}

		// Add to storage
		allSpawns[type].Add(newObj);
		allObjects.Add(newObj);
	}

	/// <summary>
	/// Gets the nearest spawned object of the specified SpawnType to the specified origin position.
	/// </summary>
	/// <param name="type">The SpawnType to search for.</param>
	/// <param name="origin">The position to search from.</param>
	/// <returns>The nearest spawned object of the specified SpawnType, or null if none exist.</returns>
	public static GameObject GetNearest(SpawnType type, Vector3 origin)
	{
		if (!allSpawns.ContainsKey(type)) {
			// Not logging erroring here. This will happen if a SpawnType has 0 spawns, which isn't necessarily an error.
			return null;
		}

		return allSpawns[type].FindNearest(origin);
	}
}