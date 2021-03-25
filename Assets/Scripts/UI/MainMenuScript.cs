using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuScript : MonoBehaviour
{
     [SerializeField]
     public Slider m_LoadingSlider;

     [SerializeField]
     public GameObject m_Video;

     [SerializeField]
     public VideoPlayer m_VideoPlayer;

     [SerializeField]
     public GameObject m_UICamera;

     public static bool s_IsPressed = false;

     GameObject m_DiffBut;

     private void Start()
     {
          s_IsPressed = false;
          m_DiffBut = GameObject.Find("DIFFICULTYBut");
          m_DiffBut.GetComponentInChildren<Text>().text = "Difficulty: " + ZombieCreator.s_Difficulty.ToString();
     }

     private void Update()
     {
          if (!s_IsPressed)
          {
               if (Input.anyKeyDown)
               {
                    RaycastHit hit;
                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    if (Physics.Raycast(ray, out hit))
                         if (hit.collider != null)
                         {
                              switch (hit.transform.name)
                              {
                                   case ("PLAYNOWBut"):
                                        s_IsPressed = true;
                                        playVideo();
                                        break;
                                   case ("DIFFICULTYBut"):
                                        ZombieCreator.ChnageToNextDiff();
                                        m_DiffBut.GetComponentInChildren<Text>().text = "Difficulty: " + ZombieCreator.s_Difficulty.ToString();
                                        break;
                                   case ("HOWTOPLAYBut"):
                                        s_IsPressed = true;
                                        changeScenceWithSlider(3);
                                        break;
                                   case ("TOP10But"):
                                        s_IsPressed = true;
                                        changeScenceWithSlider(4);
                                        break;
                                   case ("EXITBut"):
                                        Application.Quit();
                                        break;
                              }
                         }
               }
          }
     }

     private void playVideo()
     {
          m_UICamera.SetActive(false);
          GameObject.Find("MenuCanvas").SetActive(false);
          GameObject.Find("HandGun").SetActive(false);
          GameObject.Find("AmbeintMusic").GetComponent<AudioSource>().volume = 0.2f;
          m_Video.SetActive(true);
          m_VideoPlayer.loopPointReached += videoDonePlaying;
     }

     private void videoDonePlaying(VideoPlayer i_VideoPlayer)
     {
          m_UICamera.SetActive(true);
          for (int i = 0; i < m_UICamera.transform.childCount; ++i)
          {
               m_UICamera.transform.GetChild(i).gameObject.SetActive(true);
          }
          changeScenceWithSlider(1);
     }

     private void changeScenceWithSlider(int i_SceneToChangeTo)
     {
          s_IsPressed = true;
          m_LoadingSlider.gameObject.SetActive(true);
          m_LoadingSlider.StartCoroutine(AsynchronousLoad(i_SceneToChangeTo));
     }

     IEnumerator AsynchronousLoad(int scene)
     {
          m_LoadingSlider.gameObject.SetActive(true);
          AsyncOperation scenceLoad = SceneManager.LoadSceneAsync(scene);
          scenceLoad.allowSceneActivation = false;

          while (!scenceLoad.isDone)
          {
               float progress = Mathf.Clamp01(scenceLoad.progress / 0.9f);
               m_LoadingSlider.value = progress;
               if (Mathf.Approximately(scenceLoad.progress,0.9f))
               {
                    scenceLoad.allowSceneActivation = true;
               }

               yield return null;
          }
     }
}
