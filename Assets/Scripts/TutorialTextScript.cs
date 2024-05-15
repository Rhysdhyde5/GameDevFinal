using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialTextScript : MonoBehaviour
{
    public GameObject object1; 
    public GameObject object2; 

    void Start()
    {
        StartCoroutine(ShowObjects());
    }

    IEnumerator ShowObjects()
    {
        object1.SetActive(true);
        object2.SetActive(false);
        yield return new WaitForSeconds(5);
        
        object1.SetActive(false);
        object2.SetActive(true);
        yield return new WaitForSeconds(5);
        
        object2.SetActive(false);
    }
}