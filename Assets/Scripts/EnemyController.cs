using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed;
    public GameObject looks;
	private Rigidbody2D rb;
	private Transform leftCheck, rightCheck;
	bool isMovingRight, rightWall, leftWall;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		leftCheck = transform.Find("leftCheck");
		rightCheck = transform.Find("rightCheck");

		rb.velocity = isMovingRight ? Vector3.right * speed : Vector3.left * speed;
        if (!isMovingRight)
            Flip();
	}
	
	// Update is called once per frame
	void Update () {
		rightWall = Physics2D.Linecast(gameObject.transform.position, rightCheck.position, 1 << LayerMask.NameToLayer("wall"));
		leftWall = Physics2D.Linecast(gameObject.transform.position, leftCheck.position, 1 << LayerMask.NameToLayer("wall"));

		if (rightWall && isMovingRight){
			isMovingRight = false;
			rb.velocity = Vector3.left * speed;
            Flip();
		}

		if (leftWall && !isMovingRight){
			isMovingRight = true;
			rb.velocity = Vector3.right * speed;
            Flip();
		}

	}

	public void SetDirection(bool isMovingRight){
		this.isMovingRight = isMovingRight;
	}

    private void Die()
    {
        GameObject.Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("sword"))
        {
            Die();
        }

    }

    void Flip()
    {
        Vector3 toFlip = looks.transform.localScale;
        toFlip.x *= -1;
        looks.transform.localScale = toFlip;
    }
}
