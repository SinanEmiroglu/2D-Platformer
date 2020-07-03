using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Lives { get; private set; }

    public event Action<int> OnLivesChanged;

    public event Action<int> OnCoinsChanged;

    private int coin;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Restart();
        }
    }

    internal void KillPlayer()
    {
        Lives--;
        if (OnLivesChanged != null)
        {
            OnLivesChanged(Lives);
        }
        if (Lives <= 0)
            Restart();
        else
            SendPlayerToCheckpoint();
    }

    private void SendPlayerToCheckpoint()
    {
        var checkpointManager = FindObjectOfType<CheckpointManager>();

        var checkpoint = checkpointManager.GetLastCheckpointThatWasPassed();

        var player = FindObjectOfType<PlayerMovementController>();

        player.transform.position = checkpoint.transform.position;
    }

    internal void AddCoin()
    {
        coin++;

        if (OnCoinsChanged != null)
        {
            OnCoinsChanged(coin);
        }
    }

    private void Restart()
    {
        if (Lives <= 0)
        {
            Lives = 3;
            coin = 0;
            if (OnCoinsChanged != null)
            {
                OnCoinsChanged(coin);
            }
            SceneManager.LoadScene(0);
        }
    }
}