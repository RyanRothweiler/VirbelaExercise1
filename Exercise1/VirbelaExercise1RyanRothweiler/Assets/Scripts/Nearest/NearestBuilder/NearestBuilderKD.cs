using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestBuilderKD : IGetNearestBuilder
{
	public IGetNearest BuildImplementation()
	{
		return new KDTree();
	}
}
