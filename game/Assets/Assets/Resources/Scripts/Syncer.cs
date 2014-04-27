using UnityEngine;
using System.Collections;

public class Syncer : MonoBehaviour {

    public GameObject Shockwave;

    // Use this for initialization
    void Start()
    {
        Shockwave = GameObject.Find("ShockwaveContainer").GetComponent<ParticleContainer>().ParticleEffect;
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
		
		otherPlayer.transform.position = new Vector2(basePlayerPos.x, otherPlayerPos.y);

        Instantiate(Shockwave, this.transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
