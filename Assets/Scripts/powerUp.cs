using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Slow,
        Stop
    }

    public PowerUpType powerUpType;
    public float effectDuration = 5f;
    public float slowFactor = 0.5f;
    private GameObject[] objectsToAffectsTag;
    public GameObject specialLog1;
    public GameObject specialLog2;

    private bool isActivated = false;

    private void Start(){
        objectsToAffectsTag = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            
            ActivatePowerUp();
        }
    }

    private void ActivatePowerUp()
    {
        isActivated = true;
        StartCoroutine(TriggerPowerUpEffect());
    }

    private IEnumerator TriggerPowerUpEffect()
    {
        switch (powerUpType)
        {
            case PowerUpType.Slow:
                foreach (GameObject obj in objectsToAffectsTag)
                {
                    if (obj != null && obj.activeSelf)
                    {
                        obj.GetComponent<MoveCycle>().SlowDown(slowFactor);
                    }
                }
                specialLog1.GetComponent<LogCyclePower>().SlowDown(slowFactor);
                specialLog2.GetComponent<LogCyclePower>().SlowDown(slowFactor);
                    
                break;
            case PowerUpType.Stop:
                foreach (GameObject obj in objectsToAffectsTag)
                {
                    if (obj != null && obj.activeSelf)
                    {
                        obj.GetComponent<MoveCycle>().StopMoving();
                    }
                }
                specialLog1.GetComponent<LogCyclePower>().StopMoving();
                specialLog2.GetComponent<LogCyclePower>().StopMoving();
                break;
        }

        yield return new WaitForSeconds(effectDuration);

        switch (powerUpType)
        {
            case PowerUpType.Slow:
                foreach (GameObject obj in objectsToAffectsTag)
                {
                    if (obj != null && obj.activeSelf)
                    {
                        obj.GetComponent<MoveCycle>().RestoreSpeed();
                    }
                }
                specialLog1.GetComponent<LogCyclePower>().RestoreSpeed();
                specialLog2.GetComponent<LogCyclePower>().RestoreSpeed();
                break;
            case PowerUpType.Stop:
                foreach (GameObject obj in objectsToAffectsTag)
                {
                    if (obj != null && obj.activeSelf)
                    {
                        obj.GetComponent<MoveCycle>().ResumeMoving();
                    }
                }
                specialLog1.GetComponent<LogCyclePower>().ResumeMoving();
                specialLog2.GetComponent<LogCyclePower>().ResumeMoving();
                break;
        }

        gameObject.SetActive(false);
    }
}