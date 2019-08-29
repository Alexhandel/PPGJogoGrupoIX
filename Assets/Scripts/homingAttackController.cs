using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingAttackController : MonoBehaviour, pausable
{
    public Vector3 direction;
    public float speed;
    private float boundaryUp = 0.409f;
    private float boundaryDown = -0.808f;
    private float boundaryLeft = -1.233f;
    private float boundaryRight = 1.234f;
    public GameObject player;
    public Vector3 heading;

    public bool isPaused { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused) 
        {
            heading = transform.position - player.transform.position;
            //direction = -((heading) / heading.magnitude);
            direction += -((heading) / heading.magnitude) * 0.05f;
            direction = direction / direction.magnitude;
            transform.position += direction * speed;
            if ((transform.position.x >= boundaryRight) || (transform.position.x <= boundaryLeft) || (transform.position.y >= boundaryUp) || (transform.position.y <= boundaryDown))
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            Destroy(this.gameObject);

        }

    }
}
