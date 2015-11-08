using UnityEngine;
using System.Collections;

public class CoffeeScriptHueHueHue : MonoBehaviour {

	public Sprite[] coffeeProgressSprites;
	private static Sprite[] sprites;
	private static SpriteRenderer sr;

	private static int beanCount = 0;


	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
		sprites = coffeeProgressSprites;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void ReceiveBean(){
		sr.sprite = sprites[beanCount];
		if(beanCount == sprites.Length - 1){
			//you win the game
		}
		beanCount++;
	}

	public static void ReceiveEnemy(){
		//you lose the game
	}

}
