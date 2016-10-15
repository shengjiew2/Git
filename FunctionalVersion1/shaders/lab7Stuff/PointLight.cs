using UnityEngine;
using System.Collections;

public class PointLight : MonoBehaviour {

    public Color color;

    public Vector4 GetWorldPosition()
    {
        Vector4 r = new Vector4();
        r.x = transform.position.x;
        r.y = transform.position.y;
        r.z = transform.position.z;
        r.w = 0;

        return r;//.this.transform.position;
    }
}
