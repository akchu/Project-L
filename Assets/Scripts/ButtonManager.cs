using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonManager : BoardManager 
{
	public Button Roll;

	public Text text=null ;
	public int Dno=0;
	void Start()
	{
		Button btn = Roll.GetComponent<Button>();
		btn.onClick.AddListener (ClickTask);

	}

	// Update is called once per frame
	void ClickTask ()
	{
		int d1 = Random.Range (1, 7);
		int d2 = Random.Range (1, 7);
		int Dno = (d1 + d2) / 2;
		text.text = ""+Dno;
//		PlayerMovement (Dno);

	}
}

