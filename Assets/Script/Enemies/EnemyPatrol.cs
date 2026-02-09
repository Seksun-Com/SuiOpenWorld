using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private GameObject enemy;

    [Header("Movements Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    private Animator anim;

    private void Awake()
    {
        initScale = enemy.transform.localScale;
        anim = enemy.GetComponent<Animator>();
    }
    private void OnDisable()
    {
        anim.SetBool("run", false);
    }
    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.transform.position.x >= leftEdge.position.x)MoveInDirection(-1);
            else DirectionChange();
        }
        else
        {
            if(enemy.transform.position.x <= rightEdge.position.x)MoveInDirection(1);
            else DirectionChange();
        }
    }
    private void DirectionChange()
    {
        anim.SetBool("run",false);
        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration) movingLeft = !movingLeft;
    }
    private void MoveInDirection(float _direction)
    {
        idleTimer = 0;
        anim.SetBool("run", true);

        // Make enemy face direction
        enemy.transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,initScale.y,initScale.z);
        
        // Move in that direction
        enemy.transform.position = new Vector3(enemy.transform.position.x + Time.deltaTime * _direction * speed,
            enemy.transform.position.y,enemy.transform.position.z);
    }
}
