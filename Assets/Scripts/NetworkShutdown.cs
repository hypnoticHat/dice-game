using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkShutdown : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
        });
    }

}
