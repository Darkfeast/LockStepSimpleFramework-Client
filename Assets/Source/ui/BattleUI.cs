//
// @brief: 战斗UI
// @version: 1.0.0
// @author helin
// @date: 03/7/2018
// 
// 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

    public Button btnStartBattle;
    public Button btnSendSoldier;
    public Button btnEndBattle;
    public Button btnReplay;
    public Button btnAdjustSpeed;
    public Text textResult;
    public static string m_scServerInfo = "";

    public BattleLogicMonoBehaviour monoBehaviour;

	// Use this for initialization
	void Start () {
        btnStartBattle = btnStartBattle.GetComponent<Button>();
        btnSendSoldier = btnSendSoldier.GetComponent<Button>();
        btnEndBattle = btnEndBattle.GetComponent<Button>();
        btnReplay = btnReplay.GetComponent<Button>();
        var txtAdjustSpeed = btnAdjustSpeed.transform.Find("Text").GetComponent<Text>();
        btnAdjustSpeed = btnAdjustSpeed.GetComponent<Button>();
        textResult = textResult.GetComponent<Text>();

        var battleLogic = monoBehaviour.getBattleLogic();
        

        //开始战斗
        btnStartBattle.onClick.AddListener(delegate ()
        {
            textResult.text = "校验结果:--";
            GameData.g_bRplayMode = false;
            battleLogic.startBattle();
        });

        //出兵
        btnSendSoldier.onClick.AddListener(delegate ()
        {
            battleLogic.createSoldier();
        });

        //停止战斗
        btnEndBattle.onClick.AddListener(delegate ()
        {
            battleLogic.stopBattle();
        });

        //回放战斗录像
        btnReplay.onClick.AddListener(delegate ()
        {
            battleLogic.setBattleRecord(UnityTools.playerPrefsGetString("battleRecord"));
            battleLogic.replayVideo();
        });

        //调整战斗速度
        btnAdjustSpeed.onClick.AddListener(delegate ()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 2;
                txtAdjustSpeed.text = "x2";
            }
            else if (Time.timeScale == 2)
            {
                Time.timeScale = 4;
                txtAdjustSpeed.text = "x4";
            }
            else if (Time.timeScale == 4)
            {
                Time.timeScale = 1;
                txtAdjustSpeed.text = "x1";
            }
        });
    }
	
	// Update is called once per frame
	void Update () {
        if (m_scServerInfo != "")
        {
            if (BattleLogic.s_uGameLogicFrame == int.Parse(m_scServerInfo))
            {
                textResult.text = "校验成功";
            }
            else
            {
                textResult.text = "校验失败";
            }

            m_scServerInfo = "";
        }
	}
}
