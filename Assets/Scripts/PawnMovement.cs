using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PawnMovement : MonoBehaviour
{
    public GameObject cursor;
    public string type;
    public bool firstMove = true;

    void OnMouseDown()
    {
        if (this.gameObject.name.Contains("King"))
            Debug.Log("okokok");

        if (!GUIMenu.Pause)
        {
            if (this.gameObject.layer.Equals(manager.turn))
            {
                manager.resetLight();
                manager.objSelected = this.gameObject;
                manager.findCell(this.gameObject, type);
            }
            else
            {
                var sub = GameObject.FindGameObjectsWithTag("light");
                foreach (GameObject light in sub)
                {
                    if(light.transform.position.Equals(this.transform.position))
                    light.GetComponent<cursor>().OnMouseDown();
                }
            }
        }
    }


}
