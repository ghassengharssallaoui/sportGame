using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField] private int starIndex; // The index of this star (0–4)
    [SerializeField] private Sprite litSprite; // The sprite for the "lit" state
    [SerializeField] private Sprite unlitSprite; // The sprite for the "unlit" state

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private bool isLit = false; // Current state of the star (default is unlit)

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite(); // Ensure the correct sprite is displayed initially
    }
    private void OnEnable()
    {
        GameManager.Instance.OnStarsHit += StarScored;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStarsHit -= StarScored;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            int scoringPlayer = gameObject.CompareTag("PlayerOne") ? 1 : 2;
            GameManager.Instance.NotifyStarsHit(scoringPlayer, starIndex);
        }
    }
    private void StarScored(int scoredPlayer, int index)
    {
        ToggleState(scoredPlayer, index);
    }
    private void ToggleState(int scoredPlayer, int index)
    {
        // Only toggle if this star matches the index and belongs to the scoring player
        int thisStarBelongsTo = gameObject.CompareTag("PlayerOne") ? 1 : 2;
        if (starIndex == index && scoredPlayer == thisStarBelongsTo)
        {
            isLit = !isLit;
            UpdateSprite();
            GameManager.Instance.CheckAllStarsLit(scoredPlayer);
        }
    }

    public void ResetStar()
    {
        isLit = false;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = isLit ? litSprite : unlitSprite;
    }
    public bool GetIsLit() => isLit;
}
