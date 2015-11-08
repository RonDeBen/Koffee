using UnityEngine;
using System.Collections;

public class MyBean : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "coffee_pot"){
			SpriteRenderer sprender = gameObject.GetComponent<SpriteRenderer>();
			if(sprender.enabled){
				sprender.enabled = false;
				Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
				rb.isKinematic = true;
				rb.gravityScale = 0f;
				GameObject player = GameObject.FindWithTag("Player");
				gameObject.transform.position = player.transform.position;
				BeanSpawner.SpawnNewBean();
				Score.AddPoints(250);
				CoffeeScriptHueHueHue.ReceiveBean();
			}
		}
	}
}
