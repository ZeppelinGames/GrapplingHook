using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Setup")]
    public float moveSpeed = 5;

    [Header("Jump Setup")]
    public float jumpForce = 5;
    public Transform isGroundedChecker;
    public float checkGroundRadius=0.1f;
    public LayerMask groundLayer;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float coyoteTime=0.1f;
    float lastTimeGrounded;
    bool isGrounded;

    private Rigidbody2D rig;
    private SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rig.velocity = new Vector2(h * moveSpeed, rig.velocity.y);

        CheckIfGrounded();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || Time.time - lastTimeGrounded <= coyoteTime)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        rig.velocity = new Vector2(rig.velocity.x, jumpForce);
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }
}
