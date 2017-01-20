using UnityEngine;
using System.Collections;

public class TestMapper : IMapper {


	public int scaleFactor
	{
		get {
			return 1;
		}
	}

	public Vector3 worldPos
	{
		get {
			return new Vector3 (0, 0, 0);
		}
	}

	public bool[,] tileGrid {
		get {
			bool[,] b = new bool[30, 30];
			b [13, 15] = true;
			b [17, 15] = true;
			b [17, 16] = true;
			return b;
		}
	}
}
