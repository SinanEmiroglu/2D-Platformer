using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private IMove mover;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mover = GetComponent<IMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float Speed = mover.Speed;
        animator.SetFloat("Speed", Mathf.Abs(Speed));

        if (Speed != 0)
        {
            spriteRenderer.flipX = Speed > 0;
        }
    }
}