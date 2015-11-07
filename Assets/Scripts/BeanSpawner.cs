using UnityEngine;
using System.Collections;

public class BeanSpawner : MonoBehaviour {

	public GameObject beanObj;
	public GameObject[] beanLocationObjects;

	private static Vector3[] beanLocations;
	private static GameObject bean;

	// Use this for initialization
	void Awake () {
		beanLocations = new Vector3[beanLocationObjects.Length];
		for(int k = 0; k < beanLocationObjects.Length; k++){
			beanLocations[k] = beanLocationObjects[k].transform.position;
		}

		bean = beanObj;
	}

	void Start(){
		SpawnNewBean();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void SpawnNewBean(){
		Vector3 beanPosition = beanLocations[Random.Range(0, beanLocations.Length)];
		GameObject go = Instantiate(bean, beanPosition, Quaternion.identity) as GameObject;
		GameObject UpAndDownGuy = GameObject.Find("UpAndDownGuy");
		go.transform.parent = UpAndDownGuy.transform;
	}
}
