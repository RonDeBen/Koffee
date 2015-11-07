using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoffeeScriptHueHueHue : MonoBehaviour {

	public Sprite[] coffeeProgressSprites;
	public GameObject panelObj;
	public Text gameOverTextObj;
	public Image loseImageObj, winImageObj;
	public Color gameOverColor;
	public Color victoryColor;
    public static CoffeeScriptHueHueHue coffeePot;
	private static Color winColor, loseColor;
	private static Sprite[] sprites;
	private static SpriteRenderer sr;
	private static Image canvasImage, winImage, loseImage;
	private static Text gameOverText;

	private static bool winOrLoss = false;

	private static int beanCount = 1;


	// Use this for initialization
	void Awake () {
        if(coffeePot != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            coffeePot = this;
        }
		sr = gameObject.GetComponent<SpriteRenderer>();
		sprites = coffeeProgressSprites;
		winColor = victoryColor;
		loseColor = gameOverColor;

		winImage = winImageObj;
		loseImage = loseImageObj;

		canvasImage = panelObj.GetComponent<Image>();
		gameOverText = gameOverTextObj;
	}

	void Start(){
		MusicMiddleware.loopSound("Yo Cuppa Joe", true);
	}
	
	// Update is called once per frame
	void Update () {
		if (winOrLoss && Input.GetKeyDown(KeyCode.R)){
			RestartGame();
		}
	}

	public static void RestartGame(){
		beanCount = 1;
		Application.LoadLevel("final");
		Time.timeScale = 1;
	}

	public static void ReceiveBean(){
        if (beanCount == sprites.Length + 1)
        {
            PlayerPrefs.SetInt("Score", Score.GetScore());
            PlayerPrefs.SetInt("Level", Score.GetLevel() + 1);
            canvasImage.color = winColor;
            winImage.enabled = true;
            Time.timeScale = 0;
            MusicMiddleware.pauseAllSounds();
            MusicMiddleware.playSound("Happy Jingle");
            winOrLoss = true;
        }
        else
        {
            sr.sprite = sprites[beanCount - 1];
        }
        Debug.Log(beanCount);
		
		beanCount++;
        //Debug.Break();
	}

	public static void ReceiveEnemy(){
		//you lose the game
		winOrLoss = true;
		loseImage.enabled = true;
		PlayerPrefs.SetInt("Score", 0);
		PlayerPrefs.SetInt("Level", 1);
		canvasImage.color = loseColor;
		gameOverText.enabled = true;
		Time.timeScale = 0;
		MusicMiddleware.pauseAllSounds();
		MusicMiddleware.playSound("Sad Jingle");
	}

}
