using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    //Main
    public static CharacterPlayer manage;
    public float speed = 1f;
    public float jumpforce = 1.0f;
    public Text cashAmount;
    public Text cashShop;
    private Rigidbody myrigidbody;
    private Collider coll;
    
    //Classes
    public MenuGUI menuGUI;
    public _Game game;
    public _Player playerConfig;
    public Save saveManager;
    public _Audio _audio;
    public _Shop shop;

    private GameObject _PlayerRig;
    private GameObject _pauseAnim;
    private GameObject _settingAnim;
    private GameObject _scorePanelAnim;
    private GameObject _restartAnim;
    private GameObject _jumpButtonAnim;
    private GameObject _animPointText;
    private GameObject _animTopPointText;
    private GameObject _HighScoreTextAnim;
    private GameObject _PointAnim;
    private GameObject bullets;
    private GameObject enemyActiveL;
    private GameObject enemyActiveR;
    private GameObject BlowBloodActive_sound;
    private GameObject SwordActive;
    private GameObject FirearmActive;
    public GameObject _background;
    public GameObject _Ok;


    

    //public SpriteRenderer PlayerFlip;
    public static bool _jump = false;
    public bool tapToPlay = false;
    bool onGround = true;
    bool isStartProgressTime = false;
    bool isTopScore = false;
    public bool isDead = false;
    public bool isPaused = false;


    public GameObject tapToPlayButton;
    public GameObject startAnimText;
    public GameObject Panel;
    public GameObject pauseActive;
    public GameObject settingActive;
    public GameObject pausePanel;
    public GameObject settingPanel;
    public GameObject scorePanel;
    public GameObject playerActive;
    public GameObject bloodActive;
    public GameObject rayActive;
    public GameObject ShopA1;
    public GameObject NoAds;

    #region Anim
    public GameObject textPoint;
    public GameObject textPointLabel;
    public GameObject textTopPoint;
    public GameObject textTopPointLabel;
    #endregion


    //playerConfig
    private float progressFill;
    private float _levelUp;
    public float Skill = 0f;
    public float lv = 0;
    public float Health = 100f;
    //private static int n;
    public int Damage;


    [System.Serializable]
    public class MenuGUI
    {
        public Image imageHP;
        public Image imageLV;

    }

    [System.Serializable]
    public class _Player
    {
        
        public Text HpTx;
        public Text HpShopTx;
        public Text LvTx;
        public Text metr;
        public Text DamageShopTx;

        public Text Points;
        public Text HighPoints;
        public Text timerText;
        public float startTime;
        //public int max;

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

    [System.Serializable]
    public class _Shop
    {
        public int[] itemPrice;
        

        public Text _head;
        public Text _Ears;
        public Text _hair;
        public Text _Eyebrown;
        public Text _Eyes;
        public Text _mounth;
        public Text _beard;
        public Text _body;
        public Text _glasses;
        public Text _mask;
        public Text _earning;
        public Text _cape;
        public Text _back;
        public Text _helmet;
        public Text _Armor;
        public Text _Shield;
        public Text _Melle1;
        public Text _Melle2;
        public Text _Firearms1;
        public Text _Firearms2;


        public GameObject EnoughMoney, buyHead, buyEars, buyHair, buyEyebrows, buyEyes, buyMouth, buyBeard, buyBody, buyGlasses, buyMask,
                          buyEarnings, BuyCape, buyBack, buyFirearmsH1, buyFirearmsH2, buyHelmet, buyArmor, buyShield, buyMelee1H, buyMelee2H;
        public GameObject buySound, BuyFailedSound;

    }

    void Awake()
    {
        manage = this;
        _PlayerRig = GameObject.FindGameObjectWithTag("Player");

        _pauseAnim = GameObject.FindGameObjectWithTag("Paus");
        _background = GameObject.FindGameObjectWithTag("background");
        _settingAnim = GameObject.FindGameObjectWithTag("setting");
        _scorePanelAnim = GameObject.FindGameObjectWithTag("ScoreTable");
        _HighScoreTextAnim = GameObject.FindGameObjectWithTag("HighTextAnim");
        bullets = GameObject.FindGameObjectWithTag("bulletCount");
        _PointAnim = GameObject.FindGameObjectWithTag("Point");

        enemyActiveL = GameObject.FindGameObjectWithTag("enemy");
        enemyActiveR = GameObject.FindGameObjectWithTag("enemyR");

        myrigidbody = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        LoadSettings();
    }

    void Start()
    {

        tapToPlayButton.SetActive(true);
        _Ok.SetActive(false);
        startAnimText.SetActive(true);
        pauseActive.SetActive(false);
        settingActive.SetActive(true);
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        scorePanel.SetActive(false);
        rayActive.SetActive(false);
        textPoint.SetActive(false);
        textTopPoint.SetActive(false);
        textPointLabel.SetActive(false);
        textTopPointLabel.SetActive(false);
        ShopA1.SetActive(false);
        shop.EnoughMoney.SetActive(false);
        bloodActive.SetActive(false);
        saveManager.top = PlayerPrefs.GetInt("topSave");
        saveManager.current = PlayerPrefs.GetInt("currentSave");
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = false;
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = false;
        _PlayerRig.GetComponentInChildren<Animator>().SetBool("Ready", false);
        SwordActive = GameObject.FindGameObjectWithTag("DragonTooth");
        FirearmActive = GameObject.FindGameObjectWithTag("FireArmEq");
        ShieldBuying();

        //PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash")+10000f);

    }

    void FixedUpdate()
    {
        if (tapToPlay && !isDead)
        {

            //Move();
            
                Panel.GetComponent<Animator>().SetBool("Open", true);
                NoAds.GetComponent<Animator>().SetBool("start",true);
                _PlayerRig.GetComponentInChildren<Animator>().SetBool("Ready", true);
                _PlayerRig.GetComponentInChildren<Animator>().SetBool("Crouch",true);
                pauseActive.SetActive(true);
                settingActive.SetActive(false);
                isStartProgressTime = true;
                tapToPlayButton.SetActive(false);
                startAnimText.SetActive(false);
                _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = true;
                _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = true;
                _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().FixHorizontal = true;
                

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
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Walk", false);
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Stand", true);
           
        }
        else
        {
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Walk", true);
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Stand", false);
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
        #region Check the ground for 2D physics
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        //onGround = colliders.Length != 1;
        #endregion
    }

    public void Jump()
    {

        if ( tapToPlay && CheckGround())
        {
            _PlayerRig.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Jump", true);
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Walk", false);
        }
        else
        {
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Walk", true);
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Jump", false);

        }

    }

    void Latency()
    {
        _jump = false;
        Invoke("TimeZone", 0.3f);
        _PlayerRig.GetComponentInChildren<Animator>().SetBool("Jump", false);
        if (!CheckGround())
        {
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Climb", true);
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
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = false;
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = false;
        Time.timeScale = 0f;
        
        _audio.main.mute = true;

    }

    public void Resume()

    {
        Time.timeScale = 0.8f;
        pausePanel.SetActive(false);
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.WeaponControls>().enabled = true;
        _PlayerRig.GetComponent<Assets.HeroEditor.Common.CharacterScripts.CharacterFlip>().enabled = true;
        if (PlayerPrefs.GetInt("Music")== 1)
        {
            _audio.main.mute = true;
        }
        else
        {
            _audio.main.mute = false;
        }
      //  isStartProgressTime = true;
      //  isPaused = false;
      //  enemyActiveL.GetComponent<SkinnedMeshRenderer>().enabled = true;
      //  enemyActiveR.GetComponent<SkinnedMeshRenderer>().enabled = true;

    }

    public void Restart()

    {
        
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 0.8f;
        _audio.main.mute = false;
        AdsManage.manage.ShowAdDefault();
    }

    public void Settings()
    {
        settingPanel.SetActive(true);
        if (!tapToPlay)
        {
            startAnimText.SetActive(false);
        }
        _settingAnim.GetComponentInChildren<Animator>().SetBool("open", true);
        _settingAnim.GetComponentInChildren<Animator>().SetBool("close", false);
        _PlayerRig.SetActive(false);
        _audio.main.mute = true;
        ShopA1.SetActive(false);
        enemyActiveL.GetComponent<SkinnedMeshRenderer>().enabled = false;
        enemyActiveR.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }

    public void SettingClose()
    {
        settingPanel.SetActive(false);
        if (!tapToPlay)
        {
            startAnimText.SetActive(true);
        }
        _PlayerRig.SetActive(true);
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            _audio.main.mute = true;
        }
        else
        {
            _audio.main.mute = false;
        }
        enemyActiveL.GetComponent<SkinnedMeshRenderer>().enabled = true;
        enemyActiveR.GetComponent<SkinnedMeshRenderer>().enabled = true;

    }

    public void TapToPlay()
    {
        tapToPlay = true;
        ShopA1.SetActive(false);
        enemyActiveL.GetComponent<SkinnedMeshRenderer>().enabled = true;
        enemyActiveR.GetComponent<SkinnedMeshRenderer>().enabled = true;
        _PlayerRig.GetComponent<Transform>().position = new Vector3(2.14f, -0.633f);
        _PlayerRig.GetComponent<Transform>().localScale = new Vector3(0.11f, 0.11f);
        _PlayerRig.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ResolutionOnAwake()
    {
        Screen.SetResolution(game.xResolution, game.yResolution, true);

        Camera.main.aspect = 16f / 9f;
    }

    public void LoadSettings()
    {
        game.audioT.isOn = (PlayerPrefs.GetInt("Sound") == 0) ? true : false;
        AudioListener.pause = (PlayerPrefs.GetInt("Sound") == 1) ? true : false;
        game.soundT.isOn = (PlayerPrefs.GetInt("Music") == 0) ? true : false;
        _audio.main.mute = (PlayerPrefs.GetInt("Music") == 1) ? true : false;
        game.vibroT.isOn = (PlayerPrefs.GetInt("VibrationActive") == 1) ? true : false;

    }

    public void Sound(Toggle tgl)
    {
        if (tgl.isOn)
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            AudioListener.pause = true;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }

    public void Music(Toggle tgl)
    {
        if (tgl.isOn)
        {
            _audio.main.mute = false;
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            _audio.main.mute = true;
            PlayerPrefs.SetInt("Music", 1);
        }
    }

    public void Vibro(Toggle toggle)
    {
        if (toggle.isOn)
            PlayerPrefs.SetInt("VibrationActive", 1);
        else
            PlayerPrefs.SetInt("VibrationActive", 0);
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
        AdsManage.manage.ShowAdDefault();
        Application.Quit();
    }

    #endregion

    #region Shop

    public void Shop()
    {

        ShopA1.SetActive(true);
        ShopA1.GetComponent<Animator>().SetBool("Shop",true);
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        startAnimText.SetActive(false);
        enemyActiveL.GetComponent<SkinnedMeshRenderer>().enabled = false;
        enemyActiveR.GetComponent<SkinnedMeshRenderer>().enabled = false;
        _PlayerRig.GetComponent<Transform>().position = new Vector3(1.15f,-0.31f);
        _PlayerRig.GetComponent<Transform>().localScale = new Vector3(0.3f, 0.3f);
        _PlayerRig.GetComponent<Rigidbody>().isKinematic = true;
        tapToPlayButton.SetActive(false);
        _Ok.SetActive(true);

    }

    public void Price()
    {
        shop._head.text = shop.itemPrice[0].ToString() + "$";
        shop._Ears.text = shop.itemPrice[1].ToString() + "$";
        shop._hair.text = shop.itemPrice[2].ToString() + "$";
        shop._Eyebrown.text = shop.itemPrice[3].ToString() + "$";
        shop._Eyes.text = shop.itemPrice[4].ToString() + "$";
        shop._mounth.text = shop.itemPrice[5].ToString() + "$";
        shop._beard.text = shop.itemPrice[6].ToString() + "$";
        shop._body.text = shop.itemPrice[7].ToString() + "$";
        shop._glasses.text = shop.itemPrice[8].ToString() + "$";
        shop._mask.text = shop.itemPrice[9].ToString() + "$";
        shop._earning.text = shop.itemPrice[10].ToString() + "$";
        shop._cape.text = shop.itemPrice[11].ToString() + "$";
        shop._back.text = shop.itemPrice[12].ToString() + "$";
        shop._helmet.text = shop.itemPrice[13].ToString() + "$";
        shop._Armor.text = shop.itemPrice[14].ToString() + "$";
        shop._Shield.text = shop.itemPrice[15].ToString() + "$";
        shop._Melle1.text = shop.itemPrice[16].ToString() + "$";
        shop._Melle2.text = shop.itemPrice[17].ToString() + "$";
        shop._Firearms1.text = shop.itemPrice[18].ToString() + "$";
    }

    public void Ok_Btn()
    {
        _Ok.SetActive(false);
        ShopA1.SetActive(false);
        startAnimText.SetActive(true);
        enemyActiveL.GetComponent<SkinnedMeshRenderer>().enabled = true;
        enemyActiveR.GetComponent<SkinnedMeshRenderer>().enabled = true;
        _PlayerRig.GetComponent<Transform>().position = new Vector3(2.14f, -0.633f);
        _PlayerRig.GetComponent<Transform>().localScale = new Vector3(0.11f, 0.11f);
        _PlayerRig.GetComponent<Rigidbody>().isKinematic = false;
        tapToPlayButton.SetActive(true);
    }

    public void cashAmnt()
    {
        cashAmount.text = PlayerPrefs.GetFloat("Cash").ToString();
        cashShop.text = PlayerPrefs.GetFloat("Cash").ToString();
    }

    public void MoneyNotEnough()
    {
        shop.EnoughMoney.SetActive(false);
    }

    public void LoadUpgrade()
    {
        if (PlayerPrefs.GetInt("head") == 1)
        {
            shop.buyHead.SetActive(false);
        }
        else
        {
            shop.buyHead.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Ears") == 1)
        {
            shop.buyEars.SetActive(false);
        }
        else
        {
            shop.buyEars.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Hair") == 1)
        {
            shop.buyHair.SetActive(false);
        }
        else
        {
            shop.buyHair.SetActive(true);
        }

        if (PlayerPrefs.GetInt("EyeBrown") == 1)
        {
            shop.buyEyebrows.SetActive(false);
        }
        else
        {
            shop.buyEyebrows.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Eyes") == 1)
        {
            shop.buyEyes.SetActive(false);
        }
        else
        {
            shop.buyEyes.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Mouth") == 1)
        {
            shop.buyMouth.SetActive(false);
        }
        else
        {
            shop.buyMouth.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Beard") == 1)
        {
            shop.buyBeard.SetActive(false);
        }
        else
        {
            shop.buyBeard.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Body") == 1)
        {
            shop.buyBody.SetActive(false);
        }
        else
        {
            shop.buyBody.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Glasses") == 1)
        {
            shop.buyGlasses.SetActive(false);
        }
        else
        {
            shop.buyGlasses.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Mask") == 1)
        {
            shop.buyMask.SetActive(false);
        }
        else
        {
            shop.buyMask.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Earrning") == 1)
        {
            shop.buyEarnings.SetActive(false);
        }
        else
        {
            shop.buyEarnings.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Cape") == 1)
        {
            shop.BuyCape.SetActive(false);
        }
        else
        {
            shop.BuyCape.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Back") == 1)
        {
            shop.buyBack.SetActive(false);
        }
        else
        {
            shop.buyBack.SetActive(true);
        }

        if (PlayerPrefs.GetInt("FirearmsH1") == 1)
        {
            shop.buyFirearmsH1.SetActive(false);
        }
        else
        {
            shop.buyFirearmsH1.SetActive(true);
        }
        /*
        if (PlayerPrefs.GetInt("FirearmsH2") == 1)
        {
            shop.buyFirearmsH2.SetActive(false);
        }
        else
        {
            shop.buyFirearmsH2.SetActive(true);
        }
        */
        if (PlayerPrefs.GetInt("Helmet") == 1)
        {
            shop.buyHelmet.SetActive(false);
        }
        else
        {
            shop.buyHelmet.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Armor") == 1)
        {
            shop.buyArmor.SetActive(false);
        }
        else
        {
            shop.buyArmor.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Shield") == 1)
        {
            shop.buyShield.SetActive(false);
        }
        else
        {
            shop.buyShield.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Melle1H") == 1)
        {
            shop.buyMelee1H.SetActive(false);
        }
        else
        {
            shop.buyMelee1H.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Melle2H") == 1)
        {
            shop.buyMelee2H.SetActive(false);
        }
        else
        {
            shop.buyMelee2H.SetActive(true);
        }

    }

    public void HpUpForCoin()
    {
        if (PlayerPrefs.GetFloat("Cash") >= 150)
        {
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - 150);
            Health += 50;
        } else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
        }
    }

    public void DamageUpForCash()
    {
        if (PlayerPrefs.GetFloat("Cash") >= 200)
        {
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - 200);
            EnemyLeft.manage.DamageFromPlayer += 5;
            EnemyRight.manage.DamageFromPlayer += 5;
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play(); 
        }
    }

    #region items

    public void buyHead()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[0])
        {
            PlayerPrefs.SetInt("head", 1);
            shop.buyHead.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[0]);
        } else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("head", 0);
        }
    }

    public void buyEars()
    {
       if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[1])
       {
            shop.buyEars.SetActive(false);
            PlayerPrefs.SetInt("Ears", 1);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[1]);
        } else
       {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Ears", 0);

        }   
    }

    public void buyHair()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[2])
        {
            PlayerPrefs.SetInt("Hair", 1);
            shop.buyHair.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[2]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Hair", 0);
        }
    }

    public void EyeBrown()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[3])
        {
            PlayerPrefs.SetInt("EyeBrown", 1);
            shop.buyEyebrows.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[3]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("EyeBrown", 0);
        }
    }

    public void Eyes()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[4])
        {
            PlayerPrefs.SetInt("Eyes", 1);
            shop.buyEyes.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[4]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Eyes", 0);
        }
    }

    public void Mouth()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[5])
        {
            PlayerPrefs.SetInt("Mouth", 1);
            shop.buyMouth.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[5]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Mouth", 0);
        }
    }

    public void Beard()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[6])
        {
            PlayerPrefs.SetInt("Beard", 1);
            shop.buyBeard.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[6]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Beard", 0);
        }
    }

    public void Body()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[7])
        {
            PlayerPrefs.SetInt("Body", 1);
            shop.buyBody.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[7]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Body", 0);
        }
    }

    public void Glasses()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[8])
        {
            PlayerPrefs.SetInt("Glasses", 1);
            shop.buyGlasses.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[8]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Glasses", 0);
        }
    }

    public void Mask()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[9])
        {
            PlayerPrefs.SetInt("Mask", 1);
            shop.buyMask.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[9]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Mask", 0);
        }
    }

    public void Earrning()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[10])
        {
            PlayerPrefs.SetInt("Earrning", 1);
            shop.buyEarnings.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[10]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Earrning", 0);
        }
    }

    public void Cape()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[11])
        {
            PlayerPrefs.SetInt("Cape", 1);
            shop.BuyCape.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[11]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Cape", 0);
        }
    }

    public void Back()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[12])
        {
            PlayerPrefs.SetInt("Back", 1);
            shop.buyBack.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[12]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Back", 0);
        }
    }

    public void Helmet()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[13])
        {
            PlayerPrefs.SetInt("Helmet", 1);
            shop.buyHelmet.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[13]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Helmet", 0);
        }
    }

    public void Armor()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[14])
        {
            PlayerPrefs.SetInt("Armor", 1);
            shop.buyArmor.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[14]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Armor", 0);
        }
    }

    public void Shield()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[15])
        {
            PlayerPrefs.SetInt("Shield", 1);
            Health += 150;
            shop.buyShield.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[15]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Shield", 0);
        }
    }

    public void Melle1H()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[16])
        {
            PlayerPrefs.SetInt("Melle1H", 1);
            shop.buyMelee1H.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[16]);
            SwordActive.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Melle1H", 0);
            SwordActive.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void Melle2H()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[17])
        {
            PlayerPrefs.SetInt("Melle2H", 1);
            shop.buyMelee2H.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[17]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("Melle2H", 0);
        }
    }

    public void FirearmsH1()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[18])
        {
            PlayerPrefs.SetInt("FirearmsH1", 1);
            shop.buyFirearmsH1.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[18]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("FirearmsH1", 0);
        }
    }

    public void FirearmsH2()
    {
        if (PlayerPrefs.GetFloat("Cash") >= shop.itemPrice[19])
        {
            PlayerPrefs.SetInt("FirearmsH2", 1);
            shop.buyFirearmsH2.SetActive(false);
            shop.buySound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") - shop.itemPrice[19]);
        }
        else
        {
            shop.EnoughMoney.SetActive(true);
            shop.BuyFailedSound.GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("FirearmsH2", 0);
        }
    }

    void ShieldBuying()
    {
        if (PlayerPrefs.GetInt("Shield") == 1)
        {
            Health += 150;
        }
    }

    #endregion

    #endregion

    #region Game Play

    void HP()
    {
        
        progressFill = Health / 100;
        menuGUI.imageHP.fillAmount = progressFill;
        #region timer
        /* if (tapToPlay)
         {

             float t = Time.time - playerConfig.startTime;
             string minutes = ((int)t / 60).ToString();
             string seconds = (t % 60).ToString("f2");
             //playerConfig.timerText.text = minutes + ":" + seconds;
             textTime.GetComponent<Text>().text = minutes + ":" + seconds;
         }*/
        #endregion
        if (Health < 0)
        {
            Health = 0;

            playerConfig.HpTx.text = 0.ToString();
        }
        else if (Health == 0)
        {


            Time.timeScale = 0.1f;
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("DieBack", true);
            _background.GetComponentInChildren<Animator>().SetBool("Death", true);
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Crouch", false);

            isDead = true;
            _audio.main.mute = true;
            _audio.die.mute = false;
            isPaused = true;
            if (saveManager.current > saveManager.top)
            {
                saveManager.top = saveManager.current;
                UpdateTop();
                isTopScore = true;

            }

                
            Invoke("GameOver", 0.5f);

            
        }
    }

    void LevelUp()
    {
        menuGUI.imageLV.fillAmount = Skill;
        playerConfig.LvTx.text = ((int)lv).ToString();
    }

    public void GameOver()
    {
        if (isTopScore)
        {
            textTopPoint.SetActive(true);
            textPoint.SetActive(true);
            _HighScoreTextAnim.GetComponentInChildren<Animator>().SetBool("high", true);
            _PointAnim.GetComponentInChildren<Animator>().SetBool("TopNormal",true);
        } else
        {
            textTopPoint.SetActive(true);
            textPoint.SetActive(true);
            _PointAnim.GetComponentInChildren<Animator>().SetBool("normal", true);
        }

        textTopPoint.SetActive(true);
        scorePanel.SetActive(true);
        textPoint.SetActive(true);
        Invoke("TextActive",1f);
        _audio.die.mute = true;
        _audio.AnimSound.mute = false;
        _audio.AnimSound.Play();
        _audio.score.mute = false;
        Time.timeScale = 0.8f;
        _PlayerRig.SetActive(false);
    }
   
    void TagsDetection()
    {
        BlowBloodActive_sound = GameObject.FindGameObjectWithTag("bloodExplose");
        enemyActiveL = GameObject.FindGameObjectWithTag("enemy");
        enemyActiveR = GameObject.FindGameObjectWithTag("enemyR");
    }

    void MeleeDetected()
    {
        if (PlayerPrefs.GetInt("Melle1H")== 0)
        {
            SwordActive.GetComponent<BoxCollider>().enabled = false;
        }

        if (SwordActive.GetComponent<SpriteRenderer>().sprite == null)
        {
            SwordActive.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            SwordActive.GetComponent<BoxCollider>().enabled = true;
        }

        if (SwordActive.GetComponent<SpriteRenderer>().sprite != null)
        {
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "DragonTooth")
            {
                EnemyLeft.manage.SwordDamage = 3;
                EnemyRight.manage.SwordDamage = 3;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "EnergyKnife")
            {
                EnemyLeft.manage.SwordDamage = 1;
                EnemyRight.manage.SwordDamage = 1;

            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "EnergyKnife [Paint]")
            {
                EnemyLeft.manage.SwordDamage = 1;
                EnemyRight.manage.SwordDamage = 1;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "EnergySword")
            {
                EnemyLeft.manage.SwordDamage = 4;
                EnemyRight.manage.SwordDamage = 4;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "EnergySword [Paint]")
            {
                EnemyLeft.manage.SwordDamage = 3;
                EnemyRight.manage.SwordDamage = 3;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "LightSword")
            {
                EnemyLeft.manage.SwordDamage = 5;
                EnemyRight.manage.SwordDamage = 5;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "LightSword2")
            {
                EnemyLeft.manage.SwordDamage = 6;
                EnemyRight.manage.SwordDamage = 6;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "SantaHelperWand")
            {
                EnemyLeft.manage.SwordDamage = 9;
                EnemyRight.manage.SwordDamage = 9;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "SantaHelperWand")
            {
                EnemyLeft.manage.SwordDamage = 9;
                EnemyRight.manage.SwordDamage = 9;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "SantaWand")
            {
                EnemyLeft.manage.SwordDamage = 11;
                EnemyRight.manage.SwordDamage = 11;
            }
        }
    }

    void FireArmDetected()
    {
         if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "LR-100")
        {
            Damage = 2 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "PR-5000")
        {
            Damage = 2 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "SpreadGun")
        {
            Damage = 3 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "HB-2000")
        {
            Damage = 7 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "LR-300")
        {
            Damage = 2 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "LR-300RX")
        {
            Damage = 2 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "LR-500")
        {
            Damage = 3 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "OblivionPlasmaGun")
        {
            Damage = 10 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "OblivionPlasmaGun")
        {
            Damage = 10 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "PR-9000")
        {
            Damage = 7 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "PR-9000")
        {
            Damage = 7 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "RL-800")
        {
            Damage = 70 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "RL-DIY200")
        {
            Damage = 70 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "RRII")
        {
            Damage = 10 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "SG-V")
        {
            Damage = 3 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "SR-25")
        {
            Damage = 50 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (FirearmActive.GetComponent<Assets.HeroEditor.Common.CharacterScripts.Firearm>().Params.Name == "SR-MDK")
        {
            Damage = 50 + EnemyLeft.manage.DamageFromPlayer;
        }
        if (SwordActive.GetComponent<SpriteRenderer>().sprite != null)
        {
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "DragonTooth")
            {
                Damage = 13 + EnemyLeft.manage.DamageFromPlayer;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "EnergyKnife")
            {
                Damage = 11 + EnemyLeft.manage.DamageFromPlayer;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "EnergyKnife [Paint]")
            {
                Damage = 11 + EnemyLeft.manage.DamageFromPlayer;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "EnergySword")
            {
                Damage = 14 + EnemyLeft.manage.DamageFromPlayer;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "EnergySword [Paint]")
            {
                Damage = 13 + EnemyLeft.manage.DamageFromPlayer;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "LightSword")
            {
                Damage = 15 + EnemyLeft.manage.DamageFromPlayer;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "LightSword2")
            {
                Damage = 16 + EnemyLeft.manage.DamageFromPlayer;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "SantaHelperWand")
            {
                Damage = 19 + EnemyLeft.manage.DamageFromPlayer;
            }
            if (SwordActive.GetComponent<SpriteRenderer>().sprite.name == "SantaWand")
            {
                Damage = 21 + EnemyLeft.manage.DamageFromPlayer;
            }
        }

    }
    
    void TextActive()
    {
        textPointLabel.SetActive(true);
        textTopPointLabel.SetActive(true);
    }

    void Distance()
    {
            saveManager.current  = (int)(PlayerPrefs.GetFloat("fragL")+ PlayerPrefs.GetFloat("fragR"));
            UpdateCurrent();
            playerConfig.Points.text = ((int)PlayerPrefs.GetFloat("Currents")).ToString();
            playerConfig.HighPoints.text = saveManager.top.ToString() + "";
    }

    void GameProgressFill()
    {
        if (isStartProgressTime)
        {
            progressFill = transform.position.x / 10f;
            menuGUI.imageHP.fillAmount = progressFill;
        }
        else
        {
            return;
        }

    }

    void isFilledDone()
    {
        if (menuGUI.imageHP.fillAmount == 1)
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

    void bulletCount()
    {
        bullets.GetComponent<Text>().text = (Assets.HeroEditor.Common.CharacterScripts.Firearm.manage.Params.MagazineCapacity - Assets.HeroEditor.Common.CharacterScripts.Firearm.manage.AmmoShooted).ToString();
    }

    void OnTriggerEnter(Collider col)
    {
        
        if(col.tag == "EnemySpark")
        {

            Health = Health - 8;
            playerConfig.HpTx.text = (Health).ToString();
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Climb",true);
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Walk", false);
            if (Health == 0)
            {
                _audio.die.mute = false;
                _audio.die.Play();
                isDead = true;
                _PlayerRig.GetComponentInChildren<Animator>().SetBool("Climb", false);
                _background.GetComponentInChildren<Animator>().SetBool("Death",true);
            }
        }
        if (col.tag == "Axe1")
        {
            Health = Health - 15;
            playerConfig.HpTx.text = ((int)Health).ToString();
            BlowBloodActive_sound.GetComponent<AudioSource>().Play();
            bloodActive.SetActive(true);
            if (PlayerPrefs.GetInt("VibrationActive") == 1)
            {
                Handheld.Vibrate();
            }
            //_PlayerRig.GetComponentInChildren<Animator>().SetBool("Climb", true);
            if (Health == 0)
            {
                _audio.die.mute = false;
                _audio.die.Play();
                isDead = true;
                _PlayerRig.GetComponentInChildren<Animator>().SetBool("Climb", false);
                _background.GetComponentInChildren<Animator>().SetBool("Death", true);
            }
        }
        if (col.tag == "Axe1right")
        {
            Health = Health - 15;
            playerConfig.HpTx.text = ((int)Health).ToString();
            BlowBloodActive_sound.GetComponent<AudioSource>().Play();
            bloodActive.SetActive(true);
            if (PlayerPrefs.GetInt("VibrationActive") == 1)
            {
                Handheld.Vibrate();
            }
            //_PlayerRig.GetComponentInChildren<Animator>().SetBool("Climb", true);
            if (Health == 0)
            {
                _audio.die.mute = false;
                _audio.die.Play();
                isDead = true;
                
                _PlayerRig.GetComponentInChildren<Animator>().SetBool("Climb", false);
                _background.GetComponentInChildren<Animator>().SetBool("Death", true);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "EnemySpark")
        {
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Climb", false);
        }
        if (col.tag == "Axe1")
        {
            _PlayerRig.GetComponentInChildren<Animator>().SetBool("Climb", false);
        }
    }

    void HpUpdate()
    {
        
        playerConfig.HpTx.text = ((int)Health).ToString();
        playerConfig.HpShopTx.text = ((int)Health).ToString();
        playerConfig.DamageShopTx.text = Damage.ToString();
    }

    void DamageDisplay()
    {

    }

    void TimerBulletTime()
    {
        float curr = 10;
        curr -= 1 * Time.time;
        print(curr);
        if (curr < 3f)
        { 
            Time.timeScale = 0.3f;
        }
        if (curr < 3f && curr<0)
        {
            Time.timeScale = 1f;
        }
    }

    #endregion

    void Update()
    {
        
        HP();
        TagsDetection();
        Distance();
        bulletCount();
        HpUpdate();
        LevelUp();
        Price();
        cashAmnt();
        MeleeDetected();
        FireArmDetected();
        LoadUpgrade();
        // PlayerPrefs.DeleteAll();

    }
}
