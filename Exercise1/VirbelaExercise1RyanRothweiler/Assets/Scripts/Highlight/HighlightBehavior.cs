using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Highlights the nearest HighlightItem in SpawnManager
/// <summary>
/// Highlights the nearest HighlightItem in SpawnManager.
/// Only runs when new objects are spawned or when this gameobject moves
/// </summary>
public class HighlightBehavior : MonoBehaviour
{
	public SpawnType typeHighlighting;

	private Vector3 prevPos;
	private HighlightItem prevNearest;

	private int countSeen;

	void Start()
	{
		HighlightNearest();
	}

	void Update()
	{
		if (
		    prevPos != this.transform.position ||
		    SpawnManager.GetAllSpawned().Count != countSeen
		) {
			HighlightNearest();
		}
	}

	// Highlights the nearest HighlightItem in SpawnManager
	/// <summary>
	/// Highlights the nearest HighlightItem in SpawnManager
	/// </summary>
	void HighlightNearest()
	{
		countSeen = SpawnManager.GetAllSpawned().Count;
		prevPos = this.transform.position;

		if (prevNearest != null) {
			prevNearest.SetHighlight(false);
		}

		GameObject nearest = SpawnManager.GetNearest(typeHighlighting, this.transform.position);

		// Not an error. Can happen if there are 0 spawns.
		if (nearest == null) { return; }

		HighlightItem hItem = nearest.GetComponent<HighlightItem>();
		if (hItem != null) {
			hItem.SetHighlight(true);
			prevNearest = hItem;
		}
	}
}
