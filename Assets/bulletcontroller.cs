using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroller : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    //private float boundaryUp = 2.03f;
    //private float boundaryDown = -4.26f;
    //private float boundaryLeft = -7.7f;
    //private float boundaryRight = 7.7f;
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
        transform.position += direction * speed;
        if ((transform.position.x >= boundaryRight) || (transform.position.x <= boundaryLeft) || (transform.position.y >= boundaryUp) || (transform.position.y <= boundaryDown))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="player")
        {
            Debug.Log("colidiuBB");
            Destroy(this.gameObject);

        }

    }
}
