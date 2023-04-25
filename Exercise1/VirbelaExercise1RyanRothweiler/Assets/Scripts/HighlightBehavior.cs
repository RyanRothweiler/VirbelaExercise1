using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBehavior : MonoBehaviour
{
	private Vector3 prevPos;

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
	}
}
