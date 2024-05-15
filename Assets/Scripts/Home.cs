using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Home : MonoBehaviour
{
    public GameObject frog;
    private BoxCollider2D boxCollider;
    [SerializeField] int homeNum;

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
            FindObjectOfType<GameManager>().HomeOccupied(homeNum);

        
        }
    }

}