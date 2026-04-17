using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour {
    [Header("References")]
    public Transform player;
    public Camera playerCamera;
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Sight Check")]
    public float maxSightDistance = 30f;
    public LayerMask visibilityMask = ~0;

    [Header("Target Point On Ghost")]
    public Transform lookTarget;

    [Header("Stun")]
    public float defaultStunDuration = 3f;

    [Header("Attack")]
    public float attackDistance = 2f;
    public float damage = 1f;
    public float attackCooldown = 1.5f;

    private bool isBeingWatched;
    private bool isStunned = false;
    private float stunTimer = 0f;
    private float attackTimer = 0f;

    void Start() {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (lookTarget == null)
            lookTarget = transform;
    }

    void Update() {
        if (player == null || playerCamera == null || agent == null)
            return;

        if (isStunned) {
            stunTimer -= Time.deltaTime;
            agent.isStopped = true;

            if (stunTimer <= 0f)
            {
                isStunned = false;
                stunTimer = 0f;
            }

            UpdateAnimation();
            return;
        }

        isBeingWatched = CanPlayerSeeGhost();

        if (isBeingWatched) {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }

        // attack player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackDistance) {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f) {
                if (PlayerStats.Instance != null)
                    PlayerStats.Instance.TakeDamage(damage);

                attackTimer = attackCooldown;
            }
        } else {
            attackTimer = 0f;
        }

        UpdateAnimation();
    }

    public void StunGhost() {
        StunGhost(defaultStunDuration);
    }

    public void StunGhost(float duration) {
        if (duration > stunTimer)
            stunTimer = duration;

        isStunned = true;
        agent.isStopped = true;
    }

    bool CanPlayerSeeGhost() {
        Vector3 targetPos = lookTarget.position;
        Vector3 viewportPoint = playerCamera.WorldToViewportPoint(targetPos);

        if (viewportPoint.z <= 0f)
            return false;

        if (viewportPoint.x < 0f || viewportPoint.x > 1f || viewportPoint.y < 0f || viewportPoint.y > 1f)
            return false;

        Vector3 dir = targetPos - playerCamera.transform.position;
        float distance = dir.magnitude;

        if (distance > maxSightDistance)
            return false;

        Ray ray = new Ray(playerCamera.transform.position, dir.normalized);

        if (Physics.Raycast(ray, out RaycastHit hit, distance, visibilityMask)) {
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
                return true;
        }

        return false;
    }

    void UpdateAnimation() {
        if (animator == null)
            return;

        bool isMoving = !agent.isStopped && agent.velocity.magnitude > 0.1f;

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isIdle", !isMoving);
    }
}