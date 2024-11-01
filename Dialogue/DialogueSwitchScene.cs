using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace PlayerDialogue
{
    public class DialogueSwitchScene : MonoBehaviour
    {
        //Scene Switch
        [SerializeField] 
        private string newLevel;
        public Animator crossFade;

        [SerializeField]
        private GameObject dialogueCanvas;

        [SerializeField]
        private GameObject interactItem;

        [SerializeField]
        private GameObject interactPopUp;

        [SerializeField]
        private GameObject charImage1;

        [SerializeField]
        private GameObject charImage2;

        [SerializeField]
        private TextMeshProUGUI speakerText;

        [SerializeField]
        private TextMeshProUGUI dialogueText;

        [SerializeField]
        private Image portraitImage1;

        [SerializeField]
        private Image portraitImage2;

        [SerializeField]
        private string[] speaker;

        [SerializeField]
        private Sprite[] portrait1;

        [SerializeField]
        private Sprite[] portrait2;

        [SerializeField]
        [TextArea(3, 10)]
        private string[] dialogueWords;

        private bool dialogueActived;
        private int step;

        [SerializeField]
        private float typingSpeed = 0.02f;
        private Coroutine typingRoutine;
        private bool canContinueText = true;

        private TopDownMovement playerMovement;

        void Start()
        {
            playerMovement = GameObject.Find("Player").GetComponent<TopDownMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Interact") && dialogueActived == true && canContinueText == true)
            {
                if (typingRoutine != null)
                {
                    StopCoroutine(typingRoutine);
                }
                if (step >= speaker.Length)
                {
                    dialogueCanvas.SetActive(false);
                    interactPopUp.SetActive(false);
                    charImage1.SetActive(false);
                    charImage2.SetActive(false);
                    playerMovement.currentSpeed = 2f;
                    playerMovement.moveSpeed = 2f;
                    playerMovement.sprintSpeed = 2f;
                    step = 0;
                    StartCoroutine(LoadNextScene());
                }
                else
                {
                    typingRoutine = StartCoroutine(Typing(dialogueText.text = dialogueWords[step]));
                    dialogueCanvas.SetActive(true);
                    interactPopUp.SetActive(true);
                    charImage1.SetActive(true);
                    charImage2.SetActive(true);
                    playerMovement.currentSpeed = 0f;
                    playerMovement.moveSpeed = 0f;
                    playerMovement.sprintSpeed = 0f;
                    speakerText.text = speaker[step];
                    portraitImage1.sprite = portrait1[step];
                    portraitImage2.sprite = portrait2[step];
                    step++;
                }
            }
        }

        private IEnumerator Typing(string line)
        {
            dialogueText.text = "";
            canContinueText = false;
            bool addingRichTextTag = false;
            yield return new WaitForSeconds(.5f);
            foreach (char letter in line.ToCharArray())
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogueText.text = line;
                    break;
                }

                if (letter == '<' || addingRichTextTag)
                {
                    addingRichTextTag = true;
                    dialogueText.text += letter;
                    if (letter == '>')
                    {
                        addingRichTextTag = false;
                    }
                }
                else
                {
                    dialogueText.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
            }
            canContinueText = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                dialogueActived = true;
                interactPopUp.SetActive(true);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            dialogueActived = false;
            interactPopUp.SetActive(false);
            dialogueCanvas.SetActive(false);
        }

        private IEnumerator LoadNextScene()
        {
            crossFade.SetTrigger("Start");
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(newLevel);
            interactItem.SetActive(false);
        }
    }
}
