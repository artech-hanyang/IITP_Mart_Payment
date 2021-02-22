using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    int lang;
    string gameMode;
    string level;

    public AudioSource audioSource;
    public AudioClip sys_error;
    public AudioClip sys_succ;
    public AudioClip scanned;
    public AudioClip ring;
    public AudioClip message;

    AudioClip vocal;
    public AudioClip vocal_kr_test;
    public AudioClip vocal_kr_easy;
    public AudioClip vocal_kr_normal;
    public AudioClip vocal_kr_hard;
    public AudioClip vocal_en_test;
    public AudioClip vocal_en_easy;
    public AudioClip vocal_en_normal;
    public AudioClip vocal_en_hard;
    public AudioClip vocal_fr_test;
    public AudioClip vocal_fr_easy;
    public AudioClip vocal_fr_normal;
    public AudioClip vocal_fr_hard;

    public static SoundManager instance;

    void Awake(){
        if(SoundManager.instance == null){
            SoundManager.instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        audioSource = GetComponent<AudioSource>();

        gameMode = GameObject.Find("v_gameMode").GetComponent<Text>().text;
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        level = GameObject.Find("v_level").GetComponent<Text>().text;

        // Set Vocal Sound
        if (GlobalEnv.GAMEMODE_START.Equals(gameMode)){
            if (GlobalEnv.KR.Equals(lang.ToString())){
                if (GlobalEnv.LEVEL_EASY.Equals(level)) {
                    vocal = vocal_kr_easy;
                }else if (GlobalEnv.LEVEL_NORMAL.Equals(level)) {
                    vocal = vocal_kr_normal;
                }else if (GlobalEnv.LEVEL_HARD.Equals(level)) {
                    vocal = vocal_kr_hard;
                }

            }else if (GlobalEnv.FR.Equals(lang.ToString())){
                if (GlobalEnv.LEVEL_EASY.Equals(level)) {
                    vocal = vocal_fr_easy;
                }else if (GlobalEnv.LEVEL_NORMAL.Equals(level)) {
                    vocal = vocal_fr_normal;
                }else if (GlobalEnv.LEVEL_HARD.Equals(level)) {
                    vocal = vocal_fr_hard;
                }

            }else if (GlobalEnv.EN.Equals(lang.ToString())){
                if (GlobalEnv.LEVEL_EASY.Equals(level)) {
                    vocal = vocal_en_easy;
                }else if (GlobalEnv.LEVEL_NORMAL.Equals(level)) {
                    vocal = vocal_en_normal;
                }else if (GlobalEnv.LEVEL_HARD.Equals(level)) {
                    vocal = vocal_en_hard;
                }
            }
        }else {
            if (GlobalEnv.KR.Equals(lang.ToString())){
                vocal = vocal_kr_test;
            }else if (GlobalEnv.FR.Equals(lang.ToString())){
                vocal = vocal_fr_test;
            }else if (GlobalEnv.EN.Equals(lang.ToString())){
                vocal = vocal_en_test;
            }
        }
    }

    /**
     * @Function: Sound Manager Function
     * 
     * @Author: Minjung KIM
     * @Date: 2020.May.6
     * @History
     *      2020.09.10 레벨에 따른 보컬 음성 및 테스트 음성 추가
     */
    public void PlaySound(string audioName, int lang){
        
        // Sys Scanned
        if(audioName.Equals(GlobalEnv.SOUND_SCANNED)){
            audioSource.PlayOneShot(scanned);

        // Sys SUCC
        }else if(audioName.Equals(GlobalEnv.SOUND_SUCC)){
            audioSource.PlayOneShot(sys_succ);

        // Sys Error 
        }else if (audioName.Equals(GlobalEnv.SOUND_ERROR)){
            audioSource.PlayOneShot(sys_error);

        // Calling sound
        }else if (audioName.Equals(GlobalEnv.SOUND_CALL)){
            audioSource.PlayOneShot(ring);

        // Message sound
        }else if (audioName.Equals(GlobalEnv.SOUND_MESSAGE)){
            audioSource.PlayOneShot(message);

        // Vocal
        }else if (audioName.Equals(GlobalEnv.SOUND_VOCAL)){
            audioSource.PlayOneShot(vocal);
        }
    }
}