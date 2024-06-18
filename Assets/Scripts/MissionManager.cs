using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public int Money; // ���������� �����;
    public Texture2D Coin;
    public Texture2D MissionIcon1; //������ ������ � ����������� 1 � 2
    public Texture2D MissionIcon2;
    public Texture2D MissionIcon;
    public string LastAction = "";
    public bool MissionInformationOnScreen = false;
    public int MissionID;
    private OpenInventory OI;
    public bool IsInventoryOpen = false;

    public List<string> MissionsInProgress = new List<string>();
    public List<string> MissionsPriority = new List<string>();

    void Start()
    {
        OI = GameObject.FindGameObjectWithTag("InvCanvas").GetComponent<OpenInventory>();
    }

    void Update()
    {
        IsInventoryOpen = OI.OpenInventoryCheck;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(27, 57, 1000, 30), "" + Money);
        GUI.Label(new Rect(5, 55, 25, 25), Coin);

        if (!IsInventoryOpen)
        {
            foreach (string mission in MissionsInProgress)
            {
                if (MissionsPriority[MissionsInProgress.IndexOf(mission)] == "1")
                {
                    MissionIcon = MissionIcon1;
                }

                if (MissionsPriority[MissionsInProgress.IndexOf(mission)] == "2")
                {
                    MissionIcon = MissionIcon2;
                }

                if (GUI.Button(new Rect(5, 80 + 25 * MissionsInProgress.IndexOf(mission), 30, 30), MissionIcon, "Label") || GUI.Button(new Rect(35, 85 + 25 * MissionsInProgress.IndexOf(mission), 200, 25), mission + "\n", "Label"))
                {
                    MissionInformationOnScreen = true;
                    MissionID = MissionsInProgress.IndexOf(mission);
                    break;
                }

                if (MissionInformationOnScreen == true && MissionID == MissionsInProgress.IndexOf(mission)) // ����� ���������� � ������
                {
                    GUI.Box(new Rect((Screen.width - 300) / 2, (Screen.height - 300) / 2, 300, 300), mission);
                    if (GUI.Button(new Rect((Screen.width - 100) / 2 - 25, (Screen.height - 300) / 2 + 250, 150, 40), "�������"))
                    {
                        MissionInformationOnScreen = false;
                    }
                }
            }
        }
        GUI.Label(new Rect(5, Screen.height - 25, 1000, 25), LastAction);
    }
}
