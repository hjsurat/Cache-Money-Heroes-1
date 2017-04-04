using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfRadius : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyMelee")
        {
            if (transform.parent && transform.parent.gameObject != other.gameObject)
            {
                var playerWolf = GetComponentInParent<PlayerWolf>();
                playerWolf.enemyTarget = other.gameObject.transform;
            }
        }
    }

}
