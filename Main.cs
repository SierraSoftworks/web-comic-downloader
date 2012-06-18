using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using SierraLib.HTMLParsing;
using SierraLib.Updates;
using WebcomicDownloader.XMLSearcher;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using SierraLib.ByteManipulation;
using SierraLib.Updates.Advanced;
using SierraLib.UI.Animations;
using SierraLib.UI.TaskDialog;
using SierraLib;
using SierraLib.Security;
using SierraLib.UI;
using SierraLib.Updates.Automatic;
using SierraLib.Analytics.GoogleAnalytics;

namespace WebcomicDownloader
{
    public partial class Main : Form
    {
        List<Comic> comics = new List<Comic>();
        BackgroundWorker bwDownloader = new BackgroundWorker();
        string destination = "";

        UpdateDialogs updates;

        public string UserAgent
        {
            get { return "Mozilla/5.0 (WindowsNT) Sierra Softworks; Web Comic Downloader; v" + AssemblyInformation.GetAssemblyVersion().ToString(3); }
        }

        private delegate void DefaultDelegate();

        public Main()
        {
            //Set google analytics account code
            
            bwDownloader.DoWork += new DoWorkEventHandler(bwDownloader_DoWorkNew);
            bwDownloader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwDownloader_RunWorkerCompleted);
            bwDownloader.WorkerReportsProgress = true;
            bwDownloader.WorkerSupportsCancellation = true;

            InitializeComponent();

            //panelProgress.RoundedCorners = new SierraLib.UserInterface.AeroControls.RoundedCorners(0, 0, 5, 0);
            //panelStatus.RoundedCorners = new SierraLib.UserInterface.AeroControls.RoundedCorners(5, 0, 0, 0);
            panelDetails.RoundedCorners = new RoundedCorners(0, 0, 0, 9);
            panelSettings.RoundedCorners = new RoundedCorners(0, 0, 9, 0);
            //panelBrowse.RoundedCorners = new SierraLib.UserInterface.AeroControls.RoundedCorners(0, 5, 0, 0);

            if (!Directory.Exists(Environment.ExpandEnvironmentVariables("%AppData%\\Sierra Softworks\\Web Comic Downloader")))
                Directory.CreateDirectory(Environment.ExpandEnvironmentVariables("%AppData%\\Sierra Softworks\\Web Comic Downloader"));

            Tracker.CurrentInstance.ProductName = "Web Comic Downloader";
            Tracker.CurrentInstance.ProductVersion = SierraLib.AssemblyInformation.GetAssemblyVersion().ToString();
            Tracker.CurrentInstance.StartSession("UA-9682191-3", Environment.ExpandEnvironmentVariables("%AppData%\\Sierra Softworks\\Web Comic Downloader\\analytics.db"), false, 10);

            SierraLib.Settings.SettingsFileName = "WKDSettings.xml";
            SierraLib.Settings.SettingsFilePath = Environment.ExpandEnvironmentVariables("%AppData%\\Sierra Softworks\\Web Comic Downloader");
            SierraLib.Settings.ApplicationVersion = SierraLib.AssemblyInformation.GetAssemblyVersion(Assembly.GetExecutingAssembly());

            updates = new UpdateDialogs("Web Comic Downloader", "Sierra Softworks", "http://sierrasoftworks.com", "contact@sierrasoftworks.com",
                SierraLib.AssemblyInformation.GetAssemblyVersion(), Settings.GetSetting("CheckForUpdates") == "true");

            updates.Available += (e) =>
                {
                    if (SierraLib.Settings.GetSetting("UsageTracking") == "Enabled")
                    {
                        Tracker.CurrentInstance.CustomVariables[1] = new CustomVariable(1, "CurrentVersion", SierraLib.AssemblyInformation.GetAssemblyVersion().ToString());
                        Tracker.CurrentInstance.CustomVariables[2] = new CustomVariable(2, "LatestVersion", e.ApplicationVersion.ToString());
                        Tracker.CurrentInstance.TrackPageView("/WebComicDownloader/Updates/Available", "Web Comic Downloader Update Available");
                    }
                };

            updates.Downloading += (e) =>
                {
                    if (Settings.GetSetting("UsageTracking") == "Enabled")
                    {
                        Tracker.CurrentInstance.CustomVariables[1] = new CustomVariable(1, "CurrentVersion", SierraLib.AssemblyInformation.GetAssemblyVersion().ToString());
                        Tracker.CurrentInstance.CustomVariables[2] = new CustomVariable(2, "LatestVersion", e.ApplicationVersion.ToString());
                        Tracker.CurrentInstance.TrackPageView("/WebComicDownloader/Updates/Downloading", "Web Comic Downloader Update Downloading");
                    }                                    
                };

            updates.Downloaded += (e) =>
                {


                    if (Settings.GetSetting("UsageTracking") == "Enabled")
                    {
                        Tracker.CurrentInstance.CustomVariables[1] = new CustomVariable(1, "CurrentVersion", SierraLib.AssemblyInformation.GetAssemblyVersion().ToString());
                        Tracker.CurrentInstance.CustomVariables[2] = new CustomVariable(2, "LatestVersion", e.ApplicationVersion.ToString());
                        Tracker.CurrentInstance.TrackPageView("/WebComicDownloader/Updates/Downloaded", "Web Comic Downloader Update Downloaded");
                    }
                                    
                };

            updates.CheckForUpdatesAutomaticallyEnabled += (o, e) =>
                {
                    Settings.SetSetting("CheckForUpdates", "true");
                };

            updates.CheckForUpdatesAutomaticallyDisabled += (o, e) =>
                {
                    Settings.SetSetting("CheckForUpdates", "false");
                };

            Updates.UpdateServers.Add("http://sierrasoftworks.com/downloads/wkd/AdvancedUpdates.xml");
            

            if (SierraLib.Settings.GetSetting("UsageTracking") == "Enabled")
            {
                Tracker.CurrentInstance.CustomVariables[1] = new CustomVariable(1, "CurrentVersion", SierraLib.AssemblyInformation.GetAssemblyVersion().ToString());
                Tracker.CurrentInstance.TrackPageView("/WebComicDownloader/Started", "Web Comic Downloader Update Available");
            }

        }

