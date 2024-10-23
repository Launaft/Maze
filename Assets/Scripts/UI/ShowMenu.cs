using UnityEngine;

public class ShowMenu : MonoBehaviour
{
    public bool cursorLock = false;
    public bool canvasShow = true;

    [SerializeField] private GameObject canvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = !cursorLock;
            canvasShow = !canvasShow;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (canvasShow)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
