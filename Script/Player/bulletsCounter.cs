using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class bulletsCounter : MonoBehaviour
{
    
    public Text bulletsCount;

    void Update()
    {

        bulletsCount.text = (Assets.HeroEditor.Common.CharacterScripts.Firearm.manage.Params.MagazineCapacity - Assets.HeroEditor.Common.CharacterScripts.Firearm.manage.AmmoShooted).ToString();
    }
}
