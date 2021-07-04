using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Net;
using CefSharp;
using System.Net.Http;
using System.Windows.Threading;

namespace RenovateBrowser
{
    public partial class Browser : Window
    {
        int tabcount = 1;
        bool issignedin = false;

        public Browser()
        {
            InitializeComponent();
            checkifsignedin();
            browser.LifeSpanHandler = new MyCustomLifeSpanHandler(); // Handle new tabs
            browser.LoadingStateChanged += ChromeView_NavStateChanged; // Handle loading events
            browser.DisplayHandler = new DisplayHandler(); // Handle new tabs
            browser2.LifeSpanHandler = new MyCustomLifeSpanHandler(); // Handle new tabs
            browser2.LoadingStateChanged += ChromeView_NavStateChanged2; // Handle loading events
            browser3.LifeSpanHandler = new MyCustomLifeSpanHandler(); // Handle new tabs
            browser3.LoadingStateChanged += ChromeView_NavStateChanged3; // Handle loading events
            browser4.LifeSpanHandler = new MyCustomLifeSpanHandler(); // Handle new tabs
            browser4.LoadingStateChanged += ChromeView_NavStateChanged4; // Handle loading events
            browser5.LifeSpanHandler = new MyCustomLifeSpanHandler(); // Handle new tabs
            browser5.LoadingStateChanged += ChromeView_NavStateChanged5; // Handle loading events
        }

        void checkifsignedin ()
        {           
            if (issignedin)
            {
                // User has already signed in, no need to show the box

            }
            else
            {
                // User has not yet signed in. Show the box.
                signintext.Visibility = Visibility.Visible;
                StartCloseTimer();
            }
        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(8d);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            signintext.Visibility = Visibility.Hidden;
        }

