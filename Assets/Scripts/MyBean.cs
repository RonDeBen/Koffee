using UnityEngine;
using System.Collections;

public class MyBean : MonoBehaviour {

	void OnBecameInvisible(){
		SpriteRenderer sprender = gameObject.GetComponent<SpriteRenderer>();
		sprender.enabled = false;
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		rb.isKinematic = true;
		rb.gravityScale = 0f;
		GameObject player = GameObject.FindWithTag("Player");
		gameObject.transform.position = player.transform.position;
		BeanSpawner.SpawnNewBean();
	}
}
