using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config parameters

    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparkelsVFX;
    [SerializeField] Sprite[] hitSprites;
    int maxHits;

    //cached reference

    Level level;

    //state variables

    [SerializeField] int timesHit;   //only serialized for debugging

    private void Start()
    {
      
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
                level.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            maxHits = hitSprites.Length + 1;
            timesHit++;
            if (timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextHitSprites();
            }
        }
       
    }

    private void ShowNextHitSprites()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("hitSprite array out of bounds"+gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        level.BlockDestroyed();
        TriggerSparklesVFX();
        FindObjectOfType<GameSession>().AddtoScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject, 0f);
    }
    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparkelsVFX,transform.position,transform.rotation);
        Destroy(sparkles, 1f);
    }
}
