using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;

    [Range(1f, 100f)]
    public float playerSpeed = 2.0f;

    void Start() => controller = GetComponent<CharacterController>();

    private void playerMove()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move.Normalize();
        controller.Move(transform.TransformDirection(move) * Time.deltaTime * playerSpeed);
    }

    void Update() => playerMove();
}
