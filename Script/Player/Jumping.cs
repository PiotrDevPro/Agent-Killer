using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private GameObject _Playa;
    private GameObject _background;
    private GameObject Pausa;

    public static bool isSlowMo = false;

    void Start()
    {
        _Playa = GameObject.FindGameObjectWithTag("Player");
        _background = GameObject.FindGameObjectWithTag("background");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            BackgoundColorAttackEnter();
            isSlowMo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            BackgoundColorAttackExit();
            isSlowMo = false;
        }
    }

    void Update()
    {
    }

    void BackgoundColorAttackEnter()
    {
        _background.GetComponent<Animator>().SetBool("bgAttack", true);
        Time.timeScale = 0.3f;
    }

    void BackgoundColorAttackExit()
    {
        _background.GetComponent<Animator>().SetBool("bgAttack", false);
        Time.timeScale = 0.9f;
    }
}
