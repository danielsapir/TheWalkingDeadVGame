using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlaySctipt : MonoBehaviour
{
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
                         if(hit.transform.name == "BACKTOMAINMENUBut")
                              StartCoroutine(AsynchronousLoad(0));
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
