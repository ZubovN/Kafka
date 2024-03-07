using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResetButtonSelection : MonoBehaviour, IPointerClickHandler
{
    public Button button;

    public void OnPointerClick(PointerEventData eventData)
    {
        button.OnDeselect(null); // Сброс выделения кнопки
    }
}