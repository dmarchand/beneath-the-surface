using UnityEngine;
using System.Collections;

public class Swapper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
		player.transform.position = new Vector2(otherPlayerPos.x, basePlayerPos.y);

		Destroy(this.gameObject);
	}
}
