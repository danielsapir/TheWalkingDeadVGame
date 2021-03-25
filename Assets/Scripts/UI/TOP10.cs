using UnityEngine;
using UnityEngine.UI;

public class TOP10 : MonoBehaviour
{
     public struct UserScoreAndName
     {
          public string m_Name;
          public int m_Score;
     }

     private Text m_Top10Text;

     public static UserScoreAndName[] s_Top10Arr = new UserScoreAndName[10];

     private void Start()
     {
          m_Top10Text = GetComponent<Text>();
          printTop10TableToScreen();
     }

     private void Update()
     {
          if (Input.anyKeyDown)
          {
               RaycastHit hit;
               Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
               if (Physics.Raycast(ray, out hit))
                    if (hit.collider != null)
                    {
                         if (hit.transform.name == "RESETBut")
                         {
                              PlayerPrefs.DeleteAll();
                              printTop10TableToScreen();
                         }
                    }
          }
     }

     private void printTop10TableToScreen()
     {
          GetTOP10Arr();
          m_Top10Text.text = "";
          if (s_Top10Arr[0].m_Score == 0)
          {
               m_Top10Text.text += "No top scores yet, go ahead and be the first to complete the game!";
          }
          for (int i = 0; i < 10; i++)
          {
               if (s_Top10Arr[i].m_Score != 0)
               {
                    if (i != 9)
                    {
                         m_Top10Text.text += (i + 1) + ". " + s_Top10Arr[i].m_Name + "\t" + s_Top10Arr[i].m_Score + "\n";
                    }
                    else
                    {
                         m_Top10Text.text += (i + 1) + "." + s_Top10Arr[i].m_Name + "\t" + s_Top10Arr[i].m_Score + "\n";
                    }
               }
          }
     }

     public static UserScoreAndName[] GetTOP10Arr()
     {
          for (int i = 0; i < 10; i++)
          {
               if (PlayerPrefs.GetString("name" + i) == null)
               {
                    s_Top10Arr[i].m_Name = "";
               }
               else
               {
                    s_Top10Arr[i].m_Name = PlayerPrefs.GetString("name" + i);
               }
               s_Top10Arr[i].m_Score = PlayerPrefs.GetInt("score" + i);
          }
          return s_Top10Arr;
     }

     public static void InsertToValueToTOP10Arr(int i_NewScore, string i_NewName)
     {
          UserScoreAndName newUser;
          newUser.m_Score = i_NewScore;
          newUser.m_Name = i_NewName;
          s_Top10Arr[9] = newUser;
          for (int i = 8; i >= 0; i--)
          {
               if (i_NewScore > s_Top10Arr[i].m_Score)
               {
                    SwapUsers(ref s_Top10Arr[i], ref s_Top10Arr[i + 1]);
               }
          }
          for (int i = 0; i < 10; i++)
          {
               PlayerPrefs.SetString("name" + i, s_Top10Arr[i].m_Name);
               PlayerPrefs.SetInt("score" + i, s_Top10Arr[i].m_Score);
          }
     }

     public static void SwapUsers(ref UserScoreAndName io_User1, ref UserScoreAndName io_User2)
     {
          UserScoreAndName temp;
          temp = io_User1;
          io_User1 = io_User2;
          io_User2 = temp;
     }
}
