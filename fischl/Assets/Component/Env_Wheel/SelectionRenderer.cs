using UnityEngine;

public class SelectionRenderer : MonoBehaviour
{
    [SerializeField] Sprite unselectedSprite;
    [SerializeField] Sprite selectedSprite;
    private SpriteRenderer spriteRenderer;
    private bool isSelected;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unselectedSprite;
    }

    public void OnClick(){
        spriteRenderer.sprite = isSelected ? unselectedSprite : selectedSprite;
        isSelected = !isSelected;
    }
}
