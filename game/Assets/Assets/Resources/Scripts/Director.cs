using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {

	public float GameSpeed;

	GameObject _player, _reflectedPlayer;
	GUIText _scoreGUI;
	public GameObject[] chunkPrefabs;
	int _score = 0;
	float _distanceTraveled = 0;
	float _distanceToScore = 100;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find("NormalPlayer");
		_reflectedPlayer = GameObject.Find ("ReflectedPlayer");
		_scoreGUI = GameObject.Find("ScoreGUI").GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		float updateSpeed = GameSpeed * Time.deltaTime;

		GameObject[] results = GameObject.FindGameObjectsWithTag("Ground");

		for(int i = 0; i < results.Length; i++) {
			results[i].transform.position = new Vector2(results[i].transform.position.x - updateSpeed, results[i].transform.position.y);
			results[i].GetComponent<VisibilityChecker>().Move(updateSpeed);
		}

		_distanceTraveled += updateSpeed;
		if(_distanceTraveled >= _distanceToScore)
		{
			print("score ++");
			_score++;
			_distanceTraveled = 0;

			if(_score % 10 == 0)
			{
				GameSpeed++;
			}
		}
	}

	public void SpawnSegment() {
		int index = Random.Range(0, chunkPrefabs.Length);
		GameObject result = (GameObject)Instantiate(chunkPrefabs[index]);
		result.transform.position = new Vector2(12.8f, 0);
		_score++;
		_scoreGUI.text = "Score: " + _score;

		if(_score % 10 == 0)
		{
			GameSpeed++;
		}
	}
}
