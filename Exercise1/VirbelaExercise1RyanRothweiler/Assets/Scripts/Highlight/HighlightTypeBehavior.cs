using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Highlights the nearest HighlightItem of the specified spawn type in SpawnManager
/// </summary>
public class HighlightTypeBehavior : HighlightBehaviorBase
{
	public SpawnType typeHighlighting;

	public override GameObject GetNearest()
	{
		return SpawnManager.GetNearest(typeHighlighting, this.transform.position);
	}
}
