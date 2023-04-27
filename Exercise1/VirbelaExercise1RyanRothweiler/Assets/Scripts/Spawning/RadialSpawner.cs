using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialSpawner : MonoBehaviour
{
	[SerializeField] private SpawnType type;
	public void SetType(SpawnType type) { this.type = type; }

	[SerializeField] private float radius;
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

	public void Spawn(int count)
	{
		for (int i = 0; i < count; i++) {
			Vector3 spawnPos = this.transform.position + (Random.insideUnitSphere * radius);
			SpawnManager.Spawn(type, spawnPos, this.transform);
		}
	}
}