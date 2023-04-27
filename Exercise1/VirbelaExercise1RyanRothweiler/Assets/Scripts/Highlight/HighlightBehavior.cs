using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
