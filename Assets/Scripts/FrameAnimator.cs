using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FrameAnimator : MonoBehaviour
{
    [System.Serializable]
    private class FrameAnimation
    {
        public string name;
        public List<Sprite> frames;
    }

    private int _frameCounter;

    private SpriteRenderer _sr;

    private FrameAnimation _currentAnimation;
    [SerializeField] private List<FrameAnimation> _animations;

    // Start is called before the first frame update
    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _currentAnimation = _animations[0];
    }

    public void OnBeat(int beatNumber, int totalBeatNumber)
    {
        _frameCounter = (totalBeatNumber - 1) % 2;
        Debug.Log(_frameCounter);
        _sr.sprite = _currentAnimation.frames[_frameCounter];
    }
}
