using StaGeGames.SmartUI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainPCManager : MonoBehaviour
{

    #region Variables
    //main panels throught the app, the needed buttons, images and all go's
    [Header("Main Menu Obj")]
    public GameObject pnlWindScreen;
    public GameObject pnlStringScreen;
    public GameObject pnlPiraeusInstrument;
    public GameObject pnlMegaraInstrument;
    public Button btnAulosDafnis;
    public Button btnAulosMegara64;
    public Button btnAulosMegara65;
    public Button btnTrigono;
    public Button btnLyra;
    public GameObject pnlMainWindInstrument;
    public GameObject pnlMainStringInstrument;
    public GameObject pnlInstrumentMiddleScreen;
    public GameObject pnlNavigateButtons;
    public Button btnNext;
    public Button btnPrevious;    
    public TextMeshProUGUI txtInstrument;
    public Button btnBackToMain;
    public GameObject pnlMuseumScreen;
    public Button btnMuseumPiraeus, btnMuseumMegara;
    public TextMeshProUGUI txtMuseumName;
    public GameObject prefab3Dins;
    public GameObject imgHoleExplain;
    public GameObject imgDafnis;
    public GameObject imgMegarwnL;
    public GameObject imgMegarwnH;
    public Button btnExtraMenu;
    public GameObject separate;
    public Button btnMuseum;
    public GameObject pnlExtraMenu;
    public GameObject pnlBottom;
    public Image imgMnesiasBack;


    //to show what each scriptable object has as info already.
    [Space]
    [Header("Elements On Panel")]
    public TextMeshProUGUI txtMainTextInstrument;
    public GameObject imgContainer;
    public TextMeshProUGUI txtCaptionInstrument;
    public GameObject imgContainerExtraInfo;
    public Sprite imgAulosExtra;
    public Sprite imgAulosMegarwnExtra;

    //when we enter either wind or string main panels for all the go's needed to change values, hide/unhide buttons,3d objects and images
    [Space]
    [Header("On Instrument Buttons")]
    public Button btnChangeValue;
    public Button btnSubmit;
    public Button btn3DScene;
    public Button btnMoreInfo;
    public Button btnCloseWind;
    public Button btnCloseString;
    public Button btnReset;
    public GameObject windInsDafnis3D;
    public GameObject windInsMeg64H3D;
    public GameObject windInsMeg64L3D;
    public GameObject windInsMeg65H3D;
    public GameObject windInsMeg65L3D;
    public GameObject trigonoIns3D;
    public GameObject LyraIns3D;
    public GameObject imgWindContainer;
    public GameObject imgStringContainer;
    public Sprite imgTrigono;
    public Sprite imgHelys;
    public Sprite imgWindDafnis;
    public Sprite imgWindMegarwn64H;
    public Sprite imgWindMegarwn64L;
    public Sprite imgWindMegarwn65H;
    public Sprite imgWindMegarwn65L;


    //the help panel go's
    [Space]
    [Header("Help panel")]
    public GameObject pnlHelp;
    public TextMeshProUGUI txtTitle;
    public TextMeshProUGUI txtMain;
    public Button btnHelp;


    //the panel for the first time and when a user uses a wrong combination
    [Space]
    [Header("Warning panel")]
    public GameObject pnlWarning;
    public Button btnCloseWarning;


    //when we want to show extra info, 3d model and videos this is where we assing the go's
    [Space]
    [Header("ExtraInfo panel")]
    public GameObject pnlExtraInfo;
    public VideoClip[] videoClip;
    public VideoPlayer videoPlayer;
    public GameObject[] textureVideos;
    public GameObject pnlMainRawVideo;
    public Button btnCloseVideo;
    public Slider slVideo;
    public DatabaseAudio myAudio;//for audio
    public ButtonLevel buttonLevel;//to load levels, close application etc.
    public Button btnVideo;
    public Sprite btnPlaySp;
    public Sprite btnPauseSp;
    [HideInInspector] public bool isMegarwnH64, isMegarwnL64, isDafnis, isMegarwnL65, isMegarwnH65, isTrigono, isLyra; //to check on which instrument we are in and open/close panels, methods etc.

    [HideInInspector] public bool isPiraeus, isMegara;//for the window name change

    [HideInInspector] public bool hasSelected;
    [Space]
    [Header("Instrument Elements")]
    public InstrumentSystem[] instrument;//the sciptable objects we have already created on our resources folder with all the information needed.

    [Space]
    [Header("Managers")]
    public WindManager wm;//the wind manager in order to access methods, variables, etc
    public StringManager sm;//the string manager in order to access methods, variables, etc

    public MobileScript ms;//script that is only used when we are on a device platform

    public SmartMotion helpMotion;//in order to get the method to close the panel with motion.
    public SmartMotion menuMotion;//in order to get the method to close the panel with motion.
    #endregion

    #region UnityMethods
    private void Awake()
    {
        pnlMainWindInstrument.SetActive(false);
        prefab3Dins.SetActive(false);
        btnExtraMenu.gameObject.SetActive(false);
        separate.SetActive(false);
        imgDafnis.SetActive(false);
        imgMegarwnH.SetActive(false);
        imgMegarwnL.SetActive(false);
        imgHoleExplain.SetActive(false);
        imgWindContainer.gameObject.SetActive(false);
        imgStringContainer.gameObject.SetActive(false);
        txtInstrument.gameObject.SetActive(false);

        ClosedPanelsGO(true,false, false, false, false, false, false,false);
        myAudio = FindObjectOfType<DatabaseAudio>();
        btnSubmit.gameObject.SetActive(false);
        btnReset.gameObject.SetActive(false);

        buttonLevel = GetComponent<ButtonLevel>();
        pnlWindScreen.SetActive(false);
        pnlStringScreen.SetActive(false);
        btnBackToMain.gameObject.SetActive(false);
        pnlWarning.SetActive(false);

        imgMnesiasBack.gameObject.SetActive(true);
        btnPrevious.gameObject.SetActive(false);


        btnCloseWarning.onClick.AddListener(ClosePanels);
        windInsDafnis3D.SetActive(false);
        windInsMeg64H3D.SetActive(false);
        windInsMeg64L3D.SetActive(false);
        windInsMeg65H3D.SetActive(false);
        windInsMeg65L3D.SetActive(false);
        trigonoIns3D.SetActive(false);
        LyraIns3D.SetActive(false);
        pnlBottom.SetActive(false);

        imgContainerExtraInfo.SetActive(false);
        LoadInstrumentFiles("Instruments/");

        pnlExtraInfo.SetActive(false);
        hasSelected = false;

        ms = GetComponent<MobileScript>();
        ms.pnlSound.SetActive(false);
        ms.txtTitleInstrument.gameObject.SetActive(false);

    }


    // Start is called before the first frame update
    void Start()
    {
        btnBackToMain.gameObject.SetActive(false);

        SubscribedButtons();
        foreach (GameObject obj in textureVideos)
        {
            obj.SetActive(true);
        }

    }

#endregion

    //to assign most buttons of the project
    void SubscribedButtons()
    {
        btnAulosDafnis.onClick.AddListener(OpenPiraeusMiddleWindScene);
        btnAulosMegara64.onClick.AddListener(OpenMegara64MiddleScene);
        btnAulosMegara65.onClick.AddListener(OpenMegara65MiddleScene);

        btnPrevious.onClick.AddListener(BackPanels);
        btnHelp.onClick.AddListener(OpenHelpPanel);
        
        btnBackToMain.onClick.AddListener(CloseMainPanels);

        btnCloseWind.onClick.AddListener(ClosePanels);
        btnCloseString.onClick.AddListener(ClosePanels);
        btnCloseVideo.onClick.AddListener(ClosePanels);

        btnMuseumPiraeus.onClick.AddListener(OpenPanelPiraeus);
        btnMuseumMegara.onClick.AddListener(OpenPanelMegara);

        btnTrigono.onClick.AddListener(OpenTrigonoMiddleScene);
        btnLyra.onClick.AddListener(OpenHelysMiddleScene);

        btnExtraMenu.onClick.AddListener(() => pnlExtraMenu.SetActive(true));

        btnMoreInfo.onClick.AddListener(OpenMoreInfoPanel);


        ms.btnPlayPanel.onClick.AddListener(OpenPlayPanel);
        ms.btnClosePlayPanel.onClick.AddListener(ClosePanels);

    }

#region Panels

    //main wind scene where player can change values in order to listen to the correct sounds
    public void OpenAulosMainScene()
    {
        //btnBackToMain.gameObject.SetActive(true);
        btnExtraMenu.gameObject.SetActive(true);
        separate.SetActive(true);
        

        ClosedPanelsGO(false,false, false, false, false, false, false,false);
        pnlWindScreen.SetActive(true);
        pnlStringScreen.SetActive(false);
        pnlMainWindInstrument.SetActive(true);
        pnlMainStringInstrument.SetActive(false);
        imgMnesiasBack.gameObject.SetActive(true);
        wm.isBack = false;
        
        imgStringContainer.SetActive(false);


        btn3DScene.gameObject.SetActive(true);
        btn3DScene.onClick.RemoveAllListeners();

        if (isDafnis && !(isMegarwnH64 || isMegarwnL64 || isMegarwnH65 || isMegarwnL65))
        {
            txtInstrument.gameObject.SetActive(true);
            txtInstrument.text = "> " + btnAulosDafnis.GetComponentInChildren<TextMeshProUGUI>().text;
            wm.btnBarrytone.interactable = false;

            LoadClips("dafnis/");

        }
        else if (isMegarwnH64 || isMegarwnL64 && !isDafnis && !isMegarwnL65 && !isMegarwnH65)
        {
            txtInstrument.gameObject.SetActive(true);
            txtInstrument.text = "> " + btnAulosMegara64.GetComponentInChildren<TextMeshProUGUI>().text;
            wm.btnBarrytone.interactable = true;

            

            LoadClips("megarwnH64/");
        }
        else if (isMegarwnL65 || isMegarwnH65 && !isDafnis && !isMegarwnH64 && !isMegarwnL64)
        {
            txtInstrument.gameObject.SetActive(true);
            txtInstrument.text = "> " + btnAulosMegara65.GetComponentInChildren<TextMeshProUGUI>().text;
            wm.btnBarrytone.interactable = true;


        }

        btnSubmit.onClick.AddListener(wm.OnSubmit);
        btnChangeValue.onClick.AddListener(wm.OpenChangeValuesPanel);
        trigonoIns3D.SetActive(false);
        LyraIns3D.SetActive(false);

        if (pnlHelp.activeSelf) CloseHelpPanel();

        btnReset.onClick.AddListener(wm.OnReset);

        ms.btnPlayPanel.gameObject.SetActive(true);

    }


    //main string scene, for user to change values and listen to the correct sounds
    public void OpenStringMainScene()
    {

        btnExtraMenu.gameObject.SetActive(true);
        separate.SetActive(true);

        btn3DScene.gameObject.SetActive(true);
        ClosedPanelsGO(false,false, false, false, false, false, true,false);
        pnlWindScreen.SetActive(false);
        pnlStringScreen.SetActive(true);
        pnlMainWindInstrument.SetActive(false);
        pnlMainStringInstrument.SetActive(true);
        imgMnesiasBack.gameObject.SetActive(true);

        windInsDafnis3D.SetActive(false);
        windInsMeg65H3D.SetActive(false);
        windInsMeg64L3D.SetActive(false);
        windInsMeg64H3D.SetActive(false);
        windInsMeg65L3D.SetActive(false);

        if (pnlHelp.activeSelf) CloseHelpPanel();

        btnSubmit.onClick.RemoveAllListeners();
        btnChangeValue.onClick.RemoveAllListeners();
        btn3DScene.onClick.RemoveAllListeners();
        btnReset.onClick.RemoveAllListeners();

        btnSubmit.onClick.AddListener(sm.OnSubmitString);
        btnChangeValue.onClick.AddListener(sm.OpenChangeValuesPanelString);

        imgWindContainer.SetActive(false);
        imgStringContainer.SetActive(true);

        if (isTrigono && !isLyra)
        {
            sm.OpenValues(1);
            sm.CloseListOfGameObjectsTrigono();

            trigonoIns3D.SetActive(true);
            LyraIns3D.gameObject.SetActive(false);
            btn3DScene.gameObject.SetActive(true);
            btn3DScene.onClick.AddListener(() => sm.Load3DIns(trigonoIns3D));
            

            LoadClips("Trigonon/");

           
            imgStringContainer.GetComponent<Image>().sprite = imgTrigono;

        }
        else if (isLyra && !isTrigono)
        {
            sm.OpenValues(210);
            sm.CloseListOfGameObjectsTrigono();

            btn3DScene.gameObject.SetActive(true);
            LyraIns3D.SetActive(true);
            btn3DScene.onClick.AddListener(() => sm.Load3DIns(LyraIns3D));
            trigonoIns3D.gameObject.SetActive(false);

            LoadClips("Helys/");

            imgStringContainer.GetComponent<Image>().sprite = imgHelys;
        }

        btnReset.onClick.AddListener(sm.OnResetString);//reset values

        ms.btnPlayPanel.gameObject.SetActive(false);

    }


    //when clicking on back button, check on which condition we are on and close the correct panel, plus reset values when we "close" the main scenes
    private void BackPanels()
    {
        imgMnesiasBack.gameObject.SetActive(true);
        #region NotInUse
        /*if (pnlMainWindInstrument.activeSelf)
        {
            if (pnlExtraInfo.activeSelf)
            {
                pnlExtraInfo.SetActive(false);
                pnlMainWindInstrument.SetActive(true);
            }
            else
            {
                wm.isBack = true;
                wm.OnReset();

                if (isDafnis || isMegarwnH64 || isMegarwnL64 || isMegarwnH65 || isMegarwnL65)
                {
                    ClosedPanelsGO(false, false, true, true, true, false);
                }

                pnlMainWindInstrument.SetActive(false);
                pnlWindScreen.SetActive(false);
                btnBackToMain.gameObject.SetActive(true);
                btnExtraMenu.gameObject.SetActive(false);
                separate.SetActive(false);
                windInsDafnis3D.SetActive(false);
                windInsMeg65H3D.SetActive(false);
                windInsMeg64L3D.SetActive(false);
                windInsMeg64H3D.SetActive(false);
                windInsMeg65L3D.SetActive(false);
                trigonoIns3D.SetActive(false);
                LyraIns3D.SetActive(false);
            }

            Debug.Log("1. isDafnis: " + isDafnis + " Megara: " + isMegara + " Piraeus; " + isPiraeus);
        }
        else if (pnlPiraeusInstrument.activeSelf)
        {
            if (pnlExtraInfo.activeSelf)
            {
                pnlExtraInfo.SetActive(false);
                pnlPiraeusInstrument.SetActive(true);
            }
            else
            {
                pnlMuseumScreen.SetActive(true);
                btnBackToMain.gameObject.SetActive(false);
                btnExtraMenu.gameObject.SetActive(false);
                separate.SetActive(false);
                ClosedPanelsGO(false, false, false, false, false, false);
                txtInstrument.gameObject.SetActive(false);
                txtMuseumName.text = "Επιλογη Μουσειου";
            }
            Debug.Log("2");
        }
        else if (pnlMegaraInstrument.activeSelf)
        {
            if (pnlExtraInfo.activeSelf)
            {
                pnlExtraInfo.SetActive(false);
                pnlMegaraInstrument.SetActive(true);
            }
            else
            {
                pnlMuseumScreen.SetActive(true);
                btnBackToMain.gameObject.SetActive(false);
                btnExtraMenu.gameObject.SetActive(false);
                separate.SetActive(false);
                ClosedPanelsGO(false, false, false, false, false, false);
                txtInstrument.gameObject.SetActive(false);
                txtMuseumName.text = "Επιλογη Μουσειου";
            }

            Debug.Log("4");
        }
        else if (pnlInstrumentMiddleScreen.activeSelf)
        {
            btnExtraMenu.gameObject.SetActive(true);
            separate.SetActive(true);
            btn3DScene.gameObject.SetActive(false);


            if (isDafnis || isTrigono || isLyra)
            {
                ClosedPanelsGO(true, false, false, false, true, false);
                OpenPanelPiraeus();

            }
            else
            {
                ClosedPanelsGO(false, true, false, false, true, false);
                OpenPanelMegara();
            }
            txtInstrument.gameObject.SetActive(false);
            Debug.Log("8. isDafnis: " + isDafnis + " Megara: " + isMegara + " Piraeus; " + isPiraeus);
        }
        else if (pnlMainStringInstrument.activeSelf)
        {
            if (pnlExtraInfo.activeSelf)
            {
                pnlExtraInfo.SetActive(false);
                pnlMainStringInstrument.SetActive(true);
            }
            else
            {
                sm.OnResetString();
                if (isTrigono || isLyra) ClosedPanelsGO(false, false, true, true, true, false);

                pnlMainStringInstrument.SetActive(false);
                pnlStringScreen.SetActive(false);
                btnExtraMenu.gameObject.SetActive(false);
                separate.SetActive(false);
            }

            Debug.Log("7");
        }*/
        #endregion
        if (prefab3Dins.activeSelf)
        {
            if (pnlExtraInfo.activeSelf)
            {
                pnlExtraInfo.SetActive(false);
                prefab3Dins.SetActive(true);
            }
            else
            {
                prefab3Dins.SetActive(false);
                if (isDafnis || isMegara || isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65 && !(isTrigono || isLyra))
                {
                    pnlMainWindInstrument.SetActive(true);
                    pnlMainStringInstrument.SetActive(false);
                    OpenAulosMainScene();
                }
                else if (isTrigono || isLyra && !(isDafnis || isMegara || isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65))
                {
                    pnlMainStringInstrument.SetActive(true);
                    pnlMainWindInstrument.SetActive(false);
                    OpenStringMainScene();
                }

                ClosedPanelsGO(false,false, false, false, false, false, true,false);

                btnExtraMenu.gameObject.SetActive(true);
                separate.SetActive(true);
                btnBackToMain.gameObject.SetActive(true);
            }

            Debug.Log("6");
        }
        


        if (pnlHelp.activeSelf) CloseHelpPanel();
        if (pnlExtraMenu.activeSelf) CloseMenuPanel();
    }

    //close panels for wind instruments

    //we use it in order to close specific panels
    public void ClosePanels()
    {
        if (pnlWarning.activeSelf)
        {
            pnlWarning.SetActive(false);
        }
        if (wm.pnlChangeValuesScreen.activeSelf)
        {
            wm.pnlChangeValuesScreen.SetActive(false);
            pnlBottom.gameObject.SetActive(true);
            ms.btnPlayPanel.gameObject.SetActive(true);
        }
        if (pnlMainRawVideo.activeSelf)
        {
            pnlMainRawVideo.SetActive(false);
            if (videoPlayer.isPlaying || videoPlayer.isPaused) videoPlayer.Stop();
        }

        if (sm.pnlChangeValuesScreen.activeSelf)
        {
            sm.pnlChangeValuesScreen.SetActive(false);
            pnlBottom.SetActive(true);
            
        }


        if (ms.pnlSound.activeSelf)
        {
            ms.pnlSound.SetActive(false);
            pnlBottom.gameObject.SetActive(true);
        }
        
        if (pnlHelp.activeSelf) CloseHelpPanel();
        if (pnlExtraMenu.activeSelf) CloseMenuPanel();


    }

    //this method is created in order to minimize problems between panels.
    public void CloseMainPanels()
    {
        if (pnlMainWindInstrument.activeSelf)
        {
            pnlMainWindInstrument.SetActive(false);

            wm.isBack = true;
            wm.OnReset();
            ClosedPanelsGO(true, false, false, true, false, false, false, false);

            pnlMainWindInstrument.SetActive(false);
            pnlWindScreen.SetActive(false);
            btnBackToMain.gameObject.SetActive(false);
            btnExtraMenu.gameObject.SetActive(false);
            separate.SetActive(false);
            windInsDafnis3D.SetActive(false);
            windInsMeg65H3D.SetActive(false);
            windInsMeg64L3D.SetActive(false);
            windInsMeg64H3D.SetActive(false);
            windInsMeg65L3D.SetActive(false);
            trigonoIns3D.SetActive(false);
            LyraIns3D.SetActive(false);
            separate.SetActive(false);

            Debug.Log("1. isDafnis: " + isDafnis + " Megara: " + isMegara + " Piraeus; " + isPiraeus);
        }
        if (pnlPiraeusInstrument.activeSelf)
        {
            pnlPiraeusInstrument.SetActive(false);

            btnBackToMain.gameObject.SetActive(false);
            btnExtraMenu.gameObject.SetActive(false);
            separate.SetActive(false);
            ClosedPanelsGO(true, false, false, false, false, false, false, false);
            txtInstrument.gameObject.SetActive(false);
            txtMuseumName.text = "Επιλογη Μουσειου";

            Debug.Log("2");
        }
        if (pnlMegaraInstrument.activeSelf)
        {
            pnlMegaraInstrument.SetActive(false);

            btnBackToMain.gameObject.SetActive(false);
            btnExtraMenu.gameObject.SetActive(false);
            separate.SetActive(false);
            ClosedPanelsGO(true, false, false, false, false, false, false, false);
            txtInstrument.gameObject.SetActive(false);
            txtMuseumName.text = "Επιλογη Μουσειου";

            Debug.Log("4");
        }
        if (prefab3Dins.activeSelf)
        {
            prefab3Dins.SetActive(false);

            pnlMainWindInstrument.SetActive(false);
            pnlMainStringInstrument.SetActive(false);
            btnBackToMain.gameObject.SetActive(false);

            ClosedPanelsGO(true, false, false, false, false, false, false, false);

            btnExtraMenu.gameObject.SetActive(false);
            separate.SetActive(false);

            Debug.Log("6");
        }
        if (pnlInstrumentMiddleScreen.activeSelf)
        {
            btnExtraMenu.gameObject.SetActive(false);
            btnBackToMain.gameObject.SetActive(false);
            separate.SetActive(false);
            btn3DScene.gameObject.SetActive(false);
            ClosedPanelsGO(true, false, false, false, false, false, false, false);

            txtInstrument.gameObject.SetActive(false);
            Debug.Log("8. isDafnis: " + isDafnis + " Megara: " + isMegara + " Piraeus; " + isPiraeus);
        }
        else if (pnlMainStringInstrument.activeSelf)
        {
            pnlMainStringInstrument.SetActive(false);

            sm.OnResetString();
            if (isTrigono || isLyra) ClosedPanelsGO(true, false, false, false, false, false, false, false);

            pnlMainStringInstrument.SetActive(false);
            pnlStringScreen.SetActive(false);
            btnExtraMenu.gameObject.SetActive(false);
            separate.SetActive(false);
            btnBackToMain.gameObject.SetActive(false);

            Debug.Log("7");
        }

    }
    //if user won;t choose high or low, a warning appears in order to load the default values for each instrument
    public void WarningOnFirstChoice()
    {
        pnlWarning.SetActive(true);
        pnlWarning.GetComponentInChildren<TextMeshProUGUI>().text = "Παρακαλώ επιλέξτε ηχητικό σωλήνα πρώτα.";
    }

    //when we open the aulos middle panel
    private void OpenPiraeusMiddleWindScene()
    {
        ClosedPanelsGO(false,false, false, true, true, true, false,false);

        windInsDafnis3D.SetActive(false);
        windInsMeg65H3D.SetActive(false);
        windInsMeg65L3D.SetActive(false);
        windInsMeg64H3D.SetActive(false);
        windInsMeg64L3D.SetActive(false);
        trigonoIns3D.SetActive(false);
        LyraIns3D.SetActive(false);
        btnExtraMenu.gameObject.SetActive(false);
        separate.SetActive(false);

        isDafnis = true;
        isTrigono = false;
        isLyra = false;
        isMegarwnH64 = false;
        isMegarwnL64 = false;
        isMegarwnL65 = false;
        isMegarwnH65 = false;

        for (int i = 0; i < instrument.Length; i++)
        {
            txtInstrument.gameObject.SetActive(true);

            ms.txtTitleInstrument.gameObject.SetActive(true);


            if (instrument[i].nameInstrument.Contains(btnAulosDafnis.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                OpenExtraInfoImage();
                txtInstrument.text = "> " + instrument[i].nameInstrument;
                imgContainer.GetComponent<Image>().sprite = instrument[i].imgInstrument;
                txtMainTextInstrument.text = instrument[i].mainText;
                txtCaptionInstrument.text = instrument[i].txtUnderImage;

                ms.txtTitleInstrument.text = instrument[i].nameInstrument;


            }


        }

        if (pnlHelp.activeSelf) CloseHelpPanel();
        btnNext.onClick.AddListener(OpenAulosMainScene);
    }


    //when we open the megara64
    private void OpenMegara64MiddleScene()
    {
        ClosedPanelsGO(false,false, false, true, true, true, false,false);
        windInsDafnis3D.SetActive(false);
        windInsMeg65H3D.SetActive(false);
        windInsMeg65L3D.SetActive(false);
        windInsMeg64H3D.SetActive(false);
        windInsMeg64L3D.SetActive(false);
        trigonoIns3D.SetActive(false);
        LyraIns3D.SetActive(false);
        btnExtraMenu.gameObject.SetActive(false);
        separate.SetActive(false);

        isDafnis = false;
        isTrigono = false;
        isLyra = false;
        isMegarwnH64 = true;
        isMegarwnL64 = true;
        isMegarwnH65 = false;
        isMegarwnL65 = false;

        for (int i = 0; i < instrument.Length; i++)
        {
            txtInstrument.gameObject.SetActive(true);
            ms.txtTitleInstrument.gameObject.SetActive(true);

            if (instrument[i].nameInstrument.Contains(btnAulosMegara64.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                OpenExtraInfoImage();
                txtInstrument.text = "> " + instrument[i].nameInstrument;
                imgContainer.GetComponent<Image>().sprite = instrument[i].imgInstrument;
                txtMainTextInstrument.text = instrument[i].mainText;
                txtCaptionInstrument.text = instrument[i].txtUnderImage;
                ms.txtTitleInstrument.text = instrument[i].nameInstrument;
            }


        }

        Debug.Log("megara64 Scene");
        btnNext.onClick.AddListener(OpenAulosMainScene);
        if (pnlHelp.activeSelf) CloseHelpPanel();

    }

    //when we open the megara65
    private void OpenMegara65MiddleScene()
    {
        //btnBackToMain.gameObject.SetActive(false);
        ClosedPanelsGO(false,false, false, true, true, true, false,false);


        windInsDafnis3D.SetActive(false);
        windInsMeg65H3D.SetActive(false);
        windInsMeg65L3D.SetActive(false);
        windInsMeg64H3D.SetActive(false);
        windInsMeg64L3D.SetActive(false);
        trigonoIns3D.SetActive(false);
        LyraIns3D.SetActive(false);

        btnExtraMenu.gameObject.SetActive(false);
        separate.SetActive(false);

        isDafnis = false;
        isTrigono = false;
        isLyra = false;
        isMegarwnH65 = true;
        isMegarwnL65 = true;
        isMegarwnH64 = false;
        isMegarwnL64 = false;


        ms.txtTitleInstrument.gameObject.SetActive(true);

        for (int i = 0; i < instrument.Length; i++)
        {
            txtInstrument.gameObject.SetActive(true);


            if (instrument[i].nameInstrument.Contains(btnAulosMegara65.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                OpenExtraInfoImage();
                txtInstrument.text = "> " + instrument[i].nameInstrument;
                imgContainer.GetComponent<Image>().sprite = instrument[i].imgInstrument;
                txtMainTextInstrument.text = instrument[i].mainText;
                txtCaptionInstrument.text = instrument[i].txtUnderImage;


                ms.txtTitleInstrument.text = instrument[i].nameInstrument;

            }


        }
        if (pnlHelp.activeSelf) CloseHelpPanel();

        Debug.Log("Middle65 Scene");
        btnNext.onClick.AddListener(OpenAulosMainScene);

    }

    //when we open the trigono panel
    void OpenTrigonoMiddleScene()
    {

        ClosedPanelsGO(false,false, false, true, true, true, false,false);

        isDafnis = false;
        isTrigono = true;
        isLyra = false;
        btnExtraMenu.gameObject.SetActive(false);
        separate.SetActive(false);


        isMegarwnH64 = false;
        isMegarwnL64 = false;
        isMegarwnL65 = false;
        isMegarwnH65 = false;

        for (int i = 0; i < instrument.Length; i++)
        {
            txtInstrument.gameObject.SetActive(true);

            ms.txtTitleInstrument.gameObject.SetActive(true);


            if (instrument[i].nameInstrument.Contains(btnTrigono.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                OpenExtraInfoImage();
                txtInstrument.text = "> " + instrument[i].nameInstrument;
                imgContainer.GetComponent<Image>().sprite = instrument[i].imgInstrument;
                txtMainTextInstrument.text = instrument[i].mainText;
                txtCaptionInstrument.text = instrument[i].txtUnderImage;

                ms.txtTitleInstrument.text = instrument[i].nameInstrument;

            }


        }
        if (pnlHelp.activeSelf) CloseHelpPanel();
        Debug.Log("Trigono middle Scene " + isTrigono);
        btnNext.onClick.AddListener(OpenStringMainScene);
    }


    //when we open the helys panel
    void OpenHelysMiddleScene()
    {

        ClosedPanelsGO(false,false, false, true, true, true, false,false);
        btnExtraMenu.gameObject.SetActive(false);
        separate.SetActive(false);

        isDafnis = false;
        isTrigono = false;
        isLyra = true;
        isMegarwnH64 = false;
        isMegarwnL64 = false;
        isMegarwnL65 = false;
        isMegarwnH65 = false;


        for (int i = 0; i < instrument.Length; i++)
        {
            txtInstrument.gameObject.SetActive(true);

            ms.txtTitleInstrument.gameObject.SetActive(true);


            if (instrument[i].nameInstrument.Contains(btnLyra.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                OpenExtraInfoImage();
                txtInstrument.text = "> " + instrument[i].nameInstrument;
                imgContainer.GetComponent<Image>().sprite = instrument[i].imgInstrument;
                txtMainTextInstrument.text = instrument[i].mainText;
                txtCaptionInstrument.text = instrument[i].txtUnderImage;

                ms.txtTitleInstrument.text = instrument[i].nameInstrument;

            }


        }
        if (pnlHelp.activeSelf) CloseHelpPanel();
        Debug.Log("Helys middle Scene " + isLyra);
        btnNext.onClick.AddListener(OpenStringMainScene);
    }


    //on each panel we have a help info to show information to the user
    void OpenHelpPanel()
    {

        if (pnlMuseumScreen)
        {
            txtTitle.text = "Επιλογή Μουσείου";
            txtMain.text = "Σε αυτην την οθόνη επιλέγετε το Μουσείο." +
                "που ψάχνετε";
        }
        if (pnlPiraeusInstrument.activeSelf || pnlMegaraInstrument.activeSelf)
        {

            txtTitle.text = "Επιλογή Οργάνου";
            txtMain.text = "Σε αυτην την οθόνη επιλέγετε το μουσικό όργανο που θέλετε. Με την επιλογή <b>γρανάζι</b> και την επιλογή <b> Περισσότερες πληροφορίες</b> μπορείτε να δείτε και κάποια βίνετο σχετικά με το Μουσείο που επιλέξατε.";

        }
        if (pnlInstrumentMiddleScreen.activeSelf)
        {
            if (isDafnis && !(isTrigono || isLyra || isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65))
            {
                txtTitle.text = btnAulosDafnis.GetComponentInChildren<TextMeshProUGUI>().text;

            }
            else if (!(isDafnis || isTrigono || isLyra) && (isMegarwnL64 || isMegarwnH64) && !(isMegarwnH65 || isMegarwnL65))
            {
                txtTitle.text = btnAulosMegara64.GetComponentInChildren<TextMeshProUGUI>().text;
                Debug.Log("Meg64 " + isTrigono);
            }
            else if (!(isDafnis || isTrigono || isLyra) && !(isMegarwnL64 || isMegarwnH64) && (isMegarwnH65 || isMegarwnL65))
            {
                txtTitle.text = btnAulosMegara65.GetComponentInChildren<TextMeshProUGUI>().text;
                Debug.Log("Meg65 " + isTrigono);
            }
            else if (isTrigono && !(isDafnis || isLyra || isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65))
            {
                txtTitle.text = btnTrigono.GetComponentInChildren<TextMeshProUGUI>().text;
                Debug.Log("Trigono");
            }
            else if (isLyra && !(isDafnis || isTrigono || isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65))
            {
                txtTitle.text = btnLyra.GetComponentInChildren<TextMeshProUGUI>().text;
                Debug.Log("Lyra");
            }


            txtMain.text = "Σε αυτην την οθόνη βρίσκετε πληροφορίες σχετικά με το όργανο που παρουσιάζεται στο " +
                "αντίστοιχο μουσείο. Με την επιλογή <b>Παράμετροι</b> οδηγείστε στην επόμενη οθόνη για αναπαραγωγή ήχων " +
                "και αλλαγών παραμέτρων";
        }
        if (pnlMainWindInstrument.activeSelf || pnlMainStringInstrument.activeSelf && !(pnlInstrumentMiddleScreen.activeSelf || pnlPiraeusInstrument.activeSelf || pnlMegaraInstrument.activeSelf || pnlMuseumScreen.activeSelf))
        {
            if (isDafnis && !(isMegarwnL64 || isMegarwnH64 || isMegarwnH65 || isMegarwnL65 || isTrigono || isLyra))
            {
                txtTitle.text = btnAulosDafnis.GetComponentInChildren<TextMeshProUGUI>().text;
            }
            else if (!(isDafnis || isTrigono || isLyra) && (isMegarwnL64 || isMegarwnH64) && !(isMegarwnH65 || isMegarwnL65))
            {
                txtTitle.text = btnAulosMegara64.GetComponentInChildren<TextMeshProUGUI>().text;
            }
            else if (!(isDafnis || isTrigono || isLyra) && !(isMegarwnL64 || isMegarwnH64) && (isMegarwnH65 || isMegarwnL65))
            {
                txtTitle.text = btnAulosMegara65.GetComponentInChildren<TextMeshProUGUI>().text;
            }
            else if (isTrigono && !(isMegarwnL64 || isMegarwnH64 || isMegarwnH65 || isMegarwnL65 || isDafnis || isLyra))
            {
                txtTitle.text = btnTrigono.GetComponentInChildren<TextMeshProUGUI>().text;
            }
            else if (isLyra && !(isMegarwnL64 || isMegarwnH64 || isMegarwnH65 || isMegarwnL65 || isDafnis || isTrigono))
            {
                txtTitle.text = btnLyra.GetComponentInChildren<TextMeshProUGUI>().text;
            }

            txtMain.text = "Σε αυτην την οθόνη μπορείτε να επεξεργαστείτε κάποιες παραμέτρους σχετικά με το μουσικό όργανο που επιλέξαμε.\n \n" +
                "Πατώντας το κουμπί <b>Τροποποίηση</b> μπορείτε να επιλέξετε μια από τις τιμές που εμφανίζονται στις εκάστοτε επιλογές. " +
                "Μόλις επιλέξετε την τιμή της αρεσκείας, πατήστε το κουμπί <b> Καταχώρηση</b> για να φορτώσετε τους αντίστοιχους ήχους. " +
                "Σε περίπτωση μη αποδεκτού συνδυασμού τιμών, εμφανίζεται κατάλληλο μήνυμα.\n \n Πατώντας το κουμπί με τις <b>τρείς τελείες</b> μπορείτε να δείτε κάποιες έξτρα επιλογές.";
        }
        if (prefab3Dins.activeSelf && !(pnlInstrumentMiddleScreen.activeSelf || pnlPiraeusInstrument.activeSelf || pnlMegaraInstrument.activeSelf || pnlMainWindInstrument.activeSelf || pnlMainStringInstrument.activeSelf || pnlMuseumScreen.activeSelf))
        {
            if (!isTrigono && (isDafnis || isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65 || isLyra))
            {
                txtTitle.text = "Τρισδιάστατη απεικόνηση Αυλού";
                txtMain.text = "Σε αυτην την οθόνη βλέπουμε την 3D απεικόνηση του αυλού. Μπορούμε να δούμε το όργανο με την βοήθεια του ποντικιού μας και πατώντας το <b>αριστερό κλικ</b> παρατεταμένα" +
                    " περιστρέφουμε το όργανο. Επίσης με το <b>δεξί κλικ</b>, μπορούμε να μετακινήσουμε την κάμερά μας δεξιά ή αριστερά απο το όργανο και μετά να του κάνουμε περιστροφή" +
                    " άμα θέλουμε. Τέλος, με την <b>ροδέλα</b> μας κάνουμε zoom in ή zoom out στο όργανο.";
#if PLATFORM_ANDROID || PLATFORM_IOS
                txtMain.text = "Σε αυτην την οθόνη βλέπουμε την 3D απεικόνηση του τριγώνου. Μπορούμε να δούμε το όργανο με την βοήθεια του χεριού μας και πατώντας και σέρνοντας το <b>δάχτυλό μας</b>" +
                    " περιστρέφουμε το όργανο. Επίσης με τη χρήση <b>δυο δαχτύλων</b>, μπορούμε να μετακινήσουμε την κάμερά μας δεξιά ή αριστερά απο το όργανο και μετά να του κάνουμε περιστροφή" +
                    " άμα θέλουμε. Τέλος, με την χρήση πάλι <b>των δυο δαχτύλων</b> μας κάνουμε zoom in ή zoom out στο όργανο.";
#endif
            }
            else if (isTrigono && !(isDafnis || isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65 || isLyra))
            {
                txtTitle.text = "Τρισδιάστατη απεικόνηση Τριγώνου";

                txtMain.text = "Σε αυτην την οθόνη βλέπουμε την 3D απεικόνηση του τριγώνου. Μπορούμε να δούμε το όργανο με την βοήθεια του ποντικιού μας και πατώντας το <b>αριστερό κλικ</b> παρατεταμένα" +
                    " περιστρέφουμε το όργανο. Επίσης με το <b>δεξί κλικ</b>, μπορούμε να μετακινήσουμε την κάμερά μας δεξιά ή αριστερά απο το όργανο και μετά να του κάνουμε περιστροφή" +
                    " άμα θέλουμε. Τέλος, με την <b>ροδέλα</b> μας κάνουμε zoom in ή zoom out στο όργανο.";

#if PLATFORM_ANDROID || PLATFORM_IOS
                txtMain.text = "Σε αυτην την οθόνη βλέπουμε την 3D απεικόνηση του τριγώνου. Μπορούμε να δούμε το όργανο με την βοήθεια του χεριού μας και πατώντας και σέρνοντας το <b>δάχτυλό μας</b>" +
                    " περιστρέφουμε το όργανο. Επίσης με τη χρήση <b>δυο δαχτύλων</b>, μπορούμε να μετακινήσουμε την κάμερά μας δεξιά ή αριστερά απο το όργανο και μετά να του κάνουμε περιστροφή" +
                    " άμα θέλουμε. Τέλος, με την χρήση πάλι <b>των δυο δαχτύλων</b> μας κάνουμε zoom in ή zoom out στο όργανο.";
#endif
            }
            else if (isLyra && !(isDafnis || isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65 || isTrigono))
            {
                txtTitle.text = "Τρισδιάστατη απεικόνηση Χέλυς";

                txtMain.text = "Σε αυτην την οθόνη βλέπουμε την 3D απεικόνηση του τριγώνου. Μπορούμε να δούμε το όργανο με την βοήθεια του ποντικιού μας και πατώντας το <b>αριστερό κλικ</b> παρατεταμένα" +
                    " περιστρέφουμε το όργανο. Επίσης με το <b>δεξί κλικ</b>, μπορούμε να μετακινήσουμε την κάμερά μας δεξιά ή αριστερά απο το όργανο και μετά να του κάνουμε περιστροφή" +
                    " άμα θέλουμε. Τέλος, με την <b>ροδέλα</b> μας κάνουμε zoom in ή zoom out στο όργανο.";

#if PLATFORM_ANDROID || PLATFORM_IOS
                txtMain.text = "Σε αυτην την οθόνη βλέπουμε την 3D απεικόνηση της Χέλυς. Μπορούμε να δούμε το όργανο με την βοήθεια του χεριού μας και πατώντας και σέρνοντας το <b>δάχτυλό μας</b>" +
                    " περιστρέφουμε το όργανο. Επίσης με τη χρήση <b>δυο δαχτύλων</b>, μπορούμε να μετακινήσουμε την κάμερά μας δεξιά ή αριστερά απο το όργανο και μετά να του κάνουμε περιστροφή" +
                    " άμα θέλουμε. Τέλος, με την χρήση πάλι <b>των δυο δαχτύλων</b> μας κάνουμε zoom in ή zoom out στο όργανο.";
#endif
            }

        }
        if (pnlExtraInfo.activeSelf)
        {
            txtTitle.text = "Video Μουσείων και Μουσικών οργάνων";
            txtMain.text = "Σε αυτην την οθόνη βλέπουμε κάποια έξτρα video σχετικά με το Μουσείο ή το αντίστοιχο όργανο. Σε περίπτωση buffering του video, καλό είναι με το <b>Χ</b> να κλείσουμε το βίντεο" +
                "και να το ξανα επιλέξουμε.";
        }

    }


    //when we select piraeus museum
    void OpenPanelPiraeus()
    {
        isPiraeus = true;
        isMegara = false;

        isDafnis = false;
        isLyra = false;
        isTrigono = false;
        isMegarwnH65 = false;
        isMegarwnH64 = false;
        isMegarwnL64 = false;
        isMegarwnL65 = false;

        ClosedPanelsGO(false,true, false, false, false, true, false,false);

        txtMuseumName.text = btnMuseumPiraeus.GetComponentInChildren<TextMeshProUGUI>().text + " Πειραια";
        btnBackToMain.gameObject.SetActive(true);

        btnMuseum.onClick.RemoveAllListeners();
        btnExtraMenu.gameObject.SetActive(true);
        separate.SetActive(true);
        btn3DScene.gameObject.SetActive(false);


        LoadClips("Piraeus/");


        if (isPiraeus && !isMegara) btnMuseum.onClick.AddListener(() => buttonLevel.URl("http://odysseus.culture.gr/h/1/gh151.jsp?obj_id=3371"));

        if (pnlHelp.activeSelf) CloseHelpPanel();
    }


    //when we select megara museum
    void OpenPanelMegara()
    {
        isPiraeus = false;
        isMegara = true;


        isDafnis = false;
        isLyra = false;
        isTrigono = false;
        isMegarwnH65 = false;
        isMegarwnH64 = false;
        isMegarwnL64 = false;
        isMegarwnL65 = false;

        ClosedPanelsGO(false,false, true, false, false, true, false,false);

        txtMuseumName.text = btnMuseumMegara.GetComponentInChildren<TextMeshProUGUI>().text + " Μεγαρων";
        btnBackToMain.gameObject.SetActive(true);
        btn3DScene.gameObject.SetActive(false);

        btnMuseum.onClick.RemoveAllListeners();
        btnExtraMenu.gameObject.SetActive(true);
        separate.SetActive(true);

        LoadClips("Megara/");


        if (!isPiraeus && isMegara) btnMuseum.onClick.AddListener(() => buttonLevel.URl("http://odysseus.culture.gr/h/1/gh151.jsp?obj_id=3473"));
        if (pnlHelp.activeSelf) CloseHelpPanel();

    }


    //when we select more info to show the videos
    void OpenMoreInfoPanel()
    {
        pnlExtraInfo.SetActive(true);
        pnlMainRawVideo.SetActive(false);

        if (pnlExtraMenu.activeSelf) CloseMenuPanel();
    }

    //reference to the Method motion close from smartMotion script
    public void CloseHelpPanel()
    {
        helpMotion.HidePanel();

    }

    //reference to the Method motion close from smartMotion script
    public void CloseMenuPanel()
    {
        menuMotion.HidePanel();
    }

    //panels that are frequently used to open and close throughout the app, method to open and close them respectetly
    public void ClosedPanelsGO(bool pnlMuseum,bool pnlPir, bool pnlMeg, bool pnlPirMS, bool navBtn, bool btnMus, bool btnBot, bool btnPre)
    {
        pnlMuseumScreen.SetActive(pnlMuseum);
        pnlPiraeusInstrument.SetActive(pnlPir);
        pnlMegaraInstrument.SetActive(pnlMeg);
        pnlInstrumentMiddleScreen.SetActive(pnlPirMS);
        pnlBottom.SetActive(btnBot);
        pnlNavigateButtons.SetActive(navBtn);
        btnMuseum.gameObject.SetActive(btnMus);
        btnPrevious.gameObject.SetActive(btnPre);

    }


    //when in mobile, we have extra panel to play sounds that appear on top of the changed values
    public void OpenPlayPanel()
    {

        ms.pnlSound.SetActive(true);
        pnlBottom.gameObject.SetActive(false);
    }


    //again on mobile when we open extra info, we can see the picture of each instrument
    public void OpenExtraInfoImage()
    {
        if (isDafnis && !(isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65 || isLyra || isTrigono))
        {
            imgContainerExtraInfo.SetActive(true);
            imgContainerExtraInfo.GetComponent<Image>().sprite = imgAulosExtra;
        }
        else if (isMegarwnH64 || isMegarwnH65 || isMegarwnL64 || isMegarwnL65 && !(isLyra || isTrigono || isDafnis))
        {
            imgContainerExtraInfo.SetActive(true);
            imgContainerExtraInfo.GetComponent<Image>().sprite = imgAulosMegarwnExtra;
        }
        else
        {
            imgContainerExtraInfo.SetActive(false);
        }
    }

#endregion

#region GameObjects
    //activate and deactivate buttons when changing or not the values (slide B.4a)
    public void ButtonsWhenChangeValues()
    {
        btnSubmit.gameObject.SetActive(true);
        btnReset.gameObject.SetActive(true);
        btn3DScene.gameObject.SetActive(false);
        btnMoreInfo.gameObject.SetActive(false);
    }

    //activate and deactivate buttons when changing or not the values and then we selected the subit button or reset
    public void ButtonsWhenSubmitOrReset()
    {
        btnSubmit.gameObject.SetActive(false);
        btnReset.gameObject.SetActive(false);
        btnMoreInfo.gameObject.SetActive(true);
        btnChangeValue.gameObject.SetActive(true);
    }

    //load instrument scriptable obkects from resources

    public void LoadInstrumentFiles(string fileName)
    {
        instrument = Resources.LoadAll(fileName, typeof(InstrumentSystem)).Cast<InstrumentSystem>().ToArray();

        Debug.Log("Folder Name Audio: " + fileName);
    }


    //load clips from resources folder when not on webgl
    public void LoadClips(string filename)
    {

        videoClip = Resources.LoadAll(filename, typeof(VideoClip)).Cast<VideoClip>().ToArray();
        for (int i = 0; i < videoClip.Length; i++)
        {
            textureVideos[i].SetActive(true);
            textureVideos[i].GetComponentInChildren<TextMeshProUGUI>().text = videoClip[i].name;
            if (videoClip.Length != textureVideos.Length) textureVideos[i + 1].SetActive(false);
        }

        videoPlayer.time = 0;
    }

    //when platform except webgl to play specific video (we use the same way as the sounds here. This method is public cause we assign it on each button from editor)

    public void PlayVideo(int num)
    {
        videoPlayer.clip = videoClip[num];

        pnlMainRawVideo.SetActive(true);
        
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
           
        }
        else
        {
            videoPlayer.Play();
          
        }
        Debug.Log("Num: " + num);

    }


    //this method works only on video, when we tap on it to pause or play the video.
    public void PlayPauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }
    private void Update()
    {
        if (videoPlayer.isPlaying)
        {
            slVideo.value = (float)videoPlayer.frame / (float)videoPlayer.clip.frameCount;
            btnVideo.gameObject.GetComponent<Image>().sprite = btnPauseSp;
        }
        else
        {
            btnVideo.gameObject.GetComponent<Image>().sprite = btnPlaySp;
        }
       
    }
    
   
#endregion


}
