using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // To adjust character movement speed in the Inspector window.
    private bool isMoving; // To identify whether the playable character is moving.
    private Vector2 input;
    private Animator animator;
    public LayerMask SolidObjectsLayer;

    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;

    // Fixed movement boundaries
    private readonly Vector2 minBounds = new Vector2(-22, -14);
    private readonly Vector2 maxBounds = new Vector2(22, 14);

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Gets the generic animator component.
    }

    public void Start()
    {
        upButton.onClick.AddListener(() => OnMoveInput(Vector2.up));
        downButton.onClick.AddListener(() => OnMoveInput(Vector2.down));
        leftButton.onClick.AddListener(() => OnMoveInput(Vector2.left));
        rightButton.onClick.AddListener(() => OnMoveInput(Vector2.right));
    }

    public void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal"); // Horizontal movement is stored as input.x.
            input.y = Input.GetAxisRaw("Vertical"); // Vertical movement is stored as input.y.

            if (input.x != 0) input.y = 0; // Prevent diagonal movement.

            if (input != Vector2.zero) // Check for movement input.
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                // Check bounds and walkability before moving
                if (isWithinBounds(targetPos) && isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

    private void OnMoveInput(Vector2 direction)
    {
        if (!isMoving)
        {
            input = direction;
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);

            var targetPos = transform.position;
            targetPos.x += input.x;
            targetPos.y += input.y;

            // Check bounds and walkability before moving
            if (isWithinBounds(targetPos) && isWalkable(targetPos))
            {
                StartCoroutine(Move(targetPos));
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

    private bool isWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, 0.2f, SolidObjectsLayer) == null;
    }

    private bool isWithinBounds(Vector3 targetPos)
    {
        return targetPos.x >= minBounds.x && targetPos.x <= maxBounds.x &&
               targetPos.y >= minBounds.y && targetPos.y <= maxBounds.y;
    }

    IEnumerator Move(Vector3 targetPos)
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
