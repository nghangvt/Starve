using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public PolygonCollider2D spawnArea;  // Vùng polygon bạn đã vẽ
    public NavMeshAutoBaker navMeshAutoBaker; // Tham chiếu đến NavMeshAutoBaker

    // Class để quản lý từng loại cây
    [System.Serializable]
    public class TreeType
    {
        public GameObject treePrefab;  // Prefab của cây
        public int maxNumberOfTrees;   // Số lượng cây tối đa
        public float respawnDelay;     // Thời gian chờ trước khi respawn
        [HideInInspector]
        public List<GameObject> trees = new List<GameObject>();  // Danh sách các cây hiện có
    }


    public List<TreeType> treeTypes = new List<TreeType>(); // Danh sách các loại cây

    void Start()
    {
        foreach (TreeType treeType in treeTypes)
        {
            SpawnInitialTrees(treeType);
        }
    }

    void Update()
    {
        foreach (TreeType treeType in treeTypes)
        {
            RespawnTreesIfNecessary(treeType);
        }
    }

    void SpawnInitialTrees(TreeType treeType)
    {
        for (int i = 0; i < treeType.maxNumberOfTrees; i++)
        {
            SpawnTree(treeType);
        }
    }


    void RespawnTreesIfNecessary(TreeType treeType)
    {
        // Loại bỏ cây đã bị phá hủy khỏi danh sách
        treeType.trees.RemoveAll(tree => tree == null);

        // Nếu số lượng cây hiện tại nhỏ hơn maxNumberOfTrees, spawn thêm cây với độ trễ
        if (treeType.trees.Count < treeType.maxNumberOfTrees)
        {
            StartCoroutine(RespawnTreeWithDelay(treeType));
        }
    }


    IEnumerator RespawnTreeWithDelay(TreeType treeType)
    {
        // Chờ trước khi spawn lại cây
        yield return new WaitForSeconds(treeType.respawnDelay);

        // Spawn cây sau khoảng thời gian chờ
        if (treeType.trees.Count < treeType.maxNumberOfTrees)  // Kiểm tra lại số lượng cây
        {
            SpawnTree(treeType);
        }
    }

    void SpawnTree(TreeType treeType)
    {
        Vector2 randomPosition = GetRandomPointInPolygon();
        GameObject newTree = Instantiate(treeType.treePrefab, randomPosition, Quaternion.identity);
        treeType.trees.Add(newTree);  // Thêm cây vào danh sách

        // Gọi phương thức BakeNavMesh từ NavMeshAutoBaker
        if (navMeshAutoBaker != null)
        {
            navMeshAutoBaker.BakeNavMesh();
        }
    }

    Vector2 GetRandomPointInPolygon()
    {
        Bounds bounds = spawnArea.bounds;
        Vector2 randomPoint;

        // Lặp lại cho đến khi tìm được điểm hợp lệ trong polygon
        do
        {
            randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );
        }
        while (!IsPointInPolygon(randomPoint));

        return randomPoint;
    }

    bool IsPointInPolygon(Vector2 point)
    {
        // Kiểm tra xem điểm có nằm trong vùng polygon không
        return spawnArea.OverlapPoint(point);
    }
}
