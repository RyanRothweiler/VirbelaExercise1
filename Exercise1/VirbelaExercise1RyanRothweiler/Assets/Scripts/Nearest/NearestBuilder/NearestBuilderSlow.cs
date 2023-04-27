using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestBuilderSlow : IGetNearestBuilder
{
	public IGetNearest BuildImplementation()
	{
		return new GetNearestSlow();
	}
}
