using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    float minX=0f;
    float maxX =14f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float mousePositionInUnits= Input.mousePosition.x/Screen.width * screenWidthInUnits;
        Vector2 paddlePosition= new Vector2(Mathf.Clamp(mousePositionInUnits,minX,maxX),transform.position.y);
        transform.position = paddlePosition;
    }
}
