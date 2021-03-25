using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LIFEScript : MonoBehaviour
{
     [SerializeField]
     public Image[] m_LifeArr = new Image[5];

     [SerializeField]
     public AudioSource m_ScreamingSound;

     public static float s_AttackTime = 0f;

     bool m_Died = false;

     private void Start()
     {
          KILLSScript.s_NumOfKills = 0;
          s_AttackTime = 0f;
          foreach (Image life in m_LifeArr)
          {
               life.enabled = true;
          }
     }

    private void Update()
    {
          if(s_AttackTime >= 2f && s_AttackTime < 4f)
          {
               m_LifeArr[4].enabled = false;
          }
          if (s_AttackTime >= 4f && s_AttackTime < 6f)
          {
               m_LifeArr[3].enabled = false;
          }
          if (s_AttackTime >= 6f && s_AttackTime < 8f)
          {
               m_LifeArr[2].enabled = false;
          }
          if (s_AttackTime >= 8f && s_AttackTime < 10f)
          {
               m_LifeArr[1].enabled = false;
          }
          if(s_AttackTime >= 10f)
          {
               m_LifeArr[0].enabled = false;
               if(!m_Died)
               {
                    m_ScreamingSound.Play();
                    m_Died = true;
                    SceneManager.LoadScene(2);
               }
          }
     }
}
