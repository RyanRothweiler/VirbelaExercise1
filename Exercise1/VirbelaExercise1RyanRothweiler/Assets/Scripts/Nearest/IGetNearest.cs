using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for objects that can find the nearest GameObject from a given origin.
/// </summary>
public interface IGetNearest
{
	/// <summary>
	/// Adds the specified GameObject to the list of objects to search for.
	/// </summary>
	/// <param name="obj">The GameObject to add.</param>
	public void Add(GameObject obj);

	/// <summary>
	/// Finds the nearest GameObject from the specified origin.
	/// </summary>
	/// <param name="origin">The origin point to search from.</param>
	/// <returns>The nearest GameObject.</returns>
	public GameObject FindNearest(Vector3 origin);
}
