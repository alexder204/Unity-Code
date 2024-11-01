using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneTimer : MonoBehaviour
{
    [SerializeField] private string newLevel;
    [SerializeField] private float time = 2f;

    public Animator crossFade;

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(newLevel);
    }
}
