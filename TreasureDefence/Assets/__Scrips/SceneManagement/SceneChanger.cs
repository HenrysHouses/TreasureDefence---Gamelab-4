using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void loadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

       public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
