using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject castPoint;
    public int hitCount;
    public float damage;
    public bool isDead = false;
    public bool canPlayerAttack = true;
    public bool playerAttacking = false;
    public bool bossEnemy;

    LayerMask mask;
    Vector3 currentPoint = Vector3.zero;
    float pointDistance;
    float playerDistance;
    bool playerNear = false;
    bool dontAttackPlayer = false;

    NavMeshAgent agent;

    void Start()
    {
        mask = 1 << 14;
        currentPoint = pointA.transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(2f);
        dontAttackPlayer = false;
    }
    void Update()
    {
        if (!bossEnemy)
        {
            NormalEnemies();
        }
        else if (bossEnemy)
        {
            Boss();
        }
    }

    void NormalEnemies()
    {
        if (!playerAttacking)
        {
            agent.isStopped = false;
            playerDistance = Vector3.Distance(player.transform.position, transform.position);
            pointDistance = Vector3.Distance(transform.position, currentPoint);
            agent.SetDestination(currentPoint);

            //Enemy Ping pong between two position control center
            if (pointDistance <= 1 && !playerNear)
            {
                if (currentPoint == pointA.transform.position)
                {
                    currentPoint = new Vector3(pointB.transform.position.x, pointB.transform.position.y, pointB.transform.position.z);
                }
                else if (currentPoint == pointB.transform.position)
                {
                    currentPoint = new Vector3(pointA.transform.position.x, pointA.transform.position.y, pointA.transform.position.z);
                }
            }
            Debug.Log(playerDistance);
            //Player detection center
            Ray ray = new Ray(castPoint.transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && !dontAttackPlayer)
            {
                Debug.Log(playerDistance);
                //Preventing player from attacking enemy
                if (hit.transform.gameObject.tag == "Player")
                {
                    canPlayerAttack = false;
                }
                if (playerDistance < 4 && playerDistance > 2)
                {
                    playerNear = true;
                    agent.SetDestination(player.transform.position);
                }
                else if (playerDistance <= 2 && hit.transform.gameObject.tag == "Player")
                {
                    playerNear = true;
                    hit.collider.gameObject.GetComponent<PlayerBehaviour>().health -= damage;
                    StartCoroutine(hit.collider.gameObject.GetComponent<PlayerBehaviour>().PinkyHit());
                    Debug.Log(hit.collider.gameObject.GetComponent<PlayerBehaviour>().health);
                    dontAttackPlayer = true;
                    StartCoroutine(WaitForNextAttack());
                }
            }
            else
            {
                //Enabling player to attack enemy
                canPlayerAttack = true;
                playerNear = false;
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void Boss()
    {
        if (!playerAttacking)
        {
            agent.isStopped = false;
            playerDistance = Vector3.Distance(player.transform.position, transform.position);
            pointDistance = Vector3.Distance(transform.position, currentPoint);
            agent.SetDestination(currentPoint);

            //Enemy Ping pong between two position control center
            if (pointDistance <= 1 && !playerNear)
            {
                if (currentPoint == pointA.transform.position)
                {
                    currentPoint = new Vector3(pointB.transform.position.x, pointB.transform.position.y, pointB.transform.position.z);
                }
                else if (currentPoint == pointB.transform.position)
                {
                    currentPoint = new Vector3(pointA.transform.position.x, pointA.transform.position.y, pointA.transform.position.z);
                }
            }
            
            //Player detection center
            Ray ray = new Ray(castPoint.transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f) && !dontAttackPlayer)
            {
                //Preventing player from attacking enemy
                if (hit.transform.gameObject.tag == "Player")
                {
                    canPlayerAttack = false;
                }
                if (playerDistance < 15 && playerDistance > 8)
                {
                    playerNear = true;
                    agent.SetDestination(player.transform.position);
                }
                else if (playerDistance <= 8 && hit.transform.gameObject.tag == "Player")
                {
                    playerNear = true;
                    hit.collider.gameObject.GetComponent<PlayerBehaviour>().health -= damage;
                    StartCoroutine(hit.collider.gameObject.GetComponent<PlayerBehaviour>().PinkyHit());
                    Debug.Log(hit.collider.gameObject.GetComponent<PlayerBehaviour>().health);
                    dontAttackPlayer = true;
                    StartCoroutine(WaitForNextAttack());
                }
            }
            else
            {
                //Enabling player to attack enemy
                canPlayerAttack = true;
                playerNear = false;
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
