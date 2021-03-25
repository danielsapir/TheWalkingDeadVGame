using System;
using UnityEngine;
using UnityEngine.UI;

public class EndOfGameScript : MonoBehaviour
{
     [SerializeField]
     public Text m_Text;

     void Start()
    {
          if(KILLSScript.s_NumOfKills < KILLSScript.s_NumberOfZombies)
          {
               m_Text.text = "YOU DIED" + Environment.NewLine + "And you killed " + KILLSScript.s_NumOfKills + " zombies";
          }
          else
          {
               m_Text.text = "You killed all the zombies!";
          }
     }
}
