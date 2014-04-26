using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {

	public float GameSpeed;

	GameObject _player, _reflectedPlayer;
	GUIText _scoreGUI, _levelGUI, _highScoreGUI;
	public GameObject[] chunkPrefabs;
	public GameObject[] giftPrefabs;
	bool _spawnGift = false;
	int _score = 0;
	float _distanceTraveled = 0;
	float _distanceToScore = 30;
	int _level = 1;
    bool _gamePaused = true;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find("NormalPlayer");
		_reflectedPlayer = GameObject.Find ("ReflectedPlayer");
		_scoreGUI = GameObject.Find("ScoreGUI").GetComponent<GUIText>();
        _levelGUI = GameObject.Find("LevelGUI").GetComponent<GUIText>();
        _highScoreGUI = GameObject.Find("HighScoreGUI").GetComponent<GUIText>();

        _highScoreGUI.text = "High Score: " + GameStats.HighScore;
        GameStats.Score = 0;


	}
	
	// Update is called once per frame
	void Update () {
		float updateSpeed = GameSpeed * Time.deltaTime;

		GameObject[] results = GameObject.FindGameObjectsWithTag("Ground");

		for(int i = 0; i < results.Length; i++) {
			results[i].transform.position = new Vector2(results[i].transform.position.x - updateSpeed, results[i].transform.position.y);
			results[i].GetComponent<LevelChunkController>().Move(updateSpeed);
		}

		_distanceTraveled += updateSpeed;
		if(_distanceTraveled >= _distanceToScore)
		{
			//print("score ++");
			_score++;
            GameStats.Score = _score;
			_distanceTraveled = 0;
			_scoreGUI.text = "Score: " + _score;

			if(_score % 8 == 0)
			{
				SpeedUp ();
			}

            if (_score > GameStats.HighScore)
            {
                _highScoreGUI.text = "High Score: " + _score;
            }
		}
	}

	public void SpawnSegment() {
		if(_spawnGift)
		{
			int index = Random.Range(0, giftPrefabs.Length);
			GameObject result = (GameObject)Instantiate(giftPrefabs[index]);
			result.transform.position = new Vector2(12.8f, 0);
			_spawnGift = false;
		} else {
			int index = Random.Range(0, chunkPrefabs.Length);
			GameObject result = (GameObject)Instantiate(chunkPrefabs[index]);
			result.transform.position = new Vector2(12.8f, 0);
		}

	}

	void SpeedUp() {

		GameSpeed+=.5f;

		_player.GetComponent<PlatformerCharacter2D>().maxSpeed+=2;
		_reflectedPlayer.GetComponent<PlatformerCharacter2D>().maxSpeed+=2;

		_spawnGift = true;

		_level++;
		_levelGUI.text = "Level: " + _level;
	}


    public static class GameStats
    {

        public static int Score;
        public static string DeathText;
        public static int HighScore;
    }
}
