using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool IsReflected;

    AudioSource _death;
    float _timeElapsedBeforeSceneChange = 0f;
    float _sceneChangeDelay = 3f;
    bool _gameOver = false;
    public GameObject ExplosionPrefab;

	// Use this for initialization
	void Start () {
        _death = GameObject.Find("Explosion").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckGameOver();

        if (_gameOver) { return; }
		if(IsReflected && this.transform.position.y > 30)
		{
			print ("Reflected char fell");
            Director.GameStats.DeathText = "Your drone exited the bounds of the dimensional anomaly!";
            Die();
            ReflectDeath();
		}

		if(!IsReflected && this.transform.position.y < -30)
		{
			print ("Regular char fell");
            Director.GameStats.DeathText = "Your drone exited the bounds of the dimensional anomaly!";
            Die();
            ReflectDeath();
		}

		if(this.transform.position.x < -15)
		{
			print ("Fell behind");
            Director.GameStats.DeathText = "Your drone exited the bounds of the dimensional anomaly!";
            Die();
            ReflectDeath();
            
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.GetComponent<PlayerController>() != null && !_gameOver)
		{
			print ("Hit other player");
            Director.GameStats.DeathText = "Your drones collided, violating the dimensional anomaly's physical laws\n and caused it to collapse!";
            Die();
		}
	}

    void ReflectDeath()
    {
        PlayerController otherPlayer;
        if (IsReflected)
        {
            otherPlayer = GameObject.Find("NormalPlayer").GetComponent<PlayerController>();
        }
        else
        {
            otherPlayer = GameObject.Find("ReflectedPlayer").GetComponent<PlayerController>();
        }

        otherPlayer.Die();
    }

    public void Die()
    {
        _death.Play();
        Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);
        _gameOver = true;
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
