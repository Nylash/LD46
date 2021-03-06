using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
#pragma warning disable 0649
	[Header("CONFIGURATION")]
	[SerializeField] private float jumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
	[SerializeField] private bool airControl = false;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private Transform groundCheck;
#pragma warning restore 0649

	[Header("COMPONENTS")]
	public Rigidbody2D rb;
	public Transform JLFXspot;

	[Header("VARIABLES")]
	public GameObject JLFX;
	public ParticleSystem walkFX;
	const float groundedRadius = .2f;
	public bool grounded;
	bool facingRight = true;
	Vector3 velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = grounded;
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
				if (!wasGrounded)
				{
					PlayerManager.instance.animController.SetBool("Landing", true);
					walkFX.Play();
					Instantiate(JLFX, JLFXspot.transform.position, JLFX.transform.rotation);
				}	
			}
		}
		if (!grounded)
		{
			if (walkFX.isEmitting)
				walkFX.Stop();
			PlayerManager.instance.animController.SetBool("Landing", false);
		}
			
	}


	public void Move(float horizontalMove, bool jump, bool callFromLadder = false)
	{
		if (grounded)
		{
			Vector3 targetVelocity = new Vector2(horizontalMove * 10f, rb.velocity.y);
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

			if (horizontalMove > 0 && !facingRight)
				Flip();
			else if (horizontalMove < 0 && facingRight)
				Flip();
		}
		if (airControl)
		{
			Vector3 targetVelocity = new Vector2(horizontalMove * 5f, rb.velocity.y);
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

			if (horizontalMove > 0 && !facingRight)
				Flip();
			else if (horizontalMove < 0 && facingRight)
				Flip();
		}
		if ((grounded || callFromLadder) && jump)
		{
			Instantiate(JLFX, JLFXspot.transform.position, JLFX.transform.rotation);
			grounded = false;
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(new Vector2(0f, jumpForce));
			PlayerManager.instance.animController.speed = 1;
			PlayerManager.instance.animController.SetTrigger("Jump");
		}
	}

	public void Move(float horizontalMove, float verticalMove)
	{
		if (grounded)
		{
			Vector3 targetVelocity;
			if (verticalMove != 0f)
				targetVelocity = new Vector2(horizontalMove * 2.5f, verticalMove * 10f);
			else
				targetVelocity = new Vector2(horizontalMove * 10f, rb.velocity.y);
			
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

			if (horizontalMove > 0 && !facingRight)
				Flip();
			else if (horizontalMove < 0 && facingRight)
				Flip();
		}
		else
		{
			Vector3 targetVelocity = new Vector2(horizontalMove * 2.5f, verticalMove * 10f);
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

			if (horizontalMove > 0 && !facingRight)
				Flip();
			else if (horizontalMove < 0 && facingRight)
				Flip();
		}
	}


	private void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
