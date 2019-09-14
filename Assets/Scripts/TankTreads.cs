using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTreads : MonoBehaviour
{
    public GameObject tankTread;
    //
    public Transform treadLeft;
    public Transform treadRight;
    //
    public AnimationCurve fadeCurve;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        SpawnTreads();
    }

    void SpawnTreads()
    {
        GameObject tLeft = (GameObject)Instantiate(tankTread, treadLeft.position, transform.rotation * tankTread.transform.rotation);
        GameObject tRight = (GameObject)Instantiate(tankTread, treadRight.position, transform.rotation * tankTread.transform.rotation);

        StartCoroutine(FadeOut(tLeft));
        StartCoroutine(FadeOut(tRight));
    }

    IEnumerator FadeOut(GameObject tread)
    {
        SpriteRenderer obj = tread.GetComponent<SpriteRenderer>();
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            obj.color = new Color(obj.color[0], obj.color[1], obj.color[2], a);
            yield return 0;
        }
        Destroy(tread);
    }
}
