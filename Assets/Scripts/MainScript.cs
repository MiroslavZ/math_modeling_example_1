using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class MainScript : MonoBehaviour
{
    public GameObject Box;
    public GameObject Rail;
    public Transform RailCenter;
    //
    public float BoxFriction = 0.3f;
    public float RailFriction = 0.3f;
    public float Mass = 1;
    float SupportReactionForce = 0;
    public float Gravity = -9.81f;
    //
    PhysicMaterial BoxPhysMaterial;
    PhysicMaterial RailPhysMaterial;
    Rigidbody boxRigidBody;
    RailScript railScript;
    public bool PhysicsActivated = true;
    public bool IsAxisOffsetPhysics = false;

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
        if (PhysicsActivated)
        {
            if (IsAxisOffsetPhysics)
                AxisPhysics();
            else
                VectorPhysics();
        }
    }

    void DrawForceRays()
    {
        var temp = Box.transform.position.x < RailCenter.position.x ? 1 : -1;
        var railAngle = Vector3.Angle(Rail.transform.right, Vector3.right);
        var G = new Vector3(0, Gravity, 0) * Mass;
        Debug.DrawRay(Box.transform.position, G, Color.yellow);
        var N = Quaternion.AngleAxis(railAngle * temp, Vector3.forward) * G * Mass * Mathf.Cos(railAngle);
        Debug.DrawRay(Box.transform.position, N, Color.red);
        var FF = Quaternion.AngleAxis(90 * temp, Vector3.back) * N * RailFriction * temp;
        Debug.DrawRay(Box.transform.position, FF, Color.blue);
        var Ma = G + N + FF;
        Debug.DrawRay(Box.transform.position, Ma, Color.green);
    }

    void VectorPhysics()
    {
        Debug.Log("VectorPhysics enabled!");
        var temp = Box.transform.position.x < RailCenter.position.x ? 1 : -1;
        if (railScript.BoxIsOnTheRail)
        {
            var railAngle = Vector3.Angle(Rail.transform.right, Vector3.right);
            var G = new Vector3(0, Gravity, 0) * Mass;
            var N = Quaternion.AngleAxis(railAngle * temp, Vector3.forward) * G * Mass * Mathf.Cos(railAngle);
            var temp1 = Box.transform.position.x < RailCenter.position.x ? -1 : 1;
            var FF = Quaternion.AngleAxis(90 * -1 * temp1 * temp, Vector3.back) * N * RailFriction * temp;
            var Ma = G + N + FF;
            boxRigidBody.AddForce(Ma, ForceMode.Acceleration);
        }
        else
            boxRigidBody.AddForce(new Vector3(0, Gravity, 0), ForceMode.Acceleration);
    }

    bool tempFlag = false;
    void AxisPhysics()
    {
        Debug.Log("AxisPhysics enabled!");
        var temp = Box.transform.position.x < RailCenter.position.x ? 1 : -1;
        if (railScript.BoxIsOnTheRail&&tempFlag)
        {
            var railAngle = Vector3.Angle(Rail.transform.right, Vector3.right);
            SupportReactionForce = Mass * Gravity * Mathf.Cos(railAngle);
            var tempx = SupportReactionForce * RailFriction * Mathf.Cos(railAngle)
                - SupportReactionForce * Mathf.Sin(railAngle);
            var tempy = Mass * Gravity - SupportReactionForce * RailFriction * Mathf.Sin(railAngle)
                - SupportReactionForce * Mathf.Cos(railAngle);
            Debug.DrawRay(Box.transform.position, new Vector3(tempx * temp * 1.0f, tempy, 0), Color.yellow, 0.3f);
            boxRigidBody.AddForce(new Vector3(tempx * temp*1.0f, tempy, 0), ForceMode.Acceleration);
            tempFlag = true;
        }
        else
            boxRigidBody.AddForce(new Vector3(0, Gravity, 0), ForceMode.Acceleration);
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
