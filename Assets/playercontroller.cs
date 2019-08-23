using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour, pausable
{
    // Start is called before the first frame update 

    public float speed, radius, currentShield, maxShieldMeter;
    private float boundaryUp = 0.372f;
    private float boundaryDown = -0.778f;
    private float boundaryLeft = -1.196f;
    private float boundaryRight = 1.198f;
    public int health;
    public bool alive, isShieldUp;
    public Slider shieldGaugeSlider;

    public bool isPaused { get; set; }

    void Start()
    {
        //radius = 0.5f;
        alive = true;
        isShieldUp = false;
        currentShield = maxShieldMeter;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            changeAttackPosition();
            if (Input.GetKeyDown("space"))
            {
                playerAttack();
            }
            if (Input.GetKeyDown("z"))
            {
                activateShield();
            }
            if (Input.GetKeyUp("z"))
            {
                deactivateShield();
            }
            if (isShieldUp)
            {
                currentShield -= Time.deltaTime;
                if (currentShield <= 0)
                {
                    deactivateShield();
                }
            }
            else if (!isShieldUp && currentShield <= maxShieldMeter)
            {
                currentShield += Time.deltaTime / 2;
                if (currentShield > 5)
                {
                    currentShield = 5f;
                }
            }
            shieldGaugeSlider.value = currentShield;
            if (Input.GetKey("up") && !(transform.position.y + radius > boundaryUp))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed);
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
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("colidiuAA");
        if (collision.tag == "attack" && !isShieldUp)
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
    void activateShield()
    {
        isShieldUp = true;
        transform.Find("Player Shield").gameObject.SetActive(true);
    }
    void deactivateShield()
    {
        isShieldUp = false;
        transform.Find("Player Shield").gameObject.SetActive(false);
    }

}
