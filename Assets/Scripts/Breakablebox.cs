using UnityEngine;

public class Breakablebox : MonoBehaviour, ITakeShellHits
{
    public void HandleShellHits(ShellFlipped shellFlipped)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.WasHitByPlayer() &&
            collision.WasBottom())
        {
            Destroy(gameObject);
        }
    }
}