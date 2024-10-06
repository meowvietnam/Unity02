using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
   public List<string> tubeData = new List<string>();
   public void CreateLevel(ref List<Tube> tubes)
   {
		for (int i = 0; i < tubeData.Count; i++)
		{
			Tube tube = Instantiate(GameManager.instance.prefabTube, GameManager.instance.listTubeGO.transform).GetComponent<Tube>();
			tubes.Add(tube);
			int[] idBalls = Array.ConvertAll(tubeData[i].Split(','),int.Parse);

			// create ball
			for (int j = 0; j < idBalls.Length; j++)
			{
				if (idBalls[j] == -1) continue;
				// tạo bóng
				Instantiate(GameManager.instance.prefabBall, tube.listPosGO.transform.GetChild(j).position,Quaternion.identity,tube.listBallGO.transform).GetComponent<Image>().sprite = GameManager.instance.spriteBalls[idBalls[j]] ;

			}
			tube.InitTube();
		}
   }    
}
