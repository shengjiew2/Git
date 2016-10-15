using UnityEngine;
using System.Collections;

public class DisplayCurrentValue : MonoBehaviour {
    public LayerMask dieColliderLayer = 8;
    public int currentValue = 1;
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
	    if (Physics.Raycast(transform.position,Vector3.up,out hit, Mathf.Infinity, dieColliderLayer))
        {
            currentValue = hit.collider.GetComponent<DieValue>().value;
        }
	}
    void onGUI()
    {
        GUILayout.Label(currentValue.ToString());
    }
}
