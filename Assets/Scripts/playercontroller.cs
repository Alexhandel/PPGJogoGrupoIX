using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playercontroller : MonoBehaviour, pausable
{
    // Start is called before the first frame update 

    public float speed, radius, currentShield, maxShieldMeter, invulTimer, invulTime;
    private float boundaryUp = 0.372f;
    private float boundaryDown = -0.778f;
    private float boundaryLeft = -1.196f;
    private float boundaryRight = 1.198f;
    public int health, maxHealth;
    public bool alive, isShieldUp, isInvul;
    public Slider shieldGaugeSlider, healthSlider;
    public Sprite front, back, left, right;
    public GameObject difficultyKeeper;

    public bool isPaused { get; set; }

    [FMODUnity.EventRef]
    public string playerHurtSound = "event:/SFX/Player/EnzoHit";
    [FMODUnity.EventRef]
    public string playerShieldHitSound = "event:/SFX/Player/EnzoShieldHit";
    [FMODUnity.EventRef]
    public string playerAtkSound = "event:/SFX/Player/EnzoAttack";
    [FMODUnity.EventRef]
    public string playerShieldSound = "event:/SFX/Player/EnzoShield";

    void Start()
    {
        difficultyKeeper = GameObject.FindGameObjectWithTag("numberKeeper");
        maxHealth= difficultyKeeper.GetComponent<difficultyKeeeper>().playerHealth;
        alive = true;
        isShieldUp = false;
        currentShield = maxShieldMeter;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        shieldGaugeSlider.maxValue = maxShieldMeter;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (isInvul)
            {
                invulTimer += Time.deltaTime;
                if (invulTimer>=invulTime)
                {
                    invulTimer = 0;
                    isInvul = false;
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color(this.gameObject.GetComponent<SpriteRenderer>().color.r, this.gameObject.GetComponent<SpriteRenderer>().color.g, this.gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
                }
            }
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
            healthSlider.value = health;
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
        if (collision.tag == "attack" && !isShieldUp && !isInvul)
        {
            Debug.Log("colidiuAA");
            FMODUnity.RuntimeManager.PlayOneShot(playerHurtSound, transform.position);
            health -= 1;
            isInvul = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(this.gameObject.GetComponent<SpriteRenderer>().color.r, this.gameObject.GetComponent<SpriteRenderer>().color.g, this.gameObject.GetComponent<SpriteRenderer>().color.b,0.3f);
            if (health==0)
            {
                alive = false;
                this.gameObject.SetActive(false);
            }
        }
        if (collision.tag == "attack" && isShieldUp)
        {
            FMODUnity.RuntimeManager.PlayOneShot(playerShieldHitSound, transform.position);
        }
    }
    void changeAttackPosition()
    {
        if (Input.GetKeyDown("up"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            gameObject.GetComponent<SpriteRenderer>().sprite = back;
        }
        else if (Input.GetKeyDown("down"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            gameObject.GetComponent<SpriteRenderer>().sprite = front;
        } else if (Input.GetKeyDown("left"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            gameObject.GetComponent<SpriteRenderer>().sprite = left;
        } else if (Input.GetKeyDown("right"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            gameObject.GetComponent<SpriteRenderer>().sprite = right;
        }
    }
    void playerAttack()
    {
        transform.Find("player attack").gameObject.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot(playerAtkSound, transform.position);
    }
    void activateShield()
    {
        isShieldUp = true;
        transform.Find("Player Shield").gameObject.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot(playerShieldSound, transform.position);
    }
    void deactivateShield()
    {
        isShieldUp = false;
        transform.Find("Player Shield").gameObject.SetActive(false);
    }

}
