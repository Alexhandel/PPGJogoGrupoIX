using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodingBulletController : MonoBehaviour, pausable
{
    public Vector3 direction;
    public float speed;
    private float boundaryUp = 0.372f;
    private float boundaryDown = -0.778f;
    private float boundaryLeft = -1.196f;
    private float boundaryRight = 1.198f;
    public float timer, timeLimit;
    public GameObject bulletPrefab;
    private GameObject temp;

    public bool isPaused { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        timeLimit = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            timer += Time.deltaTime;
            transform.position += direction * speed;
            if ((transform.position.x >= boundaryRight) || (transform.position.x <= boundaryLeft) || (transform.position.y >= boundaryUp) || (transform.position.y <= boundaryDown) || (timer >= timeLimit))
            {
                burst();
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
    void burst()
    {
        temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
        temp.GetComponent<bulletcontroller>().direction = new Vector3(1, 1);
        temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
        temp.GetComponent<bulletcontroller>().direction = new Vector3(1, 0);
        temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
        temp.GetComponent<bulletcontroller>().direction = new Vector3(1, -1);
        temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
        temp.GetComponent<bulletcontroller>().direction = new Vector3(-1, 1);
        temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
        temp.GetComponent<bulletcontroller>().direction = new Vector3(-1, 0);
        temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
        temp.GetComponent<bulletcontroller>().direction = new Vector3(-1, -1);
        temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
        temp.GetComponent<bulletcontroller>().direction = new Vector3(0, 1);
        temp = Instantiate(bulletPrefab, transform.position, transform.rotation);
        temp.GetComponent<bulletcontroller>().direction = new Vector3(0, -1);
    }
}
