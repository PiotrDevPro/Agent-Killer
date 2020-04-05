using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyRight : MonoBehaviour
{
    public static EnemyRight manage;
    public _EnemyUI _enemy;

    [SerializeField]
    public int enemyHP;

    [SerializeField]
    float moveSpeed;


    Rigidbody rb;
    Collider coll;
    Animator _Anim;

    
    private int healths = 20;
    private float _sliderHP;
    public float fragR;
    public int DamageFromPlayer;
    public int SwordDamage;
    int randomHealthIndex;

    //Res
    public GameObject blood;

    public GameObject[] enemyPrefab;
    public GameObject[] effectPref; 
    private GameObject spawnPoint;
    private GameObject _enemyPos;
    private GameObject _explosionPos;

    private GameObject _playerTarget;
    private GameObject _enemyParent;
    private GameObject sliderAnim;
    //weapons
    private GameObject AxeActive;

    private GameObject BloodActive_sound;
    private GameObject BlowBloodActive_sound;
    private GameObject levelUp_sound;


    bool isDetected = false;
    bool _isDead = false;
    bool isSound = false;
    [System.Serializable]
    public class _EnemyUI
    {

        public Image healthSlider;
        public GameObject healthSliderActive;
        public float differentTime = 0.1f;
    }


    void Awake()
    {
        manage = this;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        _Anim = GetComponentInChildren<Animator>();
        
        sliderAnim = GameObject.FindGameObjectWithTag("SliderAnim");
        _playerTarget = GameObject.FindGameObjectWithTag("Player");
        _enemyParent = GameObject.FindGameObjectWithTag("EnemyParentt");
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnR");

    }
    void Start()
    {
        blood.SetActive(false);
        _enemy.healthSliderActive.SetActive(false);
        
        

    }
    void OnCollisionEnter(Collision collision)
    {

            if (collision.collider.CompareTag("PlasmaB"))
            {
                _enemy.healthSliderActive.SetActive(true);
                healths -= 2 + DamageFromPlayer;
                blood.SetActive(true);
                BloodActive_sound.GetComponent<AudioSource>().Play();

            }

            if (collision.collider.CompareTag("PlasmaG"))
            {
                _enemy.healthSliderActive.SetActive(true);
                healths -= 50 + DamageFromPlayer;
                BloodActive_sound.GetComponent<AudioSource>().Play();

            }

            if (collision.collider.CompareTag("PlasmaP"))
            {
                _enemy.healthSliderActive.SetActive(true);
                healths -= 10 + DamageFromPlayer;
                blood.SetActive(true);
                BloodActive_sound.GetComponent<AudioSource>().Play();

            }

            if (collision.collider.CompareTag("PlasmaR"))
            {
                _enemy.healthSliderActive.SetActive(true);
                healths -= 7 + DamageFromPlayer;
                blood.SetActive(true);
                BloodActive_sound.GetComponent<AudioSource>().Play();

            }

            if (collision.collider.CompareTag("PlasmaY"))
            {
                _enemy.healthSliderActive.SetActive(true);
                healths -= 3 + DamageFromPlayer;
                blood.SetActive(true);
                BloodActive_sound.GetComponent<AudioSource>().Play();

            }
            if (collision.collider.CompareTag("Rocket"))
            {
                _enemy.healthSliderActive.SetActive(true);
                healths -= 70 + DamageFromPlayer;
                blood.SetActive(true);
                BloodActive_sound.GetComponent<AudioSource>().Play();

            }

        if (collision.collider.CompareTag("DragonTooth"))
        {
            _enemy.healthSliderActive.SetActive(true);
            healths -= 10 + DamageFromPlayer;
            blood.SetActive(true);
            BloodActive_sound.GetComponent<AudioSource>().Play();

        }

        if (healths <= 0 )
            {

                KillSelf();
                fragR += 1;
                PlayerPrefs.SetFloat("fragR", fragR);
                AxeActive.GetComponent<BoxCollider>().enabled = false;
                _isDead = true;
            
        }
        
        else
            {
                Invoke("latencyBlood", 2.0f);
                Invoke("latencyCrouch", 0.01f);
            }

        if (fragR >= 1 && (!collision.collider.CompareTag("PlasmaB") && !collision.collider.CompareTag("PlasmaG")
        && !collision.collider.CompareTag("PlasmaP") && !collision.collider.CompareTag("PlasmaR") && !collision.collider.CompareTag("PlasmaY") &&
        !collision.collider.CompareTag("Rocket")))
        {
            BlowBloodActive_sound.GetComponent<AudioSource>().Play();

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "DragonTooth")
        {
            _enemy.healthSliderActive.SetActive(true);
            healths -= 10 + DamageFromPlayer + SwordDamage;
            blood.SetActive(true);
            BloodActive_sound.GetComponent<AudioSource>().Play();

        }

        if (healths <= 0)
        {

            KillSelf();
            fragR += 1;

            PlayerPrefs.SetFloat("fragR", fragR);
            AxeActive.GetComponent<BoxCollider>().enabled = false;
            _isDead = true;

        }

        else
        {
            Invoke("latencyBlood", 2.0f);
            Invoke("latencyCrouch", 0.01f);
        }
    }

    #region Invoke
    void latencyBlood()
    {
        blood.SetActive(false);
    }

     void latencyCrouch()
    {
        _Anim.SetBool("Crouch", false);
        //print("Crounch");
    }


    #endregion

    #region Enemy mechanics

     void Move()
    {
        if (CharacterPlayer.manage.tapToPlay && !CharacterPlayer.manage.isPaused && !_isDead)
        {
            FollowToPlayer();
            
        }
        else
        {
            StopFollowToPlayer();
            
        }
        
    }

     void FollowToPlayer()
    {
        if (transform.position.x > _playerTarget.GetComponent<Transform>().position.x && !isDetected && !_isDead)
        {
            //left side of the player, so move right
            rb.velocity = new Vector2(-moveSpeed, 0);
            _Anim.SetBool("Walk", true);
            _Anim.SetBool("Attack", false);
            if (healths <= 0)
            {

                _Anim.SetBool("Walk", false);
                _Anim.SetBool("Dead", true);


            }
        }
        else
        {
            _Anim.SetBool("Walk", false);
            _Anim.SetBool("Attack", true);
            AxeActive.GetComponent<BoxCollider>().enabled = true;
            if (CharacterPlayer.manage.isDead)
            {
                _Anim.SetBool("Idle", true);

            }

        }

    }

     void targetDetected()
    {
        Debug.DrawRay(transform.position + transform.up/6f, transform.right * 0.2f, Color.yellow);
        RaycastHit info;
        int mask = 1 << 9;
        if (Physics.Raycast(transform.position + transform.up/6f, transform.right, out info, 0.2f, mask))
        {
            //print(info.collider.gameObject);
            isDetected = true;

        }
        else if (CharacterPlayer.manage.tapToPlay)
        {
            FollowToPlayer();
            isDetected = false;
        }
    }

     void StopFollowToPlayer()
    {
        rb.velocity = new Vector2(0,0);
        _Anim.SetBool("Walk", false);
    }

    #endregion

    private void KillSelf()
    {
        
        _Anim.SetBool("Walk", false);
        _Anim.SetBool("Attack", false);
        _Anim.SetBool("Dead", true);
        AxeActive.GetComponentInChildren<BoxCollider>().enabled = false;
        _explosive();
        LevelUp();
        Invoke("Spawn", _enemy.differentTime);
        // Invoke("ColliderActive", 1.3f);
        Destroy(gameObject, _enemy.differentTime);

    }

    void HPslider()
    {
        _sliderHP = healths / 20f;
        _enemy.healthSlider.fillAmount = _sliderHP;

    }

    void _explosive()
    {
        _explosionPos = Instantiate(effectPref[0]);
        _explosionPos.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z + .3f);
        Destroy(_explosionPos,2f);

    }

    private void Spawn()
    {

        //randomIndex = Random.Range(0, enemyPrefab.Length);
         _enemyPos = Lean.Pool.LeanPool.Spawn(enemyPrefab[0], spawnPoint.GetComponent<Transform>().position, spawnPoint.GetComponent<Transform>().rotation) as GameObject;
         _enemyPos.transform.SetParent(_enemyParent.GetComponent<Transform>());


    }

    int RandomPrefabIndex()
    {
        if (enemyPrefab.Length <= 1)
            return 0;
        int randomIndex;
            randomIndex = Random.Range(0, enemyPrefab.Length);
        return randomIndex;
    }

    void ColliderActive()
    {
        coll.gameObject.SetActive(false);
    }

    void LevelUp()
    {
        if (fragR + EnemyLeft.manage.fragL == 3)
        {
            
            CharacterPlayer.manage.Skill = 0.3f;
        }

        if (fragR + EnemyRight.manage.fragR == 5)
        {
            CharacterPlayer.manage.Skill = 0.5f;

        }
        if (fragR + EnemyLeft.manage.fragL == 10)
         {
            levelUp_sound.GetComponent<AudioSource>().Play();
            CharacterPlayer.manage.Skill = 1.0f;
            CharacterPlayer.manage.lv = 1;
            moveSpeed = RandomMoveSpeed();
            _enemy.differentTime -= 0.01f;
            
            if (CharacterPlayer.manage.lv == 1)
            {
                CharacterPlayer.manage.Skill = 0f;
                CharacterPlayer.manage.Health += 50f;
            }
        }
        
        if (fragR + EnemyLeft.manage.fragL == 13)
        {
            CharacterPlayer.manage.Skill = 0.1f;
            moveSpeed = RandomMoveSpeed();
            Time.timeScale = 0.3f;
            CharacterPlayer.manage._background.GetComponentInChildren<Animator>().SetBool("Death", true);
        }

        if (fragR + EnemyLeft.manage.fragL == 15)
        {
            CharacterPlayer.manage.Skill = 0.2f;
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 15f);
        }
        if (fragR + EnemyLeft.manage.fragL == 18)
        {
            CharacterPlayer.manage.Skill = 0.23f;
            Time.timeScale = 1f;
            CharacterPlayer.manage._background.GetComponentInChildren<Animator>().SetBool("Death", false);
        }

        if (fragR + EnemyLeft.manage.fragL == 22)
        {
            CharacterPlayer.manage.Skill = 0.25f;
            healths = 1000;
        }


        if (fragR + EnemyLeft.manage.fragL == 33)
        {
            CharacterPlayer.manage.Skill = 0.35f;
            //_enemy.differentTime -= 0.03f;
            moveSpeed = RandomMoveSpeed();

        }

        if (fragR + EnemyLeft.manage.fragL == 40)
        {
            CharacterPlayer.manage.Skill = 0.45f;
        }

        if (fragR + EnemyLeft.manage.fragL == 50)
        {
            CharacterPlayer.manage.Skill = 0.85f;
            //_enemy.differentTime -= 0.04f;
            moveSpeed = RandomMoveSpeed();
            Time.timeScale = 0.2f;
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 70f);
        }

        if (fragR + EnemyLeft.manage.fragL == 55)
        {
            CharacterPlayer.manage.Skill = 0.90f;
            //_enemy.differentTime -= 0.01f;
            moveSpeed = RandomMoveSpeed();
            Time.timeScale = 1f;
           // healths = RandomHealtsgeneration();
        }

        if (fragR + EnemyLeft.manage.fragL == 70)
        {
            CharacterPlayer.manage.Skill = 0.75f;
            //_enemy.differentTime -= 0.01f;
            moveSpeed = RandomMoveSpeed();
            CharacterPlayer.manage.lv = 2;
            Time.timeScale = 0.3f;
            levelUp_sound.GetComponent<AudioSource>().Play();
            if (CharacterPlayer.manage.lv == 2)
            {
                CharacterPlayer.manage.Skill = 0f;
                CharacterPlayer.manage.Health += 70f;

            }
        }

        if (fragR + EnemyLeft.manage.fragL == 73)
        {
            CharacterPlayer.manage.Skill = 0.05f;
            //_enemy.differentTime -= 0.01f;
            moveSpeed = RandomMoveSpeed();

        }

        if (fragR + EnemyLeft.manage.fragL == 74)
        {
            CharacterPlayer.manage.Skill = 0.08f;
            Time.timeScale = 1f;
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 90f);
        }

        if (fragR + EnemyLeft.manage.fragL == 77)
        {
            CharacterPlayer.manage.Skill = 0.1f;
            moveSpeed = RandomMoveSpeed();
           // healths = RandomHealtsgeneration();
        }

        if (fragR + EnemyLeft.manage.fragL == 79)
        {
            CharacterPlayer.manage.Skill = 0.15f;
           // _enemy.differentTime -= 0.02f;
            moveSpeed = 0.03f;
        }

        if (fragR + EnemyLeft.manage.fragL == 80)
        {
            CharacterPlayer.manage.Skill = 0.16f;

        }

        if (fragR + EnemyLeft.manage.fragL == 85)
        {
            CharacterPlayer.manage.Skill = 0.25f;

        }

        if (fragR + EnemyLeft.manage.fragL == 86)
        {
            CharacterPlayer.manage.Skill = 0.35f;
            //_enemy.differentTime += 0.01f;
            moveSpeed = RandomMoveSpeed();
        }

        if (fragR + EnemyLeft.manage.fragL == 95)
        {
            CharacterPlayer.manage.Skill = 0.65f;
           // _enemy.differentTime -= 0.005f;
            moveSpeed = RandomMoveSpeed();
           // healths = RandomHealtsgeneration();
        }

        if (fragR + EnemyLeft.manage.fragL == 96)
        {
            CharacterPlayer.manage.Skill = 0.75f;

        }
        if (fragR + EnemyLeft.manage.fragL == 97)
        {
            CharacterPlayer.manage.Skill = 0.8f;

        }
        if (fragR + EnemyLeft.manage.fragL == 99)
        {
            CharacterPlayer.manage.Skill = 0.85f;
            moveSpeed = RandomMoveSpeed();

        }

        if (fragR + EnemyLeft.manage.fragL == 110)
        {
            CharacterPlayer.manage.Skill = 0.99f;
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 70f);

        }

        if (fragR + EnemyLeft.manage.fragL == 115)
        {
            CharacterPlayer.manage.Skill = 0.99f;
            moveSpeed = RandomMoveSpeed();
            CharacterPlayer.manage.lv = 3;
            Time.timeScale = 0.1f;
            levelUp_sound.GetComponent<AudioSource>().Play();
            CharacterPlayer.manage._background.GetComponentInChildren<Animator>().SetBool("Death", true);
            if (CharacterPlayer.manage.lv == 3)
            {
                CharacterPlayer.manage.Skill = 0f;
                CharacterPlayer.manage.Health += 120f;

            }

        }

        if (fragR + EnemyLeft.manage.fragL == 120)
        {
            CharacterPlayer.manage.Skill = 0.1f;
            Time.timeScale = 1f;
            CharacterPlayer.manage._background.GetComponentInChildren<Animator>().SetBool("Death", false);
        }

        if (fragR + EnemyLeft.manage.fragL == 125)
        {
            CharacterPlayer.manage.Skill = 0.15f; 
        }

        if (fragR + EnemyLeft.manage.fragL == 130)
        {
            CharacterPlayer.manage.Skill = 0.19f;
            moveSpeed = RandomMoveSpeed();
            healths = RandomHealtsgeneration();
        }
        if (fragR + EnemyLeft.manage.fragL == 133)
        {
            CharacterPlayer.manage.Skill = 0.22f;
            moveSpeed = RandomMoveSpeed();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 50f);
        }
        if (fragR + EnemyLeft.manage.fragL == 155)
        {
            CharacterPlayer.manage.Skill = 0.35f;
            moveSpeed = RandomMoveSpeed();
        }
        if (fragR + EnemyLeft.manage.fragL == 165)
        {
            CharacterPlayer.manage.Skill = 0.75f;
            healths = RandomHealtsgeneration();
        }

        if (fragR + EnemyLeft.manage.fragL == 185)
        {
            CharacterPlayer.manage.Skill = 0.99f;
            moveSpeed = RandomMoveSpeed();
            CharacterPlayer.manage.lv = 4;
            Time.timeScale = 0.1f;
            levelUp_sound.GetComponent<AudioSource>().Play();
            if (CharacterPlayer.manage.lv == 4)
            {
                CharacterPlayer.manage.Skill = 0f;
                CharacterPlayer.manage.Health += 100f;

            }
        }

        if (fragR + EnemyLeft.manage.fragL == 190)
        {
            CharacterPlayer.manage.Skill = 0.15f;
            Time.timeScale = 1f;
        }

        if (fragR + EnemyLeft.manage.fragL == 195)
        {
            CharacterPlayer.manage.Skill = 0.19f;
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 50f);
            moveSpeed = RandomMoveSpeed();
        }
        if (fragR + EnemyLeft.manage.fragL == 205)
        {
            CharacterPlayer.manage.Skill = 0.22f;
            healths = RandomHealtsgeneration();
        }
        if (fragR + EnemyLeft.manage.fragL == 215)
        {
            CharacterPlayer.manage.Skill = 0.35f;
            moveSpeed = RandomMoveSpeed();
        }
        if (fragR + EnemyLeft.manage.fragL == 225)
        {
            CharacterPlayer.manage.Skill = 0.45f;
            moveSpeed = RandomMoveSpeed();
        }
        if (fragR + EnemyLeft.manage.fragL == 245)
        {
            CharacterPlayer.manage.Skill = 0.65f;
            moveSpeed = RandomMoveSpeed();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 40f);
        }
        if (fragR + EnemyLeft.manage.fragL == 255)
        {
            CharacterPlayer.manage.Skill = 0.75f;
            moveSpeed = RandomMoveSpeed();
        }
        if (fragR + EnemyLeft.manage.fragL == 275)
        {
            CharacterPlayer.manage.Skill = 0.99f;
            moveSpeed = RandomMoveSpeed();
            CharacterPlayer.manage.lv = 5;
            levelUp_sound.GetComponent<AudioSource>().Play();
            if (CharacterPlayer.manage.lv == 5)
            {
                CharacterPlayer.manage.Skill = 0f;
                CharacterPlayer.manage.Health += 100f;

            }
        }
        if (fragR + EnemyLeft.manage.fragL == 285)
        {
            CharacterPlayer.manage.Skill = 0.10f;
            moveSpeed = RandomMoveSpeed();
            Time.timeScale = 0.2f;
        }
        if (fragR + EnemyLeft.manage.fragL == 288)
        {
            CharacterPlayer.manage.Skill = 0.25f;
            moveSpeed = RandomMoveSpeed();
            Time.timeScale = 1f;
        }
        if (fragR + EnemyLeft.manage.fragL == 290)
        {
            CharacterPlayer.manage.Skill = 0.35f;
            moveSpeed = RandomMoveSpeed();
        }

        if (fragR + EnemyLeft.manage.fragL == 295)
        {
            CharacterPlayer.manage.Skill = 0.37f;
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 50f);
        }

        if (fragR + EnemyLeft.manage.fragL == 305)
        {
            CharacterPlayer.manage.Skill = 0.45f;
            Time.timeScale = 0.1f;
        }

        if (fragR + EnemyLeft.manage.fragL == 308)
        {
            CharacterPlayer.manage.Skill = 0.55f;
            Time.timeScale = 1f;
        }

        if (fragR + EnemyLeft.manage.fragL == 320)
        {
            CharacterPlayer.manage.Skill = 0.75f;
            moveSpeed = RandomMoveSpeed();
        }

        if (fragR + EnemyLeft.manage.fragL == 335)
        {
            CharacterPlayer.manage.Skill = 0.85f;
            moveSpeed = RandomMoveSpeed();
        }

        if (fragR + EnemyLeft.manage.fragL == 340)
        {
            CharacterPlayer.manage.Skill = 0.99f;
            moveSpeed = RandomMoveSpeed();
            Time.timeScale = 0.1f;
            CharacterPlayer.manage.lv = 6;
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 10f);
            levelUp_sound.GetComponent<AudioSource>().Play();
            if (CharacterPlayer.manage.lv == 6)
            {
                CharacterPlayer.manage.Skill = 0f;
                CharacterPlayer.manage.Health += 100f;

            }
        }
        if (fragR + EnemyLeft.manage.fragL == 344)
        {
            CharacterPlayer.manage.Skill = 0.10f;
            moveSpeed = RandomMoveSpeed();
            Time.timeScale = 1.0f;
        }

        if (fragR + EnemyLeft.manage.fragL == 394)
        {
            CharacterPlayer.manage.Skill = 0.30f;
            moveSpeed = RandomMoveSpeed();
        }

        if (fragR + EnemyLeft.manage.fragL == 450)
        {
            CharacterPlayer.manage.Skill = 0.70f;
            moveSpeed = RandomMoveSpeed();
        }

        if (fragR + EnemyLeft.manage.fragL == 500)
        {
            CharacterPlayer.manage.Skill = 0.99f;
            moveSpeed = 3.5f;
            Time.timeScale = 0.1f;
            CharacterPlayer.manage.lv = 7;
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 100f);
            levelUp_sound.GetComponent<AudioSource>().Play();
            if (CharacterPlayer.manage.lv == 7)
            {
                CharacterPlayer.manage.Skill = 0f;
                CharacterPlayer.manage.Health += 70f;

            }
        }

        if (fragR + EnemyLeft.manage.fragL == 510)
        {
            CharacterPlayer.manage.Skill = 0.14f;
            Time.timeScale = 1f;
        }
        if (fragR + EnemyLeft.manage.fragL == 520)
        {
            CharacterPlayer.manage.Skill = 0.30f;
            moveSpeed = RandomMoveSpeed();

        }
        if (fragR + EnemyLeft.manage.fragL == 540)
        {
            CharacterPlayer.manage.Skill = 0.50f;
            moveSpeed = RandomMoveSpeed();
            Time.timeScale = 0.2f;
        }

        if (fragR + EnemyLeft.manage.fragL == 560)
        {
            moveSpeed = RandomMoveSpeed();
            Time.timeScale = 1f;
        }

        if (fragR + EnemyLeft.manage.fragL == 600)
        {
            CharacterPlayer.manage.Skill = 0.70f;
            moveSpeed = 4.5f;
        }
        if (fragR + EnemyLeft.manage.fragL == 700)
        {
            CharacterPlayer.manage.Skill = 0.99f;
            moveSpeed = 3.5f;
            Time.timeScale = 0.1f;
            CharacterPlayer.manage.lv = 8;
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 100f);
            levelUp_sound.GetComponent<AudioSource>().Play();
            if (CharacterPlayer.manage.lv == 8)
            {
                CharacterPlayer.manage.Skill = 0f;
                CharacterPlayer.manage.Health += 70f;

            }
        }
        if (fragR + EnemyLeft.manage.fragL == 720)
        {
            CharacterPlayer.manage.Skill = 0.70f;
            Time.timeScale = 1f;
            moveSpeed = 6.5f;
        }

    }

    float RandomMoveSpeed()
    {
        float RandomId;
        RandomId = Random.Range(0.5f, 3.05f);
        return RandomId;
    }

     int RandomHealtsgeneration()
    {
        randomHealthIndex = Random.Range(5000, 7000);
        return randomHealthIndex;
    }

    void TagDetected()
    {
        AxeActive = GameObject.FindGameObjectWithTag("Axe1right");
        BloodActive_sound = GameObject.FindGameObjectWithTag("bloodS1right");
        BlowBloodActive_sound = GameObject.FindGameObjectWithTag("blowS1right");
        levelUp_sound = GameObject.FindGameObjectWithTag("up_sound1");
    }

     void FixedUpdate()
    {
        targetDetected();
        Move();
        HPslider();
        TagDetected();
        //print(healths);


    }

}
