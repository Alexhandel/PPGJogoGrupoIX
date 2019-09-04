using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficultyKeeeper : MonoBehaviour
{
    public int enemyHealth, playerHealth, bombNumber;
    public float enemyAttackSpeed, enemyShieldTimer;
    public bool hasParent = false;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    private void Update()
    {
        if (!hasParent)
        {
            Destroy(this.gameObject);
        }
    }
}
