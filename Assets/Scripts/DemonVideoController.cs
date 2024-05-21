using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class DemonVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    public VideoClip demonStart;
    public VideoClip demonIdle;
    public VideoClip demonRun;
    public VideoClip demonRunFull;
    public VideoClip demonAttack;

    public bool sprint;
    public bool playerAttacking;

    private bool isPlayingAttack;
    private VideoClip nextClip;
    private bool loopNextClip;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        PlayClip(demonStart);
    }

    void PlayClip(VideoClip clip, bool loop = false)
    {
        nextClip = clip;
        loopNextClip = loop;

        if (videoPlayer.isPrepared)
        {
            videoPlayer.clip = nextClip;
            videoPlayer.isLooping = loopNextClip;
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.clip = nextClip;
            videoPlayer.isLooping = loopNextClip;
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += OnPrepareCompleted;
        }
    }

    void OnPrepareCompleted(VideoPlayer vp)
    {
        videoPlayer.prepareCompleted -= OnPrepareCompleted;
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (isPlayingAttack)
        {
            isPlayingAttack = false;
            PlayClip(demonRunFull, true);
        }
        else if (videoPlayer.clip == demonStart)
        {
            PlayClip(demonIdle, true);
        }
        else if (videoPlayer.clip == demonRun || videoPlayer.clip == demonRunFull)
        {
            if (sprint)
            {
                PlayClip(demonRunFull, true);
            }
            else
            {
                PlayClip(demonIdle, true);
            }
        }
        else if (videoPlayer.clip == demonIdle)
        {
            if (sprint)
            {
                PlayClip(demonRun, true);
            }
            else
            {
                PlayClip(demonIdle, true);
            }
        }
    }

    void Update()
    {
        sprint = GetComponent<PlayerMotor>().sprinting;
        playerAttacking = GetComponent<PlayerAttack>().attacking;

        if (playerAttacking && !isPlayingAttack)
        {
            isPlayingAttack = true;
            PlayClip(demonAttack, false);
        }
    }
}
