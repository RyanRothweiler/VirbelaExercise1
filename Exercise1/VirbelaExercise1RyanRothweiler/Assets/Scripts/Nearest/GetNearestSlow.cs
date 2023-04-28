using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implementation of the interface that finds the nearest using a slow search method. O(n)
/// </summary>
public class GetNearestSlow : IGetNearest
{
	/// <summary>
	/// The list of instances to search through.
	/// </summary>
	public List<GameObject> objects = new List<GameObject>();

	/// <summary>
	/// Adds an instance to the search list.
	/// </summary>
	/// <param name="obj">The instance to add.</param>
	public void Add(GameObject obj)
	{
		objects.Add(obj);
	}

	/// <summary>
	/// Finds the instance nearest to the specified point. Uses a naive approach
	/// </summary>
	/// <param name="origin">The point to find the nearest instance to.</param>
	/// <returns>The instance nearest to the specified point.</returns>
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
