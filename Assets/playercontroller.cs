using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    // Start is called before the first frame update 

    public float speed, radius;
    //private float boundaryUp = 2.03f;
    //private float boundaryDown = -4.26f;
    //private float boundaryLeft = -7.7f;
    //private float boundaryRight = 7.7f;
    private float boundaryUp = 2.75f;
    private float boundaryDown = -5.66f;
    private float boundaryLeft = -9.14f;
    private float boundaryRight = 9.14f;
    public int health;
    public bool alive;
    void Start()
    {
        //radius = 0.5f;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") && !(transform.position.y+radius>boundaryUp))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y+speed);
        }
        if (Input.GetKey("down") && !(transform.position.y - radius < boundaryDown))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed);
        }
        if (Input.GetKey("left") && !(transform.position.x - radius < boundaryLeft))
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y);
        }
        if (Input.GetKey("right") && !(transform.position.x + radius > boundaryRight))
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y);
        }
        changeAttackPosition();
        if (Input.GetKeyDown("space"))
        {
            playerAttack();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("colidiuAA");
        if (collision.tag == "attack")
        {
            Debug.Log("colidiuAA");
            health -= 1;
            if (health==0)
            {
                alive = false;
                this.gameObject.SetActive(false);
            }
        }
    }
    void changeAttackPosition()
    {
        if (Input.GetKeyDown("up"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }else if (Input.GetKeyDown("down"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        } else if (Input.GetKeyDown("left"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        } else if (Input.GetKeyDown("right"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    void playerAttack()
    {
        transform.Find("player attack").gameObject.SetActive(true);
    }
    
}
