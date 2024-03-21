using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSimulator : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
