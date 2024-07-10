using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Networking;

public class DeckOfCardsAPIManager : MonoBehaviour
{
    public static DeckOfCardsAPIManager Instance;
    public string result;

    [SerializeField]
    private string _baseURL;

    public void CreateDeck()
    {
        this.StartCoroutine(this.RequestDeck());
    }

    private IEnumerator RequestDeck()
    {
        string url = this._baseURL + "new/shuffle/";

        using (UnityWebRequest request = new UnityWebRequest(url, "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            Debug.Log($"Response CODE : {request.responseCode}");

            if (string.IsNullOrEmpty(request.error))
            {
                //do one thing or another
            }

            else
            {
                Debug.Log($"ERROR {request.error}");
            }

            if(request.result == UnityWebRequest.Result.Success)
            {
                this.result = request.downloadHandler.text;
            }

            Debug.Log(this.result);

        }
    }

    private void Awake()
    {
        if(Instance== null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        this.CreateDeck();
    }
}
