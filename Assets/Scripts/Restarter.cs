using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MusicMiddleware.playSound("Happy Jingle");
	
	}
	
	// Update is called once per frame
	void Update () {
        bool restart = Input.GetKeyDown(KeyCode.R);
        if (restart)
        {
            CoffeeScriptHueHueHue.RestartGame();
        }
	}
}
