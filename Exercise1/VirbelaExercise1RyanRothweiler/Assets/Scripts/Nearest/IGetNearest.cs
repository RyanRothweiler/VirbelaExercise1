using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetNearest
{
	public void Add(GameObject obj);
	public GameObject FindNearest(Vector3 origin);
}
