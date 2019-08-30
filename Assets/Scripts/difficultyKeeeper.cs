using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficultyKeeeper : MonoBehaviour
{
    public int enemyHealth, playerHealth, bombNumber;
    public float enemyAttackSpeed, enemyShieldTimer;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
