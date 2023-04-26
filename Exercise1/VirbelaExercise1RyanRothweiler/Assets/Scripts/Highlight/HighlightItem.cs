using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightItem : MonoBehaviour
{
	[SerializeField] private SpawnType type;

	private Renderer rend = null;
	private SpawnItemDefinition def = null;

	private bool isAnimating = false;
	private Color targetColor;
	private Color currColor = Color.white;

	// Stop animating when the target is close enough.
	private const float closeEnough = 0.01f;
	private const float lerpSpeed = 25f;

	// Do in awake so that other behaviors can use this. Also good practice to get components in awake
	public void Awake()
	{
		rend = this.GetComponent<Renderer>();
		def = SpawnManager.GetSpawnItemDefinition(type);
	}

	public void Update()
	{
		/*
		 lerp the color. There are other lerp methods that maybe avoid using Update functions or target subtraction.
		 But this method always feels the best, is super simple, and is easily readable, easily change-able, and easily understood. So I prefer this.
		*/
		if (isAnimating) {

			// Lerp color
			currColor = Color.Lerp(currColor, targetColor, Time.deltaTime * lerpSpeed);
			rend.material.SetColor(def.highlightColorMaterialProperty, currColor);

			// Check if at end
			Color dist = currColor - targetColor;
			if (
			    Mathf.Abs(dist.r) < closeEnough &&
			    Mathf.Abs(dist.g) < closeEnough &&
			    Mathf.Abs(dist.b) < closeEnough
			) {
				isAnimating = false;
			}
		}
	}

	public void SetHighlight(bool state)
	{
		// Have things we need
		if (rend == null || def == null) { return; }

		// Material has property we need
		if (!rend.material.HasProperty(def.highlightColorMaterialProperty)) {
			Debug.LogError($"The material is missing the properties {def.highlightColorMaterialProperty} needed to highlight.");
			return;
		}

		isAnimating = true;

		// Update target
		if (state) {
			targetColor = def.highlightColor;
		} else {
			targetColor = Color.white;
		}
	}
}
