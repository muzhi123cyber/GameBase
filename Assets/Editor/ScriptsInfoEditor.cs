using System;
using System.IO;
using UnityEditor;

namespace MyEditorCustom
{
    public class ScriptsInfoEditor : UnityEditor.AssetModificationProcessor
    {
        //public const string authorName = "tackor(修改为你自己的名称即可)";
        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (path.EndsWith(".cs"))
            {
                string str = File.ReadAllText(path);
                //str = str.Replace("#AUTHORNAME#", authorName);
                str = str.Replace("#FILEEXTENSION#", path.Substring(path.LastIndexOf(".")));
                //str = str.Replace("Plane", Environment.UserName);
                str = str.Replace("#SMARTDEVELOPERS#", PlayerSettings.companyName);
                str = str.Replace("#CREATETIME#", string.Concat(DateTime.Now.ToString("d"), " ", DateTime.Now.Hour, ":", DateTime.Now.Minute, ":", DateTime.Now.Second));
                str = str.Replace("#PROJECTNAME#", PlayerSettings.productName);
                File.WriteAllText(path, str);
            }
        }
    }
}
