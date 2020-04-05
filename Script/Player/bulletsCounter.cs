using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class bulletsCounter : MonoBehaviour
{
    private GameObject bullets;

    void Awake()
    {
        bullets = GameObject.FindGameObjectWithTag("bulletCount");
    }

    void Update()
    {
        print(bullets);
        //(Assets.HeroEditor.Common.CharacterScripts.Firearm.manage.Params.MagazineCapacity - Assets.HeroEditor.Common.CharacterScripts.Firearm.manage.AmmoShooted);
        bullets.GetComponent<Text>().text = (Assets.HeroEditor.Common.CharacterScripts.Firearm.manage.Params.MagazineCapacity - Assets.HeroEditor.Common.CharacterScripts.Firearm.manage.AmmoShooted).ToString();
    }
}
