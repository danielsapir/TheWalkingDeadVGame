using System.Collections;
using UnityEngine;
using TalesFromTheRift;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyboardWithRayCastScript : MonoBehaviour
{
     [SerializeField]
     public OpenCanvasKeyboard m_OpenCanvasKeyboard;

     [SerializeField]
     public Button m_NameHolder;

     [SerializeField]
     public Slider m_LoadingSlider;

    private void Update()
    {
          if (Input.anyKeyDown)
          {
               RaycastHit hit;
               Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
               if (Physics.Raycast(ray, out hit))
                    if (hit.collider != null)
                    {
                         if (hit.transform.name == "NameHolder")
                         {
                              m_NameHolder.GetComponentInChildren<Text>().text = "";
                              m_OpenCanvasKeyboard.OpenKeyboard();
                         }
                         else if (hit.transform.name == "DONE")
                         {
                              TOP10.InsertToValueToTOP10Arr(KILLSScript.s_NumOfKills, m_NameHolder.GetComponentInChildren<Text>().text);
                              StartCoroutine(AsynchronousLoad(0));
                         }
                         else
                         {
                              if (GameObject.Find("CanvasKeyboard(Clone)") != null)
                              {
                                   CanvasKeyboardASCII keyboard = GameObject.FindObjectOfType<CanvasKeyboardASCII>();
                                   keyboard.OnKeyDown(hit.transform.gameObject);
                              }
                         }
                    }
          }
     }

     IEnumerator AsynchronousLoad(int i_Scene)
     {
          m_LoadingSlider.gameObject.SetActive(true);
          AsyncOperation scenceLoad = SceneManager.LoadSceneAsync(i_Scene);
          scenceLoad.allowSceneActivation = false;

          while (!scenceLoad.isDone)
          {
               float progress = Mathf.Clamp01(scenceLoad.progress / 0.9f);
               m_LoadingSlider.value = progress;
               if (Mathf.Approximately(scenceLoad.progress, 0.9f))
               {
                    scenceLoad.allowSceneActivation = true;
               }

               yield return null;
          }
     }
}


