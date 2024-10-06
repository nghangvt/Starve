using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class NavMeshAutoBaker : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    void Start()
    {
        if (navMeshSurface == null)
        {
            navMeshSurface = FindObjectOfType<NavMeshSurface>();
        }

        if (navMeshSurface != null)
        {
            BakeNavMesh();
        }
        else
        {
            Debug.LogError("No NavMeshSurface found!");
        }
    }

    public void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh(); // Xây dựng lại NavMesh
    }


    void Update()
    {
        
    }
}
