using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public Animator anim;
    public Text buttonSmasherDisplay;
    public Text lolliePop;
    public Slider healthSlider;
    public int totalLolliePopCount;
    public float health;
    public float moveSpeed = 5f;
    public float timerSpeed;
    public bool canJump = false;
    public bool OnGround = false;
    public bool bugFight = false;
    public GameObject GameOverPanel;
    public GameObject LevelCompletion4Panel;
    public GameObject LevelCompletion;
    public GameObject attackNotify;
    public GameObject net;
    public Camera midCam;
    public AudioSource jump;
    public AudioSource fight;
    public AudioSource dead;
    public AudioSource bg;
    public AudioSource candy;

    int hitCount;
    int lolliePopCount;
    int count;
    int rand;
    float camFov;
    float timer;
    bool wrongKey = false;
    bool facingRight = true;
    bool isDead = false;
    bool isHit = false;
    bool catchingBug = false;

    LayerMask enemy;
    RaycastHit hit;
    CharacterController cc;

    Vector3 moveDirection;

    void Start()
    {
        net.SetActive(false); 
        lolliePopCount = 0;
        camFov = midCam.fieldOfView;
        enemy = 1 << 15;
        anim.SetBool("isIdle", true);
        timer = timerSpeed;
        rand = Random.Range(0, 4);
        cc = GetComponent<CharacterController>();
    }

    void CheckWhereToFace()
    {
        if (moveDirection.x > 0)
        {
            facingRight = true;
        }
        else if (moveDirection.x < 0)
        {
            facingRight = false;
        }

        if (facingRight)
        {
            transform.localEulerAngles = new Vector3(0, 90, 0);
        }
        else if (!facingRight)
        {
            transform.localEulerAngles = new Vector3(0, -90, 0);
        }
    }

    void AttackController()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, enemy))
        {
            if (hit.collider.tag == "Enemy")
            {
                float dist = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
                if (dist < 4 && hit.collider.gameObject.GetComponent<EnemyBehaviour>().canPlayerAttack && !bugFight)
                {
                    attackNotify.SetActive(true);
                }
                else
                {
                    attackNotify.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.R) && !bugFight && dist < 4)
                {
                    attackNotify.SetActive(false);
                    hitCount = hit.collider.gameObject.GetComponent<EnemyBehaviour>().hitCount;
                    //To check weather Enemy is not facing player and To freezeEnemy
                    if (hit.collider.gameObject.GetComponent<EnemyBehaviour>().canPlayerAttack)
                    {
                        anim.SetTrigger("isAttacking");
                        hit.collider.gameObject.GetComponent<EnemyBehaviour>().playerAttacking = true;
                        bugFight = true;
                        //Setting QTE(Quick Timed Event)
                        timer = timerSpeed;
                    }
                }
            }
            else if(hit.collider.tag=="Boss")
            {
                float dist = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
                if (dist < 9 && hit.collider.gameObject.GetComponent<EnemyBehaviour>().canPlayerAttack && !bugFight)
                {
                    attackNotify.SetActive(true);
                }
                else
                {
                    attackNotify.SetActive(false);
                }
                Debug.Log(dist);
                if (Input.GetKeyDown(KeyCode.R) && !bugFight && dist <= 9)
                {
                    attackNotify.SetActive(false);
                    hitCount = hit.collider.gameObject.GetComponent<EnemyBehaviour>().hitCount;
                    //To check weather Enemy is not facing player and To freezeEnemy
                    if (hit.collider.gameObject.GetComponent<EnemyBehaviour>().canPlayerAttack)
                    {
                        anim.SetTrigger("isAttacking");
                        hit.collider.gameObject.GetComponent<EnemyBehaviour>().playerAttacking = true;
                        bugFight = true;
                        //Setting QTE(Quick Timed Event)
                        timer = timerSpeed;
                    }
                }
            }
        }
        else
        {
            attackNotify.SetActive(false);
        }

        //QTE Processing center 
        if (bugFight && count < hitCount && !wrongKey)
        {
            midCam.fieldOfView = Mathf.Lerp(midCam.fieldOfView, camFov - 10, 2 * Time.deltaTime);
            if (!fight.isPlaying)
            {
                bg.volume = 0.5f;
                fight.Play();
            }
            buttonSmasherDisplay.enabled = true;
            SmashTimer();
            ButtonSmash();
        }
        else if (bugFight && (wrongKey || count == hitCount))
        {
            if (hit.collider != null && (hit.collider.tag == "Enemy" || hit.collider.tag == "Boss"))
            {
                //To disable enemy after a sucessfull QTE
                if (count == hitCount)
                {
                    StartCoroutine(BugCatch());
                }
                //To enable enemy after an unsucessfull QTE
                else if (count != hitCount)
                {
                    StartCoroutine(PinkyHit());
                    health -= hit.collider.gameObject.GetComponent<EnemyBehaviour>().damage;
                    hit.collider.gameObject.GetComponent<EnemyBehaviour>().playerAttacking = false;
                    wrongKey = false;
                }
            }
            bugFight = false;
            count = 0;
        }
    }

    void ButtonSmash()
    {
        if (rand == 0)
        {
            buttonSmasherDisplay.text = "Press W";
        }
        else if (rand == 1)
        {
            buttonSmasherDisplay.text = "Press S";
        }
        else if (rand == 2)
        {
            buttonSmasherDisplay.text = "Press A";
        }
        else if (rand == 3)
        {
            buttonSmasherDisplay.text = "Press D";
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (rand == 0)
            {
                count++;
                timer = timerSpeed;
                Debug.Log("True");
            }
            else
            {
                wrongKey = true;
                Debug.Log("Fail");
            }
            rand = Random.Range(0, 4);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (rand == 1)
            {
                count++;
                timer = timerSpeed;
                Debug.Log("True");
            }
            else
            {
                wrongKey = true;
                Debug.Log("Fail");
            }
            rand = Random.Range(0, 4);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (rand == 2)
            {
                count++;
                timer = timerSpeed;
                Debug.Log("True");
            }
            else
            {
                wrongKey = true;
                Debug.Log("Fail");
            }
            rand = Random.Range(0, 4);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (rand == 3)
            {
                count++;
                timer = timerSpeed;
                Debug.Log("True");
            }
            else
            {
                wrongKey = true;
                Debug.Log("Fail");
            }
            rand = Random.Range(0, 4);
        }
    }

    void SmashTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(PinkyHit());
            health -= hit.collider.gameObject.GetComponent<EnemyBehaviour>().damage;
            hit.collider.gameObject.GetComponent<EnemyBehaviour>().playerAttacking = false;
            bugFight = false;
            timer = 0;
            count = 0;
        }
        buttonSmasherDisplay.color = new Color(1, 1, 1, timer / timerSpeed);
    }

    void Movement()
    {
        if (!bugFight)
        {
            cc.Move(moveDirection * Time.deltaTime);
            buttonSmasherDisplay.enabled = false;
            if (cc.isGrounded)
            {
                anim.SetBool("isFalling", false);
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0) * moveSpeed;
                if (moveDirection.x != 0)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isIdle", false);
                }
                else
                {
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isWalking", false);
                }
            }
            else
            {
                anim.SetBool("isFalling", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);
            }
            moveDirection.y -= (50 * Time.deltaTime);
            /*if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
            {
                jump.Play();
                moveDirection.y = 20;
                anim.SetTrigger("isJump");
            }*/
        }
    }

    void GameManager()
    {
        if (health <= 0)
        {
            StartCoroutine(GameOverMenu());
            isDead = true;
        }
        if (lolliePopCount >= totalLolliePopCount && health > 0)
        {
            Debug.Log("Level Finished");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "LevelEnd")
        {
            if (lolliePopCount >= totalLolliePopCount)
            {
                LevelCompletion.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if(hit.collider.tag =="LevelEnd4")
        {
            LevelCompletion4Panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    IEnumerator GameOverMenu()
    {
        if (!dead.isPlaying)
        {
            bg.Stop();
            dead.Play();
        }

        yield return new WaitForSeconds(3f);
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator BugCatch()
    {
        catchingBug = true;
        net.SetActive(true);
        anim.SetTrigger("isBugCatch");
        yield return new WaitForSeconds(1.5f);
        net.SetActive(false);
        hit.transform.gameObject.SetActive(false);
        hit = new RaycastHit();
        catchingBug = false;
    }

    public IEnumerator PinkyHit()
    {
        isHit = true;
        anim.SetTrigger("isHit");
        yield return new WaitForSeconds(2f);
        isHit = false;
    }

    public void Collectable()
    {
        candy.Play();
        lolliePopCount += 1;
        anim.SetTrigger("isCollect");
    }

    public void JumperLeaf(float speed)
    {
        jump.Play();
        anim.SetTrigger("isJump");
        moveDirection.y = speed;
    }

    void Update()
    {
        if (!isDead)
        {
            if (!isHit && !catchingBug)
            {
                midCam.fieldOfView = Mathf.Lerp(midCam.fieldOfView, camFov, 2 * Time.deltaTime);
                if (fight.isPlaying && !bugFight)
                {
                    bg.volume = 1f;
                    fight.Stop();
                }
                Movement();
                AttackController();
            }
            lolliePop.text = lolliePopCount.ToString() + "/" + totalLolliePopCount.ToString();
            healthSlider.value = health / 100;
            GameManager();
        }
    }


    void LateUpdate()
    {
        CheckWhereToFace();
    }
}
