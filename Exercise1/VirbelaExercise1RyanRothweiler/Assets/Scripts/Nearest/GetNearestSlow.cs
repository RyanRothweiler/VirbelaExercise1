using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNearestSlow : IGetNearest
{
	public List<GameObject> objects = new List<GameObject>();

	public void Add(GameObject obj)
	{
		objects.Add(obj);
	}

	public GameObject FindNearest(Vector3 origin)
	{
		GameObject nearest = null;
		float nearestDist = float.MaxValue;

		foreach (GameObject obj in objects) {
			if (nearest == null) {
				nearest = obj;
				nearestDist = Vector3.Distance(obj.transform.position, origin);
			} else {
				float dist = Vector3.Distance(obj.transform.position, origin);
				if (dist < nearestDist) {
					nearestDist = dist;
					nearest = obj;
				}
			}
		}

		return nearest;
	}

}
