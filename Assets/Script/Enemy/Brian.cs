using UnityEngine;

public class Brian : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private int distanceMove;
    private Vector3 initialPosition;
    private bool movingLeft;
    private Rigidbody2D body;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collide with " + collision.name);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void Start()
    {
        initialPosition = transform.position;
        movingLeft = true;
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (movingLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= initialPosition.x - distanceMove)
            {
                movingLeft = false;
            }
        }
        else
        {

            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= initialPosition.x + distanceMove)
            {
                movingLeft = true;
            }
        }

    }
    private void FixedUpdate()
    {
        float bodyX;
        if (movingLeft)
            bodyX = -1 * Mathf.Abs(body.transform.localScale.x);
        else
            bodyX = Mathf.Abs(body.transform.localScale.x);

        transform.localScale = new Vector3(bodyX, body.transform.localScale.y, body.transform.localScale.z);      
    }
}
