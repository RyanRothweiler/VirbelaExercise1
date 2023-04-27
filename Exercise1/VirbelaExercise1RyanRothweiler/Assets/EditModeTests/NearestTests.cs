using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NearestTests
{
	void TestImplementation(IGetNearest impl)
	{
		GameObject objOne = new GameObject("First");
		objOne.transform.position = new Vector3(10, 10, 10);

		GameObject objTwo = new GameObject("Second");
		objTwo.transform.position = new Vector3(1, 1, 1);

		GameObject objThree = new GameObject("Third");
		objThree.transform.position = new Vector3(0, 100, 0);

		impl.Add(objOne);
		impl.Add(objTwo);
		impl.Add(objThree);

		Debug.Assert(impl.FindNearest(new Vector3(0, 99, 0)) == objThree);
		Debug.Assert(impl.FindNearest(new Vector3(0, 0, 0)) == objTwo);
		Debug.Assert(impl.FindNearest(new Vector3(0, 200, 0)) == objThree);
		Debug.Assert(impl.FindNearest(new Vector3(8, 8, 8)) == objOne);
	}

	[Test]
	public void KDTree()
	{
		TestImplementation(new KDTree());
	}

	[Test]
	public void Slow()
	{
		TestImplementation(new GetNearestSlow());
	}
}
