using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] Transform groundCapsule;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    float horizontal;

    public float maxSpeed;
    public float acceltime;
    public float deceltime;
    public float airAccel;
    public float jumpForgiveness;
 

    public float scaleOfMaximumSpeediness;
    public float minMaximumSpeed;
    private float targetxVelocity;
    private float oldScaleFactor;
  

    private bool m_Grounded;


    public UnityEvent OnLandEvent;

    // Tweening Variables
    private float oldHorizontal;
    float start;
    float diff;
    float progress;
    float rate;
    float time;

    // Holding that strange vertical velocity bug accountable
    float oldy;

    // collision momentum killing
    float oldx;
    float oldtime;
    float expectedAcceleration;

    bool resetFlag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        oldScaleFactor = scaleFactor();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (OnLandEvent == null) {
		    OnLandEvent = new UnityEvent();
        }
        OnLandEvent.AddListener(Landed);
        horizontal = 0;
        oldHorizontal = 0;
        oldx = transform.position.x;
        oldy = transform.position.y;
        oldtime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update() {
        if(GameManager.Instance.canMove){
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0){
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
        if (horizontal < 0 && m_Grounded) {
            spriteRenderer.flipX = true;
            //transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else if (horizontal > 0 && m_Grounded) {
            spriteRenderer.flipX = false;
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        animator.SetFloat("RawHorizontal", horizontal);
            if(Input.GetKeyDown("space") && animator.GetBool("grounded"))
{
                rb2D.AddForce(Vector2.up * 500 * rb2D.mass);
                if (rb2D.linearVelocity.y > 0.1f)
                {
                    rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0.1f);
                }

                animator.SetBool("grounded", false);
                m_Grounded = false;

                // 🔊 Play jump sound
                if (jumpSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(jumpSound, 0.25f);
                }
            }

            if (animator.GetBool("grounded") && Mathf.Abs(rb2D.linearVelocity.y) > jumpForgiveness) {
            animator.SetBool("grounded", false);
            m_Grounded = false;
        }
    }
    }
    void FixedUpdate()
    {
        rb2D.mass = Mathf.Max(GameManager.Instance.playerWater, transform.localScale.x);
        var dtime = Time.fixedTime - oldtime;
        float oldvx;

        if(dtime != 0) oldvx = (oldx - transform.position.x) / dtime;
        else oldvx = rb2D.linearVelocity.x;

        var discrepancyx = oldvx - rb2D.linearVelocity.x;

        oldx = transform.position.x;
        oldtime = Time.fixedTime;

        if(GameManager.Instance.canMove){
            if(oldHorizontal != Mathf.Ceil(Mathf.Abs(horizontal)) * Mathf.Sign(horizontal) || Mathf.Abs(discrepancyx) < 0.5 || resetFlag) {

                resetFlag = false;

                start = rb2D.linearVelocity.x * scaleFactor() / oldScaleFactor;
                diff = maxSpeed * horizontal - start;
                float speedPercent;
                if(horizontal == 0f) speedPercent = Mathf.Abs(rb2D.linearVelocity.x / maxSpeed);
                else speedPercent = Mathf.Abs((maxSpeed * horizontal - rb2D.linearVelocity.x) / maxSpeed * horizontal);
                if(horizontal == 0f) time = speedPercent * deceltime;
                else time = speedPercent * acceltime;
                if(time == 0f) rate = 0;
                else rate = 1 / time;
                progress = 0;
            }
            if(progress < 1f) {
                progress = progress + rate * Time.deltaTime * (m_Grounded ? 1 : airAccel);
                float x;
                if(progress >= 1f) x = 1;
                else if (progress * progress == 0f) x = 0;
                else x = 1 + (Mathf.Sqrt(1 - ((1 - progress) * (1 - progress))) - 1);
                if(float.IsNaN(x)) Debug.Log("");
                float newVelocity = start + x * diff;
                expectedAcceleration = (newVelocity - rb2D.linearVelocity.x);
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x * (scaleFactor() / oldScaleFactor) + expectedAcceleration * scaleFactor(), rb2D.linearVelocity.y);
                if(time == 0f) rate = 0;
                else rate = 1 / time;
            } else if (rb2D.linearVelocity.x != maxSpeed * horizontal * scaleFactor()) {
                Debug.Log("resetting acceleration");
                resetFlag = true;
            }
        } else {
            rb2D.linearVelocity = new Vector2(0f, rb2D.linearVelocity.y);
        }


        bool wasGrounded = m_Grounded;

        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(groundCapsule.position, new Vector3(transform.localScale.x * groundCapsule.localPosition.x, transform.localScale.z * groundCapsule.localPosition.y, transform.localScale.z * groundCapsule.localPosition.z), CapsuleDirection2D.Horizontal, groundCapsule.rotation.eulerAngles.z);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    animator.SetBool("grounded", true);
                    OnLandEvent.Invoke();
            }
        }
        
        float oldvy;

        oldvy = (transform.position.y - oldy) / Time.deltaTime;

        animator.SetFloat("Horizontal", rb2D.linearVelocity.x);
        animator.SetFloat("Vertical", oldvy);

        oldy = transform.position.y;

        oldHorizontal = Mathf.Ceil(Mathf.Abs(horizontal)) * Mathf.Sign(horizontal);
        oldScaleFactor = scaleFactor();
        
    }


    public void Landed() {
        animator.SetBool("grounded", true);
    }
    public float scaleFactor() {
        return (1 / Mathf.Exp(Mathf.Abs(transform.localScale.x - scaleOfMaximumSpeediness))) * (1 - minMaximumSpeed) + minMaximumSpeed;
    }
}