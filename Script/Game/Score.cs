using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    private GameObject enemyCounter;
    private float currents;

     void Awake()
    {
        enemyCounter = GameObject.FindGameObjectWithTag("enemyCounter");
    }

     void Update()
    {
        //EnemyCounter();
        enemyCounter.GetComponent<Text>().text = (EnemyLeft.manage.fragL + EnemyRight.manage.fragR).ToString();
        currents = EnemyLeft.manage.fragL + EnemyRight.manage.fragR;
        PlayerPrefs.SetFloat("Currents", currents);
        
    }

}
