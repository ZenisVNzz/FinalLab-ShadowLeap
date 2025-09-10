using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private List<StageCamera> cameras;

    private void Awake()
    {
        foreach (var stageCamera in cameras)
        {
            stageCamera.checkpoint.OnCheckpointReach += () =>
            {
                foreach (var cam in cameras)
                {
                    cam.virtualCamera.Priority = 0;
                }
                stageCamera.virtualCamera.Priority = 10;
            };
        }
    }
}

[Serializable]
public class StageCamera
{
    public CinemachineCamera virtualCamera;
    public Checkpoint checkpoint;
}