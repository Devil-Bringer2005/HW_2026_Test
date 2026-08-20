using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;

    public void Initialize(float speed)
    {
        moveSpeed = speed;

        Debug.Log("Player speed from JSON: " + moveSpeed);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        Vector3 direction = Vector3.zero;

        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            direction.z += 1f;

        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            direction.z -= 1f;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            direction.x -= 1f;

        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            direction.x += 1f;

        direction.Normalize();

        transform.position +=
            direction * moveSpeed * Time.deltaTime;
    }
}