using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpForce;

	public GameObject myBean;

    public float attackDuration;
    public GameObject holdingSword, swordLeft, swordRight;

    public Transform feet;
    public Transform hands;


	private bool grounded = false;

	private Rigidbody2D rb;
	private Transform groundCheck;
    private bool m_FacingRight = true;
    private GameObject sword;
    private bool isAttacking;

    void Start(){
		rb = GetComponent<Rigidbody2D>();
		groundCheck = transform.Find("groundCheck");
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(gameObject.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("floor")); 

		bool left = Input.GetKey("left");
		bool right = Input.GetKey("right");
		bool jump = Input.GetKeyDown(KeyCode.Z);
		bool attack = Input.GetKeyDown(KeyCode.X);


		if (left){
			rb.velocity = new Vector3(-speed, rb.velocity.y, 0f);
            if (m_FacingRight)
                Flip();
		}

		if (right){
			rb.velocity = new Vector3(speed, rb.velocity.y, 0f);
            if (!m_FacingRight)
                Flip();
        }

		if (!(right || left)){
			Vector3 playerVelocity = rb.velocity;
			playerVelocity.x = 0.0f;
			rb.velocity = playerVelocity;
		}

		if (jump && grounded){
			grounded = false;
			rb.AddForce(Vector3.up * jumpForce);
		}	

        if(sword != null && (Time.time > lastAttack + attackDuration))
        {
            sword.SetActive(false);
            holdingSword.SetActive(true);
            isAttacking = false;
        }

		if (attack){
            Attack();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "bean"){
			BeanController bean = other.gameObject.GetComponent<BeanController>();
			bean.Destroy();
			SpriteRenderer sprender = myBean.GetComponent<SpriteRenderer>();
			sprender.enabled = true;
		}

		if (other.tag == "goal"){
			SpriteRenderer sprender = myBean.GetComponent<SpriteRenderer>();
			if(sprender.enabled){
				Rigidbody2D rb = myBean.GetComponent<Rigidbody2D>();
				rb.isKinematic = false;
				rb.gravityScale = 3f;
			}
		}
	}

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        SideSwitch(feet);
        SideSwitch(hands);
    }

    private void SideSwitch(Transform feet)
    {
        Vector3 facing = feet.localScale;
        facing.x *= -1;
        feet.localScale = facing;
    }

    private float lastAttack;
    private void Attack()
    {
        if(!isAttacking)
        {
            holdingSword.SetActive(false);
            if (m_FacingRight)
            {
                swordRight.SetActive(true);
                sword = swordRight;
            }
            else
            {
                swordLeft.SetActive(true);
                sword = swordLeft;
            }
            isAttacking = true;
            lastAttack = Time.time;
        }
    }
}
