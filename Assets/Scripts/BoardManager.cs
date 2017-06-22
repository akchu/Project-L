using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour 
{
	public Ludor[,] Players{set; get;}
	private Ludor selectedplayer;
	private Ludor selectedplayer1;
	private Ludor selectedplayer2;
	private Ludor selectedplayer3;
	private Ludor selectedplayer4;

	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> playerPrefabs;
	private List<GameObject> activePlayer;

	public bool isPlayer1Turn = true;


//	private int firstsix = 0;
	//private Quaternion orientation = Quaternion.Euler(0,180,0);

	private void Start()
	{
		ButtonManager bm = UnityEngine.Component.gameObject.get<ButtonManager>();
		SpawnAllPlayers ();

	}

	private void SpawnAllPlayers()
	{
		activePlayer = new List<GameObject> ();
		Players = new Ludor[15, 15];
		//player 1
		SpawnPlayer (0, 0, 0);
		SpawnPlayer (1, 1, 0);
		SpawnPlayer (2, 0, 1);
		SpawnPlayer (3, 1, 1);

		//although only one player will spawn on joining 
		//player 2
//		SpawnPlayer (0, 14, 14);
//		SpawnPlayer (1, 13, 14);
//		SpawnPlayer (2, 14, 13);
//		SpawnPlayer (3, 13, 13);

		//player 3
//		SpawnPlayer (0, 0, 14);
//		SpawnPlayer (1, 1, 14);
//		SpawnPlayer (2, 0, 13);
//		SpawnPlayer (3, 1, 13);

		//player 4
//		SpawnPlayer (0, GetTileCenter (14, 0));
//		SpawnPlayer (1, GetTileCenter (14, 1));
//		SpawnPlayer (2, GetTileCenter (13, 0));
//		SpawnPlayer (3, GetTileCenter (13, 1));


		//WaY
//		SpawnPlayer (5, 7, 7);
//
//		for (int i = 0; i < 5; i++) 
//		{
//			SpawnPlayer (4, 7, (i + 1));
//			SpawnPlayer (4, 7, (i + 9));
//			SpawnPlayer (4, (i+1), 7);
//			SpawnPlayer (4, (i+9), 7);
//		}
	}

	private Vector3 GetTileCenter(int x,int y)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * y) + TILE_OFFSET;
		return origin;
	}


	private void Update()
	{
		DrawBoard (); 
//		if (ButtonManager.Dno != 6 && firstsix == 0) 
//		{
//			firstsix = 1;
//			while(
//		}
		if (bm.Dno == 6) 
		{
			if (Input.GetMouseButton (0)) 
			{
				if (selectionX >= 0 && selectionY >= 0) 
				{
					if (Players [selectionX, selectionY].isAlive = false)
						Players [selectionX, selectionY].isAlive = true;
					MovePlayer (1, 6);
				}
			}
		}

		else 
		{
			if(selectedplayer1.isAlive)
				MovePlayer(1,+6);
				
		}

		UpdateSelection (); 
		 
		PlayerMovement ();
	}

	public void PlayerMovement()
	{
		if (Input.GetMouseButton (0)) 
		{
			if (selectionX >= 0 && selectionY >= 0) 
			{
				SelectPlayer (selectionX, selectionY);
				MovePlayer (selectionX, selectionY);
			}
		}
	}

	private void SelectPlayer(int x,int y)
	{
//		if (Players [x, y] == null)
//			return;

		//to start match with player 1 always
		if (Players [x, y].isPlayer1 != isPlayer1Turn)
			return;
		selectedplayer = Players [x, y];
	}

	private void MovePlayer(int x,int y)
	{
		//to get dice number to move player
//		ButtonManager sn = gameObject.GetComponent<ButtonManager> ();
//		int DiceNum = sn.Dno;

		if(selectedplayer.PossibleMove(x,y))
		{
			Players[selectedplayer.CurrentX,selectedplayer.CurrentY]=null;	
			selectedplayer.transform.position= GetTileCenter(x,y);
			Players[x,y] = selectedplayer;
		}
		selectedplayer = null;
	
	}

	private void UpdateSelection()
	{
		if (!Camera.main)
			return;
		
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay (Input.mousePosition),out hit,25.0f,LayerMask .GetMask("LudoPlane")))
		{
				selectionX = (int)hit.point.x;
				selectionY = (int)hit.point.z;
		}
		else
		{
				selectionX = -1;
				selectionY = -1;
		}
	}


	private void SpawnPlayer(int index,int x,int y)
	{
		GameObject go = Instantiate (playerPrefabs [index], GetTileCenter(x,y), Quaternion.identity ) as GameObject;
		go.transform.SetParent (transform);
		Players [x, y] = go.GetComponent<Ludor> ();
		Players [x, y].SetPosition (x, y);
		activePlayer.Add (go);
	}
	 

	private void DrawBoard()
	{
		Vector3 widthLine = Vector3.right * 15;
		Vector3 heightLine = Vector3.forward * 15;

		for (int i = 0; i <= 15; i++) 
		{
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);
			for (int j = 0; j <= 15; j++) 
			{
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine);
			}
		}
		//dRAW THE sELCTION
		if (selectionX >= 0 && selectionY >= 0) 
		{
			Debug.DrawLine (
				Vector3.forward * selectionY + Vector3.right * selectionX,
				Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
			
			Debug.DrawLine (
				Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
				Vector3.forward * selectionY  + Vector3.right * (selectionX + 1));
		}

	}
}
