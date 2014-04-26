using UnityEngine;
using System.Collections;

public class ActorAnimator : MonoBehaviour
{

    public Sprite[] sprites;
    public int framesPerSecond;
    private SpriteRenderer _spriteRenderer;

    // Use this for initialization
    void Start()
    {
        _spriteRenderer = renderer as SpriteRenderer;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    void Animate()
    {
        int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
        index = index % sprites.Length;
        _spriteRenderer.sprite = sprites[index];
    }
}