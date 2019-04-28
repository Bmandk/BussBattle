using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualWarning : MonoBehaviour, IBeat
{
    public float flashTime;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        sr = GetComponent<SpriteRenderer>();

        while (true)
        {
            yield return new WaitForSeconds(flashTime * Conductor.Instance.Crotchet);
            sr.enabled = !sr.enabled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeat(int beatNumber, int totalBeatNumber, int songEnd)
    {
        if (beatNumber == 2)
        {
            Destroy(gameObject);
        }
    }
}
