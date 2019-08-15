using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceBulletController : MonoBehaviour
{
    public Vector3 direction;
    private Vector3 temp;
    public float speed;
    private float boundaryUp = 2.75f;
    private float boundaryDown = -5.66f;
    private float boundaryLeft = -9.14f;
    private float boundaryRight = 9.14f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        temp = transform.position+ (direction * speed);
        if ((temp.x >= boundaryRight))
        {
            Debug.Log("bounceRight");
            direction.x *= -1;
        }
        if (temp.x <= boundaryLeft)
        {
            Debug.Log("bounceLeft");
            direction.x *= -1;
        }
        if (temp.y >= boundaryUp)
        {
            Debug.Log("bounceUp");
            direction.y *= -1;
        }
        if (temp.y <= boundaryDown)
        {
            Debug.Log("bounceDown");
            direction.y *= -1;
        }
        transform.position += direction*speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            Destroy(this.gameObject);

        }

    }
}
