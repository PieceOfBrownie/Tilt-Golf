using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CourseButton(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LeaveButton()
    {
        Application.Quit();
    }
}
