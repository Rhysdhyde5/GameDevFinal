using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveUp : MonoBehaviour
{
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddLife();
            }

            gameObject.SetActive(false);
        }
    }
}
