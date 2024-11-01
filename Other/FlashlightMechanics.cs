using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightMechanics : MonoBehaviour
{

    public bool isOn = false;
    public GameObject lightSource;
    public AudioSource clickSound;
    public Animator lighterAnim;
    public GameObject lightFlame;
    public bool failSafe = false;

    void Start()
    {
        lighterAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FKey"))
        {
            if (isOn == false && failSafe == false)
            {
                failSafe = true;
                lightSource.SetActive(true);
                lightFlame.SetActive(true);
                clickSound.Play();
                isOn = true;
                lighterAnim.Play("LighterOpen", 0, 0);
                StartCoroutine(FailSafe());
            }
            if (isOn == true && failSafe == false)
            {
                failSafe = true;
                lightSource.SetActive(false);
                lightFlame.SetActive(false);
                clickSound.Play();
                isOn = false;
                lighterAnim.Play("LighterClose", 0, 0);
                StartCoroutine(FailSafe());
            }
        }
    }

    IEnumerator FailSafe()
    {
        yield return new WaitForSeconds(0.25f);
        failSafe = false;
    }
}
