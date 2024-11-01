using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerDialogue
{
    public class MoveScenes2D : MonoBehaviour
    {
        [SerializeField] private string newLevel;

        public Animator crossFade;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
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
}
