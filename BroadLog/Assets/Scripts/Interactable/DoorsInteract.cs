using UnityEngine;

namespace Interactable
{
    public class DoorsInteract : MonoBehaviour, IInteractable
    {
        private static readonly int IsOpen = Animator.StringToHash("isOpen");
        [SerializeField] private Transform player;
        [SerializeField] private bool moveUp;
        private Animator _animator;
        private BoxCollider2D _bColl;

        private SpriteRenderer _sr;

        private void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            _bColl = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 distance = player.position - transform.position;
            if (!(distance.magnitude > 3f)) return;

            _animator.SetBool(IsOpen, false);
            _bColl.enabled = true;
        }

        public void ShowMe()
        {
            _sr.color = Color.green;
        }

        public void HideMe()
        {
            _sr.color = Color.white;
        }

        public void Interact()
        {
            _animator.SetBool(IsOpen, true);
        }

        private void OpenDoor()
        {
            _bColl.enabled = false;
        }

        private void CloseDoor()
        {
            _bColl.enabled = true;
        }
    }
}