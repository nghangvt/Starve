using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class StrikeCollision : MonoBehaviour
{
    private Tool tool;
    public GameObject woodItemPrefab;
    public GameObject appleItemPrefab;
    public GameObject berryItemPrefab;
    public GameObject stoneItemPrefab;
    public GameObject goldItemPrefab;

    private void Start()
    {
        tool = FindObjectOfType<Tool>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tree"))
        {
            tool.hitCount++;
            Instantiate(woodItemPrefab, transform.position, quaternion.identity);
            if (tool.hitCount % 5 == 0)
            {
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("AppleTree"))
        {
            tool.hitCount++;
            Instantiate(woodItemPrefab, transform.position, quaternion.identity);
            Instantiate(appleItemPrefab, transform.position, quaternion.identity);
            if (tool.hitCount % 5 == 0)
            {
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("BerryTree"))
        {
            tool.hitCount++;
            Instantiate(woodItemPrefab, transform.position, quaternion.identity);
            Instantiate(berryItemPrefab, transform.position, quaternion.identity);
            if (tool.hitCount % 5 == 0)
            {
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("StoneOre"))
        {
            tool.hitCount++;
            Instantiate(stoneItemPrefab, transform.position, quaternion.identity);
            if (tool.hitCount % 5 == 0)
            {
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("GoldOre"))
        {
            tool.hitCount++;
            Instantiate(goldItemPrefab, transform.position, quaternion.identity);
            if (tool.hitCount % 5 == 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
