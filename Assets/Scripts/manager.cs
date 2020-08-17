using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public static GameObject objSelected;
    public GameObject prefab;
    public static GameObject cursor;
    private static GameObject light;

    public static int turn = 10; // 10 = bianco //9 = nero  ---> da sistemare

    private void Start()
    {
        cursor = prefab;
    }

    public static void resetLight()
    {
        var reset = GameObject.FindGameObjectsWithTag("light");
        foreach (GameObject light in reset)
            Destroy(light);
    }

    public static void findCell(GameObject chess,string name)
    {
        switch (name)
        {
            case "pawnDark":
                //movement
                if (chess.GetComponent<PawnMovement>().firstMove)
                {
                    if (!pawnBlock(chess.transform.position.x, chess.transform.position.z + 1))
                    {
                        Instantiate(cursor, new Vector3(chess.transform.position.x, 0, chess.transform.position.z + 1), Quaternion.Euler(90, 0, 0));

                        if (!pawnBlock(chess.transform.position.x, chess.transform.position.z + 2))
                            Instantiate(cursor, new Vector3(chess.transform.position.x, 0, chess.transform.position.z + 2), Quaternion.Euler(90, 0, 0));
                    }
                }
                else
                    if (!pawnBlock(chess.transform.position.x, chess.transform.position.z + 1))
                        Instantiate(cursor, new Vector3(chess.transform.position.x, 0, chess.transform.position.z + 1), Quaternion.Euler(90, 0, 0));

                // eat
                diagonalOccupated(chess.transform.position.x - 1, chess.transform.position.z + 1);

                diagonalOccupated(chess.transform.position.x + 1, chess.transform.position.z + 1);
                break;

            case "pawnLight":
                //movement
                if (chess.GetComponent<PawnMovement>().firstMove)
                {
                    if (!pawnBlock(chess.transform.position.x, chess.transform.position.z - 1))
                    {
                         Instantiate(cursor, new Vector3(chess.transform.position.x, 0, chess.transform.position.z - 1), Quaternion.Euler(90, 0, 0));
                        if (!pawnBlock(chess.transform.position.x, chess.transform.position.z - 2))
                            Instantiate(cursor, new Vector3(chess.transform.position.x, 0, chess.transform.position.z - 2), Quaternion.Euler(90, 0, 0));
                    }
                }
                else
                   if (!pawnBlock(chess.transform.position.x, chess.transform.position.z - 1))
                        Instantiate(cursor, new Vector3(chess.transform.position.x, 0, chess.transform.position.z - 1), Quaternion.Euler(90, 0, 0));

                // eat
                diagonalOccupated(chess.transform.position.x - 1, chess.transform.position.z - 1);

                diagonalOccupated(chess.transform.position.x + 1, chess.transform.position.z - 1);
               
                break;

            case "tower":

                for (float z = chess.transform.position.z + 1; z <= 7; z++)
                    if (cellOccupated(chess.transform.position.x, z))
                        break;

                for (float z = chess.transform.position.z - 1; z >= 0; z--)
                    if (cellOccupated(chess.transform.position.x, z))
                        break;

                for (float x = chess.transform.position.x + 1; x <= 7; x++)
                    if (cellOccupated(x, chess.transform.position.z))
                        break;

                for (float x = chess.transform.position.x - 1; x >= 0; x--)
                    if (cellOccupated(x, chess.transform.position.z))
                        break;


                break;

            case "horse":
                cellOccupated(chess.transform.position.x + 2, chess.transform.position.z - 1);

                cellOccupated(chess.transform.position.x - 2, chess.transform.position.z - 1);

                cellOccupated(chess.transform.position.x + 2, chess.transform.position.z + 1);

                cellOccupated(chess.transform.position.x - 2, chess.transform.position.z + 1);

                cellOccupated(chess.transform.position.x + 1, chess.transform.position.z - 2);

                cellOccupated(chess.transform.position.x - 1, chess.transform.position.z - 2);

                cellOccupated(chess.transform.position.x + 1, chess.transform.position.z + 2);

                cellOccupated(chess.transform.position.x - 1, chess.transform.position.z + 2);
               
                break;


            case "bishop":
                float diagonal = chess.transform.position.x;

                for (float z = chess.transform.position.z + 1; z <= 7; z++)
                {   diagonal++;
                    if (cellOccupated(diagonal, z))
                        break;
                }

                diagonal = chess.transform.position.x;
                for (float z = chess.transform.position.z - 1; z >= 0; z--)
                {
                    diagonal--;
                    if (cellOccupated(diagonal, z))
                        break;
                }

                diagonal = chess.transform.position.z;
                for (float x = chess.transform.position.x + 1; x <= 7; x++)
                {
                    diagonal--;
                    if (cellOccupated(x, diagonal))
                        break;
                }
                
                diagonal = chess.transform.position.z;
                for (float x = chess.transform.position.x - 1; x >=0; x --)
                {
                    diagonal++;
                    if (cellOccupated(x, diagonal))
                        break;
                }

                break;

            case "queen":

                //Perpendicular
                for (float z = chess.transform.position.z + 1; z <= 7; z++)
                    if (cellOccupated(chess.transform.position.x, z))
                        break;

                for (float z = chess.transform.position.z - 1; z >= 0; z--)
                    if (cellOccupated(chess.transform.position.x, z))
                        break;

                for (float x = chess.transform.position.x + 1; x <= 7; x++)
                    if (cellOccupated(x, chess.transform.position.z))
                        break;

                for (float x = chess.transform.position.x - 1; x >= 0; x--)
                    if (cellOccupated(x, chess.transform.position.z))
                        break;

                //Diagonal
                diagonal = chess.transform.position.x;
                for (float z = chess.transform.position.z + 1; z <= 7; z++)
                {
                    diagonal++;
                    if (cellOccupated(diagonal, z))
                        break;
                }

                diagonal = chess.transform.position.x;
                for (float z = chess.transform.position.z - 1; z >= 0; z--)
                {
                    diagonal--;
                    if (cellOccupated(diagonal, z))
                        break;
                }

                diagonal = chess.transform.position.z;
                for (float x = chess.transform.position.x + 1; x <= 7; x++)
                {
                    diagonal--;
                    if (cellOccupated(x, diagonal))
                        break;
                }

                diagonal = chess.transform.position.z;
                for (float x = chess.transform.position.x - 1; x >= 0; x--)
                {
                    diagonal++;
                    if (cellOccupated(x, diagonal))
                        break;
                }

                break;

            case "king":

                cellOccupated(chess.transform.position.x + 1, chess.transform.position.z + 1);

                cellOccupated(chess.transform.position.x + 1, chess.transform.position.z);

                cellOccupated(chess.transform.position.x + 1, chess.transform.position.z - 1);

                cellOccupated(chess.transform.position.x - 1, chess.transform.position.z);

                cellOccupated(chess.transform.position.x - 1, chess.transform.position.z - 1);

                cellOccupated(chess.transform.position.x - 1, chess.transform.position.z + 1);

                cellOccupated(chess.transform.position.x, chess.transform.position.z + 1);

                cellOccupated(chess.transform.position.x, chess.transform.position.z - 1);

                break;

            default:
                return;
        }
    }

    public static bool cellOccupated(float cellx,float cellz)
    {
        if (cellx < 0 || cellz < 0 || cellx > 7 || cellz > 7)
            return false;

            light = Instantiate(cursor, new Vector3(cellx, 0, cellz), Quaternion.Euler(90, 0, 0));
        
        bool occupied = false;
        var chess = GameObject.FindGameObjectsWithTag("chess");
        foreach (GameObject c in chess)
        {
            if (c.transform.position.x.Equals(cellx) && c.transform.position.z.Equals(cellz))
            {
                if (c.layer.Equals(objSelected.layer))
                {
                    light.GetComponent<BoxCollider>().enabled = false;
                    light.GetComponentInChildren<Light>().color = Color.blue;
                }
                else
                    light.GetComponentInChildren<Light>().color = Color.red;
                
                occupied = true;
            }
        }
        return occupied;
    }

    public static void diagonalOccupated(float cellx, float cellz)
    {
        var chess = GameObject.FindGameObjectsWithTag("chess");
        foreach (GameObject c in chess)
        {
            if (c.transform.position.x.Equals(cellx) && c.transform.position.z.Equals(cellz))
            {
                light = Instantiate(cursor, new Vector3(cellx, 0, cellz), Quaternion.Euler(90, 0, 0));

                if (c.layer.Equals(objSelected.layer))
                {
                    light.GetComponent<BoxCollider>().enabled = false;
                    light.GetComponentInChildren<Light>().color = Color.blue;
                }
                else
                    light.GetComponentInChildren<Light>().color = Color.red;
            }
        }
    }
        public static bool pawnBlock(float cellx, float cellz) // si può evitare (?)
        {
            var chess = GameObject.FindGameObjectsWithTag("chess");
                foreach (GameObject c in chess)
                {
                    if (c.transform.position.x.Equals(cellx) && c.transform.position.z.Equals(cellz))
                        return true;
                }
                return false;
        }

    public static void findAndDestroy(GameObject position)
    {
        var chess = GameObject.FindGameObjectsWithTag("chess");
        foreach (GameObject c in chess)
        {
            if (c.transform.position.x.Equals(position.transform.position.x) && c.transform.position.z.Equals(position.transform.position.z))
            {
                if (!c.name.Contains("King"))
                {
                    Destroy(c);
                    PlayerPrefs.DeleteKey(c.name + "x");
                    PlayerPrefs.DeleteKey(c.name + "z");
                }
                else
                {
                    Destroy(c);
                    GameObject.Find("Gui(inGame)").transform.GetChild(2).gameObject.SetActive(true);

                    if(c.name.Contains("Light"))
                    GameObject.Find("Gui(inGame)").transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "Black is the winner !";
                    else
                    GameObject.Find("Gui(inGame)").transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "White is the winner !";

                    GUIMenu.Pause = true;
                }
              
            }
        }
    }

    }
