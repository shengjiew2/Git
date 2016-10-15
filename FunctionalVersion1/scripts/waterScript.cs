using UnityEngine;
using System.Collections;

public class waterScript : MonoBehaviour {

    public Shader shader;
    public PointLight pointLight;

    // Use this for initialization
    void Start () {
        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material.shader = shader;
        
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
