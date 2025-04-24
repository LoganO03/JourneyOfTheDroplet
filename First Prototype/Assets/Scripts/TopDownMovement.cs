using UnityEngine;

public class TopDownMovement : MonoBehaviour {

    private Rigidbody2D rigidbody2D;
    float horizontal;
    float vertical;
    private Vector2 input;
    Animator animator;
    private Vector2 lastMoveDirection;
    public AudioSource walkSource;
    

    public float runSpeed = 5f;
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        float moveX = horizontal = Input.GetAxisRaw("Horizontal");
        float moveY = vertical = Input.GetAxisRaw("Vertical");

        if((moveX == 0 && moveY == 0) && horizontal != 0 || vertical != 0){
            lastMoveDirection = input;
        }

        
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();

        // animator.SetFloat("MoveX", input.x);
        // animator.SetFloat("MoveY", input.y);
        // animator.SetFloat("MoveMagnitude", input.magnitude);
        // animator.SetFloat("LastMoveX", lastMoveDirection.x);
        // animator.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    void FixedUpdate() {
        rigidbody2D.linearVelocity = input * runSpeed;
    }

}