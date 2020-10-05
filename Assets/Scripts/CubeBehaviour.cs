using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    int colorValue;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colorValue = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
    }

    //Change color on mouse click
    private void OnMouseDown()
    {
        if (colorValue == 1)
        {
            colorValue = 0;
        }
        else
        {
            colorValue = 1;
        }
    }

    //Update the cube color
    public void SetColor()
    {
        if (colorValue == 0)
        {
            meshRenderer.material.color = Color.black;
        }
        else
        {
            meshRenderer.material.color = Color.white;
        }
    }

    public int ColorValue
    {
        get
        {
            return colorValue;
        }
        set
        {
            colorValue = value;
        }
    }
}
