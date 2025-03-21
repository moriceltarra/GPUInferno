using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimatedCursor : MonoBehaviour
{
    public Texture2D[] cursorFrames; // Assign your cursor frames in the Inspector
    public float frameRate = 0.1f;  // Time between frames
    public bool pause=false;

    private int currentFrame;
    private float timer;

    void Start()
    {

        Cursor.SetCursor(cursorFrames[0], new Vector2(16, 16), CursorMode.Auto); // Set initial cursor
        StartCoroutine("changeCursor");
    }
    void Update()
    {
        if(pause){
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }   
    }

    private IEnumerator changeCursor()
    {
        while (true)
        {
            for (int i = 0; i < cursorFrames.Length; i++)
            {
                if(!pause){
                    Cursor.SetCursor(cursorFrames[i], new Vector2(100, 100), CursorMode.Auto);
                }else{
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    Debug.Log("cursor");
                }
                yield return new WaitForSeconds(frameRate); 
            }
        }
    }
    public void setPause(bool isPause){
        pause=isPause;
    }
}
