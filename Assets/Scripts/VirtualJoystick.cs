using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;





public class VirtualJoystick : MonoBehaviour, IDragHandler , IPointerUpHandler , IPointerDownHandler {

    private Image bgImage;
    private Image joystickImage;
    private Vector3 inputVector;

    private void Start()
    {
       
        bgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, ped.position, ped.pressEventCamera, out position))
        {
            position.x = (position.x / bgImage.rectTransform.sizeDelta.x);
            position.y = (position.y / bgImage.rectTransform.sizeDelta.y);

            inputVector = new Vector3(position.x * 2 + 1, 0, position.y * 2 - 1);

            //normailize
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //Move Joystick Image
            joystickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / 3), inputVector.z * (bgImage.rectTransform.sizeDelta.y / 3));

        }

    }
	public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        return Input.GetAxisRaw("Horizontal");
    }


    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        return Input.GetAxisRaw("Vertical");
    }


   
}
