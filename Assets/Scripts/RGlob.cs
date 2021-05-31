using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rglob {
    public static int GlineCount;
    public static bool gameIsPaused=false;
    public static Coroutine CorMovSave;
    public static Coroutine CorRotSave;
    public static Coroutine CorRotWrapper = null;
    private static int _instructionscount;
    public static Text instructionText;
    public static bool Lose=false;
    public static bool Win=false;
    public static bool WaitforLanding=false;

    public static int InstructionsCount {
        get {return _instructionscount;}
        set {
            _instructionscount = value;
            instructionText.text = InstructionsCount.ToString();
        }
    }



}
