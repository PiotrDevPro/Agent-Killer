using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private GameObject _Playa;
    private GameObject _background;
    private GameObject Pausa;

    public GameObject rayActiv;

    void Start()
    {
        _Playa = GameObject.FindGameObjectWithTag("Player");
        _background = GameObject.FindGameObjectWithTag("background");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
     {
            Invoke("BackgoundColorAttack",3f);
            //rayActiv.SetActive(false);
            Time.timeScale = 0.9f;
            //_background.GetComponent<Animator>().SetBool("bgAttack", false);
            
        }

}


     void Update()
    {
    }

    void BackgoundColorAttack()
    {
        //rayActiv.SetActive(true);
        _background.GetComponent<Animator>().SetBool("bgAttack", true);
        Time.timeScale = 0.3f;
        

    }
}
