using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialSpawner : MonoBehaviour
{
	[SerializeField] private float radius;
	[SerializeField] private int count;
	[SerializeField] private GameObject obj;

	public GameObject player;

	private KDTree kd = new KDTree();
	private GameObject prevNearest;

	void Start()
	{
		for (int i = 0; i < count; i++) {
			GameObject newObj = Instantiate(obj, Random.insideUnitSphere * radius, Quaternion.identity, this.transform);
			kd.Add(newObj);
		}
	}

	void Update()
	{
		GameObject nearest = kd.FindNearest(player.transform.position);
		if (nearest != prevNearest) {
			if (prevNearest != null) {
				prevNearest.transform.localScale = new Vector3(1, 1, 1);
			}
			nearest.transform.localScale = new Vector3(3, 3, 3);
		}
		prevNearest = nearest;
	}
}