using UnityEngine;

[CreateAssetMenu]
public class Mesh2D : ScriptableObject
{
    [System.Serializable]
    public class Vertex
    {
        public Vector2 Point;
        public Vector2 Normal;
        public float U; // only the U part of the UV because the V coordinate is generated.
        
        // Could also have fields for vertex color, bitangents 
    }
    
    public Vertex[] Vertices;
    public int[] LineIndices;

    public int VertexCount => Vertices.Length;
    public int LineCount => LineIndices.Length;
}