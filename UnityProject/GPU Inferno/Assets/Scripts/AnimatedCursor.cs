using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCursor : MonoBehaviour
{
   public Sprite[] cursorFrames; // Array de sprites del cursor
    public float frameRate = 0.1f; // Velocidad de animación
    private int currentFrame = 0;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % cursorFrames.Length;
            // Convertir el sprite a textura y establecerlo como cursor
            Cursor.SetCursor(SpriteToTexture(cursorFrames[currentFrame]), Vector2.zero, CursorMode.Auto);
        }
    }

    void Start()
    {
        if (cursorFrames.Length > 0)
            Cursor.SetCursor(SpriteToTexture(cursorFrames[0]), Vector2.zero, CursorMode.Auto);
    }

    // Método para convertir sprite a textura
    private Texture2D SpriteToTexture(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        Rect rect = sprite.rect;
        Texture2D newTexture = new Texture2D((int)rect.width, (int)rect.height);
        newTexture.SetPixels(sprite.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
        newTexture.Apply();
        return newTexture;
    }
}
