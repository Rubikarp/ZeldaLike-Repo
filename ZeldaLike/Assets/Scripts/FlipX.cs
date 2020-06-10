using UnityEngine;

public class FlipX : MonoBehaviour
{
    public SpriteRenderer Lesprite;
    private void OnEnable()
    {
        Lesprite.flipX = true;
    }

    private void OnDisable()
    {
        Lesprite.flipX = false;
    }
}
