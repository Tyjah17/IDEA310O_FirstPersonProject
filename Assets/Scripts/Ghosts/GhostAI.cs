using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour {
    [Header("References")]
    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Behavior")]
    public float rotationSpeed = 3f;

    [Header("Watch Disappear")]
    public float minWatchTime = 1f;
    public float maxWatchTime = 3f;

    private float currentWatchLimit;
    private float watchTimer = 0f;
    private bool isBeingWatched = false;

    private GhostVision vision;
    private GhostTeleport teleporter;

    void Awake() {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponent<Animator>();

        vision = GetComponent<GhostVision>();
        teleporter = GetComponent<GhostTeleport>();
    }

    void Start() {
        SetNewWatchTime();

        if (vision != null && vision.lookTarget == null) {
            vision.lookTarget = transform;
        }

        if (teleporter != null) {
            if (teleporter.agent == null)
                teleporter.agent = agent;
            if (teleporter.player == null)
                teleporter.player = player;
        }
    }

    void Update() {
        if (player == null || agent == null || vision == null || teleporter == null)
            return;

        isBeingWatched = vision.CanPlayerSeeGhost();
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (isBeingWatched) {
            RotateTowardPlayer();

            watchTimer += Time.deltaTime;
            if (watchTimer >= currentWatchLimit) {
                teleporter.TeleportToRandomSpawn();
                watchTimer = 0f;
                SetNewWatchTime();
                return;
            }
        } else {
            watchTimer = 0f;
        }
        UpdateAnimation();
    }

    void SetNewWatchTime() {
        currentWatchLimit = Random.Range(minWatchTime, maxWatchTime);
    }


    void RotateTowardPlayer() {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    public void StunGhost() {
        if (teleporter == null)
            return;
        teleporter.TeleportToRandomSpawn();
        watchTimer = 0f;
        SetNewWatchTime();
    }

    public void StunGhost(float duration) {
        if (teleporter == null)
            return;
        teleporter.TeleportToRandomSpawn();
        watchTimer = 0f;
        SetNewWatchTime();
    }

    void UpdateAnimation() {
        if (animator == null)
            return;
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isIdle", !isMoving);
    }
}