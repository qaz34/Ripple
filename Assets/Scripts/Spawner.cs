﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	IMapper map;
	WaveSim ws;
	public CharController playerPrefab;
	CharController[] players;
	public int[] scores;
	bool[] spawning;
	bool[] coroutineCalled;
	public float respawnTime;

	// Use this for initialization
	void Awake () {
		players = new CharController[2];
		scores = new int[2];
		spawning = new bool[2];
		coroutineCalled = new bool[2];
		spawning [0] = spawning [1] = coroutineCalled[0] = coroutineCalled[1] = true;
		ws = GameObject.FindGameObjectWithTag ("Wave").GetComponent<WaveSim> ();
		map = GameObject.FindGameObjectWithTag ("Map").GetComponent<Map>();
		StartCoroutine(spawn (0));
		StartCoroutine(spawn (1));
	}


	IEnumerator spawn (int player, bool delay = false)
	{
		if (delay)
			yield return new WaitForSeconds (respawnTime);
		else
			yield return null;
		bool needValidLocation = true;
		int x = 0;
		int y = 0;
		while (needValidLocation) {
			x = Random.Range (0, map.tileGrid.GetLength (0));
			y = Random.Range (0, map.tileGrid.GetLength (1));
			if (!map.tileGrid [x, y]) {
				needValidLocation = false;
			}
		}
		GameObject go = Instantiate<GameObject> (playerPrefab.gameObject);
		players [player] =  go.GetComponent<CharController>();
		players [player].transform.position = new Vector3(x - (map.tileGrid.GetLength (0) / 2), y - (map.tileGrid.GetLength(1)/2));
		ws.Disturb (1, players [player].transform.position);
		players [player].player = player+1;
		spawning [player] = false;

	}

	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < players.Length; i++) {
			if (players [i] == null && !spawning[i]) {
				scores [(i + 1) % 2]++;
				spawning [i] = true;
			}

			if (spawning [i] && !coroutineCalled [i]) {
				StartCoroutine (spawn (i, true));
				coroutineCalled [i] = true;
			}
			
		}

	}
}
