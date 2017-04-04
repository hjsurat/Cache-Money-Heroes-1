using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class PlayerWolf : MonoBehaviour
{

    public float health;
    public float speed;
    public float pullRange;
    public Transform player;
    public Transform enemyTarget;
    public Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > pullRange)
        {
            transform.position = new Vector3(player.position.x, player.position.y);
            enemyTarget = null;
        }

        if (enemyTarget == null)
        {
            if (distance > 30)
            {
                float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
                transform.eulerAngles = new Vector3(0, 0, z);
                rb.AddForce(gameObject.transform.up * speed);
                animator.SetInteger("Direction", 2);
                animator.SetBool("Move", true);
            }

            // Gets a vector that points from the player's position to the target's.
            var heading = transform.position - player.position;
            var myDistance = heading.magnitude;
            var direction = heading / myDistance; // This is now the normalized direction.
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (isAround0(direction.x) && isAroundNegative1(direction.y)) // NORTH
            {
                animator.SetInteger("Direction", 2);
                animator.SetBool("Move", true);
            }

            else if (isAroundNegative1(direction.x) && isAround0(direction.y)) // EAST
            {
                animator.SetInteger("Direction", 3);
                animator.SetBool("Move", true);
            }

            else if (isAroundPositive1(direction.x) && isAround0(direction.y)) // WEST
            {
                animator.SetInteger("Direction", 1);
                animator.SetBool("Move", true);
            }

            else if (isAround0(direction.x) && isAroundPositive1(direction.y)) // SOUTH
            {
                animator.SetInteger("Direction", 0);
                animator.SetBool("Move", true);
            }
            else
            {
                animator.SetBool("Move", false);
            }
        }

        else
        {

            float z = Mathf.Atan2((enemyTarget.transform.position.y - transform.position.y), (enemyTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
            transform.eulerAngles = new Vector3(0, 0, z);
            rb.AddForce(gameObject.transform.up * speed);
            animator.SetInteger("Direction", 2);
            animator.SetBool("Move", true);


            // Gets a vector that points from the player's position to the target's.
            var heading = transform.position - enemyTarget.position;
            var myDistance = heading.magnitude;
            var direction = heading / myDistance; // This is now the normalized direction.

            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (isAround0(direction.x) && isAroundNegative1(direction.y)) // NORTH
            {
                animator.SetInteger("Direction", 2);
                animator.SetBool("Move", true);
            }

            else if (isAroundNegative1(direction.x) && isAround0(direction.y)) // EAST
            {
                animator.SetInteger("Direction", 3);
                animator.SetBool("Move", true);
            }

            else if (isAroundPositive1(direction.x) && isAround0(direction.y)) // WEST
            {
                animator.SetInteger("Direction", 1);
                animator.SetBool("Move", true);
            }

            else if (isAround0(direction.x) && isAroundPositive1(direction.y)) // SOUTH
            {
                animator.SetInteger("Direction", 0);
                animator.SetBool("Move", true);
            }
            else
            {
                animator.SetBool("Move", false);
            }
        }


    }

    private bool isAround0(float x)
    {
        if (x < .2 && x > -.2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isAroundPositive1(float x)
    {
        if (x < 1.2 && x > .8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isAroundNegative1(float x)
    {
        if (x < -.8 && x > -1.2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            health--;
            Destroy(collision.gameObject);
        }
    }
}
