using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns objects in a radius around this.
/// </summary>
public class RadialSpawner : MonoBehaviour
{
	[SerializeField] private SpawnType type;

	/// <summary>
	/// Sets the type of object to spawn.
	/// </summary>
	/// <param name="type">The type of object to spawn.</param>
	public void SetType(SpawnType type) { this.type = type; }

	[SerializeField] private float radius;

	/// <summary>
	/// Sets the radius of the spawn area.
	/// </summary>
	/// <param name="radius">The radius of the spawn area.</param>
	public void SetRadius(float radius) { this.radius = radius; }

	[SerializeField] private int count;
	[SerializeField] private KeyCode manualSpawnKey;

	void Start()
	{
		Spawn(count);
	}

	void Update()
	{
		if (Input.GetKeyDown(manualSpawnKey)) {
			Spawn(1);
		}
	}

	/// <summary>
	/// Spawns the specified number of objects randomly within the defined spawn area.
	/// </summary>
	/// <param name="count">The number of objects to spawn.</param>
	public void Spawn(int count)
	{
		for (int i = 0; i < count; i++) {
			Vector3 spawnPos = this.transform.position + (Random.insideUnitSphere * radius);
			SpawnManager.Spawn(type, spawnPos, this.transform);
		}
	}
}
