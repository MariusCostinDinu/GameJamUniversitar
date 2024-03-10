using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHidden : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> sprites = new List<Sprite>();
    public int currentIndex = 0;


    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        Sprite[] loadedSprites = Resources.LoadAll<Sprite>("Sprites/" + gameObject.name);
        sprites.AddRange(loadedSprites);

    }


    public void ChangeSprite(int spriteNr)
    {
        spriteRenderer.sprite = sprites[spriteNr];
    }
}
