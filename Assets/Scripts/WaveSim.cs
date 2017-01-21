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
							waveGrid [i * deltaSScale + k, j * deltaSScale + l].conductivity = 0.2f;
						}
					}
				} else {
					for (int k = 0; k < deltaSScale; k++) {
						for (int l = 0; l < deltaSScale; l++) {
							waveGrid [i * deltaSScale + k, j * deltaSScale + l].conductivity = 1f;
						}
					}
				}
			}
		}
		Disturb (1, waveGrid.GetLength (0) / 2, waveGrid.GetLength (1) / 2);
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
				texBytes [((i + (j * waveGrid.GetLength (1))) * 4) + 3] = (byte)(waveGrid [i, j].magnitude * 255);
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
}
