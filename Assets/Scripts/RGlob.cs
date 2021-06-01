using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rglob {
    public static int GlineCount;
    public static bool gameIsPaused = false;
    public static Coroutine CorMovSave;
    public static Coroutine CorRotSave;
    public static Coroutine CorRotWrapper = null;
    private static int _instructionscount;
    public static Text instructionText;
    public static bool Lose = false;
    public static bool Win = false;
    public static bool WaitforLanding = false;

    public static int InstructionsCount {
        get { return _instructionscount; }
        set {
            _instructionscount = value;
            instructionText.text = InstructionsCount.ToString();
        }
    }

    public static Hashtable Elements = new Hashtable();
    public static Text ElementsText;

    private static string _elstr="";
    public static string ElementsStr {
        get { return _elstr; }
        set {
            if (!_elstr.Contains(value))
                _elstr += value + "\n";
                ElementsText.text = _elstr;
        }
    }

    public static void Reset() {
        GlineCount = 0;
        gameIsPaused = false;
        CorMovSave = null;
        CorRotSave = null;
        CorRotWrapper = null;
        _instructionscount = 0;
        instructionText = null;
        Lose = false;
        Win = false;
        WaitforLanding = false;
        _elstr = "";
        Elements = null;
        ElementsText = null;
        //ElementsStr = "";
    }

    public void RefreshObjs() {

    }
    /*   public static void Reset(Type type) {
           var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
           foreach (var a in fields)
               a.SetValue(null, Activator.CreateInstance(a.FieldType));
           type.TypeInitializer.Invoke(null, null);
       }
   */
}
