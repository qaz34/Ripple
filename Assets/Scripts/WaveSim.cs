using UnityEngine;
using System.Collections;

public struct WaveUnit
{
	public float magnitude;
	public float conductivity;
	public float acceleration;
	public float velocity;
}

[RequireComponent(typeof(SpriteRenderer))]
public class WaveSim : MonoBehaviour {
	public IMapper map = new TestMapper();
	public int deltaSScale = 10;
	public float decayFactor = 0.99f;
	WaveUnit[,] waveGrid;
	Color[] waveColours;
	Texture2D tex;
	Sprite texSprite;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<Map>();

		sr = GetComponent<SpriteRenderer> ();
		waveGrid = new WaveUnit[map.tileGrid.GetLength(0) * map.scaleFactor * deltaSScale, map.tileGrid.GetLength(1) * map.scaleFactor * deltaSScale];
		tex = new Texture2D(waveGrid.GetLength (0), waveGrid.GetLength (1));
		sr.sprite = texSprite;
		waveColours = new Color[waveGrid.GetLength (0) * waveGrid.GetLength (1)];
		for (int i = 0; i < map.tileGrid.GetLength(0); i++) {
			for (int j = 0; j < map.tileGrid.GetLength (1); j++) {
				if (map.tileGrid [i, j]) {
					for (int k = 0; k < deltaSScale; k++) {
						for (int l = 0; l < deltaSScale; l++) {
							waveGrid [i * deltaSScale + k, j * deltaSScale + l].conductivity = 10f;
						}
					}
				} else {
					for (int k = 0; k < deltaSScale; k++) {
						for (int l = 0; l < deltaSScale; l++) {
							waveGrid [i * deltaSScale + k, j * deltaSScale + l].conductivity = 100f;
						}
					}
				}
			}
		}
		Disturb (100, waveGrid.GetLength (0) / 2, waveGrid.GetLength (1) / 2);

		transform.position += new Vector3 (-waveGrid.GetLength (0) / (2 * map.scaleFactor * deltaSScale), -waveGrid.GetLength (1) / (2 * map.scaleFactor * deltaSScale), 0);
	}

	Color mapColour(float x)
	{

		return new Color(1,1,1,x);
	}

	// Update is called once per frame
	void Update () {

		for (int i = 1; i < waveGrid.GetLength (0) - 1; i++) {
			for (int j = 1; j < waveGrid.GetLength (1) - 1; j++) {
				float x = (((waveGrid[i, j].conductivity + waveGrid[i+1, j].conductivity)/2)*(waveGrid[i+1, j].magnitude - waveGrid[i, j].magnitude)) - (((waveGrid[i, j].conductivity + waveGrid[i-1, j].conductivity)/2)*(waveGrid[i, j].magnitude - waveGrid[i-1, j].magnitude));
				float y = (((waveGrid[i, j].conductivity + waveGrid[i, j+1].conductivity)/2)*(waveGrid[i, j+1].magnitude - waveGrid[i, j].magnitude)) - (((waveGrid[i, j].conductivity + waveGrid[i, j-1].conductivity)/2)*(waveGrid[i, j].magnitude - waveGrid[i, j-1].magnitude));
				waveGrid [i, j].acceleration = x + y;
			}
		}
		for (int i = 0; i < waveGrid.GetLength (0); i++) {
			for (int j = 0; j < waveGrid.GetLength (1); j++) {
				waveGrid [i, j].velocity += waveGrid [i, j].acceleration * Time.deltaTime;
				waveGrid [i, j].magnitude += waveGrid [i, j].velocity * Time.deltaTime;
				waveGrid [i, j].magnitude *= decayFactor;
				waveGrid [i, j].magnitude = Mathf.Max (0, waveGrid [i, j].magnitude);
				waveColours [i + (waveGrid.GetLength (0) * j)] = mapColour(waveGrid[i,j].magnitude);
			}
		}
		tex.SetPixels (waveColours);
		tex.Apply ();
		texSprite = Sprite.Create (tex, new Rect(0, 0, waveGrid.GetLength (0), waveGrid.GetLength (1)), new Vector2(0, 0), deltaSScale * map.scaleFactor);
		texSprite.name = "BOPOP";
		sr.sprite = texSprite;
	}

	public void Disturb (float amount, int x, int y)
	{
		waveGrid [x, y].magnitude = amount;
	}
}
