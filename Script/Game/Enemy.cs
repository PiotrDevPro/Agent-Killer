using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public _EnemyUI _enemy;

    [SerializeField]
    Transform _player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float stopRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody rb;
    Collider coll;
    Animator _Anim;

    private int health = 50;
    private float _sliderHP;

    public AudioSource hurt;
    public GameObject blood;

    private GameObject sliderAnim;
    private GameObject enemyCounter;
    private float frag = 0;

    [System.Serializable]
    public class _EnemyUI
    {
        

        public Image healthSlider;

        public GameObject healthSliderActive;

        public Text Counter;
        public bool Dead = false;


    }


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        _Anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        blood.SetActive(false);
        _enemy.healthSliderActive.SetActive(false);
        sliderAnim = GameObject.FindGameObjectWithTag("SliderAnim");
        enemyCounter = GameObject.FindGameObjectWithTag("enemyCounter");

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("PlasmaB"))
        {
            health -= 1;
             _Anim.SetBool("Crouch", true);
            blood.SetActive(true);
            

        }
        if (health <= 0)
        {
            KillSelf();
            frag += 1;
            enemyCounter.GetComponent<Text>().text = frag.ToString();
            _enemy.Dead = true;
        } 

        else
        {
            Invoke("latencyBlood", 2.0f);
            Invoke("latencyCrouch", 0.01f);
        }

      //  if (health < 20)
      //  {
       //     sliderAnim.GetComponentInChildren<Animator>().SetBool("lowHp", true);
     //   }
    }

    #region Invoke
    void latencyBlood()
    {
        blood.SetActive(false);
    }

    void latencyCrouch()
    {
        _Anim.SetBool("Crouch", false);
    }



    #endregion

    #region Enemy mechanics

    void Move()
    {
        if (CharacterPlayer.manage.tapToPlay)
        {
            float distToPLayer = Vector2.Distance(transform.position, _player.position);
            if (distToPLayer < agroRange)
            {
                FollowToPlayer();
                _enemy.healthSliderActive.SetActive(true);
            }
            else
            {
                //StopFollowToPlayer();
            }
        }
    }

    void FollowToPlayer()
    {
        if (transform.position.x < _player.position.x)
        {
            //left side of the player, so move right

            rb.velocity = new Vector2(moveSpeed, 0);
            _Anim.SetBool("Walk", true);
            //_enemy.healthSliderActive.SetActive(true);

        }
        else
        {
            //right side of the player,so move left
            rb.velocity = new Vector2(-moveSpeed, 0);
            _Anim.SetBool("Walk", true);
        }
    }

    #endregion



    void KillSelf()
    {
        _Anim.SetBool("Walk", false);
        _Anim.SetBool("DieBack", true);
        
        rb.useGravity = false;
        Destroy(gameObject, 2f);
    }

    void HPslider()
    {
        _sliderHP = health / 50f;
        _enemy.healthSlider.fillAmount = _sliderHP;
    }


    void FixedUpdate()
    {
        Move();
        HPslider();
        print(frag);

    }
}
