using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Static Class used to store all viable controls in list for iteration
 * 
 */
namespace ControlTools
{
    public class ControlScheme
    {
        public static List<KeyCode> viableKeys = new List<KeyCode>();

        static ControlScheme() {

            #region chars
            viableKeys.Add(KeyCode.Q);
            viableKeys.Add(KeyCode.W);
            viableKeys.Add(KeyCode.E);
            viableKeys.Add(KeyCode.R);
            viableKeys.Add(KeyCode.T);
            viableKeys.Add(KeyCode.Y);
            viableKeys.Add(KeyCode.U);
            viableKeys.Add(KeyCode.I);
            viableKeys.Add(KeyCode.O);
            viableKeys.Add(KeyCode.P);
            viableKeys.Add(KeyCode.A);
            viableKeys.Add(KeyCode.S);
            viableKeys.Add(KeyCode.D);
            viableKeys.Add(KeyCode.F);
            viableKeys.Add(KeyCode.G);
            viableKeys.Add(KeyCode.H);
            viableKeys.Add(KeyCode.J);
            viableKeys.Add(KeyCode.K);
            viableKeys.Add(KeyCode.L);
            viableKeys.Add(KeyCode.Z);
            viableKeys.Add(KeyCode.X);
            viableKeys.Add(KeyCode.C);
            viableKeys.Add(KeyCode.V);
            viableKeys.Add(KeyCode.B);
            viableKeys.Add(KeyCode.N);
            viableKeys.Add(KeyCode.M);
            #endregion

            #region numbers
            viableKeys.Add(KeyCode.Alpha0);
            viableKeys.Add(KeyCode.Alpha1);
            viableKeys.Add(KeyCode.Alpha2);
            viableKeys.Add(KeyCode.Alpha3);
            viableKeys.Add(KeyCode.Alpha4);
            viableKeys.Add(KeyCode.Alpha5);
            viableKeys.Add(KeyCode.Alpha6);
            viableKeys.Add(KeyCode.Alpha7);
            viableKeys.Add(KeyCode.Alpha8);
            viableKeys.Add(KeyCode.Alpha9);
            viableKeys.Add(KeyCode.Keypad0);
            viableKeys.Add(KeyCode.Keypad1);
            viableKeys.Add(KeyCode.Keypad2);
            viableKeys.Add(KeyCode.Keypad3);
            viableKeys.Add(KeyCode.Keypad4);
            viableKeys.Add(KeyCode.Keypad5);
            viableKeys.Add(KeyCode.Keypad6);
            viableKeys.Add(KeyCode.Keypad7);
            viableKeys.Add(KeyCode.Keypad8);
            viableKeys.Add(KeyCode.Keypad9);
            #endregion

            #region others
            viableKeys.Add(KeyCode.Space);
            viableKeys.Add(KeyCode.Comma);
            viableKeys.Add(KeyCode.Period);
            viableKeys.Add(KeyCode.Slash);
            viableKeys.Add(KeyCode.LeftBracket);
            viableKeys.Add(KeyCode.RightBracket);
            viableKeys.Add(KeyCode.Backslash);
            viableKeys.Add(KeyCode.Semicolon);
            viableKeys.Add(KeyCode.Quote);
            viableKeys.Add(KeyCode.Minus);
            viableKeys.Add(KeyCode.Equals);
            viableKeys.Add(KeyCode.BackQuote);
            #endregion
        }

    }
}