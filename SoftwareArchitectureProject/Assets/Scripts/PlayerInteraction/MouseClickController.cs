using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace PlayerInteraction
{
    public class MouseClickController : MonoBehaviour
    {
        [SerializeField] private float minClickDistance;
        public UnityEvent<Vector3> onClickPosition;
        public UnityEvent onClickSuccessful;
        public UnityEvent onClickUnsuccessful;
        
        private Vector3 clickPosition;
        
        void Update() 
        { 
            if (Input.GetMouseButtonDown(0)) 
            { 
                Ray mouseRay = Camera.main.ScreenPointToRay( Input.mousePosition ); 
                if (Physics.Raycast(mouseRay, out RaycastHit hitInfo )) 
                {
                    if (hitInfo.collider.gameObject.GetType() == typeof(ClickEvent))
                    {
                        onClickSuccessful.Invoke();
                        ClickEvent clickEvent = hitInfo.collider.GetComponent<ClickEvent>();
                        clickEvent.OnClick.Invoke();
                    }
                    clickPosition = hitInfo.point; 
                
                    onClickPosition.Invoke(clickPosition);
                }
            }
        
            Debug.DrawLine(transform.position, clickPosition, Color.yellow);
            DebugExtension.DebugWireSphere(clickPosition, Color.blue, 1);

        }

    }
}

