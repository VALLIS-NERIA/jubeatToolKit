using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ToolKit {
    public class MusicInfo {
        XmlDocument info;
        Dictionary<string, XmlNode> dict;
        XmlNode body;
        XmlNodeList music_list;
        public MusicInfo(string xmlPath) {
            Load(xmlPath);
        }
        public void Load(string songInfoPath) {
            info = new XmlDocument();
            info.Load(songInfoPath);
            dict = new Dictionary<string, XmlNode>();
            body = info.SelectSingleNode("/music_data/body");
            music_list = body.ChildNodes;
            foreach (XmlNode music in music_list) {
                string id = music.SelectSingleNode("music_id").InnerText.Trim();
                dict.Add(id, music);
            }
        }

        public void Refract(string dstid, XmlNode srcNode) {
            XmlNode src_clone = srcNode.Clone();
            body.ReplaceChild(src_clone, dict[dstid]);
        }

        public void Satisfy() {
            foreach (XmlNode music in music_list) {
                
            }
        }

        public void Save(string outputpath) {
            XmlWriterSettings s = new XmlWriterSettings();
            s.Indent = true;
            s.IndentChars = "";
            s.NewLineChars = "\r\n";
            XmlWriter xw = XmlWriter.Create("2.xml", s);
            info.Save(xw);
        }

    }
}
