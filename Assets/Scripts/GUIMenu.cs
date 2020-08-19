using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIMenu : MonoBehaviour
{
    public static bool Pause = true;
    

    public void EndGame()
    {
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = manager.turn + " is the winner !";
        Pause = true;
    }

    public void StartGame()
    {
        GameObject.Find("Main Camera").GetComponent<Animator>().SetBool("Start", true);
        Pause = false;
    }

    public void LoadGame()
    {
        var chess = GameObject.FindGameObjectsWithTag("chess");
        foreach (GameObject c in chess)
        {
            if (PlayerPrefs.HasKey(c.name + "x"))
            {
                float x = PlayerPrefs.GetFloat(c.name + "x", c.transform.position.x);
                float z = PlayerPrefs.GetFloat(c.name + "z", c.transform.position.z);

                c.transform.position = new Vector3(x, 0, z);
            }
            else
                Destroy(c);
        }

        if(PlayerPrefs.GetInt("turn", manager.turn).Equals(10))
            GameObject.Find("Main Camera").GetComponent<Animator>().Play("white");
        else
            GameObject.Find("Main Camera").GetComponent<Animator>().Play("black");
    }

    public void ResetGame()
    {
        Pause = false;

        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("turn",manager.turn);

        var chess = GameObject.FindGameObjectsWithTag("chess");
        foreach (GameObject c in chess)
        {
            PlayerPrefs.SetFloat(c.name + "x", c.transform.position.x);
            PlayerPrefs.SetFloat(c.name + "z", c.transform.position.z);
        }

        PlayerPrefs.Save();
    }

    public void OpenMenu()
    {
    Pause = true;
        Color Alpha = this.GetComponent<Image>().color;
        Alpha.a = .5f;
        this.GetComponent<Image>().color  = Alpha;
    }

   public void ResumeGame()
    {
        Pause = false;
        Color Alpha = this.GetComponent<Image>().color;
        Alpha.a = 0f;
        this.GetComponent<Image>().color = Alpha;
    }

    public void DeleteSaving()
    {
        PlayerPrefs.DeleteAll(); //maybe..
    }

}
