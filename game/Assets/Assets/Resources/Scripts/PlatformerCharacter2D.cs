using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour 
{
	public bool facingRight = true;							// For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 200f;			// Amount of force added when the player jumps.	
	[SerializeField] float extraJumpForce = 50f;
	[SerializeField] float maxJumpForce = 400f;
	float currentJumpForce = 0f;

	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
	
	Transform groundCheck;								// A position marking where to check if the player is grounded.
	float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
	bool grounded = false;								// Whether or not the player is grounded.
	Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up
	Animator anim;										// Reference to the player's animator component.

	bool isParalyzed = false;
	float paralyzeTimeRemaining = 0f;

    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();
	}


	void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);

		// Set the vertical animation
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

		if(isParalyzed)
		{
			paralyzeTimeRemaining -= Time.fixedDeltaTime;

			if(paralyzeTimeRemaining <= 0)
			{
				isParalyzed = false;
				this.GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}


	public void Move(float move, bool crouch, bool jump, bool jumpHeld)
	{

		if(isParalyzed)
		{
			return;
		}

		// If crouching, check to see if the character can stand up
		if(!crouch && anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if( Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true;
		}

		// Set whether or not the character is crouching in the animator
		anim.SetBool("Crouch", crouch);

		if(grounded)
		{
			currentJumpForce = 0f;
		}

		//only control the player if grounded or airControl is turned on
		if(grounded || airControl)
		{


			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move * crouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat("Speed", Mathf.Abs(move));

			// Move the character
			rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if(move > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(move < 0 && facingRight)
				// ... flip the player.
				Flip();
		}

        // If the player should jump...
        if (grounded && jump) {
            // Add a vertical force to the player.
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode.VelocityChange);
			//rigidbody2D.
			//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 100f);
			currentJumpForce += jumpForce;
			print ("Added " + jumpForce + ". Current force is " + currentJumpForce);
        }

		if (!grounded && jumpHeld && Mathf.Abs(currentJumpForce) < Mathf.Abs(maxJumpForce)) {

			float forceDiff = Mathf.Abs(maxJumpForce) - Mathf.Abs(currentJumpForce);
			float forceToAdd = extraJumpForce;

			if(forceDiff < Mathf.Abs(extraJumpForce))
			{
				forceToAdd = forceDiff;
				if(extraJumpForce < 0)
				{
					forceToAdd *= -1;
				}
			}

			rigidbody2D.AddForce(new Vector2(0f, forceToAdd), ForceMode.Acceleration);
			print ("Added " + forceToAdd + ". Current force is " + currentJumpForce);
			currentJumpForce += forceToAdd;
		}

		if (!grounded && !jumpHeld)
		{
			rigidbody2D.AddForce(new Vector2(0f, -extraJumpForce), ForceMode.Acceleration);
		}
	}

	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Paralyze(float duration) 
	{
		isParalyzed = true;
		paralyzeTimeRemaining = duration;
		rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		this.GetComponent<SpriteRenderer>().color = Color.yellow;
		anim.SetFloat("Speed", 0f);
	}
}


public static class Physics2DExtensions {
	public static void AddForce (this Rigidbody2D rigidbody2D, Vector2 force, ForceMode mode = ForceMode.Force) {
		switch (mode) {
		case ForceMode.Force:
			rigidbody2D.AddForce (force);
			break;
		case ForceMode.Impulse:
			rigidbody2D.AddForce (force / Time.fixedDeltaTime);
			break;
		case ForceMode.Acceleration:
			rigidbody2D.AddForce (force * rigidbody2D.mass);
			break;
		case ForceMode.VelocityChange:
			rigidbody2D.AddForce (force * rigidbody2D.mass / Time.fixedDeltaTime);
			break;
		}
	}
	
	public static void AddForce (this Rigidbody2D rigidbody2D, float x, float y, ForceMode mode = ForceMode.Force) {
		rigidbody2D.AddForce (new Vector2 (x, y), mode);
	}
}
