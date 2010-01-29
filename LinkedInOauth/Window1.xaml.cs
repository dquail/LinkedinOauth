using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
namespace Attassa
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private OAuthLinkedIn _oauth = new OAuthLinkedIn();

        public Window1()
        {
            InitializeComponent();
        }
        private void goNavigateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String requestToken = _oauth.getRequestToken();
                txtOutput.Text += "\n" + "Received request token: " + requestToken;

                _oauth.authorizeToken();
                txtOutput.Text += "\n" + "Token was authorized: " + _oauth.Token + " with verifier: " + _oauth.Verifier;
                String accessToken = _oauth.getAccessToken();
                txtOutput.Text += "\n" + "Access token was received: " + _oauth.Token;
            }
            catch (Exception exp)
            {
                txtOutput.Text += "\nException: " + exp.Message; 
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            try{
                txtOutput.Text+="\n"+ _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~", null);
            }
            catch (Exception exp)
            {
                txtOutput.Text += "\nException: " + exp.Message; 
            }

        }
        private void Status_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                xml += "<current-status>" + txtStatusUpdate.Text + "</current-status>";

                string response = _oauth.APIWebRequest("PUT", "http://api.linkedin.com/v1/people/~/current-status", xml);
                if (response == "")
                    txtOutput.Text += "\nYour new status updated.  view linkedin for status.";
            }
            catch (Exception exp)
            {
                txtOutput.Text += "\nException: " + exp.Message; 
            }
                
        }

    }
}
