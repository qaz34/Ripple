using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{

    public void StartLevel()
    {
        SceneManager.LoadScene("First Level");
    }
    public void close()
    {
        Application.Quit();
    }

}
