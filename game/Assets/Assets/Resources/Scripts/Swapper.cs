using UnityEngine;
using System.Collections;

public class Swapper : MonoBehaviour {

    public GameObject Shockwave;
    AudioSource _sound;

    // Use this for initialization
    void Start()
    {
        Shockwave = GameObject.Find("ShockwaveContainer").GetComponent<ParticleContainer>().ParticleEffect;
        _sound = GameObject.Find("swap").GetComponent<AudioSource>(); 
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		PlayerController player = coll.GetComponent<PlayerController>();

		if(player == null) { return; }

		PlayerController otherPlayer;

		if(player.IsReflected) {
			otherPlayer = GameObject.Find("NormalPlayer").GetComponent<PlayerController>();
		} else {
			otherPlayer = GameObject.Find("ReflectedPlayer").GetComponent<PlayerController>();
		}


		Vector2 basePlayerPos = player.transform.position;
		Vector2 otherPlayerPos = otherPlayer.transform.position;

		otherPlayer.transform.position = new Vector2(basePlayerPos.x, 0 - basePlayerPos.y);
		player.transform.position = new Vector2(otherPlayerPos.x, 0 - otherPlayerPos.y);


        Instantiate(Shockwave, this.transform.position, Quaternion.identity);
        _sound.Play();
		Destroy(this.gameObject);
	}
}
