using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Imgur : MonoBehaviour
{
    System.Random rnd = new System.Random();

    // end points for oAuth2 and for the calls. the two endpoint calls made
    // are for different get calls in the api. the token is for getting acces to an account to 
    // post images to. thi si a generic account that has never been used
    private const string tag_ENDPOINT = "https://api.imgur.com/3/gallery/t/";
    private const string subreddit_ENDPOINT = "https://api.imgur.com/3/gallery/r/";
    private const string access_Token = "https://api.imgur.com/oauth2/token";

    // the input feild fro the subreddit you are pulling images from
    public string subreddit;

    // client id for the api
    private static string client_id = "6e7d311d5498844";

    // number of pictures you wish to pull in
    private const int NoOfPics = 20;

    // initial api key
    private const string API_KEY = "e63ac588170bf8dde3e0d8a8ed202291bca381c1";

    public List<Image> ComputerImages;
    public List<Image> MapImages;

    public int ChosenComputerImage;
    public int ChosenMapImage;

    // for holding the url strings that are pulled in.
    public List<string> RandomURLs = new List<string>();

    private void Start()
    {
        AssignImages();
    }

    public void AssignImages()
    {
        var imageCount = ComputerImages.Count + MapImages.Count;

        // coroutine for getting the image URL's from the api
        // passes the result of it into the URLstrings
        // that is then used in the next coroutine to choose an image from the
        // array of returned strings.
        StartCoroutine(GetImages((imagesResult) =>
        {
            RandomURLs = imagesResult.Take(imageCount + 1).ToList();

            var choosenURL = RandomURLs[rnd.Next(0, RandomURLs.Count)];
            RandomURLs.Remove(choosenURL);

            ChosenComputerImage = rnd.Next(0, ComputerImages.Count);
            ChosenMapImage = rnd.Next(0, MapImages.Count);

            for (int i = 0; i < ComputerImages.Count; i++)
            {
                if (i == ChosenComputerImage)
                {
                    StartCoroutine(SetURLImage(ComputerImages[i], choosenURL));
                }
                else
                {
                    StartCoroutine(SetURLImage(ComputerImages[i], RandomURLs[i]));
                }

                ComputerImages[i].material.color = Color.black;
            }

            for (int i = 0; i < MapImages.Count; i++)
            {
                if (i == ChosenMapImage)
                {
                    StartCoroutine(SetURLImage(MapImages[i], choosenURL));
                }
                else
                {
                    StartCoroutine(SetURLImage(MapImages[i], RandomURLs[i + 3]));
                }

                MapImages[i].material.color = Color.black;
            }

        }, subreddit));
    }

    private IEnumerator SetURLImage(Image image, string URL)
    {
        WWW loader = new WWW(URL);
        yield return loader;

        image.material.color = Color.white;
        var material = new Material(image.material.shader);
        material.mainTexture = loader.texture;
        image.material = material;
    }

    // this is the coroutine for getting the scces token from the api to
    // acces the account to use put requestes onto the api
    private static IEnumerator GetAccessToken(Action<string> result)
    {
        #region-base request fro token-
        WWWForm form = new WWWForm();

        Dictionary<string, string> content = new Dictionary<string, string>();
        content.Add("refresh_token", "ab70dbf8d62716b6985e961f3276735bcfe9f35e");
        content.Add("client_id", client_id);
        content.Add("client_secret", "934c74d73e3e7c61bf364f44044fdd20f1da1b5b");
        content.Add("grant_type", "refresh_token");

        UnityWebRequest request = UnityWebRequest.Post(access_Token, content);

        yield return request.SendWebRequest();

        if (request.error == null)
        {
            string resultContent = request.downloadHandler.text;
            AccessToken json = JsonUtility.FromJson<AccessToken>(resultContent);

            //Return result
            result(json.access_token);
        }
        else
        {
            //Return null
            result("");
        }
        #endregion
    }

    // coroutine for getting the image from the api and passing the url for the image back to the
    // top to be used in the displaying of images.
    private static IEnumerator GetImages(Action<string[]> result, string subreddit)
    {

        // make varable for the current "yiff" tag
        // this methopd pulls from a subreddit.
        // choose the end point you wish to get from
        // the reddit one pulls from a subredding
        // the tag one pulls in from the whole api with items that have the
        // tag that is appended on after the end point
        UnityWebRequest www = UnityWebRequest.Get(subreddit_ENDPOINT + subreddit);

        string token = null;

        yield return GetAccessToken((tokenResult) => { token = tokenResult; });
        string poop = token;

        www.SetRequestHeader("Authorization", "Client-ID " + client_id);
        www.method = "GET";
        yield return www.SendWebRequest();
        yield return new WaitUntil(() => www.downloadHandler.isDone == true);
        if (!www.isNetworkError)
        {
            var resultContent = www.downloadHandler.text;
            string[] stringlinks = new string[NoOfPics];
            var links = JsonUtility.FromJson<ImgurRootObject>(resultContent);
            List<Datum> rootList = links.data.Take(NoOfPics).ToList();
            for (int i = 0; i < rootList.Count; i++)
            {
                stringlinks[i] = rootList[i].link;
            }

            ;
            //Return result
            result(stringlinks);
        }
        else
        {
            //Return null
            result(null);
        }
    }
}

public class AccessToken
{
    public string access_token;
}

#region-Json Classes-
[Serializable]
public class ImgurRootObject
{
    public Datum[] data;
    public bool success;
    public int status;
}

[Serializable]
public class Datum
{
    public string id;
    public string title;
    public string description;
    public int datetime;
    public string type;
    public bool animated;
    public int width;
    public int height;
    public int size;
    public int views;
    public long bandwidth;
    public object vote;
    public bool favorite;
    public bool nsfw;
    public string section;
    public object account_url;
    public object account_id;
    public bool is_ad;
    public Tags[] tags;
    public bool in_most_viral;
    public bool in_gallery;
    public string link;
    public object comment_count;
    public object ups;
    public object downs;
    public object points;
    public int score;
    public bool is_album;
}

[Serializable]
public class Tags
{
    public string id;
    public string name;
    public string display_name;
    public int followers;
    public int total_items;
    public string background_hash;
}
#endregion