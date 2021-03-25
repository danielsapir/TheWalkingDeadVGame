using System;
using UnityEngine;

public class ZombieCreator : MonoBehaviour
{
     [Flags]
     public enum eDifficulty
     {
          easy = 5,
          intermidiate = 2,
          hard = 1
     }

     [SerializeField]
     public GameObject[] m_Zombies;

     [SerializeField]
     public GameObject m_ZombiesInst;

     private int m_MinDistance = -50;
     private int m_MaxDistance = 50;

     public static eDifficulty s_Difficulty = eDifficulty.easy;

     private float m_LastInstitateTime = 0f;
     private int m_NumOfZombiesMade = 0;

     private void Start()
     {
          placeZombies();
          m_NumOfZombiesMade += KILLSScript.s_NumberOfZombies / (int)s_Difficulty;
     }

     private void Update()
     {
          if (m_LastInstitateTime + 15 <= Time.timeSinceLevelLoad)
          {
               if (m_NumOfZombiesMade != KILLSScript.s_NumberOfZombies)
               {
                    placeZombies();
                    m_NumOfZombiesMade += KILLSScript.s_NumberOfZombies / (int)s_Difficulty;
                    m_LastInstitateTime = Time.timeSinceLevelLoad;
               }
          }
     }

     private void placeZombies()
     {
          for (int i = 0; i < KILLSScript.s_NumberOfZombies / (int)s_Difficulty; i++)
          {
               Instantiate(m_Zombies[UnityEngine.Random.Range(0, m_Zombies.Length)], generatedPosition(), Quaternion.identity, m_ZombiesInst.transform);
          }
     }

     private Vector3 generatedPosition()
     {
          int x, z;
          x = UnityEngine.Random.Range(m_MinDistance, m_MaxDistance);
          z = UnityEngine.Random.Range(m_MinDistance, m_MaxDistance);
          return new Vector3(x, 2.5f, z);
     }

     public static void ChnageToNextDiff()
     {
          if(s_Difficulty == eDifficulty.easy)
          {
               s_Difficulty = eDifficulty.intermidiate;
          }
          else if(s_Difficulty == eDifficulty.intermidiate)
          {
               s_Difficulty = eDifficulty.hard;
          }
          else
          {
               s_Difficulty = eDifficulty.easy;
          }
     }
}
