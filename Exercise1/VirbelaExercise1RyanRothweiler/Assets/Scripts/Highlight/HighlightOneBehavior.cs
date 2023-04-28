using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Highlights the nearest HighlightItem in SpawnManager.
/// </summary>
public class HighlightOneBehavior : HighlightBehaviorBase
{
	public override GameObject GetNearest()
	{
		return SpawnManager.GetNearestAll(this.transform.position);
	}
}
