using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KILLSScript : MonoBehaviour
{
     [SerializeField]
     public Text m_KillsText;

     public static int s_NumberOfZombies = 50;
     public static int s_NumOfKills = 0;

     private void Start()
     {
          m_KillsText.text = "KILLS: " + s_NumOfKills.ToString();
     }

     private void Update()
     {
          m_KillsText.text = "KILLS: " + s_NumOfKills.ToString();
          if(s_NumOfKills == s_NumberOfZombies)
          {
               SceneManager.LoadScene(2);
          }
     }
}
