using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace WebcomicDownloader.XMLSearcher
{
    public static class ComicParser
    {
        public static Comic ParseComic(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            doc.Schemas.Add(Schemas.WebComicSchema);
            doc.Validate((o, e) =>
                {
                    throw e.Exception;
                });

            string defNS = "http://www.sierrasoftworks.com/WebComicDownloader";

            XmlNamespaceManager xnsm = new XmlNamespaceManager(doc.NameTable);
            xnsm.AddNamespace("s", defNS);

            XmlNode xNode = doc["Comic", defNS];

            if (xNode["Advanced", defNS] != null)
            {
                string namepat = null;
                string namexpath = null;
                bool usecomicpath = false;
                string refpath = null;
                string stoplast = null;
                string exifName = null;
                string exifNameXPath = null;
                string exifDesc = null;
                string exifDescXPath = null;
                string imageFormat = null;

                if(xNode["Advanced", defNS]["FileNaming", defNS] != null &&
                   xNode["Advanced", defNS]["FileNaming", defNS].Attributes["Pattern"] != null)
                    namepat = xNode["Advanced", defNS]["FileNaming", defNS].Attributes["Pattern"].Value;

                if (xNode["Advanced", defNS]["FileNaming", defNS] != null &&
                   xNode["Advanced", defNS]["FileNaming", defNS].Attributes["XPath"] != null)
                    namexpath = xNode["Advanced", defNS]["FileNaming", defNS].Attributes["XPath"].Value;

                if (xNode["Advanced", defNS]["Referrer", defNS] != null &&
                   xNode["Advanced", defNS]["Referrer", defNS].Attributes["UseComicPath"] != null)
                    usecomicpath = (xNode["Advanced", defNS]["Referrer", defNS].Attributes["UseComicPath"].Value == "true");

                if (xNode["Advanced", defNS]["Referrer", defNS] != null &&
                   xNode["Advanced", defNS]["Referrer", defNS].Attributes["CustomPath"] != null)
                    refpath = xNode["Advanced", defNS]["Referrer", defNS].Attributes["CustomPath"].Value;

                if (xNode["Advanced", defNS]["LastComicHandling", defNS] != null &&
                   xNode["Advanced", defNS]["LastComicHandling", defNS].Attributes["StopImmediately"] != null)
                    stoplast = xNode["Advanced", defNS]["LastComicHandling", defNS].Attributes["StopImmediately"].Value;

                if (xNode["Advanced", defNS]["ImageTagging", defNS] != null)
                {
                    if(xNode["Advanced", defNS]["ImageTagging", defNS].Attributes["TitlePattern"] != null)
                        exifName = xNode["Advanced", defNS]["ImageTagging", defNS].Attributes["TitlePattern"].Value;
                    if(xNode["Advanced", defNS]["ImageTagging", defNS].Attributes["TitleXPath"] != null)
                        exifNameXPath = xNode["Advanced", defNS]["ImageTagging", defNS].Attributes["TitleXPath"].Value;
                    if(xNode["Advanced", defNS]["ImageTagging", defNS].Attributes["DescriptionPattern"] != null)
                        exifDesc = xNode["Advanced", defNS]["ImageTagging", defNS].Attributes["DescriptionPattern"].Value;
                    if(xNode["Advanced", defNS]["ImageTagging", defNS].Attributes["DescriptionXPath"] != null)
                        exifDescXPath = xNode["Advanced", defNS]["ImageTagging", defNS].Attributes["DescriptionXPath"].Value;
                }

                if (xNode["Advanced", defNS]["Formats", defNS] != null &&
                   xNode["Advanced", defNS]["Formats", defNS].Attributes["Image"] != null)
                    imageFormat = xNode["Advanced", defNS]["Formats", defNS].Attributes["Image"].Value;
                
                Comic comic = new Comic(
                xNode["Details", defNS]["Name", defNS].Attributes["Value"].Value,
                xNode["Details", defNS]["Author", defNS].Attributes["Value"].Value,
                xNode["Details", defNS]["Address", defNS].Attributes["Value"].Value,
                xNode["XPath", defNS]["Image", defNS].Attributes["Value"].Value,
                xNode["XPath", defNS]["Next", defNS].Attributes["Value"].Value,
                xNode["XPath", defNS]["Previous", defNS].Attributes["Value"].Value,

                ((xNode["Details", defNS]["LastComicAddress", defNS] != null) ?
                xNode["Details", defNS]["LastComicAddress", defNS].Attributes["Value"].Value : null),

                xNode["Details", defNS]["FirstComicAddress", defNS].Attributes["Value"].Value,
                namepat, namexpath, refpath, usecomicpath, (stoplast == "true"),
                exifName,exifDesc,exifNameXPath,exifDescXPath,
                imageFormat
                );

                if (xNode["Advanced", defNS] != null)
                {
                    if (xNode["Advanced", defNS]["PageDownloadOptions", defNS] != null)
                    {
                        if (xNode["Advanced", defNS]["PageDownloadOptions", defNS].Attributes["UseSafeCode"] != null &&
                            xNode["Advanced", defNS]["PageDownloadOptions", defNS].Attributes["UseSafeCode"].Value == "true")
                            comic.UseSafePageDownloadCode = true;

                        if (xNode["Advanced", defNS]["PageDownloadOptions", defNS] != null)
                        {
                            if (xNode["Advanced", defNS]["PageDownloadOptions", defNS].Attributes["StopOnMissingImage"] != null &&
                                xNode["Advanced", defNS]["PageDownloadOptions", defNS].Attributes["StopOnMissingImage"].Value != "false")
                                comic.StopOnMissingImage = true;
                            else
                                comic.StopOnMissingImage = false;

                            if (xNode["Advanced", defNS]["PageDownloadOptions", defNS].Attributes["UserAgent"] != null)
                                comic.UserAgent = xNode["Advanced", defNS]["PageDownloadOptions", defNS].Attributes["UserAgent"].Value;
                            else
                                comic.StopOnMissingImage = false;

                            if (xNode["Advanced", defNS]["PageDownloadOptions", defNS].Attributes["StopOnMissingImage"] != null &&
                                xNode["Advanced", defNS]["PageDownloadOptions", defNS].Attributes["StopOnMissingImage"].Value != "false")
                                comic.StopOnMissingImage = true;
                            else
                                comic.StopOnMissingImage = false;
                        }
                    }

                    if (xNode["Advanced", defNS]["Cookies", defNS] != null)
                    {
                        //Load cookies
                        foreach (XmlNode node in xNode["Advanced", defNS]["Cookies", defNS].SelectNodes("s:Cookie", xnsm))                        
                                comic.Cookies.Add(node.Attributes["Name"].Value, node.Attributes["Value"].Value);
                        
                    }
                }
                
                return comic;
            }

            Comic comic2 = new Comic(
                xNode["Details", defNS]["Name", defNS].Attributes["Value"].Value,
                xNode["Details", defNS]["Author", defNS].Attributes["Value"].Value,
                xNode["Details", defNS]["Address", defNS].Attributes["Value"].Value,
                xNode["XPath", defNS]["Image", defNS].Attributes["Value"].Value,
                xNode["XPath", defNS]["Next", defNS].Attributes["Value"].Value,
                xNode["XPath", defNS]["Previous", defNS].Attributes["Value"].Value,

                ((xNode["Details", defNS]["LastComicAddress", defNS] != null) ?
                xNode["Details", defNS]["LastComicAddress", defNS].Attributes["Value"].Value : null),

                xNode["Details", defNS]["FirstComicAddress", defNS].Attributes["Value"].Value
                );
            
            return comic2;
        }

        public static Comic UpdateComicFromLocal(Comic originalComic, string file)
        {
            if (!File.Exists(file))
                return originalComic;

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            doc.Schemas.Add(Schemas.WebComicSchema);
            doc.Validate((o, e) =>
            {
                throw e.Exception;
            });

            string defNS = "http://www.sierrasoftworks.com/WebComicSave.xsd";

            XmlNamespaceManager xnsm = new XmlNamespaceManager(doc.NameTable);
            xnsm.AddNamespace("s", defNS);

            XmlNode xNode = doc["Comic", defNS];

            Comic comic = originalComic;

            comic.Name = xNode["Name", defNS].InnerText;
            comic.LatestLocalComicAddress = xNode["LastComicAddress", defNS].InnerText;
            comic.LatestLocalComicNumber = Convert.ToInt32(xNode["NumberOfComics", defNS].InnerText);
            comic.DownloadedSize = Convert.ToInt64(xNode["DownloadedSize", defNS].InnerText);
            comic.DownloadedImageSize = Convert.ToInt64(xNode["DownloadedImageSize", defNS].InnerText);

            return comic;
        }

        public static void UpdateLocalComic(Comic originalComic, string file)
        {
            string defNS = "http://www.sierrasoftworks.com/WebComicSave.xsd";

            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
            doc.Schemas.Add(Schemas.WebComicSaveSchema);

            XmlNode comic = doc.CreateNode(XmlNodeType.Element, "Comic", defNS);
            XmlElement name = doc.CreateElement("Name", defNS);
            name.InnerText = originalComic.Name;
            comic.AppendChild(name);

            name = doc.CreateElement("LastComicAddress", defNS);
            name.InnerText = originalComic.LatestLocalComicAddress;
            comic.AppendChild(name);

            name = doc.CreateElement("NumberOfComics", defNS);
            name.InnerText = originalComic.LatestLocalComicNumber.ToString();
            comic.AppendChild(name);

            name = doc.CreateElement("DownloadedSize", defNS);
            name.InnerText = originalComic.DownloadedSize.ToString();
            comic.AppendChild(name);

            name = doc.CreateElement("DownloadedImageSize", defNS);
            name.InnerText = originalComic.DownloadedImageSize.ToString();
            comic.AppendChild(name);

            doc.AppendChild(comic);

            FileStream fs = new FileStream(file, FileMode.Create);
            doc.Save(fs);
            fs.Close();
        }
    }

    public sealed class Comic
    {
        public string Name { get; internal set; }

        public string Author { get; internal set; }

        public string Address { get; internal set; }

        public string ImageXPath { get; internal set; }

        public string NextXPath { get; internal set; }

        public string PreviousXPath { get; internal set; }

        public string LastComicLink { get; internal set; }

        public string FirstComicAddress { get; internal set; }

        public string LatestLocalComicAddress { get; set; }

        public int LatestLocalComicNumber { get; set; }

        public long DownloadedSize { get; set; }

        public string NamingPattern { get; internal set; }

        public string NamingXPath { get; internal set; }

        public string Referrer { get; internal set; }

        public bool UseComicPathAsReferrer { get; internal set; }

        public long DownloadedImageSize { get; set; }

        public long DownloadedPageSize { get { return DownloadedSize - DownloadedImageSize; } }

        public bool StopImmediatelyOnLast { get; private set; }

        public bool UseSafePageDownloadCode { get; internal set; }

        public bool StopOnMissingImage { get; internal set; }

        public string UserAgent { get; internal set; }

        public string EXIFImageNamePattern { get; internal set; }

        public string EXIFImageDescriptionPattern { get; internal set; }

        public string EXIFImageNameXPath { get; internal set; }

        public string EXIFImageDescriptionXPath { get; internal set; }

        public string ImageFormat { get; internal set; }

        public SortedList<string, string> Cookies
        {
            get;
            internal set;
        }

        internal Comic(string name, string author, string address, string imagepath, string nextpath, string prevpath, string lastcomic, string firstcomic, string namingPattern, string namingXPath, string referrer, bool useReferrer, bool stopImmediatelyOnLast, string exifNamePattern, string exifDescriptionPattern, string exifNameXPath, string exifDescriptionXPath, string imageFormat)
        {
            Name = name;
            Author = author;
            Address = address;
            ImageXPath = imagepath;
            NextXPath = nextpath;
            PreviousXPath = prevpath;
            LastComicLink = lastcomic;
            FirstComicAddress = firstcomic;
            LatestLocalComicAddress = FirstComicAddress;
            LatestLocalComicNumber = 1;
            NamingPattern = namingPattern;
            NamingXPath = namingXPath;
            Referrer = referrer;
            UseComicPathAsReferrer = useReferrer;
            StopImmediatelyOnLast = stopImmediatelyOnLast;
            EXIFImageNamePattern = exifNamePattern;
            EXIFImageDescriptionPattern = exifDescriptionPattern;
            EXIFImageNameXPath = exifNameXPath;
            EXIFImageDescriptionXPath = exifDescriptionXPath;
            ImageFormat = imageFormat;
            StopOnMissingImage = true;

            Cookies = new SortedList<string, string>();
        }

        internal Comic(string name, string author, string address, string imagepath, string nextpath, string prevpath, string lastcomic, string firstcomic, string namingPattern, string namingXPath, string referrer, bool useReferrer, bool stopImmediatelyOnLast)
        {
            Name = name;
            Author = author;
            Address = address;
            ImageXPath = imagepath;
            NextXPath = nextpath;
            PreviousXPath = prevpath;
            LastComicLink = lastcomic;
            FirstComicAddress = firstcomic;
            LatestLocalComicAddress = FirstComicAddress;
            LatestLocalComicNumber = 1;
            NamingPattern = namingPattern;
            NamingXPath = namingXPath;
            Referrer = referrer;
            UseComicPathAsReferrer = useReferrer;
            StopImmediatelyOnLast = stopImmediatelyOnLast;
            Cookies = new SortedList<string, string>();
            StopOnMissingImage = true;
        }

        internal Comic(string name, string author, string address, string imagepath, string nextpath, string prevpath, string lastcomic, string firstcomic)
        {
            Name = name;
            Author = author;
            Address = address;
            ImageXPath = imagepath;
            NextXPath = nextpath;
            PreviousXPath = prevpath;
            LastComicLink = lastcomic;
            FirstComicAddress = firstcomic;
            LatestLocalComicAddress = FirstComicAddress;
            LatestLocalComicNumber = 1;
            Cookies = new SortedList<string, string>();
            StopOnMissingImage = true;
        }

        internal Comic(string name, string author, string imagepath, string nextpath, string latestcomicaddr, int latestcomicnum)
        {
            Name = name;
            Author = author;
            ImageXPath = imagepath;
            NextXPath = nextpath;
            LatestLocalComicAddress = latestcomicaddr;
            LatestLocalComicNumber = latestcomicnum;
            Cookies = new SortedList<string, string>();
            StopOnMissingImage = true;
        }
    }
}