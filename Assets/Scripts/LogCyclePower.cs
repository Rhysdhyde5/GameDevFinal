using UnityEngine;

public class LogCyclePower : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float speed = 1f;
    public int size = 1;

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private float originalSpeed;
    private bool isSlowed = false;

    public float spawnChance;
    public GameObject powerupPrefab;


    public GameObject log;

   
    public delegate void CycleEvent();


    public event CycleEvent OnCycle;

    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        originalSpeed = speed;
    }

    private void Update()
    {

        if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
        {
            transform.position = new Vector3(leftEdge.x - size, transform.position.y, transform.position.z);
            Cycle(); 
        }

        else if (direction.x < 0 && (transform.position.x + size) < leftEdge.x)
        {
            transform.position = new Vector3(rightEdge.x + size, transform.position.y, transform.position.z);
            Cycle(); 
        }

        else
        {

            if (isSlowed)
            {
                transform.Translate(speed * 0.5f * Time.deltaTime * direction);
            } 
            else
            {
                transform.Translate(speed * Time.deltaTime * direction);
            }
        }
    }



    public void SlowDown(float slowFactor)
    {
        isSlowed = true;
        speed *= slowFactor;
    }


    public void RestoreSpeed()
    {
        isSlowed = false;
        speed = originalSpeed;
    }


    public void StopMoving()
    {
        speed = 0f;
    }


    public void ResumeMoving()
    {
        speed = originalSpeed;
    }


        private void Cycle()
    {

        OnCycle?.Invoke();


        float randomValue = Random.value;


        if (randomValue <= spawnChance && powerupPrefab != null && log != null)
        {
            GameObject powerup = Instantiate(powerupPrefab, log.transform.position, Quaternion.identity);
            powerup.transform.SetParent(log.transform);
        }
    }
}
