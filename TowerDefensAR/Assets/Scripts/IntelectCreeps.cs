using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IntelectCreeps : MonoBehaviour
{
    public float speed;
    public float Speed { get => speed; set { speed = value; behavior.speed = value; } }
    public float range;
    public float maxHealthPoint;

    public float AttackSpeed;

    public float currentHealthPoint;



    public bool IsAlive = true; // Indicator if the enemy is alive  @Serega

    public float attackDamage = 10;
    TowerScript Tawer;

    [SerializeField]
    private GameObject SelectionIndicator;
    //private selectplane selectplane; // ����� ��� ���� ����� ���������� ������� �����
    private Animator enemyAnimator;
    private NavMeshAgent behavior;
    float time, timerAction;
    public static Vector3 post;
    // Start is called before the first frame update
    public GameObject pl;
    [SerializeField]
    private float attackRange;

    public void GetDamage(float damage)
    {
        currentHealthPoint -= damage;

        if (currentHealthPoint <= 0 && IsAlive)
        {
            IsAlive = false;

            //switch (gameObject.name) //adding points
            //{
            //    case "Simple Skeleton(Clone)": FindObjectOfType<score>().IncreaseScore(0); break;
            //    case "Heavy Skeleton(Clone)": FindObjectOfType<score>().IncreaseScore(1); break;
                
            //} Vragam na Zlo))))
            if(gameObject.name == "Simple Skeleton(Clone)")
            {
                FindObjectOfType<score>().IncreaseScore(0); 
            }
            else if(gameObject.name == "Heavy Skeleton(Clone)")
            {
                FindObjectOfType<score>().IncreaseScore(1);
            }
            else if(gameObject.name == "AceLich(Clone)")
            {
                FindObjectOfType<score>().IncreaseScore(2);
            }
            else if(gameObject.name == "DartLich(Clone)")
            {
                FindObjectOfType<score>().IncreaseScore(3);
            }
            else if(gameObject.name == "LowLich(Clone)")
            {
                FindObjectOfType<score>().IncreaseScore(4);
            }
            else if(gameObject.name == "Dragon(Clone)")
            {
                FindObjectOfType<score>().IncreaseScore(5);
            }
            FindObjectOfType<Spawnr>().SpawnedEnemies.Remove(gameObject);
            enemyAnimator.SetTrigger("Death(Clone)");
            behavior.isStopped = true;
            Destroy(gameObject, 5);
        }


    }


    public void Select()
    {
        SelectionIndicator.SetActive(true);
    }
    public void Deselect()
    {
        SelectionIndicator.SetActive(false);
    }
    // Update is called once per frame

    void Start()
    {
        if (selectplane.plate == true)
        {
            Tawer = GameObject.FindGameObjectWithTag("Tawer").GetComponent<TowerScript>();
            if (Tawer == null)
            {
                Destroy(this);
            }
            else { }
            enemyAnimator = GetComponent<Animator>();
            currentHealthPoint = maxHealthPoint;
            MoveCreep();
        }


        enemyAnimator.SetFloat("Attack Speed", AttackSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectplane.plate == true)
        {
            post = Tawer.transform.position;
            timerAction += Time.deltaTime;
            time += Time.deltaTime;
            behavior.SetDestination(post);

            if (IsAlive)
            {
                if (Vector3.Distance(post, transform.position) <= behavior.stoppingDistance)//���� �� ����������� �� ��������
                {
                    enemyAnimator.SetBool("Attack", true);
                }
                else
                {
                    enemyAnimator.SetBool("Attack", false);
                    behavior.SetDestination(Tawer.transform.position);
                }

                if (behavior.isOnNavMesh == false)
                {
                    Destroy(this);
                }
            }
        }
    }

    void MoveCreep()
    {
        behavior = GetComponent<NavMeshAgent>();
        behavior.SetDestination(post);
        enemyAnimator.Play("Run");
        behavior.speed = speed;
        behavior.stoppingDistance = attackRange;
    }

    void Attack()
    {
        Tawer.DealDamage(attackDamage);
    }


}
