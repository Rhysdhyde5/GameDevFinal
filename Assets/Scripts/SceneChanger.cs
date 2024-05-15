using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextSceneName = "Tutorial2"; // Change this to "Tutorial2" in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming your frog has the "Player" tag
        {
            SceneManager.LoadScene(nextSceneName); // Load the next scene
        }
    }
}