        //Event listener
        private void ChromeView_NavStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (e.IsLoading)
                {
                    title.Content = "Loading..";
                }
                else
                {
                    title.Content = browser.Title;
                }
            });
        }

        private void ChromeView_NavStateChanged2(object sender, LoadingStateChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (e.IsLoading)
                {
                    title2.Content = "Loading..";
                }
                else
                {
                    title2.Content = browser2.Title;
                }
            });
        }

        private void ChromeView_NavStateChanged3(object sender, LoadingStateChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (e.IsLoading)
                {
                    title3.Content = "Loading..";
                }
                else
                {
                    title3.Content = browser3.Title;
                }
            });
        }

        private void ChromeView_NavStateChanged4(object sender, LoadingStateChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (e.IsLoading)
                {
                    title4.Content = "Loading..";
                }
                else
                {
                    title4.Content = browser4.Title;
                }
            });
        }

        private void ChromeView_NavStateChanged5(object sender, LoadingStateChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (e.IsLoading)
                {
                    title5.Content = "Loading..";
                }
                else
                {
                    title5.Content = browser5.Title;
                }
            });
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {

            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void maxbutton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }      
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // add a 2nd tab
            tabcount = tabcount + 1;
            closetab.Visibility = Visibility.Visible;

            // tab specifics
            urlbox.Text = browser2.Address;
            newtab2.Visibility = Visibility.Visible;
            browser2.Visibility = Visibility.Visible;
            tab2_btn.Visibility = Visibility.Visible;
            tab2_btn.Visibility = Visibility.Visible;
            tab2.Opacity = 0.8;
            tab2.Visibility = Visibility.Visible;
            title2.Visibility = Visibility.Visible;
            title2.Opacity = 0.8;
            title2.Content = browser2.Title;
            favicon2.Visibility = Visibility.Visible;
            favicon2.Opacity = 0.8;

            // Disable the new tab icon
            newtab.Visibility = Visibility.Hidden;

            showtab2();
        }

        private void urlbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (browser.Visibility == Visibility.Visible)
                {
                    browser.Address = urlbox.Text;
                }
                if (browser2.Visibility == Visibility.Visible)
                {
                    browser2.Address = urlbox.Text;
                }
                if (browser3.Visibility == Visibility.Visible)
                {
                    browser3.Address = urlbox.Text;
                }
                if (browser4.Visibility == Visibility.Visible)
                {
                    browser4.Address = urlbox.Text;
                }
                if (browser5.Visibility == Visibility.Visible)
                {
                    browser5.Address = urlbox.Text;
                }
            }
        }

        private void browser_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Address changed        
        }

        private void browser_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Title changed
            title.Content = browser.Title;
            urlbox.Text = browser.Address;
            string url = browser.Address; // Replace with your URL
            try
            {
                favicon.Source = new BitmapImage(new Uri(url + "favicon.ico"));

                if (favicon.Source == null)
                {
                    favicon.Source = new BitmapImage(new Uri(url + "favicon.png"));
                    if (favicon.Source == null)
                    {
                        favicon.Source = new BitmapImage(new Uri("https://renovatesoftware.com:140/images/logo.png"));
                    }
                }
            }
            catch
            {

            }
            
        }

        private void account_Click(object sender, RoutedEventArgs e)
        {
            // Acount
            account_win.Visibility = Visibility.Visible;
        }

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            // Nav forwards
            if (browser.Visibility == Visibility.Visible)
            {
                browser.Forward();
            }
            if (browser2.Visibility == Visibility.Visible)
            {
                browser2.Forward();
            }
            if (browser3.Visibility == Visibility.Visible)
            {
                browser3.Forward();
            }
            if (browser4.Visibility == Visibility.Visible)
            {
                browser4.Forward();
            }
            if (browser5.Visibility == Visibility.Visible)
            {
                browser5.Forward();
            }
        }

        private void backward_Click(object sender, RoutedEventArgs e)
        {
            // Nav back
            
            if (browser.Visibility == Visibility.Visible)
            {
                browser.Back();
            }
            if (browser2.Visibility == Visibility.Visible)
            {
                browser2.Back();
            }
            if (browser3.Visibility == Visibility.Visible)
            {
                browser3.Back();
            }
            if (browser4.Visibility == Visibility.Visible)
            {
                browser4.Back();
            }
            if (browser5.Visibility == Visibility.Visible)
            {
                browser5.Back();
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            // Refresh
            if (browser.Visibility == Visibility.Visible)
            {
                browser.Reload();
            }
            if (browser2.Visibility == Visibility.Visible)
            {
                browser2.Reload();
            }
            if (browser3.Visibility == Visibility.Visible)
            {
                browser3.Reload();
            }
            if (browser4.Visibility == Visibility.Visible)
            {
                browser4.Reload();
            }
            if (browser5.Visibility == Visibility.Visible)
            {
                browser5.Reload();
            }
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            // Home

            if (browser.Visibility == Visibility.Visible)
            {
                browser.Address = "https://landing-page-sb.netlify.app/";
            }
            if (browser2.Visibility == Visibility.Visible)
            {
                browser2.Address = "https://landing-page-sb.netlify.app/";
            }

        }

        private void sec_bg_MouseLeave(object sender, MouseEventArgs e)
        {
            account_win.Visibility = Visibility.Hidden;
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            // Settings
            settingswin.Visibility = Visibility.Visible;
        }

        private void tab1_btn_Click(object sender, RoutedEventArgs e)
        {
            // Show tab 1
            showtab1();
        }

        void showtab1 ()
        {
            // Show tab 1
            tab5.Opacity = 0.3;
            title5.Opacity = 0.3;
            favicon5.Opacity = 0.3;
            browser5.Visibility = Visibility.Hidden;

            tab4.Opacity = 0.3;
            title4.Opacity = 0.3;
            favicon4.Opacity = 0.3;
            browser4.Visibility = Visibility.Hidden;

            tab3.Opacity = 0.3;
            title3.Opacity = 0.3;
            favicon3.Opacity = 0.3;
            browser3.Visibility = Visibility.Hidden;

            tab2.Opacity = 0.3;
            title2.Opacity = 0.3;
            favicon2.Opacity = 0.3;
            browser2.Visibility = Visibility.Hidden;

            tab1_bg.Opacity = 0.8;
            title.Opacity = 0.8;
            favicon.Opacity = 0.8;
            browser.Visibility = Visibility.Visible;

            urlbox.Text = browser4.Address;
        }

        void showtab2()
        {
            tab5.Opacity = 0.3;
            title5.Opacity = 0.3;
            favicon5.Opacity = 0.3;
            browser5.Visibility = Visibility.Hidden;

            tab4.Opacity = 0.3;
            title4.Opacity = 0.3;
            favicon4.Opacity = 0.3;
            browser4.Visibility = Visibility.Hidden;

            tab3.Opacity = 0.3;
            title3.Opacity = 0.3;
            favicon3.Opacity = 0.3;
            browser3.Visibility = Visibility.Hidden;

            tab2.Opacity = 0.8;
            title2.Opacity = 0.8;
            favicon2.Opacity = 0.8;
            browser2.Visibility = Visibility.Visible;

            tab1_bg.Opacity = 0.3;
            title.Opacity = 0.3;
            favicon.Opacity = 0.3;
            browser.Visibility = Visibility.Hidden;

            urlbox.Text = browser4.Address;
        }

        void showtab3()
        {
            tab5.Opacity = 0.3;
            title5.Opacity = 0.3;
            favicon5.Opacity = 0.3;
            browser5.Visibility = Visibility.Hidden;

            tab4.Opacity = 0.3;
            title4.Opacity = 0.3;
            favicon4.Opacity = 0.3;
            browser4.Visibility = Visibility.Hidden;

            tab3.Opacity = 0.8;
            title3.Opacity = 0.8;
            favicon3.Opacity = 0.8;
            browser3.Visibility = Visibility.Visible;

            tab2.Opacity = 0.3;
            title2.Opacity = 0.3;
            favicon2.Opacity = 0.3;
            browser2.Visibility = Visibility.Hidden;

            tab1_bg.Opacity = 0.3;
            title.Opacity = 0.3;
            favicon.Opacity = 0.3;
            browser.Visibility = Visibility.Hidden;

            urlbox.Text = browser4.Address;
        }

        void showtab4 ()
        {
            tab5.Opacity = 0.3;
            title5.Opacity = 0.3;
            favicon5.Opacity = 0.3;
            browser5.Visibility = Visibility.Hidden;

            tab4.Opacity = 0.8;
            title4.Opacity = 0.8;
            favicon4.Opacity = 0.8;
            browser4.Visibility = Visibility.Visible;

            tab3.Opacity = 0.3;
            title3.Opacity = 0.3;
            favicon3.Opacity = 0.3;
            browser3.Visibility = Visibility.Hidden;

            tab2.Opacity = 0.3;
            title2.Opacity = 0.3;
            favicon2.Opacity = 0.3;
            browser2.Visibility = Visibility.Hidden;

            tab1_bg.Opacity = 0.3;
            title.Opacity = 0.3;
            favicon.Opacity = 0.3;
            browser.Visibility = Visibility.Hidden;

            urlbox.Text = browser4.Address;
        }

        void showtab5()
        {
            tab5.Opacity = 0.8;
            title5.Opacity = 0.8;
            favicon5.Opacity = 0.8;
            browser5.Visibility = Visibility.Visible;

            tab4.Opacity = 0.3;
            title4.Opacity = 0.3;
            favicon4.Opacity = 0.3;
            browser4.Visibility = Visibility.Hidden;

            tab3.Opacity = 0.3;
            title3.Opacity = 0.3;
            favicon3.Opacity = 0.3;
            browser3.Visibility = Visibility.Hidden;

            tab2.Opacity = 0.3;
            title2.Opacity = 0.3;
            favicon2.Opacity = 0.3;
            browser2.Visibility = Visibility.Hidden;

            tab1_bg.Opacity = 0.3;
            title.Opacity = 0.3;
            favicon.Opacity = 0.3;
            browser.Visibility = Visibility.Hidden;

            urlbox.Text = browser4.Address;
        }

        private void tab2_btn_Click(object sender, RoutedEventArgs e)
        {
            // Show tab 2
            showtab2();
        }

        private void browser2_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Address changed
        }

        private void browser2_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // title changed for browser 2
            // Title changed
            title2.Content = browser2.Title;
            urlbox.Text = browser2.Address;
            string url = browser2.Address; // Replace with your URL
            try
            {
                favicon2.Source = new BitmapImage(new Uri(url + "favicon.ico"));
                if (favicon2.Source == null)
                {
                    favicon2.Source = new BitmapImage(new Uri(url + "favicon.png"));
                }
            }
            catch
            {

            }
        }

        private void closebutton_MouseEnter(object sender, MouseEventArgs e)
        {
            // Close button mouse enter
            closebutton.Foreground = Brushes.Black;
            closebutton.Background = Brushes.Red;
        }

        private void closebutton_MouseLeave(object sender, MouseEventArgs e)
        {
            // Close button mouse leave
            closebutton.Foreground = Brushes.Red;
            closebutton.Background = Brushes.Transparent;
        }

        private void maximbuttonfk_MouseEnter(object sender, MouseEventArgs e)
        {
            // Mouse enter maximise
            maxbutton.Foreground = Brushes.Black;
            maxbutton.Background = Brushes.LightBlue;
        }

        private void maximbuttonfk_MouseLeave(object sender, MouseEventArgs e)
        {
            // Mouse leave maximise
            maxbutton.Foreground = Brushes.LightBlue;
            maxbutton.Background = Brushes.Transparent;
        }

        private void minbuttonfk_MouseEnter(object sender, MouseEventArgs e)
        {
            // Mouse enter minimize
            minbutton.Foreground = Brushes.Black;
            minbutton.Background = Brushes.LightGray;
        }

        private void minbuttonfk_MouseLeave(object sender, MouseEventArgs e)
        {
            // Mouse leave minimize
            minbutton.Foreground = Brushes.Gray;
            minbutton.Background = Brushes.Transparent;
        }

        private void browser_LoadError(object sender, LoadErrorEventArgs e)
        {
            // Load error
            this.Dispatcher.Invoke(() =>
            {
                Console.WriteLine(e.ErrorText);
                if (e.ErrorText.ToString() == "ERR_NAME_NOT_RESOLVED")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>ERR_NAME_NOT_RESOLVED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NAME_NOT_RESOLVED</h1><p>The URL that you entered could not be resolved.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "Aw, Snap!")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>Aw, Snap!</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>Aw, Snap!</h1><p>Something went wrong while displaying this webpage.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_INTERNET_DISCONNECTED")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>ERR_INTERNET_DISCONNECTED</title><head></head><body><div style=\"padding: 2%;\"><h1>ERR_INTERNET_DISCONNECTED</h1><p>Please connect to the internet if you wish to continue browsing.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_SSL_PROTOCOL_ERROR")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>ERR_SSL_PROTOCOL_ERROR</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_SSL_PROTOCOL_ERROR</h1><p>Something isn't right about this server's SSL certificate, so we stopped you here.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_TIMED_OUT")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>ERR_CONNECTION_TIMED_OUT</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_TIMED_OUT</h1><p>The connection to the server timed out.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_RESET")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>ERR_CONNECTION_RESET</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_RESET</h1><p>Your connection was reset when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_NETWORK_CHANGED")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>ERR_NETWORK_CHANGED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NETWORK_CHANGED</h1><p>Your connection changed when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_REFUSED")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>ERR_CONNECTION_REFUSED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_REFUSED</h1><p>The server refused your connection.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_EMPTY_RESPONSE")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>ERR_EMPTY_RESPONSE</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_EMPTY_RESPONSE</h1><p>The server didn't send anything back.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CACHE_MISS")
                {
                    favicon.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser.LoadHtml("<html><title>ERR_CACHE_MISS</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CACHE_MISS</h1><p>The server didn't get the cache from your request. Please try again.</p></div></body></html>");
                }
                else
                {
                    title.Content = e.ErrorText.ToString();
                }
                
            });
            
        }

        private void closebutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private static readonly HttpClient client = new HttpClient();

        async private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Login
            string username = usernamebox.Text;
            string password = passwordbox.Password;
            if (username == "")
            {
                responsetext.Text = "Please enter\na username.";
            }
            if (password == "")
            {
                responsetext.Text = "Please enter\na password.";
            }
            if (username == "" && password == "")
            {
                responsetext.Text = "Please enter a\nusername and password.";
            }
            else
            {
                usernamebox.Text = "";
                passwordbox.Password = "";

                var values = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://renovatesoftware.com/API/2fa_authenticate/", content);
                var responseString = await response.Content.ReadAsStringAsync();

                responsetext.Text = responseString.ToString();

                if (responseString.StartsWith("Login details do not"))
                {
                    responsetext.Text = "Your login details\nwere not correct.\nPlease try again.";
                }
                else
                {
                    responsetext.Text = "Username and\npassword correct.";
                    // Show the data window

                }
            }
        }

        private void browser_Unloaded(object sender, RoutedEventArgs e)
        {
            // not yet loaded
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            settingswin.Visibility = Visibility.Hidden;
        }

        private void browser2_LoadError(object sender, LoadErrorEventArgs e)
        {
            // Load error
            this.Dispatcher.Invoke(() =>
            {
                Console.WriteLine(e.ErrorText);
                if (e.ErrorText.ToString() == "ERR_NAME_NOT_RESOLVED")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>ERR_NAME_NOT_RESOLVED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NAME_NOT_RESOLVED</h1><p>The URL that you entered could not be resolved.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "Aw, Snap!")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>Aw, Snap!</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>Aw, Snap!</h1><p>Something went wrong while displaying this webpage.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_INTERNET_DISCONNECTED")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>ERR_INTERNET_DISCONNECTED</title><head></head><body><div style=\"padding: 2%;\"><h1>ERR_INTERNET_DISCONNECTED</h1><p>Please connect to the internet if you wish to continue browsing.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_SSL_PROTOCOL_ERROR")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>ERR_SSL_PROTOCOL_ERROR</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_SSL_PROTOCOL_ERROR</h1><p>Something isn't right about this server's SSL certificate, so we stopped you here.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_TIMED_OUT")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>ERR_CONNECTION_TIMED_OUT</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_TIMED_OUT</h1><p>The connection to the server timed out.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_RESET")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>ERR_CONNECTION_RESET</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_RESET</h1><p>Your connection was reset when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_NETWORK_CHANGED")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>ERR_NETWORK_CHANGED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NETWORK_CHANGED</h1><p>Your connection changed when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_REFUSED")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>ERR_CONNECTION_REFUSED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_REFUSED</h1><p>The server refused your connection.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_EMPTY_RESPONSE")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>ERR_EMPTY_RESPONSE</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_EMPTY_RESPONSE</h1><p>The server didn't send anything back.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CACHE_MISS")
                {
                    favicon2.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser2.LoadHtml("<html><title>ERR_CACHE_MISS</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CACHE_MISS</h1><p>The server didn't get the cache from your request. Please try again.</p></div></body></html>");
                }
                else
                {
                    title2.Content = e.ErrorText.ToString();
                }

            });
        }

        private void closetab_Click(object sender, RoutedEventArgs e)
        {
            // Close current tab
            if (browser.Visibility == Visibility.Visible)
            {
                // Close tab 1 and set it to the contents of tab 2
                tab2.Visibility = Visibility.Hidden;
                browser.Address = browser2.Address;
                browser2.Visibility = Visibility.Hidden;
                title2.Visibility = Visibility.Hidden;
                favicon2.Visibility = Visibility.Hidden;
                newtab2.Visibility = Visibility.Hidden;
                newtab.Visibility = Visibility.Visible;
                tabcount = tabcount - 1;
                closetab.Visibility = Visibility.Hidden;
            }
            else if (browser2.Visibility == Visibility.Visible)
            {
                // Close tab 2 and set it to the contents of tab 3
                showtab1();
                tab2.Visibility = Visibility.Hidden;
                title2.Visibility = Visibility.Hidden;
                favicon2.Visibility = Visibility.Hidden;
                newtab2.Visibility = Visibility.Hidden;
                newtab.Visibility = Visibility.Visible;

                tabcount = tabcount - 1;
                closetab.Visibility = Visibility.Hidden;
            }
            else if (browser3.Visibility == Visibility.Visible)
            {
                // Close tab 3 and set it to the contents of tab 2
                showtab2();
                tab3.Visibility = Visibility.Hidden;
                title3.Visibility = Visibility.Hidden;
                favicon3.Visibility = Visibility.Hidden;
                newtab3.Visibility = Visibility.Hidden;
                newtab2.Visibility = Visibility.Visible;

                tabcount = tabcount - 1;
            }
            else if (browser4.Visibility == Visibility.Visible)
            {
                // Close tab 4 and set it to the contents of tab 3
                showtab3();
                tab4.Visibility = Visibility.Hidden;
                title4.Visibility = Visibility.Hidden;
                favicon4.Visibility = Visibility.Hidden;
                newtab4.Visibility = Visibility.Hidden;
                newtab3.Visibility = Visibility.Visible;

                tabcount = tabcount - 1;
            }
            else if (browser4.Visibility == Visibility.Visible)
            {
                // Close tab 5 and set it to the contents of tab 4
                showtab4();
                tab5.Visibility = Visibility.Hidden;
                title5.Visibility = Visibility.Hidden;
                favicon5.Visibility = Visibility.Hidden;
                newtab5.Visibility = Visibility.Hidden;
                newtab4.Visibility = Visibility.Visible;

                tabcount = tabcount - 1;
            }
        }

        private void newtab3_Click(object sender, RoutedEventArgs e)
        {
            // add a 4th tab
            tabcount = tabcount + 1;
            closetab.Visibility = Visibility.Visible;

            // tab specifics
            urlbox.Text = browser4.Address;
            newtab4.Visibility = Visibility.Visible;
            browser4.Visibility = Visibility.Visible;
            tab4_btn.Visibility = Visibility.Visible;
            tab4_btn.Visibility = Visibility.Visible;
            tab4.Opacity = 0.8;
            tab4.Visibility = Visibility.Visible;
            title4.Visibility = Visibility.Visible;
            title4.Opacity = 0.8;
            title4.Content = browser4.Title;
            favicon4.Visibility = Visibility.Visible;
            favicon4.Opacity = 0.8;

            // Disable the new tab icon
            newtab3.Visibility = Visibility.Hidden;

            showtab4();
        }

        private void newtab2_Click(object sender, RoutedEventArgs e)
        {
            // add a 3rd tab
            tabcount = tabcount + 1;
            closetab.Visibility = Visibility.Visible;

            // tab specifics
            urlbox.Text = browser3.Address;
            newtab3.Visibility = Visibility.Visible;
            browser3.Visibility = Visibility.Visible;
            tab3_btn.Visibility = Visibility.Visible;
            tab3_btn.Visibility = Visibility.Visible;
            tab3.Opacity = 0.8;
            tab3.Visibility = Visibility.Visible;
            title3.Visibility = Visibility.Visible;
            title3.Opacity = 0.8;
            title3.Content = browser3.Title;
            favicon3.Visibility = Visibility.Visible;
            favicon3.Opacity = 0.8;

            // Disable the new tab icon
            newtab2.Visibility = Visibility.Hidden;

            showtab3();
        }

        private void refresh_MouseEnter(object sender, MouseEventArgs e)
        {
            refresh.Foreground = Brushes.LightBlue;
        }

        private void refresh_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            refresh.Foreground = (Brush)bc.ConvertFrom("#FFB0B0B0");
        }

        private void backward_MouseEnter(object sender, MouseEventArgs e)
        {
            backward.Foreground = Brushes.Red;
        }

        private void backward_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            backward.Foreground = (Brush)bc.ConvertFrom("#FFB0B0B0");
        }

        private void forward_MouseEnter(object sender, MouseEventArgs e)
        {
            forward.Foreground = Brushes.Green;
        }

        private void forward_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            forward.Foreground = (Brush)bc.ConvertFrom("#FFB0B0B0");
        }

        private void account_MouseEnter(object sender, MouseEventArgs e)
        {
            account.Foreground = Brushes.Orange;
        }

        private void account_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            account.Foreground = (Brush)bc.ConvertFrom("#FFB0B0B0");
        }

        private void home_MouseEnter(object sender, MouseEventArgs e)
        {
            home.Foreground = Brushes.Crimson;
        }

        private void home_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            home.Foreground = (Brush)bc.ConvertFrom("#FFB0B0B0");
        }

        private void settings_MouseEnter(object sender, MouseEventArgs e)
        {
            settings.Foreground = Brushes.Azure;
        }

        private void settings_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            settings.Foreground = (Brush)bc.ConvertFrom("#FFB0B0B0");
        }

        private void newtab_MouseEnter(object sender, MouseEventArgs e)
        {
            newtab.Foreground = Brushes.White;
        }

        private void newtab_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            newtab.Foreground = (Brush)bc.ConvertFrom("#FFB0B0B0");
        }

        private void closetab_MouseEnter(object sender, MouseEventArgs e)
        {
            closetab.Foreground = Brushes.Red;
        }

        private void closetab_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            closetab.Foreground = (Brush)bc.ConvertFrom("#FFB0B0B0");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                if (browser.Visibility == Visibility.Visible)
                {
                    browser.Reload();
                }
                if (browser2.Visibility == Visibility.Visible)
                {
                    browser2.Reload();
                }
                if (browser3.Visibility == Visibility.Visible)
                {
                    browser3.Reload();
                }
                if (browser4.Visibility == Visibility.Visible)
                {
                    browser4.Reload();
                }
                if (browser5.Visibility == Visibility.Visible)
                {
                    browser5.Reload();
                }
            }
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void tab3_btn_Click(object sender, RoutedEventArgs e)
        {
            showtab3();
        }

        private void browser3_LoadError(object sender, LoadErrorEventArgs e)
        {
            // Load error
            this.Dispatcher.Invoke(() =>
            {
                Console.WriteLine(e.ErrorText);
                if (e.ErrorText.ToString() == "ERR_NAME_NOT_RESOLVED")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>ERR_NAME_NOT_RESOLVED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NAME_NOT_RESOLVED</h1><p>The URL that you entered could not be resolved.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "Aw, Snap!")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>Aw, Snap!</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>Aw, Snap!</h1><p>Something went wrong while displaying this webpage.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_INTERNET_DISCONNECTED")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>ERR_INTERNET_DISCONNECTED</title><head></head><body><div style=\"padding: 2%;\"><h1>ERR_INTERNET_DISCONNECTED</h1><p>Please connect to the internet if you wish to continue browsing.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_SSL_PROTOCOL_ERROR")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>ERR_SSL_PROTOCOL_ERROR</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_SSL_PROTOCOL_ERROR</h1><p>Something isn't right about this server's SSL certificate, so we stopped you here.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_TIMED_OUT")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>ERR_CONNECTION_TIMED_OUT</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_TIMED_OUT</h1><p>The connection to the server timed out.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_RESET")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>ERR_CONNECTION_RESET</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_RESET</h1><p>Your connection was reset when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_NETWORK_CHANGED")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>ERR_NETWORK_CHANGED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NETWORK_CHANGED</h1><p>Your connection changed when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_REFUSED")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>ERR_CONNECTION_REFUSED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_REFUSED</h1><p>The server refused your connection.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_EMPTY_RESPONSE")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>ERR_EMPTY_RESPONSE</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_EMPTY_RESPONSE</h1><p>The server didn't send anything back.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CACHE_MISS")
                {
                    favicon3.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser3.LoadHtml("<html><title>ERR_CACHE_MISS</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CACHE_MISS</h1><p>The server didn't get the cache from your request. Please try again.</p></div></body></html>");
                }
                else
                {
                    title3.Content = e.ErrorText.ToString();
                }

            });
        }

        private void browser3_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Title changed
            title3.Content = browser3.Title;
            urlbox.Text = browser3.Address;
            string url = browser3.Address; // Replace with your URL
            try
            {
                favicon3.Source = new BitmapImage(new Uri(url + "favicon.ico"));

                if (favicon3.Source == null)
                {
                    favicon3.Source = new BitmapImage(new Uri(url + "favicon.png"));
                    if (favicon3.Source == null)
                    {
                        favicon3.Source = new BitmapImage(new Uri("https://renovatesoftware.com:140/images/logo.png"));
                    }
                }
            }
            catch
            {

            }
        }

        private void tab4_btn_Click(object sender, RoutedEventArgs e)
        {
            // Show tab 4
            showtab4();
        }

        private void browser4_LoadError(object sender, LoadErrorEventArgs e)
        {
            // Load error
            this.Dispatcher.Invoke(() =>
            {
                Console.WriteLine(e.ErrorText);
                if (e.ErrorText.ToString() == "ERR_NAME_NOT_RESOLVED")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>ERR_NAME_NOT_RESOLVED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NAME_NOT_RESOLVED</h1><p>The URL that you entered could not be resolved.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "Aw, Snap!")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>Aw, Snap!</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>Aw, Snap!</h1><p>Something went wrong while displaying this webpage.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_INTERNET_DISCONNECTED")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>ERR_INTERNET_DISCONNECTED</title><head></head><body><div style=\"padding: 2%;\"><h1>ERR_INTERNET_DISCONNECTED</h1><p>Please connect to the internet if you wish to continue browsing.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_SSL_PROTOCOL_ERROR")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>ERR_SSL_PROTOCOL_ERROR</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_SSL_PROTOCOL_ERROR</h1><p>Something isn't right about this server's SSL certificate, so we stopped you here.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_TIMED_OUT")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>ERR_CONNECTION_TIMED_OUT</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_TIMED_OUT</h1><p>The connection to the server timed out.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_RESET")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>ERR_CONNECTION_RESET</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_RESET</h1><p>Your connection was reset when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_NETWORK_CHANGED")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>ERR_NETWORK_CHANGED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NETWORK_CHANGED</h1><p>Your connection changed when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_REFUSED")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>ERR_CONNECTION_REFUSED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_REFUSED</h1><p>The server refused your connection.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_EMPTY_RESPONSE")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>ERR_EMPTY_RESPONSE</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_EMPTY_RESPONSE</h1><p>The server didn't send anything back.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CACHE_MISS")
                {
                    favicon4.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser4.LoadHtml("<html><title>ERR_CACHE_MISS</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CACHE_MISS</h1><p>The server didn't get the cache from your request. Please try again.</p></div></body></html>");
                }
                else
                {
                    title4.Content = e.ErrorText.ToString();
                }

            });
        }

        private void browser4_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Title changed
            title4.Content = browser4.Title;
            urlbox.Text = browser4.Address;
            string url = browser4.Address; // Replace with your URL
            try
            {
                favicon4.Source = new BitmapImage(new Uri(url + "favicon.ico"));

                if (favicon4.Source == null)
                {
                    favicon4.Source = new BitmapImage(new Uri(url + "favicon.png"));
                    if (favicon4.Source == null)
                    {
                        favicon4.Source = new BitmapImage(new Uri("https://renovatesoftware.com:140/images/logo.png"));
                    }
                }
            }
            catch
            {

            }
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            sidebar.Opacity = 0.8;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            sidebar.Opacity = 0;
        }

        private void tab5_btn_Click(object sender, RoutedEventArgs e)
        {
            // show tab 5
            showtab5();
        }

        private void newtab5_Click(object sender, RoutedEventArgs e)
        {
            // create tab 6
        }

        private void newtab4_Click(object sender, RoutedEventArgs e)
        {
            // add a 5th tab
            tabcount = tabcount + 1;
            closetab.Visibility = Visibility.Visible;

            // tab specifics
            urlbox.Text = browser5.Address;
            newtab5.Visibility = Visibility.Visible;
            browser5.Visibility = Visibility.Visible;
            tab5_btn.Visibility = Visibility.Visible;
            tab5_btn.Visibility = Visibility.Visible;
            tab5.Opacity = 0.8;
            tab5.Visibility = Visibility.Visible;
            title5.Visibility = Visibility.Visible;
            title5.Opacity = 0.8;
            title5.Content = browser5.Title;
            favicon5.Visibility = Visibility.Visible;
            favicon5.Opacity = 0.8;

            // Disable the new tab icon
            newtab4.Visibility = Visibility.Hidden;

            showtab5();
        }

        private void browser5_LoadError(object sender, LoadErrorEventArgs e)
        {
            // Load error
            this.Dispatcher.Invoke(() =>
            {
                Console.WriteLine(e.ErrorText);
                if (e.ErrorText.ToString() == "ERR_NAME_NOT_RESOLVED")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>ERR_NAME_NOT_RESOLVED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NAME_NOT_RESOLVED</h1><p>The URL that you entered could not be resolved.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "Aw, Snap!")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>Aw, Snap!</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>Aw, Snap!</h1><p>Something went wrong while displaying this webpage.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_INTERNET_DISCONNECTED")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>ERR_INTERNET_DISCONNECTED</title><head></head><body><div style=\"padding: 2%;\"><h1>ERR_INTERNET_DISCONNECTED</h1><p>Please connect to the internet if you wish to continue browsing.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_SSL_PROTOCOL_ERROR")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>ERR_SSL_PROTOCOL_ERROR</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_SSL_PROTOCOL_ERROR</h1><p>Something isn't right about this server's SSL certificate, so we stopped you here.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_TIMED_OUT")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>ERR_CONNECTION_TIMED_OUT</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_TIMED_OUT</h1><p>The connection to the server timed out.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_RESET")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>ERR_CONNECTION_RESET</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_RESET</h1><p>Your connection was reset when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_NETWORK_CHANGED")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>ERR_NETWORK_CHANGED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_NETWORK_CHANGED</h1><p>Your connection changed when trying to connect to this server.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CONNECTION_REFUSED")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>ERR_CONNECTION_REFUSED</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CONNECTION_REFUSED</h1><p>The server refused your connection.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_EMPTY_RESPONSE")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>ERR_EMPTY_RESPONSE</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_EMPTY_RESPONSE</h1><p>The server didn't send anything back.</p></div></body></html>");
                }
                else if (e.ErrorText.ToString() == "ERR_CACHE_MISS")
                {
                    favicon5.Source = new BitmapImage(new Uri("https://image.flaticon.com/icons/png/512/595/595067.png"));
                    browser5.LoadHtml("<html><title>ERR_CACHE_MISS</title><head><link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.0.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC\" crossorigin=\"anonymous\"></head><body><div style=\"padding: 2%;\"><h1>ERR_CACHE_MISS</h1><p>The server didn't get the cache from your request. Please try again.</p></div></body></html>");
                }
                else
                {
                    title5.Content = e.ErrorText.ToString();
                }

            });
        }

        private void browser5_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Title changed
            title5.Content = browser5.Title;
            urlbox.Text = browser5.Address;
            string url = browser5.Address; // Replace with your URL
            try
            {
                favicon5.Source = new BitmapImage(new Uri(url + "favicon.ico"));

                if (favicon5.Source == null)
                {
                    favicon5.Source = new BitmapImage(new Uri(url + "favicon.png"));
                    if (favicon5.Source == null)
                    {
                        favicon5.Source = new BitmapImage(new Uri("https://renovatesoftware.com:140/images/logo.png"));
                    }
                }
            }
            catch
            {

            }
        }
    }
}
