using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2changer : MonoBehaviour
{
    public string nextSceneName = "Tutorial3";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            SceneManager.LoadScene(nextSceneName); 
        }
    }
}