using UnityEngine;

public class Enemy_Saw : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float movementDistance;

    public Collider2D cd;
    private bool moveingUp;
    private float upEdge;
    private float downEdge;

    private void Awake()
    {

        upEdge = cd.transform.position.x - movementDistance;
        downEdge = cd.transform.position.x + movementDistance;
    }
    private void Update()
    {
        if (moveingUp)
        {
            if (transform.position.x > upEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                moveingUp = false;
        }
        else
            if (transform.position.x < downEdge)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
            moveingUp = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent().TakeDamage(damage);
        }
    }
}
