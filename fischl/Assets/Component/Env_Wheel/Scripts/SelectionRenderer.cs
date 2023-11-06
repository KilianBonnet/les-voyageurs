using UnityEngine;
using UnityEngine.Events;

public class SelectionRenderer : MonoBehaviour
{
    [SerializeField] Sprite unselectedSprite;
    [SerializeField] Sprite selectedSprite;
    private SpriteRenderer spriteRenderer;
    private bool isSelected;

    public static UnityEvent ResetEvent = new UnityEvent();

    private void Start() {
        ResetEvent.AddListener(OnReset);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unselectedSprite;
    }

    public int OnClick(){
        spriteRenderer.sprite = isSelected ? unselectedSprite : selectedSprite;
        isSelected = !isSelected;
        return isSelected ? 1 : -1;
    }

    private void OnReset() {
        isSelected = false;
        spriteRenderer.sprite = unselectedSprite;
    }
}
