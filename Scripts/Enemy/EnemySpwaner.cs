using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab; // Prefab của enemy để spawn
    [SerializeField] PolygonCollider2D walkable;  // Collider của vùng Walkable
    [SerializeField] int numberOfEnemies = 1; // Số lượng enemy spawn

    private List<GameObject> spawnedEnemies = new List<GameObject>(); // Danh sách các enemy đã spawn

    void Start()
    {
        if (walkable == null)
        {
            Debug.LogError("Walkable area not found!");
            return;
        }
        walkable = GameObject.Find("Walkable").GetComponent<PolygonCollider2D>();
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        // Nếu số lượng enemy hiện tại nhỏ hơn numberOfEnemies, tiếp tục spawn
        while (spawnedEnemies.Count < numberOfEnemies)
        {
            Vector3 spawnPosition = GetRandomPositionInArea();
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(newEnemy); // Thêm enemy vào danh sách quản lý
        }
    }

    Vector3 GetRandomPositionInArea()
    {
        Vector3 randomPoint;

        // Lặp cho đến khi tìm được vị trí hợp lệ trong vùng Walkable
        do
        {
            randomPoint = new Vector3(
                Random.Range(walkable.bounds.min.x, walkable.bounds.max.x),
                Random.Range(walkable.bounds.min.y, walkable.bounds.max.y),
                walkable.bounds.center.z  // Nếu vùng của bạn 2D thì giá trị z có thể là hằng số
            );
        }
        while (!IsPointInWalkableArea(randomPoint));

        return randomPoint;
    }

    bool IsPointInWalkableArea(Vector3 point)
    {
        // Kiểm tra nếu điểm này nằm trong collider của vùng Walkable
        return walkable.OverlapPoint(point);
    }


    void Update()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null); // Loại bỏ các enemy đã bị tiêu diệt
        if (spawnedEnemies.Count < numberOfEnemies)
        {
            SpawnEnemies();
        }
    }
}