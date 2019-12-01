using Panuon.UI.Silver;
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
using AHpxLauncher.Views;

namespace AHpxLauncher.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : WindowX
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void ComBoxItem_OnlineType_Selected(object sender, RoutedEventArgs e)
        {
            TextBlock OnlineAccount = new TextBlock()
            {
                Text = "正版邮箱"
            };
            
            TextBlock OnlinePassword = new TextBlock()
            {
                Text = "正版密码"
            };


            Canvas.SetLeft(OnlineAccount,10);
            Canvas.SetTop(OnlineAccount, 40);

            Canvas.SetLeft(OnlinePassword, 10);
            Canvas.SetTop(OnlinePassword, 80);

            AnimationHelper.SetFadeIn(OnlineAccount, true);
            AnimationHelper.SetFadeIn(OnlinePassword, true);
            AccountGrid.Children.Add(OnlineAccount);
            AccountGrid.Children.Add(OnlinePassword);
        }

        private void ComBoxItem_OfflineType_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComBoxItem_Passport_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
