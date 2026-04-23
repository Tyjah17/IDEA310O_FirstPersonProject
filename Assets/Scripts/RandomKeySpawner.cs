using UnityEngine;

public class KeyRandomSpawner : MonoBehaviour
{
    [Header("Key Object")]
    public GameObject keyPrefab;

    [Header("Normal Spawn Points")]
    public Transform[] normalSpawnPoints;

    [Header("Rare Near-Door Spawn")]
    public Transform nearDoorSpawnPoint;
    [Range(0f, 1f)]
    public float nearDoorChance = 0.01f;

    private void Start()
    {
        Debug.Log("KeyRandomSpawner Start ran on: " + gameObject.name);
        SpawnKey();
    }

    private void SpawnKey()
    {
        if (keyPrefab == null)
        {
            Debug.LogError("Key prefab is NOT assigned.");
            return;
        }

        Debug.Log("Key prefab assigned: " + keyPrefab.name);

        Transform chosenPoint = null;
        float roll = Random.value;

        Debug.Log("Roll = " + roll + " | nearDoorChance = " + nearDoorChance);

        if (nearDoorSpawnPoint != null && roll <= nearDoorChance)
        {
            chosenPoint = nearDoorSpawnPoint;
            Debug.Log("Using nearDoorSpawnPoint: " + chosenPoint.name);
        }
        else
        {
            if (normalSpawnPoints == null || normalSpawnPoints.Length == 0)
            {
                Debug.LogError("No normal spawn points assigned.");
                return;
            }

            int randomIndex = Random.Range(0, normalSpawnPoints.Length);
            chosenPoint = normalSpawnPoints[randomIndex];
            Debug.Log("Using normal spawn point: " + chosenPoint.name);
        }

        GameObject spawnedKey = Instantiate(keyPrefab, chosenPoint.position, chosenPoint.rotation);

        if (spawnedKey != null)
        {
            Debug.Log("Spawned key: " + spawnedKey.name + " at " + chosenPoint.position);
        }
        else
        {
            Debug.LogError("Instantiate failed.");
        }
    }
}