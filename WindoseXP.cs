using BepInEx;
using HarmonyLib;
using ngov3;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WindoseXPWhite
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInProcess("Windose.exe")]
    public class MyPatches : BaseUnityPlugin
    {
        public const string pluginGuid = "needy.girl.xp";
        public const string pluginName = "WindoseXP";
        public const string pluginVersion = "1.0.0.1";

        public static PluginInfo PInfo { get; private set; }
        public void Awake()
        {
            PInfo = Info;

            Logger.LogInfo("WindoseXPWhite loaded.");

            // Original code for turning text to white.

            Harmony harmony = new Harmony(pluginGuid);
            MethodInfo originalWinSetName = AccessTools.Method(typeof(Window), "SetName");
            MethodInfo originalWin2DSetName = AccessTools.Method(typeof(Window2D), "SetName");
            MethodInfo originalCompactSetName = AccessTools.Method(typeof(Window_Compact), "SetName");
            MethodInfo originalNoIntSetName = AccessTools.Method(typeof(Window_NoInteractive), "SetName");
            MethodInfo originalTaskBSetName = AccessTools.Method(typeof(TaskButton), "SetName");
            MethodInfo originalDayUpdate = AccessTools.Method(typeof(DayView), "UpdateDay");
            MethodInfo originalStartUpdated = AccessTools.Method(typeof(StartButton), "OnLanguageUpdated");
            MethodInfo originalChipGetAwake = AccessTools.Method(typeof(ChipGetCover), "Awake");
            MethodInfo originalBootAwake = AccessTools.Method(typeof(Boot), "Awake");
            MethodInfo originalStartSwitchAwake = AccessTools.Method(typeof(StartMenuButtonSwitcher), "Awake");
            MethodInfo originalStartExitAwake = AccessTools.Method(typeof(StartMenuButtonSwitcher), "OnPointerExit");

            // Patch code.

            MethodInfo patchSetNameWhite = AccessTools.Method(typeof(WindoseXPWhite), "SetNameToWhite");
            MethodInfo patchSetDayWhite = AccessTools.Method(typeof(WindoseXPWhite), "UpdateDayToWhite");
            MethodInfo patchSetTaskWhite = AccessTools.Method(typeof(WindoseXPWhite), "SetTaskNameToWhite");
            MethodInfo patchSetStartNull = AccessTools.Method(typeof(WindoseXPWhite), "OnLanguageUpdated_Null");
            MethodInfo patchChipTextWhite = AccessTools.Method(typeof(WindoseXPWhite), "SetChipTextToWhite");
            MethodInfo patchStartMenuTextWhite = AccessTools.Method(typeof(WindoseXPWhite), "SetStartMenuTextToWhite");
            MethodInfo patchStartPointerExitNull = AccessTools.Method(typeof(WindoseXPWhite), "StartPointerExit_Null");

            //Patching original code to white text.

            harmony.Patch(originalWinSetName, null, new HarmonyMethod(patchSetNameWhite));
            harmony.Patch(originalWin2DSetName, null, new HarmonyMethod(patchSetNameWhite));
            harmony.Patch(originalCompactSetName, null, new HarmonyMethod(patchSetNameWhite));
            harmony.Patch(originalNoIntSetName, null, new HarmonyMethod(patchSetNameWhite));
            harmony.Patch(originalTaskBSetName, null, new HarmonyMethod(patchSetTaskWhite));
            harmony.Patch(originalDayUpdate, null, new HarmonyMethod(patchSetDayWhite));
            harmony.Patch(originalChipGetAwake, new HarmonyMethod(patchChipTextWhite));
            harmony.Patch(originalStartSwitchAwake, new HarmonyMethod(patchStartMenuTextWhite));
            harmony.Patch(originalStartExitAwake, new HarmonyMethod(patchStartPointerExitNull));
            harmony.Patch(originalStartUpdated, new HarmonyMethod(patchSetStartNull));

            //Original code.

            MethodInfo originalWindow = AccessTools.Method(typeof(Window), "Awake");
            MethodInfo originalWindow2D = AccessTools.Method(typeof(Window2D), "Awake");
            MethodInfo originalWindowCom = AccessTools.Method(typeof(Window_Compact), "Awake");
            MethodInfo originalWindowNoInt = AccessTools.Method(typeof(Window_NoInteractive), "Awake");
            MethodInfo originalDayView = AccessTools.Method(typeof(DayView), "Start");
            MethodInfo originalStartMenu = AccessTools.Method(typeof(StartMenuView), "Awake");
            MethodInfo originalStartButton = AccessTools.Method(typeof(StartButton), "Awake");
            MethodInfo originalStartButtonLang = AccessTools.Method(typeof(StartButton), "OnLanguageUpdated");
            MethodInfo originalTaskbar = AccessTools.Method(typeof(TaskbarManager), "SetTaskbarInteractive");
            MethodInfo originalNotif = AccessTools.Method(typeof(Notification), "Show");
            MethodInfo originalStat2D = AccessTools.Method(typeof(StatusTooltip2D), "SetActionName");
            MethodInfo originalCaution = AccessTools.Method(typeof(Boot), "waitAccept");

            // Patch code.

            MethodInfo patchWindowXP = AccessTools.Method(typeof(WindoseXPAssets), "UpdateWindows");
            MethodInfo patchWindowXP2D = AccessTools.Method(typeof(WindoseXPAssets), "UpdateWindows2D");
            MethodInfo patchWindowXPCom = AccessTools.Method(typeof(WindoseXPAssets), "UpdateWindowCompact");
            MethodInfo patchWindowXPNoInt = AccessTools.Method(typeof(WindoseXPAssets), "UpdateWindowNoInt");
            MethodInfo patchBootScreen = AccessTools.Method(typeof(WindoseXPAssets), "UpdateBootScreen");
            MethodInfo patchTaskButton = AccessTools.Method(typeof(WindoseXPAssets), "UpdateTaskButton");
            MethodInfo patchTaskBar = AccessTools.Method(typeof(WindoseXPAssets), "UpdateTaskBar");
            MethodInfo patchDayView = AccessTools.Method(typeof(WindoseXPAssets), "UpdateDayView");
            MethodInfo patchStartMenu = AccessTools.Method(typeof(WindoseXPAssets), "UpdateStartMenu");
            MethodInfo patchStartButton = AccessTools.Method(typeof(WindoseXPAssets), "UpdateStartButton");
            MethodInfo patchStartButtonOver = AccessTools.Method(typeof(WindoseXPAssets), "UpdateStartButton_Override");
            MethodInfo patchNotifCover = AccessTools.Method(typeof(WindoseXPAssets), "UpdateNotifCover");
            MethodInfo patchStatCover = AccessTools.Method(typeof(WindoseXPAssets), "UpdateStatCover");
            MethodInfo patchBootCaution = AccessTools.Method(typeof(WindoseXPAssets), "UpdateBootCaution");

            //Patching original code to change window texture.

            harmony.Patch(originalWindow, null, new HarmonyMethod(patchWindowXP));
            harmony.Patch(originalWindow2D, null, new HarmonyMethod(patchWindowXP2D));
            harmony.Patch(originalWindowCom, null, new HarmonyMethod(patchWindowXPCom));
            harmony.Patch(originalWindowNoInt, null, new HarmonyMethod(patchWindowXPNoInt));
            harmony.Patch(originalBootAwake, null, new HarmonyMethod(patchBootScreen));
            harmony.Patch(originalTaskBSetName, null, new HarmonyMethod(patchTaskButton));
            harmony.Patch(originalStartSwitchAwake, null, new HarmonyMethod(patchTaskBar));
            harmony.Patch(originalDayView, new HarmonyMethod(patchDayView));
            harmony.Patch(originalStartMenu, new HarmonyMethod(patchStartMenu));
            harmony.Patch(originalStartButton, new HarmonyMethod(patchStartButton));
            harmony.Patch(originalStartButtonLang, new HarmonyMethod(patchStartButton));
            //harmony.Patch(originalTaskbar, null, new HarmonyMethod(patchStartButtonOver));
            harmony.Patch(originalNotif, new HarmonyMethod(patchNotifCover));
            harmony.Patch(originalStat2D, null, new HarmonyMethod(patchStatCover));
            harmony.Patch(originalCaution, null, new HarmonyMethod(patchBootCaution));
        }
    }
    public class BuildAssets
    {
        //Building assets.

        public static AssetBundle assets = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(MyPatches.PInfo.Location), "windowsxp_assets.bundle"));
        public static Sprite baseButtonXP = assets.LoadAsset<Sprite>("BaseButton");
        public static Texture2D baseButtonDisXP = assets.LoadAsset<Texture2D>("BaseButtonDisabled");
        public static Texture2D baseButtonHovXP = assets.LoadAsset<Texture2D>("BaseButtonHovered");
        public static Sprite baseButtonPressXP = assets.LoadAsset<Sprite>("BaseButtonPressed");
        public static Sprite bootEnXP = assets.LoadAsset<Sprite>("bg_boot_en");
        public static Sprite bootXP = assets.LoadAsset<Sprite>("boot_logo");
        public static Sprite winButtonCloseXP = assets.LoadAsset<Sprite>("button_close1");
        public static Sprite dayButtonXP = assets.LoadAsset<Sprite>("button_day");
        public static Sprite winButtonMaxXP = assets.LoadAsset<Sprite>("button_maximize1");
        public static Sprite winButtonMinXP = assets.LoadAsset<Sprite>("button_minimize1");
        public static Sprite startButtonXP = assets.LoadAsset<Sprite>("button_start");
        public static Sprite startButtonJP_XP = assets.LoadAsset<Sprite>("button_start-JP");
        public static Sprite startButtonKO_XP = assets.LoadAsset<Sprite>("button_start-KO");
        public static Sprite startButtonCN_XP = assets.LoadAsset<Sprite>("button_start-CN");
        public static Sprite startButtonTW_XP = assets.LoadAsset<Sprite>("button_start-TW");
        public static Sprite backgroundXP = assets.LoadAsset<Sprite>("FHDbg");
        public static Sprite footerXP = assets.LoadAsset<Sprite>("Footer");
        public static Sprite startMenuXP = assets.LoadAsset<Sprite>("start_menu");
        public static Sprite startButtonPressXP = assets.LoadAsset<Sprite>("start_pressed");
        public static Sprite startButtonPressJP_XP = assets.LoadAsset<Sprite>("start_pressed-JP");
        public static Sprite startButtonPressKO_XP = assets.LoadAsset<Sprite>("start_pressed-KO");
        public static Sprite startButtonPressCN_XP = assets.LoadAsset<Sprite>("start_pressed-CN");
        public static Sprite startButtonPressTW_XP = assets.LoadAsset<Sprite>("start_pressed-TW");
        public static Sprite tuutiXP = assets.LoadAsset<Sprite>("tuuti");
        public static Sprite tuutinotifXP = assets.LoadAsset<Sprite>("tuuti_notif");
        public static Sprite windowActiveXP = assets.LoadAsset<Sprite>("windowbase_active");
        public static Sprite windowInActiveXP = assets.LoadAsset<Sprite>("windowbase_inactive");

    }
    // Patches that change the text color to white.
    public class WindoseXPWhite
    {
        public static void SetNameToWhite(/*string name,*/ ref TMP_Text ___title)
        {
            ___title.color = Color.white;
        }

        public static void SetTaskNameToWhite(/*string name,*/ ref TMP_Text ____title)
        {
            ____title.color = Color.white;
        }
        public static void UpdateDayToWhite(/*int index,*/ ref TMP_Text ____dayText)
        {
            ____dayText.color = Color.white;
        }
        public static bool OnLanguageUpdated_Null(ref Button ____startButton)
        {
            ____startButton.GetComponentInChildren<TMP_Text>().text = "";
            return false;
        }
        public static void SetChipTextToWhite(ref TMP_Text ____windowTitleLabel, ref Button ____next)
        {
            //____next.GetComponentInChildren<TMP_Text>().color = Color.white;
            ____windowTitleLabel.color = Color.white;
        }
        public static bool StartPointerExit_Null(PointerEventData e, ref Image ____buttonBg, ref TMP_Text ____label, ref Sprite ____defaultSprite, ref Color ____pinkWhite)
        {
            ____buttonBg.sprite = ____defaultSprite;
            ____label.color = ____pinkWhite;
            return false;
        }
        public static void SetStartMenuTextToWhite(ref TMP_Text ____label, ref Color ____pinkWhite)
        {
            ;
            ____label.color = ____pinkWhite;
        }
    }

    // Patches that change the window texture.
    public class WindoseXPAssets
    {
        public static void UpdateWindows(ref Button ___close, ref Button ___minimize, ref Button ___maximize, ref Sprite ____active, ref Sprite ____inActive)
        {
            ____active = BuildAssets.windowActiveXP;
            ____inActive = BuildAssets.windowInActiveXP;
            ___close.GetComponentInChildren<Image>().sprite = BuildAssets.winButtonCloseXP;
            ___minimize.GetComponentInChildren<Image>().sprite = BuildAssets.winButtonMinXP;
            ___maximize.GetComponentInChildren<Image>().sprite = BuildAssets.winButtonMaxXP;

        }
        public static void UpdateWindows2D(ref Button2D ___close, ref Button2D ___minimize, ref Button2D ___maximize, ref Sprite ____active, ref Sprite ____inActive)
        {
            ____active = BuildAssets.windowActiveXP;
            ____inActive = BuildAssets.windowInActiveXP;
            ___close.GetComponentInChildren<SpriteRenderer>().sprite = BuildAssets.winButtonCloseXP;
            ___minimize.GetComponentInChildren<SpriteRenderer>().sprite = BuildAssets.winButtonMinXP;
            ___maximize.GetComponentInChildren<SpriteRenderer>().sprite = BuildAssets.winButtonMaxXP;
        }
        public static void UpdateWindowCompact(ref Button ___close, ref Sprite ____active, ref Sprite ____inActive)
        {
            ____active = BuildAssets.windowActiveXP;
            ____inActive = BuildAssets.windowInActiveXP;
            ___close.GetComponentInChildren<Image>().sprite = BuildAssets.winButtonCloseXP;
        }
        public static void UpdateWindowNoInt(ref Sprite ____active, ref Sprite ____inActive)
        {
            ____active = BuildAssets.windowActiveXP;
            ____inActive = BuildAssets.windowInActiveXP;
        }
        public static void UpdateBootScreen(ref Image ___BootImage, ref Sprite ___enBoot)
        {
            ___BootImage.sprite = BuildAssets.bootXP;
            ___enBoot = BuildAssets.bootEnXP;
        }
        public static void UpdateTaskButton(ref Image ___bg, ref Sprite ___Content, ref Sprite ___Empty)
        {
            ___bg.sprite = BuildAssets.baseButtonPressXP;
            ___Content = BuildAssets.baseButtonXP;
            ___Empty = BuildAssets.baseButtonPressXP;
        }
        public static void UpdateTaskBar(ref Sprite ____defaultSprite, ref Image ____buttonBg)
        {
            GameObject.Find("WindowBase").GetComponentInChildren<Image>().sprite = BuildAssets.windowActiveXP;
            SingletonMonoBehaviour<TaskbarManager>.Instance.TaskBarGroup.GetComponentInChildren<Image>().sprite = BuildAssets.footerXP;
            GameObject.Find("MainPanel").GetComponentInChildren<Image>().sprite = BuildAssets.assets.LoadAsset<Sprite>("FHDbg");
            ____defaultSprite = BuildAssets.baseButtonXP;
            ____buttonBg.sprite = BuildAssets.baseButtonXP;
        }
        public static void UpdateDayView(ref Button ____calenderButton)
        {
            ____calenderButton.GetComponentInChildren<Image>().sprite = BuildAssets.dayButtonXP;
        }
        public static void UpdateStartMenu(ref StartButton ____startButton, ref Button ____startMenuParent)
        {
            GameObject.Find("StartMenu").gameObject.GetComponent<Image>().sprite = BuildAssets.startMenuXP;
        }
        public static void UpdateStartButton_Override(ref CanvasGroup ____taskbarGroup)
        {
            GameObject.Find("StartButton").GetComponent<Image>().overrideSprite = BuildAssets.assets.LoadAsset<Sprite>("start_pressed");
            if (____taskbarGroup.interactable == true)
            {
                GameObject.Find("StartButton").GetComponent<Image>().overrideSprite = null;
            }

        }
        public static void UpdateStartButton(StartButton __instance, ref Button ____startButton)
        {
            var buttonState = ____startButton.spriteState;
            LanguageType lang = SingletonMonoBehaviour<Settings>.Instance.CurrentLanguage.Value;
            switch (lang)
            {
                case LanguageType.JP:
                    GameObject.Find("StartButton").GetComponent<Image>().sprite = BuildAssets.startButtonJP_XP;
                    buttonState.pressedSprite = BuildAssets.startButtonPressJP_XP;
                    buttonState.disabledSprite = BuildAssets.startButtonPressJP_XP;
                    break;
                case LanguageType.KO:
                    GameObject.Find("StartButton").GetComponent<Image>().sprite = BuildAssets.startButtonKO_XP;
                    buttonState.pressedSprite = BuildAssets.startButtonPressKO_XP;
                    buttonState.disabledSprite = BuildAssets.startButtonPressKO_XP;
                    break;
                case LanguageType.CN:
                    GameObject.Find("StartButton").GetComponent<Image>().sprite = BuildAssets.startButtonCN_XP;
                    buttonState.pressedSprite = BuildAssets.startButtonPressCN_XP;
                    buttonState.disabledSprite = BuildAssets.startButtonPressCN_XP;
                    break;
                case LanguageType.TW:
                    GameObject.Find("StartButton").GetComponent<Image>().sprite = BuildAssets.startButtonTW_XP;
                    buttonState.pressedSprite = BuildAssets.startButtonPressTW_XP;
                    buttonState.disabledSprite = BuildAssets.startButtonPressTW_XP;
                    break;
                default:
                    GameObject.Find("StartButton").GetComponent<Image>().sprite = BuildAssets.startButtonXP;
                    buttonState.pressedSprite = BuildAssets.startButtonPressXP;
                    buttonState.disabledSprite = BuildAssets.startButtonPressXP;
                    break;
            }
            ____startButton.spriteState = buttonState;
        }
        public static void UpdateNotifCover(ref Button ___button, Image ___icon, TMP_Text ___nakami)
        {
            ___button.GetComponent<Image>().sprite = BuildAssets.tuutinotifXP;
            ___icon.transform.position += Vector3.up * 0.02f;
            ___nakami.transform.position += Vector3.up * 0.02f;
        }
        public static void UpdateStatCover(ref RectTransform ____rectTr)
        {
            ____rectTr.GetComponentInChildren<SpriteRenderer>().sprite = BuildAssets.tuutiXP;
        }
        public static void UpdateBootCaution()
        {
            GameObject.Find("WindowBase").gameObject.GetComponent<Image>().sprite = BuildAssets.windowActiveXP;
            GameObject.Find("Header").GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }
}


