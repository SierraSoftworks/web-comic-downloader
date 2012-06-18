using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Text;

namespace SierraLib
{
    public static class Settings
    {
        public static string SettingsFilePath = "%AppData%/SierraLib";
        public static string SettingsFileName = "settings.xml";
        public static Version ApplicationVersion = new Version(1, 0, 0, 0);
        public static bool SuppressVersionErrors = true;

        private const string defNS = "http://www.sierrasoftworks.com/settings";


        public static XmlSchema SettingsSchema
        {
            get
            {
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(WebcomicDownloader.Properties.Resources.SettingsSchema));
                try
                {
                    XmlSchema schema = XmlSchema.Read(ms, (o, e) => { if (e.Exception != null) throw e.Exception; });
                    return schema;
                }
                catch
                {
                    return new XmlSchema();
                }
            }
        }


        public static void SetSetting(string name, string value)
        {
            FileInfo fInfo = new FileInfo(System.IO.Path.Combine(ProcessPath(SettingsFilePath), SettingsFileName));

            //Fix for corrupted files:
            if (System.IO.File.Exists(fInfo.FullName))
            {
                StreamReader sr = fInfo.OpenText();
                string fText = sr.ReadToEnd();
                sr.Close();
                int index = fText.IndexOf("</Settings>");
                if (index + "</Settings>".Length < fText.Length && index > -1)
                {
                    fText = fText.Remove(fText.IndexOf("</Settings>") + "</Settings>".Length);

                    StreamWriter sw = new StreamWriter(fInfo.FullName, false);
                    sw.Write(fText);
                    sw.Close();
                }
            }

            FileStream fsReader = new FileStream(fInfo.FullName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            FileStream fsWriter = new FileStream(fInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();

            if (!System.IO.File.Exists(fInfo.FullName) || fInfo.Length == 0)
            {
                XmlDeclaration decl = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xDoc.AppendChild(decl);
                XmlElement node = xDoc.CreateElement("Settings", defNS);
                node.InnerText = " ";

                XmlAttribute atrib = xDoc.CreateAttribute("version");
                atrib.Value = ApplicationVersion.ToString();
                node.Attributes.Append(atrib);

                xDoc.AppendChild(node);
                xDoc.Save(fsWriter);
            }

            xDoc.Load(fsReader);



            xDoc.Schemas.Add(SettingsSchema);
            xDoc.Validate(new System.Xml.Schema.ValidationEventHandler((o2, e2) =>
            {
                if (e2.Exception != null)
                    throw e2.Exception;
            }));

            XmlNamespaceManager xnsm = new XmlNamespaceManager(xDoc.NameTable);
            xnsm.AddNamespace("s", defNS);

            XmlNode settings = xDoc.SelectSingleNode("s:Settings", xnsm);

            if (new Version(settings.Attributes["version"].Value) < ApplicationVersion && !SuppressVersionErrors)
                throw new Exception("The settings file is formatted for an older version of your application");

            XmlNode setting = settings.SelectSingleNode("//s:Setting[@Name=\"" + name + "\"]", xnsm);
            if (setting == null)
            {
                setting = xDoc.CreateElement("Setting", defNS);
                XmlAttribute xname = xDoc.CreateAttribute("Name");
                XmlAttribute xvalue = xDoc.CreateAttribute("Value");

                xname.Value = name;
                xvalue.Value = value;

                setting.Attributes.Append(xname);
                setting.Attributes.Append(xvalue);

                settings.AppendChild(setting);
            }
            else
            {
                setting.Attributes["Value"].Value = value;
            }

            xDoc.Save(fsWriter);

            fsReader.Close();
            fsWriter.Close();
        }

        public static void RemoveSetting(string name)
        {
            FileInfo fInfo = new FileInfo(System.IO.Path.Combine(ProcessPath(SettingsFilePath), SettingsFileName));

            //Fix for corrupted files:
            if (System.IO.File.Exists(fInfo.FullName))
            {
                StreamReader sr = fInfo.OpenText();
                string fText = sr.ReadToEnd();
                sr.Close();
                int index = fText.IndexOf("</Settings>");
                if (index + "</Settings>".Length < fText.Length && index > -1)
                {
                    fText = fText.Remove(fText.IndexOf("</Settings>") + "</Settings>".Length);

                    StreamWriter sw = new StreamWriter(fInfo.FullName, false);
                    sw.Write(fText);
                    sw.Close();
                }
            }

            FileStream fsReader = new FileStream(fInfo.FullName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            FileStream fsWriter = new FileStream(fInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();

            if (!System.IO.File.Exists(fInfo.FullName) || fInfo.Length == 0)
            {
                XmlDeclaration decl = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xDoc.AppendChild(decl);
                XmlElement node = xDoc.CreateElement("Settings", defNS);
                node.InnerText = " ";

                XmlAttribute atrib = xDoc.CreateAttribute("version");
                atrib.Value = ApplicationVersion.ToString();
                node.Attributes.Append(atrib);

                xDoc.AppendChild(node);
                xDoc.Save(fsWriter);
            }

            xDoc.Load(fsReader);

            xDoc.Schemas.Add(SettingsSchema);
            xDoc.Validate(new System.Xml.Schema.ValidationEventHandler((o2, e2) =>
            {
                if (e2.Exception != null)
                    throw e2.Exception;
            }));

            XmlNamespaceManager xnsm = new XmlNamespaceManager(xDoc.NameTable);
            xnsm.AddNamespace("s", defNS);

            XmlNode settings = xDoc.SelectSingleNode("s:Settings", xnsm);
            if (new Version(settings.Attributes["version", defNS].Value) < ApplicationVersion && !SuppressVersionErrors)
                throw new Exception("The settings file is formatted for an older version of your application");

            XmlNode remove = settings.SelectSingleNode("//s:Setting[@Name=\"" + name + "\"]", xnsm);
            if (remove != null)
                settings.RemoveChild(remove);

            xDoc.Save(fsWriter);

            fsReader.Close();
            fsWriter.Close();
        }

        public static string GetSetting(string name)
        {
            FileInfo fInfo = new FileInfo(System.IO.Path.Combine(ProcessPath(SettingsFilePath), SettingsFileName));

            //Fix for corrupted files:
            if (System.IO.File.Exists(fInfo.FullName))
            {
                StreamReader sr = fInfo.OpenText();
                string fText = sr.ReadToEnd();
                sr.Close();
                int index = fText.IndexOf("</Settings>");
                if (index + "</Settings>".Length < fText.Length && index > -1)
                {
                    fText = fText.Remove(fText.IndexOf("</Settings>") + "</Settings>".Length);

                    StreamWriter sw = new StreamWriter(fInfo.FullName, false);
                    sw.Write(fText);
                    sw.Close();
                }
            }

            FileStream fsReader = new FileStream(fInfo.FullName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            FileStream fsWriter = new FileStream(fInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();

            if (!System.IO.File.Exists(fInfo.FullName) || fInfo.Length == 0)
            {
                XmlDeclaration decl = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xDoc.AppendChild(decl);
                XmlElement node = xDoc.CreateElement("Settings", defNS);
                node.InnerText = " ";

                XmlAttribute atrib = xDoc.CreateAttribute("version");
                atrib.Value = ApplicationVersion.ToString();
                node.Attributes.Append(atrib);

                xDoc.AppendChild(node);
                xDoc.Save(fsWriter);
            }

            xDoc.Load(fsReader);

            fsReader.Close();
            fsWriter.Close();

            xDoc.Schemas.Add(SettingsSchema);
            xDoc.Validate(new System.Xml.Schema.ValidationEventHandler((o2, e2) =>
            {
                if (e2.Exception != null)
                    throw e2.Exception;
            }));

            XmlNamespaceManager xnsm = new XmlNamespaceManager(xDoc.NameTable);
            xnsm.AddNamespace("s", defNS);

            XmlNode settings = xDoc.SelectSingleNode("s:Settings", xnsm);
            if (new Version(settings.Attributes["version"].Value) < ApplicationVersion && !SuppressVersionErrors)
                throw new Exception("The settings file is formatted for an older version of your application");

            XmlNode result = settings.SelectSingleNode("//s:Setting[@Name=\"" + name + "\"]", xnsm);
            if (result != null)
                return result.Attributes["Value"].Value;

            return null;
        }

        private static string ProcessPath(string path)
        {
            string temp = path;
            Regex reg = new Regex("%(?<Var>\\w+)%");
            foreach (Match mtch in reg.Matches(temp))
            {
                try
                {
                    temp = temp.Replace(mtch.Value, Environment.GetEnvironmentVariable(mtch.Groups["Var"].Value));
                }
                catch { }
            }
            return temp;
        }
    }
}