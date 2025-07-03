using UnityEngine;
using UnityEditor.SceneManagement;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panel;
    
    public void StartGame1() 
    {
        SceneManager.LoadScene("GameScene1");
    }

    public void StartGame2()
    {
        SceneManager.LoadScene("GameScene2");
    }

    public void StartGame3()
    {
        SceneManager.LoadScene("GameScene3");
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
