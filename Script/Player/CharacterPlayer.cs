using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    public static CharacterPlayer manage;
    public float speed = 0.01f;
    public float jumpforce = 1.0f;
    private GameObject _PlayerRig;
    private GameObject _Anim;
    private GameObject _pauseAnim;
    private GameObject _settingAnim;
    private GameObject _restartAnim;
    private GameObject _jumpButtonAnim;
    private GameObject _background;
    //public SpriteRenderer PlayerFlip;
    private bool tapToPlay = false;
    public static bool _jump = false;
    bool onGround = true;

    public GameObject tapToPlayButton;
    public GameObject Panel;
    public GameObject pauseActive;
    public GameObject settingActive;
    public GameObject pausePanel;
    public GameObject settingPanel;
    public GameObject playerActive;
    public GameObject rayActive;
    public GameObject jumpButtonActive;
    
    public AudioSource main; 

    void Awake()
    {
        manage = this;
        _PlayerRig = GameObject.FindGameObjectWithTag("Player");
        _Anim = GameObject.FindGameObjectWithTag("Player");
        _pauseAnim = GameObject.FindGameObjectWithTag("Paus");
        _background = GameObject.FindGameObjectWithTag("background");
        _settingAnim = GameObject.FindGameObjectWithTag("setting");
        _jumpButtonAnim = GameObject.FindGameObjectWithTag("JumpBtnAnim");
        //PlayerFlip = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        tapToPlayButton.SetActive(true);
        pauseActive.SetActive(false);
        settingActive.SetActive(true);
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        rayActive.SetActive(false);
        jumpButtonActive.SetActive(false);
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = false;
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = false;

    }

    void FixedUpdate()
    {
        print(onGround);
        CheckGround();
        if (Input.GetMouseButtonUp(1) || tapToPlay)
            {

            Move();
            Panel.GetComponent<Animator>().SetBool("Open",true);
            pauseActive.SetActive(true);
            settingActive.SetActive(false);
            tapToPlay = true;
            tapToPlayButton.SetActive(false);
            _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = true;
            jumpButtonActive.SetActive(true);
            _jumpButtonAnim.GetComponent<Animator>().SetBool("jumpAnimBtn",true);
        }
        #region JumpOnTrigger
        // if (_jump && onGround)
        //  {
        //   Jump();
        //_Anim.GetComponentInChildren<Animator>().SetBool("Run", false);
        //Time.timeScale = 0.2f;
        // rayActive.SetActive(true);
        // Invoke("Latency", 2.8f);
        //    _background.GetComponent<Animator>().SetBool("bgAttack", true);
        //    _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = true;
        //   _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().FixHorizontal = false;
        //_Anim.GetComponentInChildren<Animator>().SetBool("Climb", true);
        //InvokeRepeating("Jump",1f,2f);
        // }
        #endregion
    }

    void Move()
    {
        Vector3 temp = Vector3.right;
        transform.position = Vector3.MoveTowards(transform.position,transform.position+temp,speed*Time.deltaTime);
        _Anim.GetComponentInChildren<Animator>().SetBool("Run", true);
        //_PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().FixHorizontal = true;
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = true;
        rayActive.SetActive(false);
        if (!onGround)
        {
            _Anim.GetComponentInChildren<Animator>().SetBool("Run", false);
            //_background.GetComponent<Animator>().SetBool("bgAttack", false);

        }
            
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        onGround = colliders.Length != 1;
    }

    public void Jump()
    {
        
        if (onGround && tapToPlay)
        {
            _PlayerRig.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
            _Anim.GetComponentInChildren<Animator>().SetBool("Jump", true);
            _Anim.GetComponentInChildren<Animator>().SetBool("Run", false);
        }
        else
        {
            _Anim.GetComponentInChildren<Animator>().SetBool("Run", true);
            _Anim.GetComponentInChildren<Animator>().SetBool("Jump", false);

        }
       
    }

    void Latency()
    {
        _jump = false;
        Invoke("TimeZone",0.3f);
        _Anim.GetComponentInChildren<Animator>().SetBool("Jump", false);
        if (!onGround)
        {
            _Anim.GetComponentInChildren<Animator>().SetBool("Climb", true);
        }
    }

    void TimeZone()
    {
        Time.timeScale = 0.8f;
    }

    public void Pause()

    {
        pausePanel.SetActive(true);
        Time.timeScale = 0.8f;
        _pauseAnim.GetComponentInChildren<Animator>().Play(0);
        _background.GetComponent<Animator>().SetBool("bgAttack",false);
        _jumpButtonAnim.GetComponent<Animator>().SetBool("jumpAnimBtn", false);
        _PlayerRig.SetActive(false);
        main.mute = true;

        //  if (_jump)
        // {
        //     jumpforce += 0.02f;
        //  }
    }

    public void Resume()

    {
        Time.timeScale = 0.8f;
        pausePanel.SetActive(false);
        _PlayerRig.SetActive(true);
        main.mute = false;
        if (_jump)
        {
            _background.GetComponent<Animator>().SetBool("bgAttack", true);
        }
        else
        {
            _background.GetComponent<Animator>().SetBool("bgAttack", false);
        }

    }

    public void Restart()

    {
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 0.8f;
        main.mute = false;
    }

    public void Settings()
    {
        settingPanel.SetActive(true);
        _settingAnim.GetComponentInChildren<Animator>().Play(0);
        _PlayerRig.SetActive(false);
    }

    public void SettingClose()
    {

        _settingAnim.GetComponentInChildren<Animator>().SetBool("close",true);
        settingPanel.SetActive(false);
        _PlayerRig.SetActive(true);

    }

     void Update()
    {
        //print(_jumpButtonAnim);
    }
}
