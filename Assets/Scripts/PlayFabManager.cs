using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using GooglePlayGames;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class PlayFabManager : MonoBehaviour
{
    public InputField EmailInput, PasswordInput, UsernameInput;
    public Text InfoText;
    public Text[] RankText = new Text[9];
    public Text StatText;
    string rank_board;

    int k;
    [SerializeField]
    private GameObject RankBoard;

    [SerializeField]
    private AudioSource clicksound;

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        GoogleLogIn();
    }
    public void GoogleLogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                InfoText.text = "구글 로그인 성공"; LoginBtn();
            }
            else InfoText.text = "구글 로그인 실패";
        });
    }


    public void GoogleLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        InfoText.text = "구글 로그아웃";
    }

    public void LoginBtn()
    {
        clicksound.Play();
        var request = new LoginWithEmailAddressRequest { Email = Social.localUser.id+"@yvps.com", Password = Social.localUser.id };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    public void RegisterBtn()
    {
        clicksound.Play();
        string t_username;
        t_username = Social.localUser.id;  // 구글 로그인이 되있는 상태에서의 구글 아이디를 t_username에 반환함
        string g_username = "";            // 플레이팹 내에 유저네임을 설정할 때 10글자를 최대로 받기 때문에 Social.localUser.id에서 10글자만 추출함
        for(int i = 0; i < 10; i++)
        {
            g_username += t_username[i];
        }
        var request = new RegisterPlayFabUserRequest{ Email =Social.localUser.id+"@yvps.com", Password = Social.localUser.id,Username= g_username, DisplayName = Social.localUser.userName };
        // 구글 아이디를 기반으로 PlayFab에 회원가입 양식 정보를 담아내고 
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        // 이 게임을 위해 생성한 PlayFab 서버에 회원가입 request를 보내고, 성공하면 Success함수, 실패하면 Failure함수를 호출하게함. 
      
    }
    public void OnNotLogin()
    {
        clicksound.Play();
        SceneManager.LoadScene("SelectScene");
    }

    void OnLoginSuccess(LoginResult result)
    {

        InfoText.text = "로그인 성공";

        //print("로그인 성공");
        SceneManager.LoadScene("SelectScene"); // 본격적으로 게임을 이용할 수 있는 Scene을 로드 시켜준다.
    }


    void OnLoginFailure(PlayFabError error)
    {
        InfoText.text = "로그인 실패";
        RegisterBtn(); // 로그인 실패 시, 다시 회원가입을 시도하게 됨.
        //print("로그인 실패");
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        InfoText.text = "회원가입 성공";
        LoginBtn();
        //print("회원가입 성공");
    }
    void OnRegisterFailure(PlayFabError error)
    {
        InfoText.text = "회원가입 실패";
        //print("회원가입 실패");
    }
    void OnPlayFabError(PlayFabError error)
    {

    }
    public void CloseRanking()
    {
        RankBoard.SetActive(false);
    }
    public void GetStat()
    {
        clicksound.Play();
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (result) =>
            {
                StatText.text = "";
                foreach (var eachStat in result.Statistics)
                    StatText.text += eachStat.StatisticName + " : " + eachStat.Value + "\n";
            },
            (error) => { StatText.text = "값 불러오기 실패"; });
    }
    public void ViewRaking()
    {
        clicksound.Play();
        RankBoard.SetActive(true);
        var request = new GetLeaderboardRequest();
        request.StartPosition = 0;
        request.StatisticName = "Score";

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, OnPlayFabError);
    }

    private void OnLeaderboardSuccess(GetLeaderboardResult obj)
    {
        k = 0;
        int rank_n = 1; //1위 부터 출력시켜주기 위해서
        
        foreach(var value in obj.Leaderboard)
        {
            if (rank_n >= 10) // 상위 10위 까지만 출력 시켜주기 위한 조건
            {
                break;
            }
           rank_board = (rank_n++)+"위 "+value.StatValue +"점 "+value.DisplayName; // 서버 dB에 저장된 값과 유저이름 정보를 출력함
           RankText[k++].text = rank_board;                        // rank_board 문자열을 text에 입힘.                
        }

        rank_board = "";
        
        
    }


}