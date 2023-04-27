using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is not a generalized kdtree. This assumes three dimensions. No balancing, etc
/// </summary>
public class KDTree : IGetNearest
{
	private KDNode root = null;

	private const int dimensionsTotal = 3;

	/// <summary>
	/// Add to the tree
	/// </summary>
	/// <param name="obj">Object adding to the tree.</param>
	public void Add(GameObject obj)
	{
		KDNode n = new KDNode();
		n.obj = obj;
		Add(n);
	}

	/// <summary>
	/// Add a new node to the tree
	/// </summary>
	/// <param name="newNode">New node adding to the tree</param>
	private void Add(KDNode newNode)
	{
		// validation
		if (newNode.obj == null) {
			Debug.LogError("Attempting to add empty obj to kd tre");
			return;
		}

		// start with root if we don't have one
		if (root == null) {
			root = newNode;
			return;
		}

		// add to tree
		KDNode current = root;
		int dimension = 0;
		while (true) {
			if (newNode.obj.transform.position[dimension] > current.obj.transform.position[dimension]) {
				// Add right

				if (current.right == null) {
					current.right = newNode;
					break;
				} else {
					current = current.right;
				}

			} else {
				// Add left, adds left if equal also

				if (current.left == null) {
					current.left = newNode;
					break;
				} else {
					current = current.left;
				}

			}

			// assumes three dimensions
			dimension = (dimension + 1) % dimensionsTotal;
		}
	}

	/// <summary>
	/// Gets the nearest GameObject to a point
	/// </summary>
	/// <returns>
	/// The GameObject that is nearest
	/// </returns>
	/// <param name="origin">The point of which to get nearest for.</param>
	public GameObject FindNearest(Vector3 origin)
	{
		return Nearest(root, origin, 0).obj;
	}

	/// <summary>
	/// Gets the nearest KDNode to a point. This is recursive.
	/// </summary>
	/// <returns>
	/// The KDNode that is nearest
	/// </returns>
	/// <param name="current">Current node in the recursion.</param>
	/// <param name="depth">Current depth of the recursion. Used to get the correct dimension.</param>
	/// <param name="origin">The point of which to get nearest for.</param>
	private KDNode Nearest(KDNode current, Vector3 origin, int depth)
	{
		if (current == null) { return null; }

		KDNode next = null;
		KDNode other = null;
		if (current.obj.transform.position[depth % dimensionsTotal] < origin[depth % dimensionsTotal]) {
			next = current.right;
			other = current.left;
		} else {
			next = current.left;
			other = current.right;
		}

		KDNode possible = Nearest(next, origin, depth + 1);
		KDNode best = Closest(current, possible, origin);

		// Handle other side of tree if its possible a point over there might be closer
		float bestCurrentDist = DistSquared(best.obj.transform.position, origin);
		float bestDistToOther = origin[depth % dimensionsTotal] - current.obj.transform.position[depth % dimensionsTotal];

		if (bestCurrentDist > bestDistToOther * bestDistToOther) {
			possible = Nearest(other, origin, depth + 1);
			best = Closest(possible, best, origin);
		}

		return best;
	}

	/// <summary>
	/// Gets distance squared. Doesn't sqrt.
	/// </summary>
	/// <returns>
	/// The distance squared without sqrt.
	/// </returns>
	/// <param name="a">The first point in the distance</param>
	/// <param name="b">The second point in the distance</param>
	float DistSquared(Vector3 a, Vector3 b)
	{
		float total = 0;

		for (int i = 0; i < dimensionsTotal; i++) {
			float diff = Mathf.Abs(a[i] - b[i]);
			total += Mathf.Pow(diff, 2);
		}
		return total;
	}

	/// <summary>
	/// Gets distance squared. Doesn't sqrt.
	/// </summary>
	/// <returns>
	/// The distance squared without sqrt.
	/// </returns>
	/// <param name="a">The first point in the distance</param>
	/// <param name="b">The second point in the distance</param>
	private KDNode Closest(KDNode current, KDNode possible, Vector3 origin)
	{
		if (possible == null) { return current; }
		if (current == null) { return possible; }

		float distPossible = Vector3.Distance(origin, possible.obj.transform.position);
		float distRoot = Vector3.Distance(origin, current.obj.transform.position);
		if (distPossible < distRoot) {
			return possible;
		} else {
			return current;
		}
	}
}
