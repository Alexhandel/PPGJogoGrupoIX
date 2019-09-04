using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackController : MonoBehaviour
{
    public playercontroller pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponentInParent<playercontroller>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        switch (GetComponentInParent<playercontroller>().facing)
        {
            case "up":
                pc.spriteRenderer.sprite = pc.back;
                break;
            case "down":
                pc.spriteRenderer.sprite = pc.front;
                break;
            case "left":
                pc.spriteRenderer.sprite = pc.left;
                break;
            default:
                break;
        }
    }
}
