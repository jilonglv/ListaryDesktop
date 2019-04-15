using System.Text;
using System.Windows.Forms;
/// 快捷键相关的类
/// </summary>
public static class HotKeyInfo
{
    /// <summary>
    /// 根据KeyEventArgs生成组合键字符串
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string GetStringByKey(KeyEventArgs e)
    {
        if (e.KeyValue == 16)
        {
            return "Shift+";
        }
        else if (e.KeyValue == 17)
        {
            return "Ctrl+";
        }
        else if (e.KeyValue == 18)
        {
            return "Alt+";
        }
        else
        {
            StringBuilder keyValue = new StringBuilder();
            if (e.Modifiers != 0)
            {
                if (e.Control)
                {
                    keyValue.Append("Ctrl+");
                }
                if (e.Alt)
                {
                    keyValue.Append("Alt+");
                }
                if (e.Shift)
                {
                    keyValue.Append("Shift+");
                }
            }
            if ((e.KeyValue >= 48 && e.KeyValue <= 57))    //0-9
            {
                keyValue.Append(e.KeyCode.ToString());
                //keyValue.Append(e.KeyCode.ToString().Substring(1));
            }
            else
            {
                keyValue.Append(e.KeyCode);
            }

            return keyValue.ToString();
        }
    }

    /// <summary>
    ///  根据按键获得单一键值对应字符串
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string GetSingleStrByKey(KeyEventArgs e)
    {
        if (e.KeyValue == 16)
        {
            return "Shift";
        }
        else if (e.KeyValue == 17)
        {
            return "Ctrl";
        }
        else if (e.KeyValue == 18)
        {
            return "Alt";
        }
        else
        {
            return e.KeyCode.ToString();
        }
    }

    /// <summary>
    /// 根据string生成KeyEventArgs
    /// </summary>
    /// <param name="strKey"></param>
    /// <returns></returns>
    public static KeyEventArgs GetKeyByString(string strKey)
    {
        Keys keyResult = new Keys();
        string[] strKeyCodes = strKey.Split('+');
        if (strKeyCodes.Length > 0)
        {
            int numberKey;
            foreach (string keyEach in strKeyCodes)
            {
                if (keyEach.Trim().ToUpper() == "CTRL")
                {
                    keyResult = keyResult | Keys.Control;
                }
                else if (keyEach.Trim().ToUpper() == "SHIFT")
                {
                    keyResult = keyResult | Keys.Shift;
                }
                else if (keyEach.Trim().ToUpper() == "ALT")
                {
                    keyResult = keyResult | Keys.Alt;
                }
                //数字
                else if (int.TryParse(keyEach, out numberKey))
                {
                    KeysConverter converter = new KeysConverter();
                    Keys getKey = (Keys)converter.ConvertFromString('D' + keyEach);
                    keyResult = keyResult | getKey;
                }
                //其他（字母，F0-F12)
                else
                {
                    KeysConverter converter = new KeysConverter();
                    Keys getKey = (Keys)converter.ConvertFromString(keyEach);
                    keyResult = keyResult | getKey;
                }
            }

        }
        KeyEventArgs newEventArgs = new KeyEventArgs(keyResult);
        return newEventArgs;
    }
}