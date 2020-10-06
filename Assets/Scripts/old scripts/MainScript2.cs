using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class MainScript2 : MonoBehaviour
{
    public GameObject Box;
    public GameObject Rail;
    public GameObject RailTrigger;
    //
    public float BoxFriction = 0.3f;
    public float RailFriction = 0.3f;
    public float Mass = 1;
    public float SupportReactionForce = 1;
    public float Gravity = -9.81f;
    public bool GravityIsEnabled = true;
    //
    PhysicMaterial BoxPhysMaterial;
    PhysicMaterial RailPhysMaterial;
    Rigidbody boxRigidBody;
    RailScript railScript;

    private void Awake()
    {
        BoxPhysMaterial = Box.GetComponent<BoxCollider>().material;
        RailPhysMaterial = Rail.GetComponent<BoxCollider>().material;
        BoxFriction = BoxPhysMaterial.dynamicFriction;
        RailFriction = RailPhysMaterial.dynamicFriction;
        boxRigidBody = Box.GetComponent<Rigidbody>();
        Mass = boxRigidBody.mass;
        railScript = Rail.GetComponent<RailScript>();
    }

    void Update()
    {
        UpdateFriction(BoxPhysMaterial, BoxFriction);
        UpdateFriction(RailPhysMaterial, RailFriction);
        UpdateMass();
    }

    void FixedUpdate()
    {
        if (GravityIsEnabled)
        {
            Debug.DrawRay(Box.transform.position, new Vector3(0, Gravity, 0), Color.yellow, 0.3f);
            boxRigidBody.AddForce(new Vector3(0, Gravity, 0), ForceMode.Acceleration);
        }
        //if (railScript.BoxIsOnTheRail)
        //{
        //    var railAngle = Vector3.Angle(Rail.transform.right, Vector3.right);
        //    Debug.Log("SupportReactionForce = " + SupportReactionForce);
        //    var tempx = SupportReactionForce * RailFriction * Mathf.Cos(railAngle) - SupportReactionForce * Mathf.Sin(railAngle);
        //    var tempy = Mass * Gravity - RailFriction * SupportReactionForce * RailFriction * Mathf.Sin(railAngle)
        //        - SupportReactionForce * Mathf.Cos(railAngle);
        //    Debug.DrawRay(Box.transform.position, new Vector3(tempx, tempy, 0), Color.yellow, 0.1f);
        //    boxRigidBody.AddForce(new Vector3(tempx, tempy, 0), ForceMode.Acceleration);
        //}
        //else
        //    boxRigidBody.AddForce(new Vector3(0, Gravity, 0), ForceMode.Acceleration);
    }

    void UpdateFriction(PhysicMaterial m, float newFrictionValue)
    {
        m.dynamicFriction = newFrictionValue;
        m.staticFriction = newFrictionValue;
    }

    void UpdateMass()
    {
        boxRigidBody.mass = Mass;
    }
}
