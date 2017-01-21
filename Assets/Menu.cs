using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void close()
    {
        Application.Quit();
    }

}
