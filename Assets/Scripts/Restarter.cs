using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool jump = Input.GetKeyDown(KeyCode.Z);
        bool attack = Input.GetKeyDown(KeyCode.X);
        if (jump || attack)
        {
            CoffeeScriptHueHueHue.RestartGame();
        }
	}
}
