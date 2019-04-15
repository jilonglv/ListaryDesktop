using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyword
{
    public class ConfigInfo
    {
        public KeyWord keywords { get; set; }
    }
    public class KeyWord
    {
        public List<Folder> folder { get; set; }
        public List<Web> web_search { get; set; }
    }
    public class Folder
    {
        public bool enabled { get; set; }
        public string keyword { get; set; }
        public string name { get; set; }
        public string path { get; set; }
    }
    public class Web
    {
        public bool enabled { get; set; }
        public string keyword { get; set; }
        public string title { get; set; }
        public string url { get; set; }
    }
}
