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
        public static DialogueManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public event Action OnShowDialogue;
        public event Action OnHideDialogue;

        public void HandleUpdate()
        {
        }

        public void ShowDialogue(Dialogue dialogue)
        {
            OnShowDialogue?.Invoke();

            dialogueBox.SetActive(true);
            StartCoroutine(TypeDialogue(dialogue.Lines[0]));
        }

        private IEnumerator TypeDialogue(string line)
        {
            dialogueText.text = "";
            foreach (var letter in line.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }
        }
    }
}