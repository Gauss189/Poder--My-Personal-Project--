using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_RUNNING = "Run Forward";
    private const string IS_ATTACKING = "IAttack";

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, playerController.IsRunning());
        animator.SetBool(IS_ATTACKING, player.IsAttacking());
    }
}
