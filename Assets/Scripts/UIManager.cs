using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    MainScript mainScript;
    GameObject Box;
    public InputField Gravity;
    public InputField Mass;
    public InputField BFriction;
    public InputField RFriction;
    public Text text;

    private void Awake()
    {
        Box = GameObject.Find("Box");
        mainScript = GetComponent<MainScript>();                    
        Gravity.text=mainScript.Gravity.ToString();         
        Mass.text = mainScript.Mass.ToString();
        BFriction.text = mainScript.BoxFriction.ToString();
        RFriction.text = mainScript.RailFriction.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mainScript.IsAxisOffsetPhysics)
            text.text = "AxisOffset";
        else
            text.text = "VectorSum";
    }

    // Update is called once per frame
    void Update()
    {
        float g;
        bool c1 = float.TryParse(Gravity.text, out g);
        float m;
        bool c2 = float.TryParse(Mass.text, out m);
        float bf;
        bool c3 = float.TryParse(BFriction.text, out bf);
        float rf;
        bool c4 = float.TryParse(RFriction.text, out rf);
        if (c1)
            mainScript.Gravity = g;
        if (c2)
            mainScript.Mass = m;
        if (c3)
            mainScript.BoxFriction = bf;
        if (c4)
            mainScript.RailFriction = rf;
    }

    public void RestartScene() 
    {
        SceneManager.LoadScene(0);
    }
    public void StartMoving()
    {
        float g;
        bool c1 = float.TryParse(Gravity.text, out g);
        float m;
        bool c2 = float.TryParse(Mass.text, out m);
        float bf;
        bool c3 = float.TryParse(BFriction.text, out bf);
        float rf;
        bool c4 = float.TryParse(RFriction.text, out rf);
        if (c1)
            mainScript.Gravity=g;
        if (c2)
            mainScript.Mass=m;
        if (c3)
            mainScript.BoxFriction=bf;
        if (c4)
            mainScript.RailFriction=rf;
        mainScript.PhysicsActivated = true;
    }

    public void SwitchPhysicsType()
    {
        if (mainScript.IsAxisOffsetPhysics)
            text.text = "VectorSum";
        else
            text.text = "AxisOffset";
        mainScript.IsAxisOffsetPhysics = !mainScript.IsAxisOffsetPhysics;
    }

    void ChoosePosition(float x,float y)
    {
        Box.transform.position = new Vector3(x, y, 0);
    }

    public void ChoosePositionTopLeft()
    {
        ChoosePosition(-0.25f, 2.28f);
    }

    public void ChoosePositionTopRight()
    {
        ChoosePosition(0.25f, 2.28f);
    }

    public void ChoosePositionBottomLeft()
    {
        ChoosePosition(-0.25f, 1.78f);
    }

    public void ChoosePositionBottomRight()
    {
        ChoosePosition(0.25f, 1.78f);
    }
}
