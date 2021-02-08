using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    ManagerStatus Status { get; }
    void Initialize();
}

public enum ManagerStatus 
{ 
    Shutdown,
    Initializing,
    Started
}
