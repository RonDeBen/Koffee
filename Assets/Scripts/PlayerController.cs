using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpForce;

	public GameObject myBean, leftFoot, rightFoot;

	public float duration, magnitude;

    public float attackDuration;
    public Transform t_RightSword;
    public Transform t_LeftSword;
    public GameObject p_RightSword;
    public GameObject p_LeftSword;


	private bool grounded = false;

	private Rigidbody2D rb;
	private Transform groundCheck;
    private bool m_FacingRight = true;
    private GameObject sword;

    private SpriteRenderer leftFootSprender, rightFootSprender;

    void Start(){
		rb = GetComponent<Rigidbody2D>();
		groundCheck = transform.Find("groundCheck");

		leftFootSprender = leftFoot.GetComponent<SpriteRenderer>();
		rightFootSprender = rightFoot.GetComponent<SpriteRenderer>();
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
			leftFootSprender.sortingOrder = -1;
			rightFootSprender.sortingOrder = 2;
			m_FacingRight = false;
		}

		if (right){
			rb.velocity = new Vector3(speed, rb.velocity.y, 0f);
			leftFootSprender.sortingOrder = 2;
			rightFootSprender.sortingOrder = -1;
			m_FacingRight = true;
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
            GameObject.Destroy(sword);
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

		if (other.tag == "enemy"){
			StartCoroutine(Shake());
		}
	}

    private float lastAttack;
    private void Attack()
    {
        if(sword == null)
        {
            
            if (m_FacingRight)
            {
                sword = GameObject.Instantiate(p_RightSword);
                sword.transform.SetParent(t_RightSword);
                sword.transform.localPosition = Vector3.zero;
                sword.transform.localScale = new Vector3(0.04783243f, 0.04783245f, 0.04783245f);
            }
            else
            {
                sword = GameObject.Instantiate(p_LeftSword);
                sword.transform.SetParent(t_LeftSword);
                sword.transform.localPosition = Vector3.zero;
                sword.transform.localScale = new Vector3(-0.04783243f, 0.04783245f, 0.04783245f);
            }
            lastAttack = Time.time;
        }
    }

    IEnumerator Shake() {
	        
	    float elapsed = 0.0f;
	    
	    Vector3 originalCamPos = Camera.main.transform.position;
	    
	    while (elapsed < duration) {
	        
	        elapsed += Time.deltaTime;          
	        
	        float percentComplete = elapsed / duration;         
	        float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
	        
	        // map value to [-1, 1]
	        float x = Random.value * 2.0f - 1.0f;
	        float y = Random.value * 2.0f - 1.0f;
	        x *= magnitude * damper;
	        y *= magnitude * damper;
	        
	        Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);
	            
	        yield return null;
	    }
	    
	    Camera.main.transform.position = originalCamPos;
	}
}
