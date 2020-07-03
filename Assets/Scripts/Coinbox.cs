using UnityEngine;

public class Coinbox : MonoBehaviour, ITakeShellHits
{
    [SerializeField]
    private SpriteRenderer enabledCoin;

    [SerializeField]
    private SpriteRenderer disabledCoin;

    [SerializeField]
    private int totalCoins = 1;

    private Animator animator;
    private int remainingCoins;

    public void HandleShellHits(ShellFlipped shellFlipped)
    {
        if (remainingCoins > 0)
        {
            TakeCoin();
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        remainingCoins = totalCoins;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (remainingCoins > 0 &&
            collision.WasHitByPlayer() &&
            collision.WasBottom())
        {
            TakeCoin();
        }
    }

    private void TakeCoin()
    {
        GameManager.Instance.AddCoin();
        animator.SetTrigger("FlipCoin");
        remainingCoins--;
        if (remainingCoins <= 0)
        {
            enabledCoin.enabled = false;
            disabledCoin.enabled = true;
        }
    }
}