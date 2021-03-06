﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //member variables
    [SerializeField] private LayerMask platformsLayermask;
    Rigidbody2D rB2D;
    BoxCollider2D boxCollider2d;
    private int count;

    //public variables
    public float runSpeed;
    public float jumpSpeed;
    public float addGravity;
    
    public TextMeshProUGUI countText;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update, gets components and sets speed
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        rB2D.velocity = new Vector2(runSpeed, rB2D.velocity.y);
        SetCountText();
    }

    //Updates count text with current data, displays win text
    void SetCountText()
    {
        countText.text = "Coins: " + count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //if player has jumped and isn't holding space, add gravity
        if(!IsGrounded() && rB2D.velocity.y > 0)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                rB2D.velocity += Vector2.down * addGravity * Time.deltaTime;
            }
        }

        //if player falls off map, reset
        if (rB2D.transform.position.y <= -50f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (rB2D.velocity.x < runSpeed)
        {
            rB2D.velocity = new Vector2(runSpeed, rB2D.velocity.y);
        }
    }

    //if player is on the ground
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayermask);
        return raycastHit2d.collider != null;
    }

    //adds y velocity
    void Jump()
    {
        rB2D.velocity = new Vector2(rB2D.velocity.x, jumpSpeed);
    }

    //called when player collides with something
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if the collider is the enemy, reset
        if (other.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    //called when player enters a trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the trigger is a pick up, update count and play sound
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
        
    }
}
