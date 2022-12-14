using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRenderer : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    public MeshCollider collider;

    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        collider.sharedMesh = null;
        collider.sharedMesh = colliderMesh;
    }
    // Start is called before the first frame update
    void Start()
    {

        UpdateCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
