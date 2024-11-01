using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string newLevel;

    public Animator crossFade;

    public void NewGameButton()
    {
        StartCoroutine(LoadNextScene());
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    IEnumerator LoadNextScene()
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(newLevel);
    }
}
