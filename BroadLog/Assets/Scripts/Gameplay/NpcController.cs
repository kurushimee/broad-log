using Interactable;
using Managers;
using UI;
using UnityEngine;

namespace Gameplay
{
    public class NpcController : MonoBehaviour, IInteractable
    {
        [SerializeField] private Dialogue dialogue;

        public void Interact()
        {
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
        }
    }
}