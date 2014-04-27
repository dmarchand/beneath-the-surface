using UnityEngine;
using System.Collections;

public class Paralyzer : MonoBehaviour {

	public float Duration;
    public GameObject Shockwave;
    AudioSource _sound;

	// Use this for initialization
	void Start () {
        Shockwave = GameObject.Find("ShockwaveContainer").GetComponent<ParticleContainer>().ParticleEffect;
        _sound = GameObject.Find("disrupt1").GetComponent<AudioSource>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {

		PlatformerCharacter2D player = coll.GetComponent<PlatformerCharacter2D>();

		if(player != null) {
			player.Paralyze(Duration);
            Instantiate(Shockwave, this.transform.position, Quaternion.identity);
            _sound.Play();
			Destroy(this.gameObject);
		}
	}
}
