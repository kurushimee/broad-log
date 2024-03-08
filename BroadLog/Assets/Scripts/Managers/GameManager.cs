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

        private GameState _state;

        private void Start()
        {
            _state = GameState.Gameplay;

            DialogueManager.Instance.OnShowDialogue += () => _state = GameState.Dialogue;
            DialogueManager.Instance.OnHideDialogue += () => _state = GameState.Gameplay;
        }

        private void Update()
        {
            switch (_state)
            {
                case GameState.Gameplay:
                    player.HandleUpdate();
                    break;
                case GameState.Dialogue:
                    DialogueManager.Instance.HandleUpdate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}