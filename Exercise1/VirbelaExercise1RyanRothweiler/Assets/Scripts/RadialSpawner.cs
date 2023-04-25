using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialSpawner : MonoBehaviour
{
	[SerializeField] private float radius;
	[SerializeField] private int count;
	[SerializeField] private GameObject obj;

	void Start()
	{
		for (int i = 0; i < count; i++) {
			Instantiate(obj, Random.insideUnitSphere * radius, Quaternion.identity, this.transform);
		}
	}
}