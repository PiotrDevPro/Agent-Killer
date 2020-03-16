using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    //Main
    public static CharacterPlayer manage;
    public float speed = 1f;
    public float jumpforce = 1.0f;
    private Rigidbody myrigidbody;
    private Collider coll;
    SpriteRenderer spriteRenderer;
    private Material matWhite;
    private Material matDefault;
    //Classes
    public MenuGUI menuGUI;
    public _Game game;
    public _Player playerConfig;
    public Save saveManager;
    public _Audio _audio;

    private GameObject _PlayerRig;
    private GameObject _Anim;
    private GameObject _pauseAnim;
    private GameObject _settingAnim;
    private GameObject _scorePanelAnim;
    private GameObject _restartAnim;
    private GameObject _jumpButtonAnim;
    private GameObject _animPointText;
    private GameObject _animTopPointText;
    private GameObject _background;
    //public SpriteRenderer PlayerFlip;
    public static bool _jump = false;
    public bool tapToPlay = false;
    bool onGround = true;
    bool isStartProgressTime = false;
    bool isDead = false;

    public GameObject tapToPlayButton;
    public GameObject startAnimText;
    public GameObject Panel;
    public GameObject pauseActive;
    public GameObject settingActive;
    public GameObject pausePanel;
    public GameObject settingPanel;
    public GameObject scorePanel;
    public GameObject playerActive;
    public GameObject rayActive;
    public GameObject jumpButtonActive;
    #region Anim
    public GameObject textPoint;
    public GameObject textPointLabel;
    public GameObject textTopPoint;
    public GameObject textTopPointLabel;
    #endregion


    //playerConfig
    private float progressFill;


    [System.Serializable]
    public class MenuGUI
    {
        public Image image;

        #region Button
        public Button settings;
        #endregion
    }

    [System.Serializable]
    public class _Player
    {
        public float Health = 100f;
        public Text HpTx;
        public Text metr;

        public Text Points;
        public Text HighPoints;
        public int max;

    }

    [System.Serializable]
    public class _Game
    {
        public GameObject enemySpark;
        public int xResolution = 720, yResolution = 1280;

        public Toggle audioT;
        public Toggle soundT;
        public Toggle vibroT;
    }

    [System.Serializable]
    public class Save
    {
        public string topSave = "topSave";
        public string currentSave = "currentSave";
        public int top = 0;
        public int current = 0;
    }

    [System.Serializable]
    public class _Audio
    {
        public AudioSource main;
        public AudioSource score;
        public AudioSource AnimSound;
        public AudioSource die;
    }

    void Awake()
    {
        manage = this;
        _PlayerRig = GameObject.FindGameObjectWithTag("Player");
        _Anim = GameObject.FindGameObjectWithTag("Player");
        _pauseAnim = GameObject.FindGameObjectWithTag("Paus");
        _background = GameObject.FindGameObjectWithTag("background");
        _settingAnim = GameObject.FindGameObjectWithTag("setting");
        _jumpButtonAnim = GameObject.FindGameObjectWithTag("JumpBtnAnim");
        _scorePanelAnim = GameObject.FindGameObjectWithTag("ScoreTable");
        myrigidbody = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        LoadSettings();
    }

    void Start()
    {

        tapToPlayButton.SetActive(true);
        startAnimText.SetActive(true);
        //pauseActive.SetActive(false);
        settingActive.SetActive(true);
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        scorePanel.SetActive(false);
        rayActive.SetActive(false);
        jumpButtonActive.SetActive(false);
        textPoint.SetActive(false);
        textTopPoint.SetActive(false);
        textPointLabel.SetActive(false);
        textTopPointLabel.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        saveManager.top = PlayerPrefs.GetInt("topSave");
        saveManager.current = PlayerPrefs.GetInt("currentSave");
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = false;
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = false;

    }

    void FixedUpdate()
    {
        if (tapToPlay && !isDead)
        {

                //Move();
                playerConfig.Health -= 0.01f;
                Panel.GetComponent<Animator>().SetBool("Open", true);
                pauseActive.SetActive(true);
                isStartProgressTime = true;
                tapToPlayButton.SetActive(false);
                startAnimText.SetActive(false);
                _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = true;
                _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = true;
                _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().FixHorizontal = true;
                // settingActive.SetActive(false);  
                //jumpButtonActive.SetActive(true);
                //_jumpButtonAnim.GetComponent<Animator>().SetBool("jumpAnimBtn", true);

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

    #region  Player Control
    void Move()
    {
        Vector3 temp = Vector3.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + temp, speed * Time.deltaTime);

        if (CheckGround())
        {
            _Anim.GetComponentInChildren<Animator>().SetBool("Walk", false);
            _Anim.GetComponentInChildren<Animator>().SetBool("Stand", true);
           
        }
        else
        {
            _Anim.GetComponentInChildren<Animator>().SetBool("Walk", true);
            _Anim.GetComponentInChildren<Animator>().SetBool("Stand", false);
        }
        
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = true;
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = true;
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().FixHorizontal = true;
       // if (!CheckGround())
       // {
       //     _Anim.GetComponentInChildren<Animator>().SetBool("Jump", true);
      //      _Anim.GetComponentInChildren<Animator>().SetBool("Walk", false);
       // }
       // else
       // {
      //      _Anim.GetComponentInChildren<Animator>().SetBool("Jump", false);
    //        _Anim.GetComponentInChildren<Animator>().SetBool("Walk", true);
            //_Anim.GetComponentInChildren<Animator>().SetBool("Climb", false);
      //  }

        if (Jumping.isSlowMo)
        {
            rayActive.SetActive(true);
        }
        else
        {
            rayActive.SetActive(false);
        }
    }

    bool CheckGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, coll.bounds.extents.y + 0.1f);
        #region Check the ground for 2D
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        //onGround = colliders.Length != 1;
        #endregion
    }

    public void Jump()
    {

        if ( tapToPlay && CheckGround())
        {
            _PlayerRig.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            _Anim.GetComponentInChildren<Animator>().SetBool("Jump", true);
            _Anim.GetComponentInChildren<Animator>().SetBool("Walk", false);
        }
        else
        {
            _Anim.GetComponentInChildren<Animator>().SetBool("Walk", true);
            _Anim.GetComponentInChildren<Animator>().SetBool("Jump", false);

        }

    }

    void Latency()
    {
        _jump = false;
        Invoke("TimeZone", 0.3f);
        _Anim.GetComponentInChildren<Animator>().SetBool("Jump", false);
        if (!CheckGround())
        {
            _Anim.GetComponentInChildren<Animator>().SetBool("Climb", true);
        }
    }

    void TimeZone()
    {
        Time.timeScale = 0.8f;
    }

    #endregion

    #region Settings
    public void Pause()

    {
        if (isDead)
        {
            return;
        }
        pausePanel.SetActive(true);
        
        _pauseAnim.GetComponentInChildren<Animator>().Play(0);
        _background.GetComponent<Animator>().SetBool("bgAttack", false);
        _jumpButtonAnim.GetComponent<Animator>().SetBool("jumpAnimBtn", false);
        _Anim.GetComponentInChildren<Animator>().SetBool("Run", false);
        Time.timeScale = 0.8f;
        _PlayerRig.SetActive(false);
        //_PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = false;
        _audio.main.mute = true;
    }

    public void Resume()

    {
        Time.timeScale = 0.8f;
        pausePanel.SetActive(false);
        //_PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = true;
        _PlayerRig.SetActive(true);
        if(PlayerPrefs.GetInt("Music")== 0)
        {
            _audio.main.mute = true;
        }
        else
        {
            _audio.main.mute = false;
        }
        isStartProgressTime = true;
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
        _audio.main.mute = false;
    }

    public void Settings()
    {
        settingPanel.SetActive(true);
        startAnimText.SetActive(false);
        _settingAnim.GetComponentInChildren<Animator>().SetBool("open", true);
        _settingAnim.GetComponentInChildren<Animator>().SetBool("close", false);
        _PlayerRig.SetActive(false);
        _audio.main.mute = true;
    }

    public void SettingClose()
    {
        settingPanel.SetActive(false);
        startAnimText.SetActive(true);
        _PlayerRig.SetActive(true);
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            _audio.main.mute = true;
        }
        else
        {
            _audio.main.mute = false;
        }
        

    }

    public void TapToPlay()
    {
        tapToPlay = true;
    }

    public void ResolutionOnAwake()
    {
        Screen.SetResolution(game.xResolution, game.yResolution, true);

        Camera.main.aspect = 16f / 9f;
    }

    public void LoadSettings()
    {
        game.audioT.isOn = (PlayerPrefs.GetInt("Sound") == 1) ? true : false;
        AudioListener.pause = (PlayerPrefs.GetInt("Sound") == 0) ? true : false;
        game.soundT.isOn = (PlayerPrefs.GetInt("Music") == 1) ? true : false;
        _audio.main.mute = (PlayerPrefs.GetInt("Music") == 0) ? true : false;

    }

    public void Sound(Toggle tgl)
    {
        if (tgl.isOn)
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            AudioListener.pause = true;
            PlayerPrefs.SetInt("Sound", 0);
        }
    }

    public void Music(Toggle tgl)
    {
        if (tgl.isOn)
        {
            _audio.main.mute = false;
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            _audio.main.mute = true;
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    public void Vibro(Toggle toggle)
    {
        if (toggle.isOn)
        {

        }
        else
        {

        }
    }

    public void UpdateTop()
    {
        PlayerPrefs.SetInt("topSave", saveManager.top);
        saveManager.top = PlayerPrefs.GetInt("topSave");
        PlayerPrefs.Save();
    }

    public void UpdateCurrent()
    {
        PlayerPrefs.SetInt("currentSave", saveManager.current);
        saveManager.current = PlayerPrefs.GetInt("currentSave");
        PlayerPrefs.Save();
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    #region Game Play

    private void HP()
    {

        progressFill = playerConfig.Health / 100;
        menuGUI.image.fillAmount = progressFill;

        if (playerConfig.Health < 0)
        {
            playerConfig.Health = 0;

            playerConfig.HpTx.text = 0.ToString();
        }
        else if (playerConfig.Health == 0)
        {
            //_Anim.GetComponentInChildren<Animator>().SetBool("Walk", false);
            Time.timeScale = 0.1f;
            _Anim.GetComponentInChildren<Animator>().SetBool("DieBack", true);
            _background.GetComponentInChildren<Animator>().SetBool("Death", true);

            isDead = true;
            _audio.main.mute = true;
            _audio.die.mute = false;
            //jumpButtonActive.SetActive(false);
            
            if(saveManager.current > saveManager.top)
            {
                saveManager.top = saveManager.current;
                UpdateTop(); 
            }
            Invoke("GameOver", 0.5f);
        }
    }

    private void GameOver()
    {
        
        scorePanel.SetActive(true);
        textPoint.SetActive(true);
        textTopPoint.SetActive(true);
        Invoke("TextActive",1f);
        _audio.die.mute = true;
        _audio.AnimSound.mute = false;
        _audio.AnimSound.Play();
        _audio.score.mute = false;
        Time.timeScale = 0.8f;
        _PlayerRig.SetActive(false);
        

    }
   
    private void EnemyTagDetection()
    {
        game.enemySpark = GameObject.FindGameObjectWithTag("EnemySpark");
    }

    private void EnemySpark()
    {

    }

    private void TextActive()
    {
        textPointLabel.SetActive(true);
        textTopPointLabel.SetActive(true);
    }

    private void Distance()
    {
        saveManager.current  = (int)PlayerPrefs.GetInt("EnemyKilled");
        UpdateCurrent();
        if (transform.position.x * 10f < 0)
        {
            playerConfig.metr.text = 0.ToString() + "m";
            playerConfig.Points.text = 0.ToString() + "m";
        }
        else
        {
            playerConfig.metr.text = saveManager.current.ToString() + "m";
            playerConfig.Points.text = saveManager.current.ToString() + "b";
            playerConfig.HighPoints.text = saveManager.top.ToString() + "b";
        }
    }

    private void GameProgressFill()
    {
        if (isStartProgressTime)
        {
            progressFill = transform.position.x / 10f;
            menuGUI.image.fillAmount = progressFill;
        }
        else
        {
            return;
        }

    }

    private void isFilledDone()
    {
        if (menuGUI.image.fillAmount == 1)
        {
            
            settingPanel.SetActive(true);
            _settingAnim.GetComponentInChildren<Animator>().Play(0);
            _PlayerRig.SetActive(false);
            _audio.main.mute = true;
            _audio.score.mute = false;
            _audio.AnimSound.mute = false;
            _audio.AnimSound.Play();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        
        if(col.tag == "EnemySpark")
        {

            playerConfig.Health = playerConfig.Health - 5;
            playerConfig.HpTx.text = (playerConfig.Health).ToString();
            _Anim.GetComponentInChildren<Animator>().SetBool("Climb",true);
            _Anim.GetComponentInChildren<Animator>().SetBool("Walk", false);
            //spriteRenderer.material = 
            if (playerConfig.Health == 0)
            {
                _audio.die.mute = false;
                _audio.die.Play();
                isDead = true;
                _Anim.GetComponentInChildren<Animator>().SetBool("Climb", false);
                _background.GetComponentInChildren<Animator>().SetBool("Death",true);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "EnemySpark")
        {
            _Anim.GetComponentInChildren<Animator>().SetBool("Climb", false);
        }
    }

    #endregion

    void Update()
    {
        HP();
        EnemyTagDetection();
        Distance();
    }
}
