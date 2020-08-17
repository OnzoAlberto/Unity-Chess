using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    public void OnMouseDown()
    {
        if (!GUIMenu.Pause)
        {

            manager.objSelected.GetComponent<PawnMovement>().firstMove = false;
            manager.resetLight();

            if (this.GetComponentInChildren<Light>().color.Equals(Color.red))
            {
                manager.findAndDestroy(this.gameObject);
            }
            GameObject.Find(manager.objSelected.name.ToString()).transform.position = this.gameObject.transform.position;

            if (manager.turn.Equals(10))
            {
                GameObject.Find("Main Camera").GetComponent<Animator>().Play("black");
                manager.turn = 9;
            }
            else
            {
                GameObject.Find("Main Camera").GetComponent<Animator>().Play("white");

                manager.turn = 10;
            }
        }
    }

}
