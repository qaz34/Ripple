using UnityEngine;
using System.Collections;

public struct WaveUnit
{
	public float magnitude;
	public float conductivity;
	public float acceleration;
	public float velocity;
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaveSim : MonoBehaviour {
	public IMapper map = new TestMapper();
	public int deltaSScale = 10;
	public float decayFactor = 0.99f;
	public float timeScale = 2;
	public float wallConductivity = 0.01f;
	public float spaceConductivity = 1;
	private Texture2D tex;
	private byte[] texBytes;
	WaveUnit[,] waveGrid;
	MeshRenderer mr;

	// Use this for initialization
	void Start () {
		mr = GetComponent<MeshRenderer> ();
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<Map>();
		waveGrid = new WaveUnit[map.tileGrid.GetLength(0) * map.scaleFactor * deltaSScale, map.tileGrid.GetLength(1) * map.scaleFactor * deltaSScale];
		tex = new Texture2D (waveGrid.GetLength (0), waveGrid.GetLength (1), TextureFormat.RGBA32, false);
		texBytes = new byte[waveGrid.GetLength (0)*waveGrid.GetLength (1)*4];
		for (int i = 0; i < texBytes.Length; i++) {
			texBytes [i] = 255;
		}
		for (int i = 0; i < map.tileGrid.GetLength(0); i++) {
			for (int j = 0; j < map.tileGrid.GetLength (1); j++) {
				if (map.tileGrid [i, j]) {
					for (int k = 0; k < deltaSScale; k++) {
						for (int l = 0; l < deltaSScale; l++) {
							waveGrid [i * deltaSScale + k, j * deltaSScale + l].conductivity = wallConductivity;
						}
					}
				} else {
					for (int k = 0; k < deltaSScale; k++) {
						for (int l = 0; l < deltaSScale; l++) {
							waveGrid [i * deltaSScale + k, j * deltaSScale + l].conductivity = spaceConductivity;
						}
					}
				}
			}
		}
		//Disturb (1, waveGrid.GetLength (0) / 2, waveGrid.GetLength (1) / 2);
	}

	// Update is called once per frame
	void Update () {

		for (int i = 1; i < waveGrid.GetLength (0) - 1; i++) {
			for (int j = 1; j < waveGrid.GetLength (1) - 1; j++) {
				float x = (((waveGrid[i, j].conductivity + waveGrid[i+1, j].conductivity)/2)*(waveGrid[i+1, j].magnitude - waveGrid[i, j].magnitude)) - (((waveGrid[i, j].conductivity + waveGrid[i-1, j].conductivity)/2)*(waveGrid[i, j].magnitude - waveGrid[i-1, j].magnitude));
				float y = (((waveGrid[i, j].conductivity + waveGrid[i, j+1].conductivity)/2)*(waveGrid[i, j+1].magnitude - waveGrid[i, j].magnitude)) - (((waveGrid[i, j].conductivity + waveGrid[i, j-1].conductivity)/2)*(waveGrid[i, j].magnitude - waveGrid[i, j-1].magnitude));
				waveGrid [i, j].acceleration = (x + y);
			}
		}
		for (int i = 0; i < waveGrid.GetLength (0); i++) {
			for (int j = 0; j < waveGrid.GetLength (1); j++) {
				waveGrid [i, j].velocity += waveGrid [i, j].acceleration * Time.deltaTime * timeScale;
				waveGrid [i, j].magnitude += waveGrid [i, j].velocity * Time.deltaTime * timeScale;
				waveGrid [i, j].magnitude *= decayFactor;
				waveGrid [i, j].magnitude = Mathf.Max (0, waveGrid [i, j].magnitude);
				if (waveGrid [i, j].magnitude > 1) {
					print (waveGrid [i, j].magnitude);
				}
				if (waveGrid [i, j].magnitude < 0.01f) {
					texBytes [((i + (j * waveGrid.GetLength (1))) * 4) + 3] = (byte)(0);
				} else {
					texBytes [((i + (j * waveGrid.GetLength (1))) * 4) + 3] = (byte)(500*waveGrid[i,j].magnitude);
				}
				//texBytes [((i + (j * waveGrid.GetLength (1))) * 4) + 3] = (byte)(254.53f/(1 + (25.35f*Mathf.Exp(-9.17f*waveGrid[i,j].magnitude))));

			}
		}

		tex.LoadRawTextureData (texBytes);
		tex.Apply ();
		mr.material.mainTexture = tex;
	}

	public void Disturb (float amount, int x, int y)
	{
		waveGrid [x, y].magnitude = amount;
	}

	public void Disturb (float amount, Vector3 worldPos)
	{
		Vector3 local = new Vector3(worldPos.x + (map.tileGrid.GetLength(0) * map.scaleFactor * 0.5f), worldPos.y + (map.tileGrid.GetLength(1) * map.scaleFactor * 0.5f));
		local *= deltaSScale;
		waveGrid [(int)local.x, (int)local.y].magnitude = amount;
	}
}
