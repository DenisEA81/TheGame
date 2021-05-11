using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolLibrary
{
    public struct ButtonKey
    {
        public enum ButtonType { None = -1, Keyboard = 0, MouseButton = 1 }

        public ButtonType KeyButtonType;
        public Keys Key;
        public MouseButtons MouseButton;

        public ButtonKey(Keys keyboardKey)
        {
            KeyButtonType = ButtonType.Keyboard;
            Key = keyboardKey;
            MouseButton = MouseButtons.None;
        }

        public ButtonKey(MouseButtons mouseButton)
        {
            KeyButtonType = ButtonType.MouseButton;
            MouseButton = mouseButton;
            Key = Keys.None;
        }
    }

    public class ButtonController
    {
        protected Dictionary<ButtonKey, bool> KeyList = null;

        public ButtonController()
        {
            KeyList = new Dictionary<ButtonKey, bool>();
        }

        public ButtonController(Dictionary<ButtonKey, bool> source) => KeyList = source;

        public ButtonController(List<ButtonKey> keyList)
        {
            KeyList = new Dictionary<ButtonKey, bool>();
            foreach (ButtonKey bk in keyList)
                KeyList.Add(bk, false);
        }

        public bool Consist(ButtonKey key) => KeyList.ContainsKey(key);
        public bool Consist(Keys key) => KeyList.ContainsKey(new ButtonKey(key));

        public bool Consist(MouseButtons button) => KeyList.ContainsKey(new ButtonKey(button));

        public void SetValue(ButtonKey key, bool value)=>KeyList[key] = value;

        public void SetValue(Keys key, bool value)=>KeyList[new ButtonKey(key)] = value;

        public void SetValue(MouseButtons button, bool value)=>KeyList[new ButtonKey(button)] = value;

        public bool GetValue(ButtonKey key) => KeyList[key];

        public bool GetValue(Keys key) => KeyList[new ButtonKey(key)];

        public bool GetValue(MouseButtons button) => KeyList[new ButtonKey(button)];


    }
}

