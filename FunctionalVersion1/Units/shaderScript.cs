using UnityEngine;
using System.Collections;
using System;

public class shaderScript : MonoBehaviour {

    public Shader shader;
    public Texture2D diffuseMap;
    public Texture2D normalMap;
    public PointLight[] pointlights;
    public int res = 30;

	// Use this for initialization
	void Start () {
        
        
        //Material mat = GetComponent<Material>();
        //mat.mainTexture = diffuseMap;
        MeshRenderer r = GetComponent<MeshRenderer>();
        //r.material.EnableKeyword("_NORMALMAP");
        //createNormalMap();

        /* THIS WORKS*/
        createDiffuseMap();
        createNormalMap();
        r.material.SetTexture("_MainTex", diffuseMap);
        r.material.SetTexture("_BumpMap", normalMap);
        

        //examine(normalMap);



        //createDiffuseMap();
        //createNormalMap();

    }

    private void examine(Texture2D normalMap)
    {
        
        
        for (int i = 0; i < normalMap.width; i++)
        {
            for (int j = 0; j <3; j++)
            {
                print(normalMap.GetPixel(i, j));
            }
        }


    }

    private void createDiffuseMap()
    {   //this will just be a solid colour, so 4*4 will do fine

        diffuseMap = new Texture2D(2, 2, TextureFormat.ARGB32, false);

        // set the pixel values
        diffuseMap.SetPixel(0, 0, Color.cyan);
        diffuseMap.SetPixel(1, 0, Color.cyan);
        diffuseMap.SetPixel(0, 1, Color.cyan);
        diffuseMap.SetPixel(1, 1, Color.cyan);

        // Apply all SetPixel calls
        diffuseMap.Apply();

        // connect texture to material of GameObject this script is attached to
        //renderer.material.mainTexture = diffuseMap; //this done will be done later

    }

    private void createNormalMap()
    {
        //this will be larger, res*res
        //each pixel will be a random normal value

        normalMap = new Texture2D(res, res, TextureFormat.ARGB32, false);


        for (int i = 0; i<res; i++)
        {
            for (int j= 0; j<res;j++)
            {
                //we have to store it as a color, so [0,1],[0,1],[0,1] [0,1]
                //but we want it as a vector, (-1,1), (0,1), (-1,1)
                //to transfer: double, -1
                //so make a colour  [0,1], [0.5,1], [0,1], 1
                Color c = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                print("normal: " + c);

                normalMap.SetPixel(i, j, c);
            }
        }

        // Apply all SetPixel calls
        normalMap.Apply();

        // connect texture to material of GameObject this script is attached to
        //renderer.material.mainTexture = diffuseMap; //this done will be done later
    }

    // Update is called once per frame
    void Update () {
	
	}
}
