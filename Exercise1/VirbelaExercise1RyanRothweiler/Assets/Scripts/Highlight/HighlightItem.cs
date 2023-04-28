using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tag an item for highlighting.
/// <summary>
/// Tag an item for highlighting.
/// Animates the highlight color
/// </summary>
public class HighlightItem : MonoBehaviour
{
	[SerializeField] private SpawnType type;
	public SpawnType GetSpawnType() { return type; }

	private Renderer rend = null;
	private SpawnItemDefinition def = null;

	private bool isAnimating = false;
	private Color targetColor;
	private Color currColor;

	// Stop animating when the target is close enough.
	private const float closeEnough = 0.01f;
	// Speed of animation
	private const float lerpSpeed = 25f;

	// Do in awake so that other behaviors can use this. Also good practice to get components in awake
	public void Awake()
	{
		// Get renderer and verify its there
		rend = this.GetComponent<Renderer>();
		if (rend == null) {
			Debug.LogError("Missing required renderer.");
			this.enabled = false;
		}
	}

	public void Start()
	{
		// Get item definition
		def = SpawnManager.GetSpawnItemDefinition(type);

		// Verify data
		if (def == null) {
			Debug.LogError("Missing required spawn item definition.");
			this.enabled = false;
			return;
		}
		if (!rend.material.HasProperty(def.materialColorProperty)) {
			Debug.LogError($"Material missing required property {def.materialColorProperty}");
			this.enabled = false;
			return;
		}

		rend.material.SetColor(def.materialColorProperty, def.baseColor);
		currColor = def.baseColor;
	}

	public void Update()
	{
		/*
		 lerp the color. There are other lerp methods that maybe avoid using Update functions or target subtraction.
		 But this method always feels the best, is super simple, is easily readable, easily change-able, and easily understood. So I prefer this.
		*/
		if (isAnimating) {

			// Lerp color.
			// This creates a new material instance. Which breaks batching. When using the base color switch to using the sharedMaterial and destroy the one created by this.
			currColor = Color.Lerp(currColor, targetColor, Time.deltaTime * lerpSpeed);
			rend.material.SetColor(def.materialColorProperty, currColor);

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

	// Sets the highlight state.
	/// <summary>
	/// Sets the highlight state to either highlighted or not.
	/// </summary>
	/// <param name="state">Is highlighted or not.</param>
	public void SetHighlight(bool state)
	{
		// Have things we need
		if (rend == null || def == null) { return; }

		// Material has property we need
		if (!rend.material.HasProperty(def.materialColorProperty)) {
			Debug.LogError($"The material is missing the properties {def.materialColorProperty} needed to highlight.");
			return;
		}

		isAnimating = true;

		// Update target
		if (state) {
			targetColor = def.highlightColor;
		} else {
			targetColor = def.baseColor;
		}
	}
}
