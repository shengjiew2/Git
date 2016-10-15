using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//turns a 2d array of heights into a plane


public class meshGenerator : MonoBehaviour {

    public GameObject water;
    private float[,] heightMap;
    
    private List<Vector3> vertices;
    private List<int> triangles;
    //private List<Vector4> colors;
    private Color[] colors;
    private int width;
    private int length;

    private float maxHeight;
    private float minHeight;

    // Use this for initialization

    public Mesh GenerateMesh (float[,] heightMap, int width, int length) {
        //set height map
        this.width = width;
        this.length = length;
        this.heightMap = heightMap;
        calcMaxMinHeights(heightMap);                                    

        vertices = new List<Vector3>();
        triangles = new List<int>();
        //colors = new List<Vector4>();
        colors = new Color[width*length];
        int count = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                vertices.Add(new Vector3(i, heightMap[i,j], j));

                //what colour should i make stuff?
                //bottem third: sandy, turning to green
                //middle third: green, getting darker
                //top third: white
                //colors.Add(new Vector4(0, 0, 0, 1));

                //bottom third of the map is sandy yellow
                if(heightMap[i,j] < minHeight + (maxHeight-minHeight)/3)
                {
                    colors[count] = Color.yellow;//new Vector4(219/255, 219/255, 77/255, 1);
                    colors[count].a = 0.1f;
                }
                //next section (lower third to top quarter), is green.
                //with the darker green at the top, more yellowy at the bottem. 
                if (heightMap[i, j] > minHeight + (maxHeight - minHeight) / 3 && heightMap[i, j] < maxHeight - (maxHeight - minHeight) / 4)
                {
                    colors[count] = Color.green;//new Vector4(0.0f, 57 / 255, 0.0f, 1);
                    colors[count].a = 1;
                }

                //top quater is white
                if (heightMap[i, j] > maxHeight - (maxHeight - minHeight) / 4)
                {
                    colors[count] = Color.white;//new Vector4(0.0f, 57 / 255, 0.0f, 1);
                    colors[count].a = 5;
                }
                //colors[count] = new Vector4(1, 1, 1, 1);
                //colors[count].a = 5;
                count++;
            }
        }
        for (int i = 0; i < width-1; i++)
        {
            for (int j = 0; j < length-1; j++)
            {
                addTriangles(i, j);
            }
        }

        Mesh mesh = new Mesh();
        

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors;

        mesh.RecalculateNormals();
        return mesh;

    }

    void calcMaxMinHeights(float[,] heightMap)
    {
        maxHeight = minHeight = heightMap[0, 0];
        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < length - 1; j++)
            {
                if(heightMap[i,j] > maxHeight)
                {
                    maxHeight = heightMap[i, j];
                }
                if (heightMap[i, j] < minHeight)
                {
                    minHeight = heightMap[i, j];
                }
            }
        }

        float waterHeight = minHeight + (maxHeight - minHeight) / 4;
        water.transform.Translate(new Vector3(0.0f, -water.transform.position.y+waterHeight, 0.0f));
        
    }
    
    void OnDrawGizmos()
    {
        if (heightMap != null)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawCube(new Vector3(i, heightMap[i, j], j), Vector3.one * .4f);
                }
            }
        }
    }
    
    private void addTriangles(int x, int y)
    {
        triangles.Add(indexOfTopLeft(x, y));
        triangles.Add(indexOfTopRight(x, y));
        triangles.Add(indexOfBottomLeft(x, y));

        triangles.Add(indexOfTopRight(x, y));
        triangles.Add(indexOfBottomRight(x, y));
        triangles.Add(indexOfBottomLeft(x, y));
    }

    private int indexOfTopLeft(int x, int y)
    {
        return y * width + x;
    }

    private int indexOfTopRight(int x, int y)
    {
        return y * width + x +1;
    }

    private int indexOfBottomLeft(int x, int y)
    {
        return (y+1) * width +x ;
    }
    private int indexOfBottomRight(int x, int y)
    {
        return (y+1) * width + x + 1;
    }


 

	
	// Update is called once per frame
	void Update () {
	
	}
}
