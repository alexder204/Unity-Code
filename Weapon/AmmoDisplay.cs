using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{

    public GameObject ammoTextUI;
    public int bulletCount;

    // Update is called once per frame
    void Update()
    {
        ammoTextUI.GetComponent<Text>().text = "AMMO: " + bulletCount;
    }
}
