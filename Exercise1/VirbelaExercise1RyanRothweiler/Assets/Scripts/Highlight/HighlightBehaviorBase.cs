using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for highlighting behavior of GameObjects.
/// </summary>
public abstract class HighlightBehaviorBase : MonoBehaviour
{
	private Vector3 prevPos;
	private int countSeen;
	private HighlightItem prevNearest;

	void Start()
	{
		Highlight();
	}

	void Update()
	{
		if (
		    prevPos != this.transform.position ||
		    SpawnManager.GetAllSpawned().Count != countSeen
		) {
			Highlight();
		}
	}

	/// <summary>
	/// Highlights the nearest GameObject.
	/// </summary>
	public void Highlight()
	{
		countSeen = SpawnManager.GetAllSpawned().Count;
		prevPos = this.transform.position;

		if (prevNearest != null) {
			prevNearest.SetHighlight(false);
		}

		GameObject nearest = GetNearest();

		// Not an error. Can happen if there are 0 spawns.
		if (nearest == null) { return; }

		HighlightItem hItem = nearest.GetComponent<HighlightItem>();
		if (hItem != null) {
			hItem.SetHighlight(true);
			prevNearest = hItem;
		}
	}

	/// <summary>
	/// Returns the nearest GameObject. Implementation must override this.
	/// </summary>
	/// <returns>The nearest GameObject.</returns>
	public abstract GameObject GetNearest();
}
