using UnityEngine;
using TMPro;

public class UILivesText : MonoBehaviour
{
    private TextMeshProUGUI tmProText;

    private void Awake()
    {
        tmProText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnLivesChanged += HandleOnLivesChanged;
        tmProText.text = GameManager.Instance.Lives.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLivesChanged -= HandleOnLivesChanged;
    }

    private void HandleOnLivesChanged(int livesRemaining)
    {
        tmProText.text = livesRemaining.ToString();
    }
}