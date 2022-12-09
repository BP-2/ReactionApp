using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject GameController;

    private EasyMode easyMode;

    public GameObject[] buttons;

    public GameObject startButton;

    StartListener sl;

    public GameObject startButton2;

    StartListener sl2;

    public GameObject startButton3;

    StartListener sl3;

    public GameObject multiplayerButton;

    StartListener slm;

    public int numShapes;

    public float gameTimer = 20;

    public float difficultyTimer = 0;

    public bool clicked = false;

    public bool inMultiplayer = false;

    public string playerData = "";
    public int playerNumber = -1;
    public int playerScore = -1;
    public int status = -1;
    public int difficulty = -1;
    public bool alreadyStarted = false;

    public class NetworkPlayer {
        public int id;
        public int playerNumber;
        public int playerScore;
        public int status;
        public int difficulty;
    }

    public NetworkPlayer otherPlayer = new NetworkPlayer();
    

    // Start is called before the first frame update
    void Start()
    {
        numShapes = buttons.Length;
        easyMode = GameController.GetComponent<EasyMode>();
        sl = startButton.GetComponent<StartListener>();
        sl2 = startButton2.GetComponent<StartListener>();
        sl3 = startButton3.GetComponent<StartListener>();

        // Setting up defaults for other player
        otherPlayer.playerNumber = -1;
        otherPlayer.playerScore = -1;
        otherPlayer.status = -1;
        otherPlayer.difficulty = -1;
        
        slm = multiplayerButton.GetComponent<StartListener>();
    }

    void Update()
    {
        // Only allows multiplayer
        if (sl.started == true && inMultiplayer && otherPlayer.playerNumber != -1)
        {
            startGame(sl.difficulty);
            sl.started = false;
        }
        else if (sl2.started == true && inMultiplayer && otherPlayer.playerNumber != -1)
        {
            startGame(sl2.difficulty);
            sl2.started = false;
        }
        else if (sl3.started == true && inMultiplayer && otherPlayer.playerNumber != -1)
        {
            startGame(sl3.difficulty);
            sl3.started = false;
        }
        else if(status == 1 && !alreadyStarted) {
            alreadyStarted = true;
            startGame(difficulty);
        }
        if(slm.started == true && inMultiplayer == false) {
            slm.started = false;
            StartCoroutine(GetRequestPlayer("https://backend-dot-lightscreendotart.uk.r.appspot.com/unityaddplayer"));
            InvokeRepeating("GetStatuses", 5.0f, 10.0f);
            inMultiplayer = true;
        }
    }

    public void GetStatuses() {
        StartCoroutine(GetRequestPlayer("https://backend-dot-lightscreendotart.uk.r.appspot.com/unitygetstatuses", true));
    }

    public IEnumerator GetRequestPlayer(string uri, bool isStatuses=false)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    playerData = webRequest.downloadHandler.text;
                    playerData = playerData.Remove(0, 1);
                    playerData = playerData.Remove(0, 1);
                    playerData = playerData.Remove(playerData.Length-3);
                    string[] tmpPlayers = playerData.Split("],[");

                    // Getting statuses if there are 2 players
                    if(isStatuses && tmpPlayers.Length > 1) {
                        string[] p1 = tmpPlayers[0].Split(",");
                        string[] p2 = tmpPlayers[1].Split(",");
                        if(int.Parse(p1[1]) == playerNumber) {
                            // This player
                            playerNumber = int.Parse(p1[1]);
                            playerScore = int.Parse(p1[2]);
                            status = int.Parse(p1[3]);
                            difficulty = int.Parse(p1[4]);
                            Debug.Log("Player Number: " + playerNumber.ToString());
                            Debug.Log("Player Score: " + playerScore.ToString());
                            Debug.Log("Status: " + status.ToString());
                            Debug.Log("Difficulty: " + difficulty.ToString());

                            // Other Player
                            otherPlayer.playerNumber = int.Parse(p2[1]);
                            otherPlayer.playerScore = int.Parse(p2[2]);
                            otherPlayer.status = int.Parse(p2[3]);
                            otherPlayer.difficulty = int.Parse(p2[4]);
                            Debug.Log("otherPlayer.Player Number: " + otherPlayer.playerNumber.ToString());
                            Debug.Log("otherPlayer.Player Score: " + otherPlayer.playerScore.ToString());
                            Debug.Log("otherPlayer.Status: " + otherPlayer.status.ToString());
                            Debug.Log("otherPlayer.Difficulty: " + otherPlayer.difficulty.ToString());
                        }
                        else {
                            // This player
                            playerNumber = int.Parse(p2[1]);
                            playerScore = int.Parse(p2[2]);
                            status = int.Parse(p2[3]);
                            difficulty = int.Parse(p2[4]);
                            Debug.Log("Player Number: " + playerNumber.ToString());
                            Debug.Log("Player Score: " + playerScore.ToString());
                            Debug.Log("Status: " + status.ToString());
                            Debug.Log("Difficulty: " + difficulty.ToString());

                            // Other Player
                            otherPlayer.playerNumber = int.Parse(p1[1]);
                            otherPlayer.playerScore = int.Parse(p1[2]);
                            otherPlayer.status = int.Parse(p1[3]);
                            otherPlayer.difficulty = int.Parse(p1[4]);
                            Debug.Log("otherPlayer.Player Number: " + otherPlayer.playerNumber.ToString());
                            Debug.Log("otherPlayer.Player Score: " + otherPlayer.playerScore.ToString());
                            Debug.Log("otherPlayer.Status: " + otherPlayer.status.ToString());
                            Debug.Log("otherPlayer.Difficulty: " + otherPlayer.difficulty.ToString());
                        }
                    }
                    else {
                        string[] tmp = tmpPlayers[0].Split(',');
                        if(tmp.Length > 1) {
                            playerNumber = int.Parse(tmp[1]);
                            playerScore = int.Parse(tmp[2]);
                            status = int.Parse(tmp[3]);
                            difficulty = int.Parse(tmp[4]);
                            Debug.Log("Player Number: " + playerNumber.ToString());
                            Debug.Log("Player Score: " + playerScore.ToString());
                            Debug.Log("Status: " + status.ToString());
                            Debug.Log("Difficulty: " + difficulty.ToString());
                        }
                    }
                    
                    
                    break;
            }
        }
    }

    public IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public void startGame(int difficultyIn)
    {
        StartCoroutine(GetRequest("https://backend-dot-lightscreendotart.uk.r.appspot.com/unitystartgame?difficulty="+difficultyIn.ToString()));
        easyMode = GameController.GetComponent<EasyMode>();
        easyMode.gameTimer = 30;
        easyMode.on = true;
        easyMode.score = 0;
        easyMode.pNumber = playerNumber;
        status = 1;
        
        switch (difficultyIn)
        {
            case (1):
                easyMode.difficulty = 4;
                break;
            case (2):
                easyMode.difficulty = 2.5f;
                break;
            case (3):
                easyMode.difficulty = 1f;
                break;
            default:
                easyMode.difficulty = 4;
                break;
        }
    }
}
