using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Transform player;
    public Transform eyes;

    [Range(50, 300)]
    public float xSens;
    [Range(50, 300)]
    public float ySens;

    Quaternion center;

    void Start() => center = eyes.localRotation;

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * ySens * Time.deltaTime;
        Quaternion yRotation = eyes.localRotation * Quaternion.AngleAxis(mouseY, -Vector3.right);

        if (Quaternion.Angle(center, yRotation) < 90)
            eyes.localRotation = yRotation;

        float mouseX = Input.GetAxis("Mouse X") * xSens * Time.deltaTime;
        Quaternion xRotation = player.localRotation * Quaternion.AngleAxis(mouseX, Vector3.up);

        player.localRotation = xRotation;
    }
}
