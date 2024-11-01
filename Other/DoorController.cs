using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeySystem
{
    public class DoorController : MonoBehaviour
    {
        public Animator doorAnim;

        private bool doorOpen = false;
        public bool doorLocked = true;

        [Header("Animation Names")]
        [SerializeField] private string openAnimation = "DoorOpen";
        [SerializeField] private string closeAnimation = "DoorClose";

        [SerializeField] private int timeToShowUI = 1;
        [SerializeField] private GameObject showDoorLockedUI = null;

        [SerializeField] private KeyInventory _keyInventory = null;

        [SerializeField] private int waitTimer = 1;
        [SerializeField] private bool pauseInteraction = false;

        private void Awake()
        {
            doorAnim = gameObject.GetComponent<Animator>();
        }

        private IEnumerator PauseDoorInteraction()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
        }

        public void PlayLockedAnimation()
        {

            if (_keyInventory.hasRedKey && doorLocked)
            {
                if (!doorOpen && !pauseInteraction)
                {
                    OpenDoor();
                }
                else if (doorOpen && !pauseInteraction)
                {
                    CloseDoor();
                }
            }
            else
            {
                StartCoroutine(ShowDoorLocked());
            }
        }

        public void PlayUnlockedAnimation()
        {
            if (!doorLocked)
            {
                if (!doorOpen && !pauseInteraction)
                {
                    OpenDoor();
                }
                else if (doorOpen && !pauseInteraction)
                {
                    CloseDoor();
                }
            }
        }

        public void OpenDoor()
        {
            doorAnim.Play(openAnimation, 0, 0.0f);
            doorOpen = true;
            StartCoroutine(PauseDoorInteraction());
        }

        public void CloseDoor()
        {
            doorAnim.Play(closeAnimation, 0, 0.0f);
            doorOpen = false;
            StartCoroutine(PauseDoorInteraction());
        }

        IEnumerator ShowDoorLocked()
        {
            showDoorLockedUI.SetActive(true);
            yield return new WaitForSeconds(timeToShowUI);
            showDoorLockedUI.SetActive(false);
        }
    }
}
