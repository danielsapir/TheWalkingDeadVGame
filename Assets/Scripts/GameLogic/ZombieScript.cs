using UnityEngine;
using UnityEngine.UI;

public class ZombieScript : MonoBehaviour
{
     private Camera m_MainCam;
     private Image m_DamageImg;

     private float m_MoveSpeed;

     private Animator m_ZombieAnimator;

     private Vector3 m_StartingPosition;
     private Vector3 m_EndingPostion;

     private bool m_IsHitted = false;
     private bool m_IsFirsttAttack = false;

     private float m_YOnFirstMove;
     private bool m_IsCollision = false;

     private Vector3 m_PositionOfAttack;

    private void Start()
    {
          m_MainCam = Camera.main;
          m_DamageImg = GameObject.Find("damged").GetComponent<Image>();
          m_MoveSpeed = Random.Range(0.7f, 1.5f);

          m_ZombieAnimator = GetComponent<Animator>();
          m_StartingPosition = transform.position;
          m_EndingPostion = m_MainCam.transform.position;
          m_EndingPostion.y = 0f;

          transform.GetComponent<Rigidbody>().transform.LookAt(m_MainCam.transform);
          m_YOnFirstMove = transform.position.y;
          m_DamageImg.enabled = false;
     }

    private void Update()
    {
          transform.LookAt(m_MainCam.transform);
          if (!m_IsHitted)
          {
               if (Mathf.Abs(transform.position.x - m_EndingPostion.x) >= 1.5 || Mathf.Abs(transform.position.z - m_EndingPostion.z) >= 1.5)
               {
                    Vector3 my_forward = transform.forward;
                    my_forward.y = 0;
                    transform.position += my_forward.normalized * m_MoveSpeed * Time.deltaTime;
               }
               else
               {
                    if(m_IsFirsttAttack == false)
                    {
                         m_IsFirsttAttack = true;
                         m_ZombieAnimator.SetTrigger("Attack");
                         m_PositionOfAttack = transform.position;
                    }
                    m_DamageImg.enabled = true;
                    LIFEScript.s_AttackTime += Time.deltaTime;
                    transform.position = m_PositionOfAttack;
               }

          }
          RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 100f);
          foreach (RaycastHit hit in hits)
          {
               if (hit.collider.gameObject == transform.gameObject)
                    continue;

               transform.position = hit.point;
               break;
          }
     }

     private void FixedUpdate()
     {
          if(m_IsCollision)
               transform.position = new Vector3(transform.position.x, m_YOnFirstMove, transform.position.z);
     }

     protected void LateUpdate()
     {
          transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
     }

     private void OnCollisionEnter(Collision collision)
     {
          m_IsCollision = true;
          m_YOnFirstMove = transform.position.y;

          if (collision.transform.name == "Bullet_45mm_Bullet(Clone)")
          {
               m_DamageImg.enabled = false;
               transform.GetComponent<CapsuleCollider>().enabled = false;
               if (!m_IsHitted)
                    KILLSScript.s_NumOfKills++;
               m_IsHitted = true;
               transform.GetChild(18).gameObject.SetActive(true);
               transform.GetChild(19).gameObject.SetActive(true);
               m_ZombieAnimator.SetTrigger("Hit");
               GetComponent<AudioSource>().Stop();
               Destroy(transform.gameObject, 0.8f);
          }
     }

     private void OnCollisionExit(Collision collision)
     {
          m_IsCollision = false;
     }
}
