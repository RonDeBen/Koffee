using UnityEngine;
using System.Collections;

public class BeanController : MonoBehaviour {

	void Start(){
		Debug.Log(gameObject.GetComponent<BoxCollider2D>().isTrigger);
	}


	public void Destroy(){
		Object.Destroy(this.gameObject);
	}

}
