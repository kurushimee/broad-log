using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private Text dialogueText;
        [SerializeField] private int lettersPerSecond;

        public int currentLine;
        private Dialogue dialogue;
        private bool isTyping;

        public static DialogueManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public event Action OnShowDialogue;
        public event Action OnHideDialogue;

        public void HandleUpdate()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            if (isTyping)
            {
                StopCoroutine(nameof(TypeDialogue));
                dialogueText.text = dialogue.Lines[currentLine];
                isTyping = false;
            }

            if (++currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                dialogueBox.SetActive(false);
                OnHideDialogue?.Invoke();
            }
        }

        public IEnumerator ShowDialogue(Dialogue newDialogue)
        {
            yield return new WaitForEndOfFrame();
            OnShowDialogue?.Invoke();

            dialogue = newDialogue;
            dialogueBox.SetActive(true);
            StartCoroutine(TypeDialogue(dialogue.Lines[0]));
        }

        private IEnumerator TypeDialogue(string line)
        {
            isTyping = true;
            dialogueText.text = "";
            foreach (var letter in line.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }

            isTyping = false;
        }
    }
}