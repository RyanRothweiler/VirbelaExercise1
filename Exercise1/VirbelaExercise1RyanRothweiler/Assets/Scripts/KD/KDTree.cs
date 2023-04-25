using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is not a generalized kdtree. This assumes three dimensions. No balancing, etc
public class KDTree
{
	private KDNode root = null;

	private const int dimensionsTotal = 3;

	public void Add(GameObject obj)
	{
		KDNode n = new KDNode();
		n.obj = obj;
		Add(n);
	}

	public void Add(KDNode newNode)
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

	public GameObject FindNearest(Vector3 origin)
	{
		return Nearest(root, origin, 0).obj;
	}

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
		float bestDist = Vector3.Distance(best.obj.transform.position, origin);

		Vector3 bestPossibleOther = current.obj.transform.position;
		bestPossibleOther[(depth + 1) % dimensionsTotal] = 0;
		bestPossibleOther[(depth + 2) % dimensionsTotal] = 0;
		float bestDistToOther = Vector3.Distance(bestPossibleOther, origin);

		if (bestDist > bestDistToOther) {
			possible = Nearest(other, origin, depth + 1);
			best = Closest(possible, best, origin);
		}

		return best;
	}

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
