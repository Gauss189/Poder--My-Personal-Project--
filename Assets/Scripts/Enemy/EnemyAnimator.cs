using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private const string IS_RUNNING = "Run";

    [SerializeField] private Enemy enemy;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, enemy.IsRunning());
    }
}
