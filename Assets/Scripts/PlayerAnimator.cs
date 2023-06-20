using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_RUNNING = "Run Forward";

    [SerializeField] private PlayerController player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, player.IsRunning());
    }
}
