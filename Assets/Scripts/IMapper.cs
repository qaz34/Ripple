using System;
using UnityEngine;

public interface IMapper
{
	float scaleFactor
	{
		get;
	}

	Vector3 worldPos
	{
		get;
	}

	bool[,] tileGrid {
		get;
	}
}


