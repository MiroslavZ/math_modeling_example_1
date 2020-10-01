using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class MainScript : MonoBehaviour
{
    public GameObject Box;
    public GameObject Rail;
    public GameObject RailTrigger;
    //эти поля можно настраивать в редакторе в реальном времени
    public float BoxFriction = 0.3f;
    public float RailFriction = 0.3f;
    public float Mass = 1;
    private float SupportReactionForce;
    public float Gravity = -9.81f;
    //
    PhysicMaterial BoxPhysMaterial;
    PhysicMaterial RailPhysMaterial;
    Rigidbody boxRigidBody;
    RailScript railScript;

    private void Awake()
    {
        //первичная инициализация
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
        //вывод угла наклона рейки в консоль (для отладки)
        var railAngle = Vector3.Angle(Rail.transform.right, Vector3.right);
        Debug.Log("RailAngle = " + railAngle);
    }

    void FixedUpdate()
    {
        //пока груз не на рейке, ни трение ни сила реакции опоры на него не действуют
        if (railScript.BoxIsOnTheRail)
        {
            //мы ошиблись. сила реакции опоры не задается нами, она равна mg*cos(a)
            Debug.Log("Box is on the rail!");
            var railAngle = Vector3.Angle(Rail.transform.right, Vector3.right);
            SupportReactionForce = Mass * Gravity * Mathf.Cos(railAngle);
            var tempx = SupportReactionForce * RailFriction * Mathf.Cos(railAngle) - SupportReactionForce * Mathf.Sin(railAngle);
            var tempy = Mass * Gravity - RailFriction * SupportReactionForce * RailFriction * Mathf.Sin(railAngle) 
                - SupportReactionForce * Mathf.Cos(railAngle);
            boxRigidBody.AddForce(new Vector3(tempx, tempy, 0), ForceMode.Acceleration);
        }
        else
            boxRigidBody.AddForce(new Vector3(0,Gravity,0),ForceMode.Acceleration);
     }

    //метод для изменения к. трения в реальном времени
    void UpdateFriction(PhysicMaterial m, float newFrictionValue)
    {
        m.dynamicFriction = newFrictionValue;
        m.staticFriction = newFrictionValue;
    }

    //метод для изменения массы груза в реальном времени
    void UpdateMass()
    {
        boxRigidBody.mass = Mass;
    }
}
