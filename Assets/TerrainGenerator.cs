using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int terrainWidth;
    public float resolution;
    public float roughness;
    public float startHeight;
    public float endHeight;

    float[] GenerateHeightMap(float startHeight, float endHeight, int count)
    {
        Debug.Log("Generating heightmap");
        // Create a heightmap array and set the start and endpoints
        float[] heightmap = new float[count + 1];
        heightmap[0] = startHeight;
        heightmap[heightmap.Length - 1] = endHeight;

        // Call the recursive function to generate the heightmap
        GenerateMidPoint(0, heightmap.Length - 1, roughness, heightmap);
        Debug.Log("Heightmap complete");
        return heightmap;
    }

    void GenerateMidPoint(int start, int end, float roughness, float[] heightmap)
    {
        // Find the midpoint of the array for this step
        int midPoint = (int)Mathf.Floor((start + end) / 2);

        if (midPoint != start)
        {
            // Find the mid height for this step
            var midHeight = (heightmap[start] + heightmap[end]) / 2;

            // Generate a new displacement between the roughness factor
            heightmap[midPoint] = midHeight + Random.Range(-roughness, roughness);

            // Repeat the process for the left side and right side of
            // the new mid point
            GenerateMidPoint(start, midPoint, roughness / 2, heightmap);
            GenerateMidPoint(midPoint, end, roughness / 2, heightmap);
        }
    }

    Vector2[] CreateTerrainVertices(float[] heightmap, float resolution)
    {
        Debug.Log("Creating terrain vertices");
        // The minimum resolution is 1
        resolution = Mathf.Max(1, resolution);



        List<Vector2> vertices = new List<Vector2>();

        // For each point, in the heightmap, create a vertex for
        // the top and the bottom of the terrain.
        for (int i = 0; i < heightmap.Length; i += 1) {
            vertices.Add(new Vector2(i / resolution, heightmap[i]));
            vertices.Add(new Vector2(i / resolution, 0));
        }

        Debug.Log("Created " + vertices.Count + " terrain vertices");
        return vertices.ToArray();//.ToBuiltin(Vector2) as Vector2[];
    }

    Vector2[] GenerateTerrainUV(float[] heightmap)
    {
        Debug.Log("Generating terrain UV co-ords");

        List<Vector2> uv = new List<Vector2>();

        float factor = 1.0f / heightmap.Length;

        // Loop through heightmap and create a UV point
        // for the top and bottom.
        for (int i = 0; i < heightmap.Length; i++) {
            uv.Add(new Vector2((factor * i) * 20, heightmap[i] / 20));
            uv.Add(new Vector2((factor * i) * 20, 0));
        }

        Debug.Log("Generated " + uv.Count + " grass UV co-ords");
        return uv.ToArray();//.ToBuiltin(Vector2) as Vector2[];
    }

    int[] Triangulate(int count)
    {
        List<int> indices = new List<int>();

        // For each group of 4 vertices, add 6 indices
        // to create 2 triangles
        for (int i = 0; i <= count - 4; i += 2) {
            indices.Add(i);
            indices.Add(i + 3);
            indices.Add(i + 1);

            indices.Add(i + 3);
            indices.Add(i);
            indices.Add(i + 2);
        }

        return indices.ToArray();
    }

    void Awake()
    {
        Debug.Log("Generating Terrain");

        // Generate the heightmap
        float[] heightmap = GenerateHeightMap(startHeight, endHeight, terrainWidth);

        // Create vertices, uv's and triangles
        Vector2[] terrainVertices = CreateTerrainVertices(heightmap, resolution);
        Vector2[] terrainUV = GenerateTerrainUV(heightmap);
        int[] terrainTriangles = Triangulate(terrainVertices.Length);

        // Create the mesh!
        GenerateMesh(terrainVertices, terrainTriangles, terrainUV, "ground", 0);

        Debug.Log("Terrain Gen complete");
    }

    void GenerateMesh(Vector2[] vertices, int[] triangles, Vector2[] uv, string texture, int z)
    {
        Debug.Log("Building Mesh");
        List<Vector3> meshVertices = new List<Vector3>();

        // Convert our Vector2's to Vector3
        foreach (Vector2 vertex in vertices)
        {
            meshVertices.Add(new Vector3(vertex.x, vertex.y, transform.position.z + z));
        }

        // Create a new mesh and set the vertices, uv's and triangles
        Mesh mesh = new Mesh();
        mesh.vertices = meshVertices.ToArray();
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Create a new game object
        GameObject go = new GameObject(texture);

        // Add the mesh to the object
        go.AddComponent<MeshRenderer>();
        MeshFilter filter = go.AddComponent<MeshFilter>();
        filter.mesh = mesh;

        // Add a texture  
        go.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture2D>("texture") as Texture;

        // Reparent as a child of this game object
        go.transform.parent = transform;

        Debug.Log("Mesh built");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
