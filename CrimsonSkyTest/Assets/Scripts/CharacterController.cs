using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Input input;

    public float movementSpeed;

    private Coroutine coMoveToClick;

    private void OnEnable()
    {
        Input.OnClickToMove += InitiateClickToMove;
    }

    private void OnDisable()
    {
        Input.OnClickToMove -= InitiateClickToMove;
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;

        if (input.direction != Vector2.zero)
        {
            Move(new Vector3(transform.position.x + input.direction.x, transform.position.y, transform.position.z + input.direction.y));
        }
    }

    // Start movement coroutine & ensure only one is running
    private void InitiateClickToMove(Vector3 target)
    {
        if (coMoveToClick != null)
            StopCoroutine(coMoveToClick);

        coMoveToClick = StartCoroutine(MoveToClick(target));
    }

    // Move at set rate towards target
    private void Move(Vector3 target)
    {
        target.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
    }

    // Move to click position
    IEnumerator MoveToClick(Vector3 target)
    {
        while (transform.position != target)
        {
            if (input.direction != Vector2.zero)
                yield break;

            Move(target);

            yield return null;
        }
    }

}
