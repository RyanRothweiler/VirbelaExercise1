using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialSpawner : MonoBehaviour
{
	[SerializeField] private SpawnType type;
	[SerializeField] private float radius;
	[SerializeField] private int count;

	void Start()
	{
		Spawn(count);
	}

	public void Spawn(int count)
	{
		for (int i = 0; i < count; i++) {
			Vector3 spawnPos = this.transform.position + (Random.insideUnitSphere * radius);
			SpawnManager.Spawn(type, spawnPos, this.transform);
		}
	}
}