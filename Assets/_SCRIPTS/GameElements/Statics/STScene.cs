using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class STScene : MonoBehaviour
{
    
    public static void GoToNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void GoTo(NameOfScanes nameOfScanes)
    {
       SceneManager.LoadScene(nameOfScanes.ToString());
    }
    public static void GoTo(int numberOfScene)
    {
        SceneManager.LoadScene(numberOfScene);
    }


}
