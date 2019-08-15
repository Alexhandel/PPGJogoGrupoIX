using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour

{
    public GameObject player, bounceAttackPrefab, straightAttackPrefab;
    private GameObject temp;
    private Vector3 heading;
    public float timer;
    public float attackTime;
    public int health;
    public bool alive;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<playercontroller>().alive)
        {
            timer += Time.deltaTime;
            if (timer >= attackTime)
            {
                straightAttack();
                //bounceAttack();
                timer = 0;
            }
        }
        if (health == 0)
        {
            alive = false;
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enemycollision");
        if (collision.tag=="playerAttack")
        {
            Debug.Log("enemycollisionAAAA");
            health -= 1;
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
        temp.GetComponent<bounceBulletController>().direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)); 
        
    }
}
