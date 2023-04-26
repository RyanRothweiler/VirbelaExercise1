using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBehavior : MonoBehaviour
{
	public SpawnType typeHighlighting;

	private Vector3 prevPos;
	private GameObject prevNearest;

	void Start()
	{
		HighlightNearest();
	}

	void Update()
	{
		if (prevPos != this.transform.position) {
			HighlightNearest();
		}
	}

	void HighlightNearest()
	{
		prevPos = this.transform.position;

		if (prevNearest != null) {
			prevNearest.transform.localScale = new Vector3(1, 1, 1);
		}

		GameObject nearest = SpawnManager.GetNearest(typeHighlighting, this.transform.position);

		// Not an error. Can happen if there are 0 spawns.
		if (nearest == null) { return; }

		nearest.transform.localScale = new Vector3(3, 3, 3);
		prevNearest = nearest;
	}
}
