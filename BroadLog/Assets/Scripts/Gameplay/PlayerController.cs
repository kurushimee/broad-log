using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool diagonalMovement;
        private bool _isMoving;
        private Vector2 _moveInput;

        private void Update()
        {
            if (_isMoving) return;
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");

            if (!diagonalMovement && _moveInput.x != 0) _moveInput.y = 0;
            
            if (_moveInput == Vector2.zero) return;
            var targetPos = transform.position;
            targetPos.x += _moveInput.x;
            targetPos.y += _moveInput.y;

            StartCoroutine(Move(targetPos));
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
