using FlickrNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadFlickr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Flickr F = new Flickr();
        private void Form1_Load(object sender, EventArgs e)
        {
            string apikey = "keyapi";
            F = new Flickr("keyapi", "keyapi");
        }

        private void btSr_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }
        public string[] BiggestVersionUrl(Photo Photo)
        {
            string[] Org = new string[2];
            string BiggestVersionUrl1 = string.Empty;
            string Size = string.Empty;
            var p = Photo;
            if (!string.IsNullOrEmpty(p.OriginalUrl))
            {
                BiggestVersionUrl1 = p.OriginalUrl;
                Size = "OriginalUrl";
            }
            else if (!string.IsNullOrEmpty(p.Large2048Url))
            {
                BiggestVersionUrl1 = p.Large2048Url;
                Size = "OriginalUrl";
            }
            else if (!string.IsNullOrEmpty(p.Large1600Url))
            {
                BiggestVersionUrl1 = p.Large1600Url;
                Size = "Large1600Url";
            }
            else if (!string.IsNullOrEmpty(p.LargeUrl))
            {
                BiggestVersionUrl1 = p.LargeUrl;
                Size = "LargeUrl";
            }
            else if (!string.IsNullOrEmpty(p.MediumUrl))
            {
                BiggestVersionUrl1 = p.MediumUrl;
                Size = "MediumUrl";
            }
            else if (!string.IsNullOrEmpty(p.SmallUrl))
            {
                BiggestVersionUrl1 = p.SmallUrl;
                Size = "OriginalUrl";
            }

            Org[0] = BiggestVersionUrl1;
            Org[1] = Size;
            return Org;
        }

        public string user;
        private void button1_Click(object sender, EventArgs e)
        {
            user = txtUser.Text.Trim();
         //   if (!backgroundWorker2.IsBusy) backgroundWorker2.RunWorkerAsync();

            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.UserId = "149565322@N04";
            searchOptions.PerPage = 10;
            searchOptions.Extras = PhotoSearchExtras.All;
            searchOptions.Extras = PhotoSearchExtras.Views;
            PhotoCollection microsoftPhotos = F.PhotosSearch(searchOptions);

            List<Photo> allPhotos = new List<Photo>();
            allPhotos.AddRange(microsoftPhotos);
            foreach (var photo in allPhotos)
            {
                using (WebClient Client = new WebClient())
                {

                    // try
                    {
                        //    this.Invoke(new MethodInvoker(delegate
                        //        {
                        //   Client.DownloadFile(BiggestVersionUrl(photo), Application.StartupPath + "\\" + txtFolder.Text + "\\" + photo.Title + ".jpg");
                        //}));
                    }
                    //  catch { }

                }

            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.Username = "德丸．tokumaru";
            searchOptions.PerPage = 10;
            searchOptions.Extras = PhotoSearchExtras.All;
            searchOptions.Extras = PhotoSearchExtras.Views;
            PhotoCollection microsoftPhotos = F.PhotosSearch(searchOptions);

            List<Photo> allPhotos = new List<Photo>();
            allPhotos.AddRange(microsoftPhotos);
            foreach (var photo in allPhotos)
            {
                using (WebClient Client = new WebClient())
                {

                    // try
                    {
                        //    this.Invoke(new MethodInvoker(delegate
                        //        {
                     //   Client.DownloadFile(BiggestVersionUrl(photo), Application.StartupPath + "\\" + txtFolder.Text + "\\" + photo.Title + ".jpg");
                        //}));
                    }
                    //  catch { }

                }

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // MessageBox.Show("DONE");
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.UserId = "namgermans";
            searchOptions.Extras = PhotoSearchExtras.All;
            PhotoCollection photos = F.PhotosSearch(searchOptions);
            List<Photo> allPhotos = new List<Photo>();
            allPhotos.AddRange(photos);
            foreach (var photo in allPhotos)
            {
                using (WebClient Client = new WebClient())
                {

                 //   Client.DownloadFile(BiggestVersionUrl(photo), Application.StartupPath + "\\" + txtFolder.Text + "\\" + photo.Title + ".jpg");

                }

            }

            MessageBox.Show("DONE");
        }

        public int total = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            total = int.Parse(txtSize.Text);
            backgroundWorker3.RunWorkerAsync();
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.Username = "ruanjin";
            searchOptions.Extras = PhotoSearchExtras.All;
            searchOptions.SafeSearch = SafetyLevel.None;
            searchOptions.PerPage = 100;
            int i = 0;
            for (int j = 0; j < total; j++)
            {
                searchOptions.Page = j;
                PhotoCollection photos = F.PhotosSearch(searchOptions);
                List<Photo> allPhotos = new List<Photo>();
                allPhotos.AddRange(photos);
                foreach (var photo in allPhotos)
                {

                    string[] Type = BiggestVersionUrl(photo);
                    this.Invoke(new MethodInvoker(delegate
                    {
                        dataGridView1.Rows.Add(Type[0]);
                        dataGridView1[1, i].Value = Type[1];
                    }));
                    i++;

                }
                toolStripStatusLabel1.Text = i.ToString();
            }
            MessageBox.Show("DONE");
        }
    }
}
