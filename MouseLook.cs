using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour {
    [Tooltip("How fast the player can look up and down")] public float mouseVerticalSensitivity = 100f;
    [Tooltip("How fast the player can look left and right")] public float mouseHorizontalSensitivity = 100f;
    [Tooltip("Variable used to assist in horizontal rotation")] public Transform playerBody;
    float xRotation = 0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (Keyboard.current.escapeKey.isPressed)
            Cursor.lockState = CursorLockMode.None;

        float mouseX = Mouse.current.delta.ReadValue().x * mouseHorizontalSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.ReadValue().y * mouseVerticalSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
