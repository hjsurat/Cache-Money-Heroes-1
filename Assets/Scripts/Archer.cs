using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Archer : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer SpriteRend;
    public Rigidbody2D rb2d;
    public GameObject ArrowPrefab;
    public Transform ArrowSpawn;


    // Player Stats
    public float speed = 1F;
    public int health = 3;
    public int focus = 3;

    // Firing Speed
    public int shotSpeed = 12000;
    public float fireDelay = 0.25F;
    private float nextFire = 0.25F;
    private float myTime = 0.0F;
    private float invisibleTimeStamp;
    private float immuneTimeStamp;
    public static bool isInvisible = false;
    private static bool isImmune = false;


    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        SpriteRend = this.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyMelee")
        {
            if (!isImmune)
            {
                health--;
                rb2d.velocity = new Vector2((transform.position.x - collision.gameObject.transform.position.x) * 25, rb2d.velocity.y);
                SpriteRend.color = new Color(255F, 0F, 0F, .75F);
                isImmune = true;
                immuneTimeStamp = Time.time + .35F;
            }
        }

        else if (collision.gameObject.tag == "PlayerWeapon")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (isInvisible == true)
        {
            if (invisibleTimeStamp < Time.time)
            {
                SpriteRend.color = new Color(1F, 1F, 1F, 1F);
                isInvisible = false;
            }
        }

        if (isImmune == true)
        {
            if (immuneTimeStamp < Time.time)
            {
                SpriteRend.color = new Color(1F, 1F, 1F, 1F);
                isImmune = false;
            }
        }

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        myTime = myTime + Time.deltaTime;

        // Controls Movement
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            animator.SetInteger("Direction", 2);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            animator.SetInteger("Direction", 0);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey("right") || Input.GetKey("d"))
        {
            animator.SetInteger("Direction", 3);
            animator.SetBool("Move", true);
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            animator.SetInteger("Direction", 1);
            animator.SetBool("Move", true);
        }
        else if (!Input.anyKey)
        {
            animator.SetBool("Move", false);
        }

        // Controls Attack and FireRate
        if (Input.GetKeyDown(KeyCode.Space) && myTime > nextFire)
        {
            // Need to fix the fire rate
            animator.SetTrigger("Attack");
            nextFire = myTime + fireDelay;
            Invoke("Fire", 0.7692308F / 2);
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && myTime > nextFire)
        {
            // Need to fix the fire rate
            animator.SetTrigger("Attack");
            nextFire = myTime + fireDelay;
            Invoke("TripleShot", 0.7692308F / 2);
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }

        // Cloak Ability
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetTrigger("UseAbility");
            invisibleTimeStamp = Time.time + 5;
            Cloak();
        }

        // Move the player
        transform.Translate(horizontal * speed, vertical * speed, 0);
    }

    void TripleShot()
    {
        if (animator.GetInteger("Direction") == 0)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 90, 90));
            arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
            var arrow2 = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, 90));
            arrow2.GetComponent<Rigidbody2D>().AddForce(arrow2.transform.right * -1 * shotSpeed);
            Destroy(arrow2, 3.0f);
            var arrow3 = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(90, 0, 90));
            arrow3.GetComponent<Rigidbody2D>().AddForce(arrow3.transform.right * -1 * shotSpeed);
            Destroy(arrow3, 3.0f);

        }
        else if (animator.GetInteger("Direction") == 1)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, 360));
            arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
        else if (animator.GetInteger("Direction") == 2)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, 270));
            arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
        else if (animator.GetInteger("Direction") == 3)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 180, 0));
            arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
    }

    void Fire()
    {
        if (animator.GetInteger("Direction") == 0)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, 90));
            arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
        else if (animator.GetInteger("Direction") == 1)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, 360));
            arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
        else if (animator.GetInteger("Direction") == 2)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 0, 270));
            arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
        else if (animator.GetInteger("Direction") == 3)
        {
            var arrow = (GameObject)Instantiate(ArrowPrefab, ArrowSpawn.position, Quaternion.Euler(0, 180, 0));
            arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * -1 * shotSpeed);
            Destroy(arrow, 3.0f);
        }
    }

    void Cloak()
    {
        SpriteRend.color = new Color(1F, 1F, 1F, 0.25F);
        isInvisible = true;
        health++;
        focus--;
    }
}