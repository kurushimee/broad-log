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
        [SerializeField] private LayerMask solidObjectsLayer;

        private Animator animator;

        private bool isMoving;
        private Vector2 moveInput;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void HandleUpdate()
        {
            if (!isMoving)
            {
                moveInput.x = Input.GetAxisRaw("Horizontal");
                moveInput.y = Input.GetAxisRaw("Vertical");

                if (!diagonalMovement && moveInput.x != 0) moveInput.y = 0;

                if (moveInput != Vector2.zero)
                {
                    animator.SetFloat(MoveX, moveInput.x);
                    animator.SetFloat(MoveY, moveInput.y);

                    var targetPos = transform.position;
                    targetPos.x += moveInput.x;
                    targetPos.y += moveInput.y;

                    if (IsWalkable(targetPos))
                        StartCoroutine(Move(targetPos));
                }
            }

            animator.SetBool(IsMoving, isMoving);
        }

        private bool IsWalkable(Vector3 targetPos)
        {
            return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) is null;
        }

        private IEnumerator Move(Vector3 targetPos)
        {
            isMoving = true;

            while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPos;

            isMoving = false;
        }
    }
}