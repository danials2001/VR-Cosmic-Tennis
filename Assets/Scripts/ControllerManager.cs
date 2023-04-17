using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    GameObject selectedObject;
    GameObject grabbedObject;
    [SerializeField]
    private float radius = 10.0f;
    [SerializeField]
    private float ThrowForce= 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetButtonPressed())
        {
            //Debug.Log("Pressed");
            CastRay();
            if(selectedObject)
            {
                if (selectedObject.GetComponent<SelectableObject>())
                {
                    selectedObject.GetComponent<SelectableObject>().Highlight();
                }
            }
        }

        float tpval = GetTriggerPress();
        if(tpval != 0.0f) 
        {
            //Debug.Log("trigger");
            GrabObject(tpval);
            if (OVRInput.GetUp(OVRInput.Button.Two))
            {
                grabbedObject.transform.SetParent(null);
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().AddForce(GetPointingDir() * ThrowForce, ForceMode.Impulse);
            }
        }
        else
        {
            if (grabbedObject)
            {
                grabbedObject.transform.SetParent(null);
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                //grabbedObject.GetComponent<Rigidbody>().AddForce(GetPointingDir() * ThrowForce, ForceMode.Impulse);

            }
            grabbedObject = null;
        }
    }

    void CastRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(GetPosition(), GetPointingDir(), out hit))
        {
            //Debug.Log("Did Hit");
            selectedObject = hit.transform.gameObject;
        }
        else
        {
            selectedObject = null;
            //Debug.Log("Miss");
        }
    }

    Vector3 GetPointingDir()
    {
        return transform.forward;
    }

    Vector3 GetPosition()
    {
        return transform.position;
    }

    bool GetButtonPressed()
    {
        return OVRInput.GetUp(OVRInput.Button.One);
    }

    float GetTriggerPress()
    {
        return OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
    }

    void GrabObject(float pressedValue)
    {
        if(grabbedObject == null) 
        {
            //Debug.Log("ist null");
            Collider[] hitColliders = Physics.OverlapSphere(GetPosition(), radius);
            if(hitColliders.Length != 0)
            {
                
                //Collider hit = hitColliders[0];
                //grabbedObject = hit.gameObject;
                //GrabbableObject grab = grabbedObject.GetComponent<GrabbableObject>();
                foreach (Collider hit in hitColliders)
                {
                    if(hit.gameObject.GetComponent<GrabbableObject>())
                    {
                        //Debug.Log("grabbed");
                        grabbedObject = hit.gameObject;
                        grabbedObject.GetComponent<GrabbableObject>().Grab(pressedValue);
                        grabbedObject.transform.SetParent(transform);
                        // allow you to hold
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        break;
                    }
                }
                
            }  
        }
    }
}
