using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tube : MonoBehaviour , IPointerDownHandler
{
    // Tube 5 ball
    public GameObject listPosGO;
    public GameObject listBallGO;
    public List<GameObject> balls = new List<GameObject>();
    public Vector3 pointStartMove => listPosGO.transform.GetChild(4).transform.position + new Vector3(0,1,0);
    public bool isFullTube => balls[4] != null;

    public bool isTubeNull => balls[0] == null;

    // Start is called before the first frame update
    void Awake()
    {

        AwakeInitTube();

    }

    void Start()
    {
    }
    void AwakeInitTube()
    {
        for (int i = 0; i < 5; i++)
        {
            balls.Add(null);
        }
    }    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitTube()
    {
        for (int i = 0; i < listBallGO.transform.childCount; i++)
        {
            balls[i] = listBallGO.transform.GetChild(i).gameObject;
        }
        
        if (balls.Count > listBallGO.transform.childCount)
        {
            for (int i = balls.Count - 1; i > listBallGO.transform.childCount - 1; i--)
            {
                balls[i] = null;
            }
        } 
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.instance.currentTube == null)
        {
            if( !isTubeNull )
            {
                GameManager.instance.currentTube = this;
                BallMoveUp(GetBall(), 0.2f);
            }    
           
        }    
        else
        {
            GameObject currentBall = GameManager.instance.currentTube.GetBall();
            if(GameManager.instance.currentTube == this || this.isFullTube || (!this.isTubeNull && currentBall.GetComponent<Image>().sprite != this.GetBall().GetComponent<Image>().sprite))
            {
                BallMoveDownReset(currentBall, 0.2f);
            }
            else
            {
                BallMoveToNewTube(currentBall,0.2f);
            }
        }
    }
    void BallMoveDownReset(GameObject ball, float time)
    {
        int indexOfBall = GameManager.instance.currentTube.balls.IndexOf(ball);
        ball.transform.DOMove(GameManager.instance.currentTube.listPosGO.transform.GetChild(indexOfBall).transform.position, time).OnComplete(() => {

            GameManager.instance.currentTube = null;
        
        });
    }
    void BallMoveToNewTube(GameObject ball, float time)
    {
        ball.transform.DOMove(pointStartMove, time/2).OnComplete(() => {

            ball.transform.DOMove(listPosGO.transform.GetChild(GetIndexBallNull()).transform.position, time / 2).OnComplete(() =>{

                ball.transform.SetParent(this.listBallGO.transform);

                GameManager.instance.currentTube.InitTube();
                this.InitTube();

                GameManager.instance.currentTube = null;

                if(this.isFullTube)
                {
                    GameManager.instance.CheckWin();

                }


            });

        
        });
    }
    void BallMoveUp(GameObject ball , float time)
    {
        ball.transform.DOMove(pointStartMove, time);
    }    
    GameObject GetBall()
    {
        for (int i = balls.Count-1; i>=0; i--)
        {
            if (balls[i]!=null)
            {
                return balls[i];
            }    
        }
        return null;
    }    
    int GetIndexBallNull()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
}
