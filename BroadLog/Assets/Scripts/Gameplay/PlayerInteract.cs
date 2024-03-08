using UnityEngine;

namespace Gameplay
{
    public class PlayerInteract : MonoBehaviour
    {
        public Vector2 sightDir;
        private IActivable _lastActive;

        private void Update()
        {
            if (_lastActive == null) return;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.C))
                _lastActive.Interact();
        }

        private void FixedUpdate()
        {
            var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .5f), sightDir, 1f);

            if (hit.collider is not null)
            {
                if (!hit.transform.TryGetComponent(out IActivable activable)) return;
                if (activable == _lastActive) return;

                _lastActive = activable;
                activable.ShowMe();
            }
            else
            {
                if (_lastActive == null) return;

                _lastActive.DontShowMe();
                _lastActive = null;
            }
        }
    }
}