using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace PlayerDialogue
{
    public class DialogueOnEnter : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogueCanvas;

        [SerializeField]
        private GameObject interactPopUp;

        [SerializeField]
        private GameObject interactItem;

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

        void Update()
        {
            if (Input.GetButtonDown("Interact") && dialogueActived == true && canContinueText == true)
            {
                if (typingRoutine != null)
                {
                    StopCoroutine(typingRoutine);
                }
                if (step + 1 >= speaker.Length)
                {
                    dialogueCanvas.SetActive(false);
                    interactItem.SetActive(false);
                    interactPopUp.SetActive(false);
                    charImage1.SetActive(false);
                    charImage2.SetActive(false);
                    playerMovement.currentSpeed = 2f;
                    playerMovement.moveSpeed = 2f;
                    playerMovement.sprintSpeed = 2f;
                    step = 0;
                }
                else
                {
                    typingRoutine = StartCoroutine(Typing(dialogueText.text = dialogueWords[step + 1]));
                    dialogueCanvas.SetActive(true);
                    interactItem.SetActive(true);
                    charImage1.SetActive(true);
                    charImage2.SetActive(true);
                    playerMovement.currentSpeed = 0f;
                    playerMovement.moveSpeed = 0f;
                    playerMovement.sprintSpeed = 0f;
                    speakerText.text = speaker[step + 1];
                    portraitImage1.sprite = portrait1[step + 1];
                    portraitImage2.sprite = portrait2[step + 1];
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
            if (typingRoutine != null)
            {
                StopCoroutine(typingRoutine);
            }

            if (collision.gameObject.tag == "Player")
            {
                typingRoutine = StartCoroutine(Typing(dialogueText.text = dialogueWords[0]));
                dialogueActived = true;
                dialogueCanvas.SetActive(true);
                interactItem.SetActive(true);
                interactPopUp.SetActive(true);
                playerMovement.currentSpeed = 0f;
                playerMovement.moveSpeed = 0f;
                playerMovement.sprintSpeed = 0f;
                speakerText.text = speaker[0];
                portraitImage1.sprite = portrait1[0];
                portraitImage2.sprite = portrait2[0];
                step = 0;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            dialogueActived = false;
            dialogueCanvas.SetActive(false);
            interactItem.SetActive(false);
            interactPopUp.SetActive(false);
            charImage1.SetActive(false);
            charImage2.SetActive(false);
            playerMovement.currentSpeed = 2f;
            playerMovement.moveSpeed = 2f;
            playerMovement.sprintSpeed = 2f;
        }
    }
}
