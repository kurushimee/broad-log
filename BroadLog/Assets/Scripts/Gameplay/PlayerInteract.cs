using Interactable;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInteract : MonoBehaviour
    {
        [HideInInspector] public Vector2 sightDirection;

        private IInteractable _lastActive;

        private void Update()
        {
            if (_lastActive == null) return;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.C))
                _lastActive.Interact();
        }

        private void FixedUpdate()
        {
            var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .5f), sightDirection,
                1f);

            if (hit.collider is not null)
            {
                if (!hit.transform.TryGetComponent(out IInteractable activable)) return;
                if (activable == _lastActive) return;

                _lastActive = activable;
                activable.ShowMe();
            }
            else
            {
                if (_lastActive == null) return;

                _lastActive.HideMe();
                _lastActive = null;
            }
        }
    }
}