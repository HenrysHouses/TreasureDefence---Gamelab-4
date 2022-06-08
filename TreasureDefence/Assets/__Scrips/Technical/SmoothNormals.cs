using UnityEngine;

public class SmoothNormals : MonoBehaviour
{
    [SerializeField] float angle;
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();

        if(filter)
            NormalSolver.RecalculateNormals(filter.mesh, angle);
        else
            NormalSolver.RecalculateNormals(renderer.sharedMesh, angle);
            
    }
}
