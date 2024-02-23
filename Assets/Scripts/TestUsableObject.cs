using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUsableObject : MonoBehaviour, IUsable
{
    private Renderer m_renderer;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    public void Use()
    {
        m_renderer.material.color = Color.green;
        var collider = GetComponent<Collider>();
        collider.enabled = false;
    }

}
