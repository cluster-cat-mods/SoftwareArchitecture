using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace PlayerInteraction
{
    public class MouseClickController : MonoBehaviour
    {
        private Vector3 clickPosition;
    
        public UnityEvent<Vector3> onClickPosition;
        void Update() { 
            if (Input.GetMouseButtonDown(0)) 
            { 
                Ray mouseRay = Camera.main.ScreenPointToRay( Input.mousePosition ); 
                if (Physics.Raycast(mouseRay, out RaycastHit hitInfo )) { 
                    clickPosition = hitInfo.point; 
                
                    onClickPosition.Invoke(clickPosition);
                }
            }
        
            Debug.DrawLine(transform.position, clickPosition, Color.yellow);
            DebugExtension.DebugWireSphere(clickPosition, Color.blue, 1);

        }

    }
}