        private void LoadComics()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, "Comic Definitions"));
            foreach (FileInfo fi in dirInfo.GetFiles("*.xml"))
            {
                try
                {
                    Comic comic = ComicParser.ParseComic(fi.FullName);
                    comics.Add(comic);
                    cbComics.Items.Add(comic.Name);
                }
                catch
                {
                    //Problem with this comic file
                    if (Environment.OSVersion.Version.Major >= 6)
                    {
                        TaskDialog.Show(fi.Name, "Problem loading Web Comic Definition",
                        "There was a problem loading this Web Comic Definition file. This may indicate file corruption or a poorly formatted comic defintion file.\n" +
                        "If you are a web comic developer please make sure that you are following the design layed out in WebComicDefinition.xsd",
                        TaskDialogButton.OK, TaskDialogIcon.SecurityError);
                    }
                    else
                        MessageBox.Show("The Web Comic Definition file \"" + fi.Name + "\" has a problem in it.\n" +
                            "This may indicate corruption or a poorly layed out file.",
                            "Problem loading Web Comic definition",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadSettings()
        {            
            string path = SierraLib.Settings.GetSetting("WebcomicsPath");
            if (Directory.Exists(path))
                txtPath.Text = path;



            string lastComic = SierraLib.Settings.GetSetting("LastComic");
            if (lastComic != null)
                cbComics.SelectedItem = lastComic;

            if (SierraLib.Settings.GetSetting("PreventStandby") == "false")
                chkPreventStandby.Checked = false;
            else
                chkPreventStandby.Checked = true;

            if (SierraLib.Settings.GetSetting("TurnOffScreen") == "true")
                chkTurnOffScreen.Checked = true;
            else
                chkTurnOffScreen.Checked = false;
        }

        #region "Animations"

        private void ShowProgressPanel()
        {
            label2.Visible = false;
            label4.Visible = false;
            linkLabel1.Visible = false;

            if (AnimationManager.HasReversed(panelProgress))
                AnimationManager.Animate(panelProgress);

            //SierraLib.UserInterface.Animations.HorizontalPanel.Show(panelProgress, SierraLib.UserInterface.Animations.HorizontalPanel.Direction.Vertical, 2, 1, 1);
        }

        private void HideProgressPanel()
        {
            label2.Visible = true;
            label4.Visible = true;
            linkLabel1.Visible = true;

            if (!AnimationManager.HasReversed(panelProgress))
                AnimationManager.Animate(panelProgress);
            
            //SierraLib.UserInterface.Animations.HorizontalPanel.Hide(panelProgress, SierraLib.UserInterface.Animations.HorizontalPanel.Direction.Vertical, 2, 1, 1);
        }

        private bool DetailsPanelShown()
        {
            return !AnimationManager.HasReversed(panelDetails);
        }

        private void ShowDetailsPanel()
        {
            if (AnimationManager.HasReversed(panelDetails))
                AnimationManager.Animate(panelDetails);
            //SierraLib.UserInterface.Animations.HorizontalPanel.Show(panelDetails, SierraLib.UserInterface.Animations.HorizontalPanel.Direction.Vertical, 1, 1, 1);
        }

        private void HideDetailsPanel()
        {
            if (!AnimationManager.HasReversed(panelDetails))
                AnimationManager.Animate(panelDetails);
            //SierraLib.UserInterface.Animations.HorizontalPanel.Hide(panelDetails, SierraLib.UserInterface.Animations.HorizontalPanel.Direction.Vertical, 1, 1, 1);
        }

        private void ShowSettingsPanel()
        {
            if (AnimationManager.HasReversed(panelSettings))
                AnimationManager.Animate(panelSettings);
        }

        private void HideSettingsPanel()
        {
            if (!AnimationManager.HasReversed(panelSettings))
                AnimationManager.Animate(panelSettings);
        }

        #endregion "Animations"

        #region "Delegates"

        private string getDestination()
        {
            return destination;
        }

        private delegate string getDestinationSafe();

        private string FormatTime(TimeSpan time)
        {
            if (time.TotalMilliseconds >= 0)
            {
                string res = "";
                if (time.Hours > 0)
                    res += time.Hours + ":";

                if (time.Minutes == 0)
                    res += "00:";
                else if (time.Minutes < 10)
                    res += "0" + time.Minutes + ":";
                else
                    res += time.Minutes + ":";

                int secs = time.Seconds;

                if (secs == 0)
                    res += "00";
                else if (secs < 10)
                    res += "0" + secs;
                else
                    res += secs;

                //if (time.Milliseconds == 0)
                //    res += "00";
                //else if (time.Milliseconds < 100)
                //    res += "0" + Math.Round((double)time.Milliseconds / 100,0);
                //else
                //    res += Math.Round((double)time.Milliseconds / 100,0);

                return res;
            }
            else
            {
                return "--:--";
            }
        }

        private void setValues(int size, int downloaded, float speed, TimeSpan remaining, Int64 totalsize, int comic, Int64 wasted, bool downloadingComic)
        {
            if (size >= 0)
                lblImageSize.Text = ((size < 1024) ? size + " KB" : Math.Round((double)size / 1024, 1) + "MB");
            else
                lblImageSize.Text = "";
            lblWasted.Text = ((wasted < 1024) ? wasted + " KB" : Math.Round((double)wasted / 1024, 1) + "MB");
            lblDownloaded.Text = ((downloaded < 1024) ? downloaded + " KB" : Math.Round((double)downloaded / 1024, 1) + "MB");
            lblSpeed.Text = Convert.ToInt32(speed).ToString() + " KB/s";
            lblRemaining.Text = FormatTime(remaining);
            lblTotalSize.Text = ((totalsize < 1024) ? totalsize + " KB" : Math.Round((double)totalsize / 1024, 1) + "MB");
            lblComic.Text = comic.ToString();

            if (downloaded == size)
            {
                pbProgress.Value = downloaded;
                pbProgress.Style = ProgressBarStyle.Marquee;
                lblStatus.Text = "Loading";
                lblProgress.Text = "--%";
            }
            else if (downloadingComic)
            {
                if (size > 0)
                {
                    pbProgress.Style = ProgressBarStyle.Continuous;
                    pbProgress.Maximum = size;
                    //Add 10% for better flow
                    pbProgress.Value = downloaded;// +Convert.ToInt32(Math.Min(size - downloaded, downloaded * 0.1));
                    lblStatus.Text = "Downloading";
                    lblProgress.Text = Math.Round((float)downloaded / size * 100, 0) + "%";
                }
                else
                {
                    pbProgress.Style = ProgressBarStyle.Marquee;
                    lblStatus.Text = "Downloading";
                    lblProgress.Text = "--%";
                }
            }
            else
            {
                if (size > 0)
                {
                    pbProgress.Style = ProgressBarStyle.Continuous;
                    pbProgress.Maximum = size;
                    //Add 10% for better flow
                    pbProgress.Value = downloaded;// +Convert.ToInt32(Math.Min(size - downloaded, downloaded * 0.1));
                    lblStatus.Text = "Loading";
                    lblProgress.Text = Math.Round((float)downloaded / size * 100, 0) + "%";
                }
                else
                {
                    pbProgress.Style = ProgressBarStyle.Marquee;
                    lblStatus.Text = "Loading";
                    lblProgress.Text = "--%";
                }
            }
        }

        private delegate void setValuesSafe(int size, int downloaded, float speed, TimeSpan remaining, Int64 totalsize, int comic, Int64 wasted, bool downloadingComic);

        private void setCurrentComic(string comicName)
        {
            cbComics.SelectedItem = comicName;
        }

        private delegate void setCurrentComicSafe(string a);

        #endregion "Delegates"

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (chkDownloadAll.Checked)
            {

                if (SierraLib.Settings.GetSetting("UsageTracking") == "Enabled")
                    Tracker.CurrentInstance.TrackEvent("Web Comic Downloader", "Downloads", "Download All");
                for (int i = 0; i < comics.Count; i++)
                {
                    comics[i] = ComicParser.UpdateComicFromLocal(comics[i], Path.Combine(destination, comics[i].Name + "\\Comic.xml"));
                }

                bwDownloader.RunWorkerAsync(comics.ToArray());

                btnBrowse.Enabled = false;
                cbComics.Enabled = false;
                btnDownload.Enabled = false;
                chkDownloadAll.Enabled = false;
                txtPath.Enabled = false;
                btnCancel.Enabled = true;

                ShowProgressPanel();

                return;
            }

            if (cbComics.SelectedIndex == -1)
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    TaskDialog.Show("Please Select a Comic to Download", "No Comic Selected",
                        "You must select a comic to download from the drop down menu before clicking 'Download'.",
                        TaskDialogButton.OK, TaskDialogIcon.Information);
                }
                else
                {
                    MessageBox.Show("You must select a comic to download from the drop down menu before clicking 'Download'",
                        "Please Select a Comic to Download",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            foreach (Comic comic in comics)
                if (comic.Name == cbComics.SelectedItem.ToString())
                {
                    //try and get the latest version of this comic from the default folder
                    Comic comic1 = comic;
                    comic1 = ComicParser.UpdateComicFromLocal(comic, Path.Combine(destination, comic.Name + "\\Comic.xml"));


                    if (SierraLib.Settings.GetSetting("UsageTracking") == "Enabled")
                        Tracker.CurrentInstance.TrackEvent("Web Comic Downloader", "Downloads", comic1.Name);

                    bwDownloader.RunWorkerAsync(new Comic[] { comic1 });

                    btnBrowse.Enabled = false;
                    cbComics.Enabled = false;
                    btnDownload.Enabled = false;
                    chkDownloadAll.Enabled = false;
                    txtPath.Enabled = false;
                    btnCancel.Enabled = true;

                    ShowProgressPanel();

                    return;
                }
        }

        #region "Path Processing"

        string getResourceName(string address, bool maintainExtension)
        {
            for (int i = address.Length - 1; i >= 0; i--)
            {
                if (address[i] == '/' && maintainExtension)
                    return address.Substring(i + 1);
                else if (address[i] == '/' && !maintainExtension)
                {
                    string res = address.Substring(i + 1);
                    int len = getExtension(res).Length;
                    return res.Remove(res.Length - len, len);
                }
            }
            return "";
        }

        string getExtension(string address)
        {
            for (int i = address.Length - 1; i >= 0; i--)
            {
                if (address[i] == '.')
                    return address.Substring(i);
            }
            return "";
        }

        string GetHostName(string address)
        {
            Regex reg = new Regex("[a-zA-Z]+://(?<host>[^/]+)");
            return reg.Match(address).Groups["host"].Value ?? address;
        }

        string GetHostName(string address, bool returnProtocol)
        {
            if (!returnProtocol)
            {
                Regex reg = new Regex("[a-zA-Z]+://(?<host>[^/]+)");
                return reg.Match(address).Groups["host"].Value ?? address;
            }
            else
            {
                Regex reg = new Regex("(?<host>[a-zA-Z]+://[^/]+)");
                return reg.Match(address).Groups["host"].Value ?? address;
            }
        }

        #endregion "Path Processing"

        /// <summary>
        /// Handles the DoWork event of the bwDownloader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        void bwDownloader_DoWorkNew(object sender, DoWorkEventArgs e)
        {
            
            #region Delegate Method Declarations

            setCurrentComicSafe setComic = new setCurrentComicSafe(setCurrentComic);
            getDestinationSafe getDest = new getDestinationSafe(getDestination);
            setValuesSafe setVals = new setValuesSafe(setValues);

            #endregion Delegate Method Declarations

            #region Pre Download Setup

            if (SierraLib.Settings.GetSetting("PreventStandby") != "false")
                SierraLib.Windows.SystemPowerState.PreventStandby();

            if (SierraLib.Settings.GetSetting("TurnOffScreen") == "true")
                SierraLib.Windows.MonitorControl.Standby();

            string comicDirectory = Invoke(getDest).ToString();

            int downloadCount = 0;

            #endregion Pre Download Setup

            #region Comic Processing Loop

            Comic[] comics = (Comic[])e.Argument;

            foreach (Comic comic in comics)
            {

                //Exit if the application is requesting the thread to do so
                if (bwDownloader.CancellationPending)
                    return;

                #region Google Analytics Tracking
                if (SierraLib.Settings.GetSetting("UsageTracking") == "Enabled")                
                    Tracker.CurrentInstance.TrackEvent("Web Comic Downloader", "Downloading Comic", comic.Name);
                

                #endregion

                #region Initial Setup

                //Set the currently downloading comic on the UI
                Invoke(setComic, comic.Name);

                //Set default values
                Invoke(setVals, 0, 0, 0.0f, TimeSpan.Zero, comic.DownloadedSize / 1024, comic.LatestLocalComicNumber, comic.DownloadedPageSize / 1024, false);

                //Declare HtmlWeb page downloader
                SierraLib.HTMLParsing.HtmlDocument doc;

                //Declare image downloading objects
                HttpWebRequest webRequest;
                HttpWebResponse webResponse;
                Stream webResponseStream;
                FileStream fileDownloadStream;
                HttpWebRequest pageDownloaderRequest = null;
                HttpWebResponse pageDownloaderResponse = null;
                Stream pageDownloaderStream = null;
                CookieContainer cookies = new CookieContainer();

                bool finished = false;
                bool imgError = false;
                bool isLast = false;
                bool pageError = false;

                //Create the directory for the webcomic if it does not exist
                if (!Directory.Exists(Path.Combine(destination, comic.Name)))
                    Directory.CreateDirectory(Path.Combine(destination, comic.Name));


                //Setup any cookies required by the comic
                if (comic.Cookies != null && comic.Cookies.Count > 0)
                {
                    for (int i = 0; i < comic.Cookies.Count; i++)
                        cookies.Add(new Cookie(comic.Cookies.Keys[i], comic.Cookies.Values[i], "/", GetHostName(comic.FirstComicAddress)));
                }

                #endregion Initial Setup

                while (!finished)
                {
                    //Exit if the application is requesting the thread to do so
                    if (bwDownloader.CancellationPending)
                        return;

                    #region Download Latest Page

                    if (!comic.UseSafePageDownloadCode)
                    {
                        #region With Progress

                        //Load latest page
                        try
                        {
                            pageDownloaderRequest = (HttpWebRequest)WebRequest.Create(comic.LatestLocalComicAddress);
                            pageDownloaderRequest.UserAgent = comic.UserAgent != null ? comic.UserAgent : UserAgent;
                            pageDownloaderRequest.CookieContainer = cookies;
                            
                            pageDownloaderResponse = (HttpWebResponse)pageDownloaderRequest.GetResponse();
                            pageDownloaderStream = pageDownloaderResponse.GetResponseStream();
                            pageError = false;
                        }
                        catch
                        {
                            if (pageError)
                                finished = true;
                            //Restart from last position
                            XMLSearcher.ComicParser.UpdateComicFromLocal(comic, Path.Combine(destination, comic.Name + "\\Comic.xml"));
                            pageError = true;
                            continue;
                        }
                        MemoryStream pageDownloadBuffer = new MemoryStream();

                        #region Pre Download Setup

                        //Pre download declarations
                        int pageCount = 0;
                        int pageOffset = 0;
                        float pageSpeed = 0.0f;
                        TimeSpan pageElapsed = new TimeSpan();
                        TimeSpan pageRemaining = new TimeSpan();
                        System.Diagnostics.Stopwatch pageStp = new System.Diagnostics.Stopwatch();
                        string pageHTML = "";

                        #endregion Pre Download Setup

                        #region Downloading

                        do
                        {
                            //Start timing this section of the download
                            pageStp.Start();

                            int tempByte = 0;
                            
                                try
                                {
                                    do
                                    {
                                        pageCount = 0;
                                        tempByte = pageDownloaderStream.ReadByte();
                                        if (tempByte != -1)
                                        {

                                            pageDownloadBuffer.WriteByte((byte)tempByte);

                                            //Download a section of the file to the buffer
                                            pageCount++;

                                            //Increase the file offset
                                            pageOffset++;
                                        }

                                        if (pageOffset % 128 == 0)
                                        {
                                            //Increase the amount of data that has been downloaded
                                            comic.DownloadedSize += pageCount;


                                            pageElapsed.Add(new TimeSpan(pageStp.ElapsedTicks));

                                            pageSpeed = (pageOffset / ((float)((pageStp.ElapsedMilliseconds == 0) ? 1 : pageStp.ElapsedMilliseconds) / 1000)) / 1024;

                                            if (pageDownloaderResponse.ContentLength == pageOffset)
                                                pageRemaining = TimeSpan.Zero;
                                            else
                                                pageRemaining = new TimeSpan(0, 0, 0, Convert.ToInt32((pageDownloaderResponse.ContentLength - pageOffset) / (pageSpeed * 1024)));

                                            Invoke(setVals, (int)(pageDownloaderResponse.ContentLength) / 1024, pageOffset / 1024, pageSpeed, pageRemaining, comic.DownloadedSize / 1024, comic.LatestLocalComicNumber, comic.DownloadedPageSize / 1024, false);
                        
                                        }

                                    } while (tempByte != -1);

                                }
                                catch
                                {
                                    //Stop timing
                                    pageStp.Stop();

                                    //wait for response to expire
                                    Thread.Sleep(pageDownloaderStream.ReadTimeout);

                                    //Generate a new response
                                    webResponse = (HttpWebResponse)pageDownloaderRequest.GetResponse();
                                    webResponseStream = webResponse.GetResponseStream();
                                    pageHTML = "";
                                    pageOffset = 0;

                                    if (bwDownloader.CancellationPending)
                                    {
                                        return;
                                    }

                                    continue;
                                }


                                pageStp.Stop();

                            }
                        while (pageCount > 0);

                        #endregion Downloading

                        #region Post Download

                        pageStp.Reset();

                        doc = new SierraLib.HTMLParsing.HtmlDocument();
                        pageHTML = Encoding.ASCII.GetString(pageDownloadBuffer.ToArray());
                        doc.LoadHtml(pageHTML);
                        comic.DownloadedSize += pageHTML.Length;

                        #endregion Post Download

                        #endregion With Progress
                    }
                    else
                    {
                        #region Without Progress

                        Invoke(setVals, 0, 0, 0, TimeSpan.Zero, comic.DownloadedSize / 1024, comic.LatestLocalComicNumber, comic.DownloadedPageSize / 1024, false);

                        HtmlWeb pageDownloaderWeb = new HtmlWeb();
                        pageDownloaderWeb.UseCookies = true;
                        pageDownloaderWeb.UserAgent = comic.UserAgent != null ? comic.UserAgent : UserAgent;
                        
                        if (comic.Cookies != null && comic.Cookies.Count > 0)
                        {                            
                            for (int i = 0; i < comic.Cookies.Count; i++)
                                pageDownloaderWeb.Cookies.Add(new Cookie(comic.Cookies.Keys[i], comic.Cookies.Values[i]));
                        }

                        doc = pageDownloaderWeb.Load(comic.LatestLocalComicAddress);
                        comic.DownloadedSize += doc.DocumentNode.InnerHtml.Length;

                        #endregion Without Progress
                    }

                    #endregion Download Latest Page

                    #region Get Image Path and Download Image

                    string imgPath = GetImagePath(doc, comic.ImageXPath);

                    #region Error Getting Image Path

                    //First error; attempt to download the file again and check
                    if (imgPath == null && !imgError)
                    {
                        imgError = true;
                        Debug.WriteLine("Missing image path, trying again [" + comic.Name + "]");
                        continue;
                    }
                    //Second error, move on to the next page (might be an invalid image or
                    //may not be an image
                    else if (imgPath == null && imgError && !comic.StopOnMissingImage)
                    {
                        //Load the next page
                        string nextAddrErr = GetNextPath(doc, comic.NextXPath);
                        if (nextAddrErr != null)
                        {
                            comic.LatestLocalComicAddress = FixPath(nextAddrErr, comic.Address, comic.LatestLocalComicAddress);
                            imgError = false;
                            continue;
                        }
                        else
                        {
                            finished = true;
                            continue;
                        }
                    }

                    #endregion Error Getting Image Path

                    #region Path found; Download image

                    else
                    {
                        //Correct behaviour

                        //Get correct image path
                        imgPath = FixPath(imgPath, comic.Address, comic.LatestLocalComicAddress);

                        //Create web request for image
                        webRequest = (HttpWebRequest)WebRequest.Create(imgPath);

                        //Set referrer if specified
                        if (comic.UseComicPathAsReferrer)
                            webRequest.Referer = comic.LatestLocalComicAddress;
                        else if (comic.Referrer != null)
                            webRequest.Referer = comic.Referrer;

                        bool imgDownloadComplete = false;
                        int retryCount = 0;
                        while (!imgDownloadComplete && retryCount < 3 && !finished)
                        {
                            try
                            {
                                webResponse = null;
                                //Query a reply from the server
                                try
                                {
                                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                                }
                                catch
                                {
                                    retryCount++;
                                    Debug.WriteLine("Image Request Failed [" + retryCount + "/3]");

                                    if (retryCount < 3 || comic.StopOnMissingImage)
                                        continue;
                                }

                                if (webResponse != null)
                                {

                                    //Get the stream that we will use to download the image
                                    webResponseStream = webResponse.GetResponseStream();

                                    #region Generate File Name and Tag values

                                    //Create the comic file name
                                    string imageFileName = "%Index% - %ImageName%"; //Default pattern
                                    string imageTitle = "%ImageName%";
                                    string imageDescription = "";
                                    string imageExtension = "";
                                    ImageFormat imageFormat = null;


                                    if (comic.ImageFormat != null)
                                    {
                                        if (comic.ImageFormat.Equals("PNG", StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            imageExtension = ".png";
                                            imageFormat = ImageFormat.Png;
                                        }
                                        else if (comic.ImageFormat.Equals("JPG", StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            imageExtension = ".jpg";
                                            imageFormat = ImageFormat.Jpeg;
                                        }
                                        else if (comic.ImageFormat.Equals("BMP", StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            imageExtension = ".bmp";
                                            imageFormat = ImageFormat.Bmp;
                                        }
                                        else
                                        {
                                            imageExtension = getExtension(imgPath);
                                            Debug.WriteLine("Unknown image format, using default [" + comic.Name + "]: " + comic.ImageFormat);
                                        }
                                    }
                                    else
                                    {
                                        imageExtension = getExtension(imgPath);
                                    }

                                    //If the comic has its own pattern then use that
                                    if (comic.NamingPattern != null)
                                        imageFileName = comic.NamingPattern;

                                    if (comic.EXIFImageDescriptionPattern != null)
                                        imageDescription = comic.EXIFImageDescriptionPattern;

                                    if (comic.EXIFImageNamePattern != null)
                                        imageTitle = comic.EXIFImageNamePattern;

                                    //Replace values into pattern
                                    imageFileName = imageFileName.Replace("%Index%", comic.LatestLocalComicNumber.ToString());
                                    imageFileName = imageFileName.Replace("%ImageName%", getResourceName(imgPath, false));
                                    imageFileName += imageExtension;

                                    imageTitle = imageTitle.Replace("%Index%", comic.LatestLocalComicNumber.ToString());
                                    imageTitle = imageTitle.Replace("%ImageName%", getResourceName(imgPath, false));

                                    imageDescription = imageDescription.Replace("%Index%", comic.LatestLocalComicNumber.ToString());
                                    imageDescription = imageDescription.Replace("%ImageName%", getResourceName(imgPath, false));


                                    //If the comic has an XPath pattern defined then replace into that
                                    if (!string.IsNullOrEmpty(comic.NamingXPath))
                                    {
                                        try
                                        {
                                            string nameXPathReplace = "";
                                            
                                            var attrib = doc.DocumentNode.SelectSingleAttribute(comic.NamingXPath);
                                            var node = doc.DocumentNode.SelectSingleNode(comic.NamingXPath);
                                            if (attrib != null)
                                                nameXPathReplace = attrib.Value;
                                            else if (node != null)
                                                nameXPathReplace = node.InnerText;

                                            if (nameXPathReplace != null)
                                                imageFileName = imageFileName.Replace("%XPath%", nameXPathReplace);
                                            else
                                                imageFileName = imageFileName.Replace("%XPath%", "");
                                        }
                                        catch
                                        {
                                            imageFileName = imageFileName.Replace("%XPath%", "");
                                            Debug.WriteLine("File Name XPath returned nothing [" + comic.Name + "]: " + comic.NamingXPath);
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(comic.EXIFImageNameXPath))
                                    {
                                        try
                                        {
                                            string nameXPathReplace = "";
                                            var attrib = doc.DocumentNode.SelectSingleAttribute(comic.EXIFImageNameXPath);
                                            var node = doc.DocumentNode.SelectSingleNode(comic.EXIFImageNameXPath);
                                            if (attrib != null)
                                                nameXPathReplace = attrib.Value;
                                            else if (node != null)
                                                nameXPathReplace = node.InnerText;

                                            if (nameXPathReplace != null)
                                                imageTitle = imageTitle.Replace("%XPath%", nameXPathReplace);
                                            else
                                                imageTitle = imageTitle.Replace("%XPath%", "");
                                        }
                                        catch
                                        {
                                            imageTitle = imageTitle.Replace("%XPath%", "");
                                            Debug.WriteLine("Image Name XPath returned nothing [" + comic.Name + "]: " + comic.EXIFImageNameXPath);
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(comic.EXIFImageDescriptionXPath))
                                    {
                                        try
                                        {
                                            string nameXPathReplace = "";
                                            var attrib = doc.DocumentNode.SelectSingleAttribute(comic.EXIFImageDescriptionXPath);
                                            var node = doc.DocumentNode.SelectSingleNode(comic.EXIFImageDescriptionXPath);
                                            if (attrib != null)
                                                nameXPathReplace = attrib.Value;
                                            else if (node != null)
                                                nameXPathReplace = node.InnerText;
                                            if (nameXPathReplace != null)
                                                imageDescription = imageDescription.Replace("%XPath%", nameXPathReplace);
                                            else
                                                imageDescription = imageDescription.Replace("%XPath%", "");
                                        }
                                        catch
                                        {
                                            imageDescription = imageDescription.Replace("%XPath%", "");
                                        }
                                    }

                                    imageFileName = ProcessCharacterCodes(imageFileName);
                                    imageFileName = FixPath(imageFileName);
                                    imageTitle = ProcessCharacterCodes(imageTitle);
                                    imageDescription = ProcessCharacterCodes(imageDescription);

                                    #endregion Generate File Name

                                    //Create a file stream to write the new file
                                    fileDownloadStream = new FileStream(Path.Combine(destination, comic.Name + "\\" + imageFileName), FileMode.Create, FileAccess.Write);

                                    //Set new values prior to downloading
                                    Invoke(setVals, (int)(webResponse.ContentLength) / 1024, 0, 0.0f, TimeSpan.Zero, comic.DownloadedSize / 1024, comic.LatestLocalComicNumber, comic.DownloadedPageSize / 1024, true);

                                    #region Pre Download Setup

                                    //Pre download declarations
                                    int count = 0;
                                    int offset = 0;
                                    float speed = 0.0f;
                                    TimeSpan elapsed = new TimeSpan();
                                    TimeSpan remaining = new TimeSpan();
                                    System.Diagnostics.Stopwatch stp = new System.Diagnostics.Stopwatch();

                                    #endregion Pre Download Setup

                                    #region Downloading

                                    do
                                    {
                                        //Start timing this section of the download
                                        stp.Start();

                                        try
                                        {
                                            int tempByte;
                                            count = 0;
                                            do
                                            {
                                                tempByte = webResponseStream.ReadByte();

                                                if (tempByte != -1)
                                                {
                                                    fileDownloadStream.WriteByte((byte)tempByte);

                                                    //Increase the file offset
                                                    offset++;
                                                    count++;
                                                }

                                                if (offset % 512 == 0)
                                                {



                                                    elapsed.Add(new TimeSpan(stp.ElapsedTicks));

                                                    speed = (offset / ((float)((stp.ElapsedMilliseconds == 0) ? 1 : stp.ElapsedMilliseconds) / 1000)) / 1024;

                                                    if (webResponse.ContentLength == offset)
                                                        remaining = TimeSpan.Zero;
                                                    else
                                                        remaining = new TimeSpan(0, 0, 0, Convert.ToInt32((webResponse.ContentLength - offset) / (speed * 1024)));

                                                    Invoke(setVals, (int)(webResponse.ContentLength) / 1024, offset / 1024, speed, remaining, comic.DownloadedSize / 1024, comic.LatestLocalComicNumber, comic.DownloadedPageSize / 1024, true);

                                                }

                                            } while (tempByte != -1);

                                            stp.Stop();

                                        }
                                        catch
                                        {
                                            //Stop timing
                                            stp.Stop();

                                            //wait for response to expire
                                            Thread.Sleep(webResponseStream.ReadTimeout);

                                            //Generate a new response
                                            webResponse = (HttpWebResponse)webRequest.GetResponse();
                                            webResponseStream = webResponse.GetResponseStream();
                                            fileDownloadStream.Position = 0;
                                            offset = 0;

                                            if (bwDownloader.CancellationPending)
                                            {
                                                Debug.WriteLine("Stop request received, deleting current image");
                                                fileDownloadStream.Close();
                                                File.Delete(fileDownloadStream.Name);
                                                return;
                                            }

                                            continue;
                                        }

                                        //Increase the amount of data that has been downloaded to reflect the new image
                                        comic.DownloadedSize += count;
                                        comic.DownloadedImageSize += count;

                                    }
                                    while (count > 0);

                                    #endregion Downloading

                                    #region Post Download

                                    stp.Reset();

                                    fileDownloadStream.Close();


                                    //Set the image's tags
                                    FileStream imageStream = new FileStream(Path.Combine(destination, comic.Name + "\\" + imageFileName), FileMode.Open, FileAccess.ReadWrite);
                                    Bitmap img = new Bitmap(imageStream);

                                    //if (img.PixelFormat != PixelFormat.Format1bppIndexed && img.PixelFormat != PixelFormat.Format4bppIndexed && img.PixelFormat != PixelFormat.Format8bppIndexed && img.PixelFormat != PixelFormat.Indexed)
                                    //{
                                    img = SetImagePropertyUTF(img, 0x9c9c, imageDescription);
                                    img = SetImagePropertyUTF(img, 0x9c9b, imageTitle);
                                    img = SetImagePropertyUTF(img, 0x9c9d, comic.Author);
                                    //img = SetImageProperty(img, 0x320, imageTitle);
                                    //img = SetImageProperty(img, 0x10e, imageDescription);
                                    //img = SetImageProperty(img, 0x13b, comic.Author);
                                    img = SetImageProperty(img, 0x110, SierraLib.AssemblyInformation.GetAssemblyVersion(Assembly.GetExecutingAssembly()).ToString());
                                    img = SetImageProperty(img, 0x8298, "Copyright " + comic.Author);
                                    img = SetImageProperty(img, 0x0131, "Web Comic Downloader");
                                    img = SetImageProperty(img, 0x9286, "UNICODE");
                                    img = SetImageProperty(img, 0x10F, "Sierra Softworks");
                                    //img = SetImageProperty(img, 0x132, DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss"));



                                    List<PropertyItem> propertyItems = new List<PropertyItem>();
                                    foreach (PropertyItem itm in img.PropertyItems)
                                        propertyItems.Add(itm);

                                    if (imageFormat == null)
                                        imageFormat = img.RawFormat;

                                    Bitmap newImage = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);

                                    Graphics g = Graphics.FromImage(newImage);
                                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                                    g.DrawImage(img, 0, 0, img.Width, img.Height);
                                    g.Dispose();
                                    img.Dispose();
                                    imageStream.Close();

                                    foreach (PropertyItem itm in propertyItems)
                                        newImage.SetPropertyItem(itm);

                                    newImage.Save(Path.Combine(destination, comic.Name + "\\" + imageFileName), imageFormat);

                                    newImage.Dispose();
                                    //}
                                    //else
                                    //{
                                    //    //We can't try to add tags to an image who's color is in Indexed mode

                                    //    img.Dispose();
                                    //    imageStream.Close();
                                    //}

                                    //Update the local comic records
                                    if (!isLast)
                                        ComicParser.UpdateLocalComic(comic, Path.Combine(destination, comic.Name + "\\Comic.xml"));

                                    downloadCount++;

                                    #endregion Post Download

                                    //Exit if the application has requested this thread to do so
                                    if (bwDownloader.CancellationPending)
                                        return;
                                }
                                //Exit if the last comic that was downloaded linked to the final
                                //strip
                                if (isLast)
                                {
                                    finished = true;
                                    Debug.WriteLine("Reached end of comic [" + comic.Name + "]");
                                    continue;
                                }

                                #region Get Next Page Path

                                string nextPath = GetNextPath(doc, comic.NextXPath);
                                if (nextPath == null)
                                {
                                    finished = true;
                                    Debug.WriteLine("No Next Path [" + comic.Name + ": " + comic.LastComicLink + "]");
                                    continue;
                                }
                                else if (nextPath == comic.LastComicLink)
                                {
                                    if (comic.StopImmediatelyOnLast)
                                    {
                                        finished = true;
                                        Debug.WriteLine("Reached end of comic (Determined by link address) [" + comic.Name + "]");
                                        continue;
                                    }
                                    else
                                    {
                                        //Probably the end of the comic
                                        isLast = true;
                                    }
                                }

                                nextPath = FixPath(nextPath, comic.Address, comic.LatestLocalComicAddress);

                                if (nextPath == comic.LatestLocalComicAddress)
                                {
                                    //Probably the end of the comic
                                    finished = true;
                                    Debug.WriteLine("Reached end of comic (Next pointed to the current page) [" + comic.Name + "]");
                                    continue;
                                }

                                comic.LatestLocalComicAddress = nextPath;
                                comic.LatestLocalComicNumber++;

                                #endregion Get Next Page Path

                                imgDownloadComplete = true;
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                System.Diagnostics.Debug.WriteLine("Access Denied: " + ex.Message);
                                retryCount++;

                                if (TaskDialog.Show("Access Denied", "Access Denied",
                                    "You do not appear to have permissions to save this Webcomic to the folder you specified.\n" +
                                    "Please make sure that you have write permissions for that folder, or try changing where you save your webcomics.\n\n" +
                                    "If you feel this is incorrect or have fixed the problem then click Retry",
                                    TaskDialogButton.Cancel | TaskDialogButton.Retry, TaskDialogIcon.SecurityError) == TaskDialogResults.Retry)
                                    continue;
                                else
                                {
                                    finished = true;
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Error: " + ex.Message);
                                retryCount++;



                                continue;
                            }
                        }
                        if (retryCount == 3 && comic.StopOnMissingImage)
                        {
                            finished = true;
                            Debug.WriteLine("Comic download failed after 3 attempts [" + comic.Name + "]");
                            continue;
                        }
                    }

                    #endregion Path found; Download image

                    #endregion Get Image Path and Download Image
                }
            }

            #endregion Comic Processing Loop

            #region Post Download Cleanup

            //Reset the power state to enable standby once again
            SierraLib.Windows.SystemPowerState.Reset();

            Tracker.CurrentInstance.TrackEvent("Web Comic Downloader", "Downloads", "Complete", downloadCount);

            #endregion Post Download Cleanup
        }

        private string FixPath(string imageFileName)
        {
            string regexSearch = string.Format("{0}{1}",
                                 new string(Path.GetInvalidFileNameChars()),
                                 new string(Path.GetInvalidPathChars()));
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(imageFileName, "");
        }

        Bitmap SetImageProperty(Bitmap img, int id, string value)
        {
            if (img.PropertyItems.Length == 0)
                return img;

            try
            {
                PropertyItem itm = img.GetPropertyItem(id);

                if (itm == null)
                    return img;

                itm.Id = id;
                itm.Type = 2; // ASCII string
                itm.Len = Encoding.ASCII.GetByteCount(value);
                itm.Value = Encoding.ASCII.GetBytes(value);
                
                img.SetPropertyItem(itm);

                return img;
            }
            catch
            {
                PropertyItem itm = img.PropertyItems[0];

                if (itm == null)
                    return img;
                
                itm.Id = id;
                itm.Type = 2; // ASCII string
                itm.Len = Encoding.ASCII.GetByteCount(value);
                itm.Value = Encoding.ASCII.GetBytes(value);

                img.SetPropertyItem(itm);


                return img;
            }

            
        }
        Bitmap SetImagePropertyUTF(Bitmap img, int id, string value)
        {
            if (img.PropertyItems.Length == 0)
                return img;

            try
            {
                PropertyItem itm = img.GetPropertyItem(id);

                if (itm == null)
                    return img;

                itm.Id = id;
                itm.Type = 1; // UTF16 string
                itm.Len = Encoding.Unicode.GetByteCount(value);
                itm.Value = Encoding.Unicode.GetBytes(value);

                img.SetPropertyItem(itm);

                return img;
            }
            catch
            {
                PropertyItem itm = img.PropertyItems[0];

                if (itm == null)
                    return img;

                itm.Id = id;
                itm.Type = 1; // UTF16 string
                itm.Len = Encoding.Unicode.GetByteCount(value);
                itm.Value = Encoding.Unicode.GetBytes(value);

                img.SetPropertyItem(itm);


                return img;
            }


        }
        Bitmap SetUserComment(Bitmap img, int id, string value)
        {
            byte[] unicodeID = new byte[] { 0x55, 0x4E, 0x49, 0x43, 0x4F, 0x44, 0x45, 0x00 };


            if (img.PropertyItems.Length == 0)
                return img;

            try
            {
                PropertyItem itm = img.GetPropertyItem(id);

                if (itm == null)
                    return img;

                itm.Id = id;
                itm.Type = 7; // Undefined

                byte[] temp = new byte[0];
                CreationFunctions.AddData(ref temp, unicodeID);
                CreationFunctions.AddData(ref temp, Encoding.Unicode.GetBytes(value));
                itm.Len = temp.Length;
                itm.Value = temp;
                img.SetPropertyItem(itm);

                return img;
            }
            catch
            {
                PropertyItem itm = img.PropertyItems[0];

                if (itm == null)
                    return img;

                itm.Id = id;
                itm.Type = 7; // Undefined

                byte[] temp = new byte[0];
                CreationFunctions.AddData(ref temp, unicodeID);
                CreationFunctions.AddData(ref temp, Encoding.Unicode.GetBytes(value));
                itm.Len = temp.Length;
                itm.Value = temp;

                img.SetPropertyItem(itm);


                return img;
            }
        }

        string ProcessCharacterCodes(string text)
        {
            string temp = text;
            Regex regex = new Regex("&#(?<code>[A-Za-z0-9]+);", RegexOptions.Compiled);
            foreach (Match mtch in regex.Matches(text))
            {
                byte tmp = 0;
                if (byte.TryParse(mtch.Groups["code"].Value, out tmp))
                    temp = temp.Replace(mtch.Value, Encoding.ASCII.GetString(new byte[] { tmp }));
                else
                    temp = temp.Replace(mtch.Value, "");
            }

            return temp;
        }

        string GetImagePath(SierraLib.HTMLParsing.HtmlDocument doc, string xpath)
        {
            string imgPath = null;

            HtmlAttribute tempAttrib = doc.DocumentNode.SelectSingleAttribute(xpath);
            HtmlNode tempNode = doc.DocumentNode.SelectSingleNode(xpath);
            if (tempAttrib != null)
            { imgPath = tempAttrib.Value; }
            else if (tempNode != null)
            { imgPath = tempNode.Attributes["src"].Value; }

            return imgPath;
        }

        /// <summary>
        /// Fixes the path.
        /// </summary>
        /// <param name="imgPath">The img path.</param>
        /// <param name="comicAddress">The comic address.</param>
        /// <param name="currentAddress">The current address.</param>
        /// <returns></returns>
        string FixPath(string imgPath, string comicAddress, string currentAddress)
        {
            return SierraLib.PathProcessing.WebDirectories.ProcessPath(imgPath, currentAddress);

            //string temp = imgPath;
            //temp = Uri.EscapeUriString(imgPath);

            //if (!Uri.IsWellFormedUriString(temp, UriKind.Absolute))
            //{
            //    if (temp.StartsWith("/"))
            //        temp = GetHostName(comicAddress, true) + temp;
            //    else if (temp.StartsWith("./")) //Incorrect handling - Should fix
            //        temp = GetHostName(comicAddress, true) + temp.Remove(0, 1);
            //    else
            //    {
            //        //Should be the parent directory + temp
            //        string parent = "";
            //        for (int i = currentAddress.Length - 1; i >= 0; i--)
            //        {
            //            if (currentAddress[i] == '/')
            //            {
            //                parent = currentAddress.Remove(i);
            //                break;
            //            }
            //        }

            //        if (parent == "")
            //            temp = GetHostName(comicAddress, true) + temp;

            //        temp = parent + "/" + temp;

            //        if (!temp.StartsWith("http://"))
            //            temp = "http://" + temp;
            //    }
            //}

            //return temp;
        }

        /// <summary>
        /// Gets the next path.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="xpath">The xpath.</param>
        /// <returns></returns>
        string GetNextPath(SierraLib.HTMLParsing.HtmlDocument doc, string xpath)
        {
            string imgPath = null;

            HtmlAttribute tempAttrib = doc.DocumentNode.SelectSingleAttribute(xpath);
            HtmlNode tempNode = doc.DocumentNode.SelectSingleNode(xpath);
            if (tempAttrib != null)
            { imgPath = tempAttrib.Value; }
            else if (tempNode != null)
            { imgPath = tempNode.Attributes["href"].Value; }

            return imgPath;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="xpath">The xpath.</param>
        /// <returns></returns>
        string GetName(SierraLib.HTMLParsing.HtmlDocument doc, string xpath)
        {
            string imgPath = null;

            HtmlAttribute tempAttrib = doc.DocumentNode.SelectSingleAttribute(xpath);
            HtmlNode tempNode = doc.DocumentNode.SelectSingleNode(xpath);
            if (tempAttrib != null)
            { imgPath = tempAttrib.Value; }
            else if (tempNode != null)
            { imgPath = tempNode.InnerText; }

            return imgPath;
        }


        /// <summary>
        /// Handles the RunWorkerCompleted event of the bwDownloader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        void bwDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!SierraLib.Windows.SystemPowerState.Reset())
            {
                //Show warning to user
            }

            btnDownload.Enabled = true;
            btnBrowse.Enabled = true;
            cbComics.Enabled = true;
            btnCancel.Enabled = false;
            chkDownloadAll.Enabled = true;
            txtPath.Enabled = true;

            if (chkDownloadAll.Enabled == false)
            {
                cbComics.Enabled = false;
                HideDetailsPanel();
            }
            HideProgressPanel();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bwDownloader.CancelAsync();
            btnCancel.Enabled = false;
        }

        private void cbComics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbComics.SelectedIndex != -1)
            {
                //HideDetailsPanel();
                foreach (Comic comic in comics)
                    if (comic.Name == cbComics.SelectedItem.ToString())
                    {
                        lblComicName.Text = comic.Name + " by " + comic.Author;
                        linkComic.Text = comic.Address;
                    }

                ShowDetailsPanel();

                SierraLib.Settings.SetSetting("LastComic", cbComics.SelectedItem.ToString());
            }
            else
            {
                HideDetailsPanel();
            }
        }

        private void linkComic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(((((LinkLabel)sender).Text.StartsWith("http://")) ? ((LinkLabel)sender).Text : "http://" + ((LinkLabel)sender).Text));
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                destination = fd.SelectedPath;
                txtPath.Text = destination;
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            destination = txtPath.Text;
            SierraLib.Settings.SetSetting("WebcomicsPath", txtPath.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sierrasoftworks.com/wkd");
        }

        private void chkDownloadAll_CheckedChanged(object sender, EventArgs e)
        {
            cbComics.Enabled = !chkDownloadAll.Checked;
            if (chkDownloadAll.Checked)
                HideDetailsPanel();
            else
                ShowDetailsPanel();
        }

        private void linkCloseSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HideSettingsPanel();
        }

        private void linkShowSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowSettingsPanel();
        }

        private void chkPreventStandby_CheckedChanged(object sender, EventArgs e)
        {
            SierraLib.Settings.SetSetting("PreventStandby", chkPreventStandby.Checked.ToString().ToLower());

            if (bwDownloader.IsBusy && chkPreventStandby.Checked == true)
                SierraLib.Windows.SystemPowerState.PreventStandby();
            else if (bwDownloader.IsBusy)
                SierraLib.Windows.SystemPowerState.Reset();
        }

        private void chkCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            SierraLib.Settings.SetSetting("CheckForUpdates", chkCheckUpdates.Checked.ToString().ToLower());
        }

        private void chkTurnOffScreen_CheckedChanged(object sender, EventArgs e)
        {
            SierraLib.Settings.SetSetting("TurnOffScreen", chkTurnOffScreen.Checked.ToString().ToLower());
            SierraLib.Windows.MonitorControl.Standby();
        }

        private void Main_Load(object sender, EventArgs e)
        {

            LoadComics();

            LoadSettings();

            bool CheckUpdates = true;

            if (String.IsNullOrEmpty(SierraLib.Settings.GetSetting("CheckForUpdates")))
                SierraLib.Settings.SetSetting("CheckForUpdates", "true");

            if (String.IsNullOrEmpty(SierraLib.Settings.GetSetting("UsageTracking")))
                SierraLib.Settings.SetSetting("UsageTracking", "Enabled");

            if (SierraLib.Settings.GetSetting("CheckForUpdates") != "true")
                CheckUpdates = false;


            //Send info about startup
            //if (SierraLib.Settings.GetSetting("UsageTracking") == "Enabled")
            //    GoogleAnalytics.FireTrackingEventAsync(
            //                "apptracking.sierrasoftworks.com/wkd",
            //                "Application Launches",
            //                Environment.OSVersion.Platform.ToString() + " - " + Environment.OSVersion.VersionString,
            //                "Web Comic Downloader - " + SierraLib.AssemblyInformation.GetAssemblyVersion(Assembly.GetExecutingAssembly()).ToString(),
            //                1);

            chkCheckUpdates.Checked = CheckUpdates;


                panelSettings.Animations.Add(new Animation(AnimationType.Position, AnimationDirection.Vertical, new TimeSpan(0, 0, 0, 0, 150), panelSettings.Location.Y, 0 - panelSettings.Height));
                panelDetails.Animations.Add(new Animation(AnimationType.Position, AnimationDirection.Vertical, new TimeSpan(0, 0, 0, 0, 150), panelDetails.Location.Y,  0 - panelDetails.Height));
                panelProgress.Animations.Add(new Animation(AnimationType.Position, AnimationDirection.Vertical, new TimeSpan(0, 0, 0, 0, 250), panelProgress.Location.Y, Height));
            

                HideDetailsPanel();
                HideSettingsPanel();
                HideProgressPanel();



                if (CheckUpdates)
                    Updates.GetUpdates();
        }
    }
}