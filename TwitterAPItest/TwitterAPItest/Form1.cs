using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using Twitterizer;
using Newtonsoft;
using System.Xml;
using MySql.Data.MySqlClient;
using System.IO;

namespace TwitterAPItest
{
    public partial class Form1 : Form
    {
        private OAuthTokenResponse oatr;
        private Uri uri;
        private OAuthTokenResponse oatrat;
        private MySqlConnection myconn;

        private string consumerkey = "1234";  //redacted for public view
        private string consumersecret = "1234";  //redacted

        private OAuthTokens oats;

        private decimal ? lastID = null;
        

        public Form1()
        {
            InitializeComponent();
            //mark my spot in the tweetstream in the database for between sessions
            webBrowser1.Visible = false;
            pinBox.Visible = false;
            pinBtn.Visible = false;
                
            myconn = new MySqlConnection("server=localhost;user=root;database=twitterdb;port=3306");
            myconn.Open();
            
            MySqlCommand checkauth = myconn.CreateCommand();
            checkauth.CommandText = "SELECT AccessToken,AccessTokenSecret FROM OAuthINFO WHERE idOAuthInfo = 1";
            MySqlDataReader sqldata = checkauth.ExecuteReader();
            sqldata.Read();
            if (sqldata[0] is System.DBNull || sqldata[1] is System.DBNull)
            {
                authorize();
            }
            else
            {
                oats = new OAuthTokens()
                {
                    AccessToken = (string) sqldata[0],
                    AccessTokenSecret = (string) sqldata[1],
                    ConsumerKey = consumerkey,
                    ConsumerSecret = consumersecret
                };

            }

            //Need to now be able to persist the last ID to bring that back.

            sqldata.Close();
            
        }        

        private void pinBtn_Click(object sender, EventArgs e)
        {
            
            
            oatrat = OAuthUtility.GetAccessToken(consumerkey, consumersecret, oatr.Token, pinBox.Text);
            oats = new OAuthTokens()
                                    {
                                        AccessToken = oatrat.Token,
                                        AccessTokenSecret = oatrat.TokenSecret,
                                        ConsumerKey = consumerkey,
                                        ConsumerSecret = consumersecret
                                    };

            MySqlCommand updateaccess = myconn.CreateCommand();
            updateaccess.CommandText = "UPDATE OAuthInfo SET AccessToken = @accesstoken, AccessTokenSecret = @accesstokensecret WHERE idOAuthInfo=1";
            updateaccess.Parameters.Add("@accesstoken",MySqlDbType.VarChar,100);
            updateaccess.Parameters.Add("@accesstokensecret",MySqlDbType.VarChar,45);
            updateaccess.Parameters["@accesstoken"].Value = oatrat.Token;
            updateaccess.Parameters["@accesstokensecret"].Value = oatrat.TokenSecret;

            updateaccess.ExecuteNonQuery();

            webBrowser1.Visible = false;
            pinBox.Visible = false;
            pinBtn.Visible = false;
                   
        }
        
        private void authorize()
        {
            webBrowser1.Visible = true;
            pinBox.Visible = true;
            pinBtn.Visible = true;
            pinBox.Text = "Enter the pin here";
            oatr = OAuthUtility.GetRequestToken(consumerkey, consumersecret, "oob");
            uri = OAuthUtility.BuildAuthorizationUri(oatr.Token);
            webBrowser1.Navigate(uri.ToString());
        
        }

        private void getMoreBtn_Click(object sender, EventArgs e)
        {                        
            TimelineOptions to = new TimelineOptions();
            
            if (lastID != null)
            {
                to.SinceStatusId = (decimal)lastID;
            }
            
            TwitterResponse<TwitterStatusCollection> homeTimeline = TwitterTimeline.HomeTimeline(oats,to);

            List<TwitterStatus> tsl = homeTimeline.ResponseObject.ToList<TwitterStatus>();
            if (tsl.Count > 0)
            {   //[0] is the last tweet to come in
                avatarBox.ImageLocation = tsl[0].User.ProfileImageLocation;
                avatarBox.SizeMode = PictureBoxSizeMode.AutoSize;
                usernameBox.Text = tsl[0].User.Name;
                tweetBox.Text = tsl[0].Text;

                if (tsl[0].Retweeted)
                    label1.Visible = true;
                else
                    label1.Visible = false;

                lastID = tsl.First().Id;
                
                tsl.Reverse();               
                
                addToDB(tsl);

                
            }
            
            

            rowsAddedTB.Text = tsl.Count + " Rows Added";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myconn.Close();
            //get ID from last post here
        }

        private void addToDB(List<TwitterStatus> twitterstatuslist)
        {
             MySqlCommand mySqlCommand = myconn.CreateCommand();
            mySqlCommand.CommandText =
                "INSERT INTO Tweets (" +
                " userName, userAvatar, tweetText,datetime" +
                ") VALUES (" +
                " @userName,@userAvatar, @tweetText, @datetime " +
                ")";

            mySqlCommand.Parameters.Add("@userName", MySqlDbType.VarChar, 45);
            mySqlCommand.Parameters.Add("@userAvatar", MySqlDbType.VarChar, 256);
            mySqlCommand.Parameters.Add("@tweetText",MySqlDbType.VarChar,140);
            mySqlCommand.Parameters.Add("@datetime", MySqlDbType.DateTime);

            foreach (TwitterStatus ts in twitterstatuslist)
            {

                mySqlCommand.Parameters["@userName"].Value = ts.User.Name;
                mySqlCommand.Parameters["@userAvatar"].Value = ts.User.ProfileImageLocation;
                mySqlCommand.Parameters["@tweetText"].Value = ts.Text;
                mySqlCommand.Parameters["@datetime"].Value = ts.CreatedDate.ToString();

                mySqlCommand.ExecuteNonQuery();
            }

            rowsAddedTB.Text = twitterstatuslist.Count + " Rows Added";
        }
        
    }
}
