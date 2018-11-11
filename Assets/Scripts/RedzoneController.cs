using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RedzoneType { Mop, Shoes, FlySwatter };

public class RedzoneController : MonoBehaviour
{
    public GameObject redzoneAreaPrefab;

    public RedzoneType Type { get; private set; }

    void Start()
    {
        Type = (RedzoneType)Random.Range(0, System.Enum.GetValues(typeof(RedzoneType)).Length);
        Debug.Log("Redzone: " + Type.ToString());

        switch (Type)
        {
            case RedzoneType.Mop:
                StartCoroutine(Mop());
                break;
            case RedzoneType.Shoes:
                StartCoroutine(Shoes());
                break;
            case RedzoneType.FlySwatter:
                StartCoroutine(FlySwatter());
                break;
        }
    }

    private IEnumerator Mop()
    {
        GameObject redzoneArea = Instantiate(redzoneAreaPrefab, gameObject.transform);
        redzoneArea.transform.localScale = new Vector3(500, 2000, 1);

        yield return new WaitForSeconds(3);

        redzoneArea.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
        redzoneArea.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    private IEnumerator Shoes()
    {
        GameObject[] redzoneAreas = new GameObject[3];

        for (int i = 0; i < 3; i++)
        {
            redzoneAreas[i] = Instantiate(redzoneAreaPrefab, gameObject.transform);
            redzoneAreas[i].transform.localScale = new Vector3(400, 400, 1);
            Vector3 relativePosition = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), 0);
            redzoneAreas[i].transform.Translate(relativePosition);
        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 3; i++)
        {
            redzoneAreas[i].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            redzoneAreas[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    private IEnumerator FlySwatter()
    {
        GameObject redzoneArea = Instantiate(redzoneAreaPrefab, gameObject.transform);
        redzoneArea.transform.localScale = new Vector3(300, 200, 1);

        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i < 5; i++)
        {
            redzoneArea.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            redzoneArea.GetComponent<BoxCollider2D>().enabled = true;

            yield return new WaitForSeconds(0.2f);

            redzoneArea.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.2f);
            redzoneArea.GetComponent<BoxCollider2D>().enabled = false;

            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject);
    }
}
