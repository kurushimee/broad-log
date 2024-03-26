using System;
using Gameplay;
using UnityEngine;

namespace Managers
{
    public enum GameState
    {
        Gameplay,
        Dialogue
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController player;

        private GameState state;

        private void Start()
        {
            state = GameState.Gameplay;

            DialogueManager.Instance.OnShowDialogue += () => state = GameState.Dialogue;
            DialogueManager.Instance.OnHideDialogue += () => state = GameState.Gameplay;
        }

        private void Update()
        {
            switch (state)
            {
                case GameState.Gameplay:
                    player.HandleUpdate();
                    break;
                case GameState.Dialogue:
                    DialogueManager.Instance.currentLine = 0;
                    DialogueManager.Instance.HandleUpdate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}