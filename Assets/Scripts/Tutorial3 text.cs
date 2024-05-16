using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial3text : MonoBehaviour
{
    public GameObject object1; 
    public GameObject object2; 
    public GameObject[] squares;

    void Start()
    {
        StartCoroutine(ShowObjects());
        StartCoroutine(ShowSquares());
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

     IEnumerator ShowSquares()
    {
        foreach (GameObject square in squares)
        {
            square.SetActive(false);
        }

        yield return new WaitForSeconds(5);

       
        foreach (GameObject square in squares)
        {
            square.SetActive(true);
        }


        yield return new WaitForSeconds(5);


        foreach (GameObject square in squares)
        {
            square.SetActive(false);
        }
    }
}