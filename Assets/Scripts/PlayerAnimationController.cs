using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        bool left = Input.GetKey("left");
        bool right = Input.GetKey("right");

       animator.SetBool("IsWalking", left || right);

    }
}
