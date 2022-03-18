using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void loadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
