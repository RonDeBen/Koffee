using UnityEngine;
using System.Collections;

public class MyBean : MonoBehaviour {

	public GameObject beanHome;

	void OnTriggerEnter2D(Collider2D other){
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (other.tag == "coffee_pot" && !Mathf.Approximately(rb.gravityScale, 0f))
        {
			SpriteRenderer sprender = gameObject.GetComponent<SpriteRenderer>();
			if(sprender.enabled){
				sprender.enabled = false;
				rb = gameObject.GetComponent<Rigidbody2D>();
				rb.isKinematic = true;
				rb.gravityScale = 0f;
				gameObject.transform.position = beanHome.transform.position;
				BeanSpawner.SpawnNewBean();
				Score.AddPoints(250);
				CoffeeScriptHueHueHue.ReceiveBean();
			}
		}
	}

    void Update()
    {
        SpriteRenderer sprender = gameObject.GetComponent<SpriteRenderer>();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if(sprender.enabled && Mathf.Approximately(rb.gravityScale,0f) && !Vector3.Equals(gameObject.transform.position, beanHome.transform.position))
        {
            gameObject.transform.position = beanHome.transform.position;
        }
    }
}
