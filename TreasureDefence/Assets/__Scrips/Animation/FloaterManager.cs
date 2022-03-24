using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterManager : MonoBehaviour
{
    public static FloaterManager instance;
    [SerializeField] Renderer WaterRenderer;
    [SerializeField] Renderer[] UpdateWaters;
    [SerializeField] MeshFilter WaterMesh;
    
    // * Noise calculation variables
    Texture2D _noiseTex;
    float _amplitude;
    Vector4 _movementDir; 
    public Vector3[] vertex;
    Vector3[] vertexStartPos;
    float _Count, _Speed, _Offset, _MinWaveHeight, _MaxWaveHeight, _Wiggle;
    float TAU = 6.28318530718f;

    public float timeOffset;

    public int _index;

    // ! recalculate the vertex displacement in order to get the world positions of the water

    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("There was two FloaterManagers in the scene, Destroying: " + this);
            Destroy(gameObject);
        }

        _noiseTex = (Texture2D)WaterRenderer.material.GetTexture("_NoiseTex");
        _amplitude = WaterRenderer.material.GetFloat("_Amplitude");
        
        vertex = WaterMesh.mesh.vertices;
        vertexStartPos = WaterMesh.mesh.vertices;
        _Count = WaterRenderer.material.GetFloat("_Count");
        _Speed = WaterRenderer.material.GetFloat("_Speed");
        _Offset = WaterRenderer.material.GetFloat("_Offset");
        _MaxWaveHeight = WaterRenderer.material.GetFloat("_MaxWaveHeight");
        _MinWaveHeight = WaterRenderer.material.GetFloat("_MinWaveHeight");
        _Wiggle = WaterRenderer.material.GetFloat("_Wiggle");

        if(!_noiseTex.isReadable)
            Debug.LogWarning(_noiseTex.name + ": readable is not true");
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Vector2[] uv = WaterMesh.mesh.uv;
        Vector3[] normal = WaterMesh.mesh.normals;

        for (int i = 0; i < vertex.Length; i++)
        {
            float wave = GetStraightWave(uv[i]);
            normal[i] *= _amplitude;
            Vector3 movingVertPos = new Vector3(normal[i].x, normal[i].y, normal[i].z) * wave;

            vertex[i] = vertexStartPos[i] + new Vector3(movingVertPos.x, movingVertPos.y, movingVertPos.z);
        }
    }

    void OnDrawGizmos()
    {
        // foreach (var vert in vertex)
        // {
        //     Vector3 worldPos = transform.TransformPoint(vert);
        //     Gizmos.DrawSphere(worldPos, 0.2f);
        // }

        // Vector3 worldpos = transform.TransformPoint(vertex[_index]);
        // Gizmos.color = Color.red;
        // Gizmos.DrawCube(worldpos, Vector3.one * 0.3f);
    }

    float GetStraightWave(Vector2 uv)
    {
        float xOffset = Mathf.Cos(uv.x * TAU * _Wiggle) * 0.01f;
        float t = Mathf.Cos((uv.x + xOffset - Time.timeSinceLevelLoad * _Speed) * TAU * _Count) * 0.5f + 0.5f;
        float waves = t;

        return waves;
    }

    public Vector4 getClosestVert(Transform _transform, ref int index)
    {
        Vector3 closest = new Vector3();
        float lastDist = Mathf.Infinity;
        for (int i = 0; i < vertex.Length; i++)
        {
            Vector3 pos = transform.TransformPoint(vertex[i]);
            float dist = Vector3.Distance(_transform.position, pos);
            if(dist < lastDist)
            {
                closest = vertex[i] *-1;
                lastDist = dist;
                index = i;
            }
        }
        // Debug.Log("closest vertex: " + index);
        return new Vector4(closest.x, closest.y, closest.z, _Offset);
    }



    Vector3 mul(Vector3 a, Vector3 b)
    {
        float x = a.x * b.x;
        float y = a.y * b.y;
        float z = a.z * b.z;
        Vector3 product = new Vector3(x, y, z);
        return product;
    }

    float GetWave(Vector2 uv){
        Vector2 uvsCentered = uv * 2 - Vector2.one;
        float radialDistance = uvsCentered.magnitude;

        float wave = Mathf.Cos((radialDistance - (Time.timeSinceLevelLoad + timeOffset) * _Speed) * TAU * _Count) * 0.5f + 0.5f;
        wave *= 1-radialDistance;
        return wave;
	}           
}