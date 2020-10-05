using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



    public class JoystickBehavior : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public GameObject joystickchild;
        private Vector3 pos;
        private Vector3 dir = new Vector3(); 
        private float radius;

        private void Start()
        {
        radius = transform.localScale.x*20000/Screen.height;        //pour ne pas que joystickchild "sorte" du joystick;
        }

        public void OnDrag(PointerEventData eventData)
        {
        radius = 0.06f*Screen.width;
        pos = eventData.position;                           //position du doigt
            if ((pos - transform.position).magnitude < radius)      // si le doigt est dans la zone du joystick
            {
                joystickchild.transform.position = pos;
                dir[0] = (joystickchild.transform.position.x - transform.position.x) * Time.deltaTime / Screen.width;
                dir[2] = (joystickchild.transform.position.y - transform.position.y) * Time.deltaTime / Screen.width;
            }
            else                                                   //sinon, joystickchild doit être le plus proche possible, Thalès.
            {
                float y1 = (pos.y - transform.position.y) * radius / ((pos - transform.position).magnitude);
                float x1 = (pos.x - transform.position.x) * radius / ((pos - transform.position).magnitude);
                joystickchild.transform.position = new Vector3(transform.position.x + x1, transform.position.y + y1, 0);
                dir[0] =  (joystickchild.transform.position.x - transform.position.x) * Time.deltaTime/Screen.width;
                dir[2] =  (joystickchild.transform.position.y - transform.position.y) * Time.deltaTime/Screen.width;
            }
        }

        public void OnEndDrag(PointerEventData eventData)           //doigt relâché, joystickchild retourne à sa position initiale
        {
            joystickchild.transform.position = transform.position;
            dir[0] = 0;
            dir[2] = 0;                             //le joueur n'avance plus
        }
        
        public Vector3 GetJoystickValues()
        {
            return dir;
        }

    }
