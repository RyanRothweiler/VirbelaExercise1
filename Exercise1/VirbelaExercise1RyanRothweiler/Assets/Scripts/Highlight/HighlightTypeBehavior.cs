using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Highlights the nearest HighlightItem of the specified spawn type in SpawnManager
/// </summary>
public class HighlightTypeBehavior : HighlightBehaviorBase
{
	public SpawnType typeHighlighting;

	// Highlights the nearest HighlightItem in SpawnManager
	/// <summary>
	/// Highlights the nearest HighlightItem in SpawnManager
	/// </summary>
	public override GameObject GetNearest()
	{
		return SpawnManager.GetNearest(typeHighlighting, this.transform.position);
	}
}
