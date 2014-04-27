using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool IsReflected;

    public AudioSource Death;
    float _timeElapsedBeforeSceneChange = 0f;
    float _sceneChangeDelay = 3f;
    bool _gameOver = false;

	// Use this for initialization
	void Start () {
        Death = GameObject.Find("Explosion").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckGameOver();

        if (_gameOver) { return; }
		if(IsReflected && this.transform.position.y > 30)
		{
			print ("Reflected char fell");
            Director.GameStats.DeathText = "Your drone exited the bounds of the dimensional anomaly!";
            Death.Play();
            _gameOver = true;
            
		}

		if(!IsReflected && this.transform.position.y < -30)
		{
			print ("Regular char fell");
            Director.GameStats.DeathText = "Your drone exited the bounds of the dimensional anomaly!";
            Death.Play();
            _gameOver = true;
		}

		if(this.transform.position.x < -15)
		{
			print ("Fell behind");
            Director.GameStats.DeathText = "Your drone exited the bounds of the dimensional anomaly!";
            Death.Play();
            _gameOver = true;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.GetComponent<PlayerController>() != null && !_gameOver)
		{
			print ("Hit other player");
            Director.GameStats.DeathText = "Your drones collided, violating the dimensional anomaly's physical laws\n and caused it to collapse!";
            Death.Play();
            _gameOver = true;
		}
	}

    void CheckGameOver()
    {
        if (_gameOver)
        {
            this.renderer.enabled = false;
            _timeElapsedBeforeSceneChange += Time.deltaTime;

            if (_timeElapsedBeforeSceneChange >= _sceneChangeDelay)
            {
                Application.LoadLevel(2);
            }
        }
    }
}
