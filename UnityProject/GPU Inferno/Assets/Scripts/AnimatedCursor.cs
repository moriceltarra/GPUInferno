using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCursor : MonoBehaviour
{
   public Texture2D[] cursorFrames; // Assign your cursor frames in the Inspector
    public float frameRate = 0.1f;  // Time between frames

    private int currentFrame;
    private float timer;

    void Start()
    {
        foreach (Texture2D cursorFrame in cursorFrames)
        {
            Texture2D texture= ResizeTexture(cursorFrame, 528, 538);
        }
        Cursor.SetCursor(cursorFrames[0], Vector2.zero, CursorMode.Auto); // Set initial cursor
    }

   Texture2D ResizeTexture(Texture2D source, int width, int height)
    {
        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(source, rt);
        RenderTexture.active = rt;
        
        Texture2D result = new Texture2D(width, height);
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        
        return result;
    }    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer -= frameRate;

            // Update the cursor frame
            currentFrame = (currentFrame + 1) % cursorFrames.Length;
            Cursor.SetCursor(cursorFrames[currentFrame], Vector2.zero, CursorMode.Auto);
        }
    }
}
