using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //member variables
    Rigidbody2D rB2D;
    private int count;

    //public variables
    public float runSpeed;
    public float jumpSpeed;
    
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        rB2D = GetComponent<Rigidbody2D>();
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
        if (Input.GetButtonDown("Jump"))
        {
            int levelMask = LayerMask.GetMask("Level");

            if (Physics2D.BoxCast(transform.position, new Vector2(1f, .1f), 0f, Vector2.down, .01f, levelMask))
            {
                Jump();
            }
        }
        
        if (rB2D.transform.position.y <= -15f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
            audioSource.PlayOneShot(munchSFX);

            SetCountText();
        }
        //if the trigger is the enemy's head, kill enemy and jump
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(cawSFX);
            Jump();
        }
    }
}
