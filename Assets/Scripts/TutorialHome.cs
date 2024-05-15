using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class TutorialHome : MonoBehaviour
{
    public GameObject frog;
    private GameObject Frogger;
    private BoxCollider2D boxCollider;
    public GameObject object1;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        frog.SetActive(true);
        boxCollider.enabled = false;
    }

    private void OnDisable()
    {
        frog.SetActive(false);
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled && other.gameObject.CompareTag("Player"))
        {
            enabled = true;

            GameObject frogger = GameObject.Find("Frogger");
            if (frogger != null)
            {
                frogger.SetActive(false);
            }

            HomeOccupiedTutorial();

        
        }
    }

    public void HomeOccupiedTutorial()
    {
        frog.gameObject.SetActive(true);
        object1.SetActive(true);
        StartCoroutine(ReturnToMainMenuAfterDelay(5f));
    }

    private IEnumerator ReturnToMainMenuAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        SceneManager.LoadScene("MainMenu");
    }

}