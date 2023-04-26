using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialSpawner : MonoBehaviour
{
	[SerializeField] private SpawnType type;
	[SerializeField] private float radius;
	[SerializeField] private int count;

	public GameObject player;

	private KDTree kd = new KDTree();
	private GameObject prevNearest;


	void Start()
	{
		Spawn(count);
	}

	void Update()
	{
		/*
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
		*/

		/*
		GameObject nearest = kd.FindNearest(player.transform.position);
		if (nearest != prevNearest) {
			if (prevNearest != null) {
				prevNearest.transform.localScale = new Vector3(1, 1, 1);
			}
			nearest.transform.localScale = new Vector3(3, 3, 3);
		}
		prevNearest = nearest;
		*/
	}

	public void Spawn(int count)
	{
		for (int i = 0; i < count; i++) {
			Vector3 spawnPos = this.transform.position + (Random.insideUnitSphere * radius);
			SpawnManager.Spawn(type, spawnPos, this.transform);
		}
	}
}