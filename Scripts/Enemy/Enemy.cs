using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    public Animator animator;
    public float detectionRadius = 5f; // Bán kính phát hiện
    private bool isChasing = false; // Kiểm tra trạng thái đuổi theo
    public EnemyHealth enemyHealth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();

        // Tìm đối tượng Player theo tag "Player"
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void Update()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= detectionRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing && target != null)
        {
            // Di chuyển đến mục tiêu
            agent.SetDestination(target.position);

            // Lấy vận tốc của agent
            Vector3 velocity = agent.velocity;

            // Cập nhật giá trị "Speed" cho Animator (cho animation đi/chạy)
            animator.SetFloat("Speed", velocity.sqrMagnitude);

            // Quay enemy trái/phải dựa trên hướng di chuyển
            if (velocity.x > 0)
                transform.localScale = new Vector3(-1, 1, 0);
            else if (velocity.x < 0)
                transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    // Đưa các hàm OnTriggerEnter và OnTriggerExit ra ngoài Update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true; // Bắt đầu đuổi theo khi Player vào phạm vi
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false; // Ngừng đuổi theo khi Player rời phạm vi
        }
    }

    public void TakeDame(int damage)
    {
        enemyHealth.addDamage(damage);
    }
}
