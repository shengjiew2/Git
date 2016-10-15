using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class heightMapGenerator : MonoBehaviour {

    public int width = 33;
    public int length = 33;
    public int n=5;
    private float[,] heightMap;
    //public int maxHeight = 20;
    public float range = 10;

    public Shader shader;
    public PointLight pointLight;

    meshGenerator meshGen;
    // Use this for initialization
    void Start () {
        meshGen = GetComponent<meshGenerator>();
        //makeRandomMap(width, length);
        makeDiamondSquareMap();
        
        Mesh mesh = meshGen.GenerateMesh(heightMap, width, length);
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material.shader = shader;

    }

    private void makeDiamondSquareMap()
    {
        width = length = (int) Math.Pow(2, n) + 1;
        heightMap = new float[width, length];
        setCorners();
        int size = width-1;
        range /= 2;
        while (size >= 2)
        {
            diamonds(size);
            squares(size);
            range /= 2;
            size/=2;
        }

        //makeRandomMap();


    }
    public String errors;

    private void squares(int size)
    {
        int focusX;
        int focusZ;
        int num;
        float sum;

        int cornerX, cornerZ;

        for (int i = 0; i < width - size; i += size)
        {
            for (int j = 0; j < length - size; j += size)
            {

                errors = "squares 1: size=" + size + " i=" + i + " j=" + j;
                focusX = i + size / 2;
                focusZ = j;
                num = 4;
                sum = 0;

                //left
                sum += heightMap[i, j];

                //top
                cornerX = focusX;
                cornerZ = focusZ - size / 2;
                if (cornerZ >0)
                {
                    sum += heightMap[cornerX, cornerZ];
                }
                else
                {
                    num -= 1;
                }

                //right 
                cornerX = focusX + size / 2;
                cornerZ = focusZ;
                if (cornerX <width)
                {
                    sum += heightMap[cornerX, cornerZ];
                }
                else
                {
                    num -= 1;
                }

                //bottom
                cornerX = focusX;
                cornerZ = focusZ + size / 2;
                if (cornerZ < length)
                {
                    sum += heightMap[cornerX, cornerZ];
                }
                else
                {
                    num -= 1;
                }

                heightMap[focusX, focusZ] = sum / num + UnityEngine.Random.Range(-range, range); 

            }
        }

        

        for (int i=-size/2; i < width; i += size)
        {
            for (int j = size/2; j < length; j += size)
            {
                errors = "squares 2: size=" + size + " i=" + i + " j=" + j;
                focusX = i + size / 2;
                focusZ = j;
                num = 4;
                sum = 0;

                //left
                cornerX = i;
                cornerZ = j;
                if (cornerX > 0)
                {
                    sum += heightMap[i, j];
                }
                else
                {
                    num -= 1;
                }

                //top
                cornerX = focusX;
                cornerZ = focusZ - size / 2;
                if (cornerZ > 0)
                {
                    sum += heightMap[cornerX, cornerZ];
                }
                else
                {
                    num -= 1;
                }

                //right 
                cornerX = focusX + size / 2;
                cornerZ = focusZ;
                if (cornerX < width)
                {
                    sum += heightMap[cornerX, cornerZ];
                }
                else
                {
                    num -= 1;
                }

                //bottom
                cornerX = focusX;
                cornerZ = focusZ + size / 2;
                if (cornerZ < length)
                {
                    sum += heightMap[cornerX, cornerZ];
                }
                else
                {
                    num -= 1;
                }

                heightMap[focusX, focusZ] = sum / num + UnityEngine.Random.Range(-range, range); 
            }
        }


    }

    private void diamonds(int size)
    {
        int focusX;
        int focusZ;

         

        for (int i = 0; i < width-1; i += size)
        {
            for (int j = 0; j <length-1; j += size)
            {
                errors = "diamond: size=" + size + " i=" + i + " j=" + j;
                focusX = i + (size) / 2;
                focusZ = j + (size) / 2;
                heightMap[focusX, focusZ] = (heightMap[i, j] + heightMap[i + size, j] + heightMap[i, j + size] 
                                                + heightMap[i + size, j + size]) / 4
                                                + UnityEngine.Random.Range(-range, range);

                
            }
        }
    }

    

    private void setCorners()
    {
        /*
        heightMap[0, 0] = UnityEngine.Random.Range(-range, range) - range;
        heightMap[0, length-1] = UnityEngine.Random.Range(-range, range)-range;
        heightMap[width-1, 0] = UnityEngine.Random.Range(-range, range)-range;
        heightMap[width-1, length-1] = UnityEngine.Random.Range(-range, range)-range;
        */
        heightMap[0, 0] = UnityEngine.Random.Range(-range, range);
        heightMap[0, length - 1] = UnityEngine.Random.Range(-range, range);
        heightMap[width - 1, 0] = UnityEngine.Random.Range(-range, range);
        heightMap[width - 1, length - 1] = UnityEngine.Random.Range(-range, range);
    }

    private void makeRandomMap()
    {
        heightMap = new float[width, length];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                heightMap[i, j] = UnityEngine.Random.Range(-1, 1);
            }
        }
    }



    // Update is called once per frame
    void Update () {
        // Get renderer component (in order to pass params to shader)
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        // Pass updated light positions to shader
        renderer.material.SetColor("_PointLightColor", this.pointLight.color);
        renderer.material.SetVector("_PointLightPosition", this.pointLight.GetWorldPosition());

    }
}
