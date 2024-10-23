using UnityEngine;
using UnityEngine.Events;

public class CameraSwitch : MonoBehaviour
{
    public static UnityEvent OnMazeGenerated = new UnityEvent();

    public Camera _camera;
    private Camera _playerCamera;

    private bool _firstPerson = false;

    private void Start()
    {
        OnMazeGenerated.AddListener(FindPlayerCamera);
    }

    private void FindPlayerCamera()
    {
        _playerCamera = GameObject.FindGameObjectWithTag("Player Camera").GetComponent<Camera>();
        _camera.enabled = false;
        _playerCamera.enabled = true;
        _firstPerson = true;
    }

    /*private void Update()
    {
        if (_playerCamera != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _firstPerson = !_firstPerson;
                Debug.Log("Switch");
            }

            if (_firstPerson)
            {
                _playerCamera.enabled = true;
                _camera.enabled = false;
            }
            else
            {
                _playerCamera.enabled = false;
                _camera.enabled = true;
            }
        }
    }*/
}
