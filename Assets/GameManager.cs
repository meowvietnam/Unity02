using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Tube> tubes = new List<Tube>();

    public GameObject prefabTube;
    public GameObject prefabBall;
    public Sprite[] spriteBalls;

    public GameObject listTubeGO;
    public Tube currentTube;
    public LevelData[] levelGame;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
        else
        {
            Destroy(gameObject);
        }    
    }
    void Start()
    {
        levelGame[0].CreateLevel(ref tubes);
    }

   
   
    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckWin()
    {
        if(IsWin())
        {
            //// panelWin hiện ra 
            ///
            Debug.Log("Win");
        }
    }
    bool IsWin()
    {
        for (int i = 0; i < tubes.Count; i++)
        {
            if (tubes[i].isTubeNull)
            {
                continue;
            }
            if (!tubes[i].isFullTube)
            {
                Debug.Log("Chua Win");
                return false;
            }
            else
            {
                Sprite spriteCompare = tubes[i].balls[0].GetComponent<Image>().sprite;
                for (int j = 1; j < tubes[i].balls.Count; j++)
                {
                    if (tubes[i].balls[j].GetComponent<Image>().sprite != spriteCompare)
                    {
                        Debug.Log("Chua Win");
                        return false;
                    }
                }
            }

           
        }
        Debug.Log("Win");
        return true;

    }
}
