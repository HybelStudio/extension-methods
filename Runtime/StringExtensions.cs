using System.Linq;
using UnityEngine;

namespace Hybel.ExtensionMethods
{
    /// <summary>
    /// Extension Methods used on strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes any spaces in a string.
        /// </summary>
        /// <param name="text">String.</param>
        /// <returns>String with no spaces.</returns>
        public static string RemoveSpaces(this string input)
        {
            string output = new string(input.ToCharArray()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());

            return output;
        }

        /// <summary>
        /// Converts string to Unity's Keycode enumeration.
        /// Cannot convert the symbols \ (write 'backslash' instead) and " (write 'double quotes' instead).
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>KeyCode.</returns>
        public static KeyCode ToKeyCode(this string key)
        {
            return key.ToLower().RemoveSpaces() switch
            {
                "" => KeyCode.None,
                "backspace" => KeyCode.Backspace,
                "tab" => KeyCode.Tab,
                "clear" => KeyCode.Clear,
                "return" => KeyCode.Return,
                "pause" => KeyCode.Pause,
                "escape" => KeyCode.Escape,
                "space" => KeyCode.Space,
                " " => KeyCode.Space,
                "exclaim" => KeyCode.Exclaim,
                "exclamation" => KeyCode.Exclaim,
                "exclamationpoint" => KeyCode.Exclaim,
                "!" => KeyCode.Exclaim,
                "doublequote" => KeyCode.DoubleQuote,
                "\"" => KeyCode.DoubleQuote,
                "hash" => KeyCode.Hash,
                "hashtag" => KeyCode.Hash,
                "#" => KeyCode.Hash,
                "dollar" => KeyCode.Dollar,
                "$" => KeyCode.Dollar,
                "percent" => KeyCode.Percent,
                "%" => KeyCode.Percent,
                "ampersand" => KeyCode.Ampersand,
                "&" => KeyCode.Ampersand,
                "quote" => KeyCode.Quote,
                "'" => KeyCode.Quote,
                "´" => KeyCode.Quote,
                "leftparen" => KeyCode.LeftParen,
                "leftparenthesis" => KeyCode.LeftParen,
                "(" => KeyCode.LeftParen,
                "rightparen" => KeyCode.RightParen,
                "rightparenthesis" => KeyCode.RightParen,
                ")" => KeyCode.RightParen,
                "asterisk" => KeyCode.Asterisk,
                "*" => KeyCode.Asterisk,
                "plus" => KeyCode.Plus,
                "+" => KeyCode.Plus,
                "comma" => KeyCode.Comma,
                "," => KeyCode.Comma,
                "minus" => KeyCode.Minus,
                "dash" => KeyCode.Minus,
                "-" => KeyCode.Minus,
                "period" => KeyCode.Period,
                "point" => KeyCode.Period,
                "dot" => KeyCode.Period,
                "." => KeyCode.Period,
                "slash" => KeyCode.Slash,
                "forwardslash" => KeyCode.Slash,
                "fslash" => KeyCode.Slash,
                "/" => KeyCode.Slash,
                "zero" => KeyCode.Alpha0,
                "0" => KeyCode.Alpha0,
                "one" => KeyCode.Alpha1,
                "1" => KeyCode.Alpha1,
                "two" => KeyCode.Alpha2,
                "2" => KeyCode.Alpha2,
                "three" => KeyCode.Alpha3,
                "3" => KeyCode.Alpha3,
                "four" => KeyCode.Alpha4,
                "4" => KeyCode.Alpha4,
                "five" => KeyCode.Alpha5,
                "5" => KeyCode.Alpha5,
                "six" => KeyCode.Alpha6,
                "6" => KeyCode.Alpha6,
                "seven" => KeyCode.Alpha7,
                "7" => KeyCode.Alpha7,
                "eight" => KeyCode.Alpha8,
                "8" => KeyCode.Alpha8,
                "nine" => KeyCode.Alpha9,
                "9" => KeyCode.Alpha9,
                "colon" => KeyCode.Colon,
                ":" => KeyCode.Colon,
                "semicolon" => KeyCode.Semicolon,
                ";" => KeyCode.Semicolon,
                "less" => KeyCode.Less,
                "lessthan" => KeyCode.Less,
                "<" => KeyCode.Less,
                "equal" => KeyCode.Equals,
                "equals" => KeyCode.Equals,
                "=" => KeyCode.Equals,
                "greater" => KeyCode.Greater,
                "greaterthan" => KeyCode.Greater,
                ">" => KeyCode.Greater,
                "question" => KeyCode.Question,
                "questionmark" => KeyCode.Question,
                "?" => KeyCode.Question,
                "at" => KeyCode.At,
                "@" => KeyCode.At,
                "leftbracket" => KeyCode.LeftBracket,
                "leftsquarebracket" => KeyCode.LeftBracket,
                "opensquarebracket" => KeyCode.LeftBracket,
                "[" => KeyCode.LeftBracket,
                "backslash" => KeyCode.Backslash,
                @"\" => KeyCode.Backslash,
                "rightbracket" => KeyCode.RightBracket,
                "rightsquarebracket" => KeyCode.RightBracket,
                "closedsquarebracket" => KeyCode.RightBracket,
                "]" => KeyCode.RightBracket,
                "caret" => KeyCode.Caret,
                "^" => KeyCode.Caret,
                "underscore" => KeyCode.Underscore,
                "_" => KeyCode.Underscore,
                "backquote" => KeyCode.BackQuote,
                "`" => KeyCode.BackQuote,
                "a" => KeyCode.A,
                "b" => KeyCode.B,
                "c" => KeyCode.C,
                "d" => KeyCode.D,
                "e" => KeyCode.E,
                "f" => KeyCode.F,
                "g" => KeyCode.G,
                "h" => KeyCode.H,
                "i" => KeyCode.I,
                "j" => KeyCode.J,
                "k" => KeyCode.K,
                "l" => KeyCode.L,
                "m" => KeyCode.M,
                "n" => KeyCode.N,
                "o" => KeyCode.O,
                "p" => KeyCode.P,
                "q" => KeyCode.Q,
                "r" => KeyCode.R,
                "s" => KeyCode.S,
                "t" => KeyCode.T,
                "u" => KeyCode.U,
                "v" => KeyCode.V,
                "w" => KeyCode.W,
                "x" => KeyCode.X,
                "y" => KeyCode.Y,
                "z" => KeyCode.Z,
                "leftcurlybracket" => KeyCode.LeftCurlyBracket,
                "opencurlybracket" => KeyCode.LeftCurlyBracket,
                "{" => KeyCode.LeftCurlyBracket,
                "pipe" => KeyCode.Pipe,
                "|" => KeyCode.Pipe,
                "rightcurlybracket" => KeyCode.RightCurlyBracket,
                "closecurlybracket" => KeyCode.RightCurlyBracket,
                "}" => KeyCode.RightCurlyBracket,
                "tilde" => KeyCode.Tilde,
                "~" => KeyCode.Tilde,
                "delete" => KeyCode.Delete,
                "del" => KeyCode.Delete,
                "num0" => KeyCode.Keypad0,
                "numinsert" => KeyCode.Keypad0,
                "num1" => KeyCode.Keypad1,
                "numend" => KeyCode.Keypad1,
                "num2" => KeyCode.Keypad2,
                "numdownarrow" => KeyCode.Keypad2,
                "num3" => KeyCode.Keypad3,
                "numpgdn" => KeyCode.Keypad3,
                "numpagedown" => KeyCode.Keypad3,
                "num4" => KeyCode.Keypad4,
                "numleftarrow" => KeyCode.Keypad4,
                "num5" => KeyCode.Keypad5,
                "num6" => KeyCode.Keypad6,
                "numrightarrow" => KeyCode.Keypad6,
                "num7" => KeyCode.Keypad7,
                "numhome" => KeyCode.Keypad7,
                "num8" => KeyCode.Keypad8,
                "numuparrow" => KeyCode.Keypad8,
                "num9" => KeyCode.Keypad9,
                "numpgup" => KeyCode.Keypad9,
                "numpageup" => KeyCode.Keypad9,
                "numperiod" => KeyCode.KeypadPeriod,
                "numdot" => KeyCode.KeypadPeriod,
                "numpoint" => KeyCode.KeypadPeriod,
                "numdel" => KeyCode.KeypadPeriod,
                "numdelete" => KeyCode.KeypadPeriod,
                "num," => KeyCode.KeypadPeriod,
                "numcomma" => KeyCode.KeypadPeriod,
                "numdivide" => KeyCode.KeypadDivide,
                "numslash" => KeyCode.KeypadDivide,
                "num/" => KeyCode.KeypadDivide,
                "nummultiply" => KeyCode.KeypadMultiply,
                "numasterisk" => KeyCode.KeypadMultiply,
                "num*" => KeyCode.KeypadMultiply,
                "numminus" => KeyCode.KeypadMinus,
                "numdash" => KeyCode.KeypadMinus,
                "num-" => KeyCode.KeypadMinus,
                "numplus" => KeyCode.KeypadPlus,
                "num+" => KeyCode.KeypadPlus,
                "numenter" => KeyCode.KeypadEnter,
                "numequals" => KeyCode.KeypadEquals,
                "up" => KeyCode.UpArrow,
                "uparrow" => KeyCode.UpArrow,
                "down" => KeyCode.DownArrow,
                "downarrow" => KeyCode.DownArrow,
                "right" => KeyCode.RightArrow,
                "rightarrow" => KeyCode.RightArrow,
                "left" => KeyCode.LeftArrow,
                "leftarrow" => KeyCode.LeftArrow,
                "insert" => KeyCode.Insert,
                "ins" => KeyCode.Insert,
                "home" => KeyCode.Home,
                "end" => KeyCode.End,
                "pgup" => KeyCode.PageUp,
                "pagegup" => KeyCode.PageUp,
                "pgdn" => KeyCode.PageDown,
                "pagegdown" => KeyCode.PageDown,
                "f1" => KeyCode.F1,
                "f2" => KeyCode.F2,
                "f3" => KeyCode.F3,
                "f4" => KeyCode.F4,
                "f5" => KeyCode.F5,
                "f6" => KeyCode.F6,
                "f7" => KeyCode.F7,
                "f8" => KeyCode.F8,
                "f9" => KeyCode.F9,
                "f10" => KeyCode.F10,
                "f11" => KeyCode.F11,
                "f12" => KeyCode.F12,
                "f13" => KeyCode.F13,
                "f14" => KeyCode.F14,
                "f15" => KeyCode.F15,
                "numlock" => KeyCode.Numlock,
                "numlk" => KeyCode.Numlock,
                "capslock" => KeyCode.CapsLock,
                "capslk" => KeyCode.CapsLock,
                "caps" => KeyCode.CapsLock,
                "scrolllock" => KeyCode.ScrollLock,
                "scrolllk" => KeyCode.ScrollLock,
                "rightshift" => KeyCode.RightShift,
                "leftshift" => KeyCode.LeftShift,
                "rightctrl" => KeyCode.RightControl,
                "rightcontrol" => KeyCode.RightControl,
                "leftctrl" => KeyCode.LeftControl,
                "leftcontrol" => KeyCode.LeftControl,
                "rightalt" => KeyCode.RightAlt,
                "leftalt" => KeyCode.LeftAlt,
                "rightcommand" => KeyCode.RightCommand,
                "rightapple" => KeyCode.RightApple,
                "leftcommand" => KeyCode.LeftCommand,
                "leftapple" => KeyCode.LeftApple,
                "leftwindows" => KeyCode.LeftWindows,
                "rightwindows" => KeyCode.RightWindows,
                "altgr" => KeyCode.AltGr,
                "help" => KeyCode.Help,
                "print" => KeyCode.Print,
                "printscreen" => KeyCode.Print,
                "prtsc" => KeyCode.Print,
                "sysrq" => KeyCode.SysReq,
                "sysreq" => KeyCode.SysReq,
                "break" => KeyCode.Break,
                "menu" => KeyCode.Menu,
                "mouse0" => KeyCode.Mouse0,
                "m0" => KeyCode.Mouse0,
                "leftmouse" => KeyCode.Mouse0,
                "leftmousebutton" => KeyCode.Mouse0,
                "mouse1" => KeyCode.Mouse1,
                "m1" => KeyCode.Mouse1,
                "rightmouse" => KeyCode.Mouse1,
                "rightmousebutton" => KeyCode.Mouse1,
                "mouse2" => KeyCode.Mouse2,
                "m2" => KeyCode.Mouse2,
                "middlemouse" => KeyCode.Mouse2,
                "middlemousebutton" => KeyCode.Mouse2,
                "mouse3" => KeyCode.Mouse3,
                "m3" => KeyCode.Mouse3,
                "mouseback" => KeyCode.Mouse3,
                "mouse4" => KeyCode.Mouse4,
                "m4" => KeyCode.Mouse4,
                "mousefront" => KeyCode.Mouse4,
                "mouse5" => KeyCode.Mouse5,
                "m5" => KeyCode.Mouse5,
                "mouse6" => KeyCode.Mouse6,
                "m6" => KeyCode.Mouse6,
                _ => KeyCode.None,
            };
        }

        /// <summary>
        /// Checks if the string contains any of the <paramref name="values"/> and outs the <paramref name="containedValue"/>.
        /// </summary>
        public static bool ContainsAny(this string @string, out string containedValue, params string[] values)
        {
            foreach (string value in values)
            {
                if (@string.Contains(value))
                {
                    containedValue = value;
                    return true;
                }
            }

            containedValue = null;
            return false;
        }

        /// <summary>
        /// Checks if the string contains any of the <paramref name="values"/>.
        /// </summary>
        public static bool ContainsAny(this string @string, params string[] values)
        {
            foreach (string value in values)
                if (@string.Contains(value))
                    return true;

            return false;
        }

        /// <summary>
        /// Makes the <paramref name="input"/> more uniform by removing spaces and making all characters lower case.
        /// </summary>
        public static string MakeUniform(this string input) => input.RemoveSpaces().ToLower();
    }
}