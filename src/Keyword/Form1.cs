using AutoHotkey.Interop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keyword
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }
        ConcurrentDictionary<string, Tuple<bool, string>> m_keywords = new ConcurrentDictionary<string, Tuple<bool, string>>();
        void LoadConfig()
        {
            var text = File.ReadAllText("keywords.json", Encoding.UTF8);
            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigInfo>(text);
            foreach (var item in LoadWebConfig())
            {
                if (item.enabled)
                    Add(item.keyword, item.url,true);
            }
            foreach (var item in config.keywords.folder)
            {
                if (item.enabled)
                {
                    Add(item.keyword, item.path);
                }
            }
            foreach (var item in config.keywords.web_search)
            {
                if (item.enabled)
                    Add(item.keyword, item.url,true);
            }

            //foreach (var line in File.ReadAllLines("keywords.json"))
            //{
            //    if (string.IsNullOrEmpty(line))
            //        continue;
            //    var keyLines = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //    if (keyLines.Length < 2)
            //        continue;
            //    var keyName = keyLines[0].Trim();
            //    m_keywords[keyName] = keyLines[1].Trim();
            //    listOnit.Add(keyName);
            //}
            cbbKeyword.Items.AddRange(listOnit.ToArray());
        }
        List<Web> LoadWebConfig()
        {
            var webs = new List<Web>();
            webs.AddRange(
                new Web[] {
                    new Web() { url = "http://www.google.com/search?q={0}", enabled = true, keyword = "gg"},
                    new Web() { url = "http://www.bing.com/search?q={0}", enabled = true, keyword = "bing"},
                    new Web() { url = "http://www.baidu.com/s?wd={0}", enabled = true, keyword = "bd"},
                    new Web() { url = "https://mail.google.com/", enabled = true, keyword = "gmail"},
                    new Web() { url = "http://youtube.com/results?q={0}", enabled = true, keyword = "youtube"},
                    new Web() { url = "http://maps.google.com/?q={0}", enabled = true, keyword = "maps"},
                    new Web() { url = "http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias%3Daps&field-keywords={0}", enabled = true, keyword = "amazon"},
                    new Web() { url = "https://en.wikipedia.org/wiki/Special:Search/{query}", enabled = true, keyword = "wiki"},
                    new Web() { url = "http://www.ebay.com/sch/?_nkw={0}", enabled = true, keyword = "ebay"},
                    new Web() { url = "http://www.imdb.com/find?s=all&q={0}", enabled = true, keyword = "imdb"},
                    new Web() { url = "https://www.facebook.com/", enabled = true, keyword = "facebook"},
                    new Web() { url = "https://translate.google.cn/", enabled = true, keyword = "gt"},
                    new Web() { url = "https://try.dot.net/", enabled = true, keyword = "td"},
                    new Web() { url = "https://github.com/", enabled = true, keyword = "github"}
                });
            return webs;
        }
        void Add(string keyName, string cmd,bool hasArguments=false)
        {
            listOnit.Add(keyName);
            m_keywords[keyName] = new Tuple<bool, string>(hasArguments,GetString(cmd));
        }
        string GetString(string data)
        {
            return Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(data));
        }

        //初始化绑定默认关键词（此数据源可以从数据库取）
        List<string> listOnit = new List<string>();
        //输入key之后，返回的关键词
        List<string> listNew = new List<string>();
        private void cbbKeyword_TextUpdate(object sender, EventArgs e)
        {
            //清空combobox
            this.cbbKeyword.Items.Clear();
            //清空listNew
            listNew.Clear();
            ComboBox cbBox = (ComboBox)sender;
            cbBox.Items.Clear();
            foreach (var item in listOnit)  //已有数据
            {
                if (item.Contains(cbBox.Text))
                {
                    listNew.Add(item);
                }
            }
            cbBox.Items.AddRange(listNew.ToArray());
            cbBox.SelectionStart = cbBox.Text.Length;
            Cursor = Cursors.Default;
            cbBox.DroppedDown = true;
        }

        static AutoHotkeyEngine ahk = AutoHotkeyEngine.Instance;
        private void cbbKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var cmd = cbbKeyword.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (cmd.Length > 0)
                    {
                        var keyName = cmd[0];
                        if (m_keywords.ContainsKey(keyName))
                        {
                            string arguments = null;
                            if (cmd.Length > 1)
                                arguments = string.Join(" ", cmd.Skip(1).ToArray());
                            var keyCmd = m_keywords[keyName];
                            var cmdName = keyCmd.Item2;
                            if (keyCmd.Item1)
                                cmdName = string.Format(keyCmd.Item2, arguments);
                            ahk.ExecRaw(cmdName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                this.Close();
            }
        }
    }
}
