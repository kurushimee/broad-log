using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("moveX");
        private static readonly int MoveY = Animator.StringToHash("moveY");
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool diagonalMovement;

        private Animator _animator;
        private bool _isMoving;
        private Vector2 _moveInput;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!_isMoving)
            {
                _moveInput.x = Input.GetAxisRaw("Horizontal");
                _moveInput.y = Input.GetAxisRaw("Vertical");

                if (!diagonalMovement && _moveInput.x != 0) _moveInput.y = 0;

                if (_moveInput != Vector2.zero)
                {
                    _animator.SetFloat(MoveX, _moveInput.x);
                    _animator.SetFloat(MoveY, _moveInput.y);

                    var targetPos = transform.position;
                    targetPos.x += _moveInput.x;
                    targetPos.y += _moveInput.y;

                    StartCoroutine(Move(targetPos));
                }
            }

            _animator.SetBool(IsMoving, _isMoving);
        }

        private IEnumerator Move(Vector3 targetPos)
        {
            _isMoving = true;

            while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPos;

            _isMoving = false;
        }
    }
}