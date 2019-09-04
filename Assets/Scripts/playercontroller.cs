﻿using System.Collections;
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
    public Sprite front, back, left, right, noWeaponFront, noWeaponBack, noWeaponSide;
    public GameObject difficultyKeeper;
    public SpriteRenderer spriteRenderer;
    public string facing;

    public bool isPaused { get; set; }

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
            if (Input.GetKeyDown("j") || Input.GetKeyDown("space"))
            {
                playerAttack();
            }
            if (Input.GetKeyDown("k") || Input.GetKeyDown("z"))
            {
                activateShield();
            }
            if (Input.GetKeyUp("k") || Input.GetKeyUp("z"))
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
            if ((Input.GetKey("w") || Input.GetKey("up")) && !(transform.position.y + radius > boundaryUp))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed);
            }
            if ((Input.GetKey("s") || Input.GetKey("down")) && !(transform.position.y - radius < boundaryDown))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed);
            }
            if ((Input.GetKey("a") || Input.GetKey("left")) && !(transform.position.x - radius < boundaryLeft))
            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y);
            }
            if ((Input.GetKey("d") || Input.GetKey("right")) && !(transform.position.x + radius > boundaryRight))
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
            health -= 1;
            isInvul = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,0.3f);
            if (health==0)
            {
                alive = false;
                this.gameObject.SetActive(false);
            }
        }
    }
    void changeAttackPosition()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            spriteRenderer.sprite = back;
            facing = "up";
        }
        else if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            spriteRenderer.sprite = front;
            facing = "down";
        } else if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            spriteRenderer.sprite = left;
            facing = "left";
        } else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            spriteRenderer.sprite = right;
            facing = "right";
        }
    }
    void playerAttack()
    {
        transform.Find("player attack").gameObject.SetActive(true);
        switch (facing)
        {
            case "up":
                spriteRenderer.sprite = noWeaponBack;
                break;
            case "down":
                spriteRenderer.sprite = noWeaponFront;
                break;
            case "left":
                spriteRenderer.sprite = noWeaponSide;
                break;
            default:
                break;
        }
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
