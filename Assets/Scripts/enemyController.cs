using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour, pausable

{
    public GameObject player, bounceAttackPrefab, straightAttackPrefab, explodingAttackPrefab, homingAttackPrefab;
    public GameObject healthUI;
    public Sprite phase2Face;
    private GameObject temp;
    private Vector3 heading;
    public float timer1, timer2, timer3, timer4, timer5, shieldTimer, phase2AttackSpeedMultiplier;
    public float attack1Time, attack2Time, attack3Time, attack4Time, attack5Time, shieldUpTime, shieldDownTime, attackSpeed;
    public int health;
    public bool alive, isShieldOn;
    private int stage, maxHealth;
    public Vector3 direction1;
    public GameObject difficultyKeeper;

    public bool isPaused { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        difficultyKeeper = GameObject.FindGameObjectWithTag("numberKeeper");
        health = difficultyKeeper.GetComponent<difficultyKeeeper>().enemyHealth;
        shieldDownTime = difficultyKeeper.GetComponent<difficultyKeeeper>().enemyShieldTimer;
        attackSpeed = difficultyKeeper.GetComponent<difficultyKeeeper>().enemyAttackSpeed;
        timer1 = 0;
        alive = true;
        stage = 1;
        maxHealth = health;
        isShieldOn = true;
        attack1Time = attack1Time * attackSpeed;
        attack2Time = attack2Time * attackSpeed;
        attack3Time = attack3Time * attackSpeed;
        attack4Time = attack4Time * attackSpeed;
        attack5Time = attack5Time * attackSpeed;
        healthUI.GetComponentInChildren<Slider>().maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            healthUI.GetComponentInChildren<Slider>().value = health;
            if (stage == 1 && health <= (maxHealth / 2))
            {
                healthUI.GetComponent<Image>().sprite = phase2Face;
                timer1 = 0;
                timer2 = 0;
                timer3 = 0;
                timer4 = 0;
                timer5 = 0;
                stage = 2;
                attack1Time = attack1Time * phase2AttackSpeedMultiplier;
                attack2Time = attack2Time * phase2AttackSpeedMultiplier;
                attack3Time = attack3Time * phase2AttackSpeedMultiplier;
                attack4Time = attack4Time * phase2AttackSpeedMultiplier;
                attack5Time = attack5Time * phase2AttackSpeedMultiplier;
            }
            if (player.GetComponent<playercontroller>().alive)
            {
                timer1 += Time.deltaTime;
                timer2 += Time.deltaTime;
                timer3 += Time.deltaTime;
                timer4 += Time.deltaTime;
                timer5 += Time.deltaTime;
                shieldTimer += Time.deltaTime;
                if (timer1 >= attack1Time)
                {
                    straightAttack();
                    timer1 = 0;
                }
                if (timer2 >= attack2Time)
                {
                    bounceAttack();
                    timer2 = 0;
                }
                if (timer3 >= attack3Time)
                {
                    explodingAttack();
                    timer3 = 0;
                }
                if (timer4 >= attack4Time)
                {
                    circleAttack1();
                    timer4 = 0;
                }
                if (timer5 >= attack5Time)
                {
                    homingAttack();
                    timer5 = 0;
                }
                if (shieldTimer >= shieldUpTime && isShieldOn)
                {
                    shieldTimer = 0;
                    isShieldOn = false;
                    transform.Find("Enemy Shield").gameObject.SetActive(false);
                }
                else if (shieldTimer >= shieldDownTime && !isShieldOn)
                {
                    shieldTimer = 0;
                    isShieldOn = true;
                    transform.Find("Enemy Shield").gameObject.SetActive(true);
                }

            }
            if (health == 0)
            {
                alive = false;
                this.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enemycollision");
        if (collision.tag=="playerAttack" && !isShieldOn)
        {
            health -= 1;
            isShieldOn = true;
            shieldTimer = 0;
            transform.Find("Enemy Shield").gameObject.SetActive(true);
        }
    }
    void straightAttack()
    {
        Debug.Log("atacou");
        temp=Instantiate(straightAttackPrefab,transform.position,transform.rotation);
        heading = transform.position - player.transform.position;
        temp.GetComponent<bulletcontroller>().direction = -((heading) / heading.magnitude);
    }
    void bounceAttack()
    {
        Debug.Log("bouncy");
        temp = Instantiate(bounceAttackPrefab, transform.position, transform.rotation);
        // heading = transform.position - player.transform.position;
        //temp.GetComponent<bounceBulletController>().direction = -((heading) / heading.magnitude);
        temp.GetComponent<bounceBulletController>().direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)); 
        
    }
    void explodingAttack()
    {
        Debug.Log("explody");
        temp = Instantiate(explodingAttackPrefab, transform.position, transform.rotation);
        heading = transform.position - player.transform.position;
        temp.GetComponent<explodingBulletController>().direction = -((heading) / heading.magnitude);
    }
    void circleAttack1()
    {
        foreach (var item1 in Enumerable.Range(-10, 41).Select(x => x * 0.05))
        {
            foreach (var item2 in Enumerable.Range(-10, 41).Select(x => x * 0.05))
            {
                if (!(item1 == 0 && item2 == 0) && (Math.Abs(item1) + Math.Abs(item2) == 0.5))
                {
                    direction1 = new Vector3((float)item1, (float)item2);
                    temp = Instantiate(straightAttackPrefab, transform.position+direction1*0.16f, transform.rotation);
                    temp.GetComponent<bulletcontroller>().direction = direction1;

                }
            }
        }
    }
    void homingAttack()
    {
        Instantiate(homingAttackPrefab, transform.position, transform.rotation);
    }

   
}
