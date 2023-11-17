using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible
{
    public string Name { get; }
    public void Collect();
}
