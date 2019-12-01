using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Panuon.UI.Silver;
using Panuon.UI.Silver.Core;
using static AHpxLauncher.Blur;
using AHpxLauncher.Views;

namespace AHpxLauncher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        public MainWindow()
        {
            InitializeComponent();
        }

        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        private void TreeViewItem_Home_Selected(object sender, RoutedEventArgs e)
        {
            try{TabItem_Home.IsSelected = true;AnimationHelper.SetSlideInFromLeft(Grid_Home, true); TreeViewItem_Home.IsSelected = true; } catch (Exception) { }
        }

        private void TreeViewItem_Account_Selected(object sender, RoutedEventArgs e)
        {
            TabItem_Account.IsSelected = true;
            TreeViewItem_Account.IsSelected = true;
            AnimationHelper.SetSlideInFromLeft(Grid_Account, true);
            for (int i = 1; i <= 20; i++)
            {
                Button bt = new Button()
                {
                    Height = 80,
                    Width = 80,
                    Content = "AHpxChina:" + i.ToString(),
                    Foreground = Colors.Transparent.ToBrush(),
                    Name = "Item" + i.ToString(),
                    Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Res/Steve.jpg")) },
                };
                bt.Click += (Object, RoutedEventArgs) => GetItemIndex(bt.Content.ToString(), new BitmapImage(new Uri("pack://application:,,,/Res/Steve.jpg")),sender,e);
                ButtonHelper.SetButtonStyle(bt, ButtonStyle.Hollow);
                ButtonHelper.SetHoverBrush(bt, Color.FromArgb(100, 255, 255, 255).ToBrush());
                ContextMenu menu = new ContextMenu() ;
                MenuHelper.SetSubmenuCornerRadius(menu, new CornerRadius(5));
                MenuItem menuItem = new MenuItem()
                {
                    Header = "删除此账号",
                    Icon = new Image()
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Res/Icons/icon_delete.png"))
                    },
                };
                menuItem.Click += (Object, RoutedEventArgs) => MenuItemClicked(bt,sender, e);
                menu.Items.Add(menuItem);
                bt.ContextMenu = menu;
                AccountMgrList.Children.Add(bt);
            }
        }

        private void MenuItemClicked(UIElement index,object sender,RoutedEventArgs e)
        {
            if (MessageBoxX.Show("您确定删除这个账号吗?\r\n这个操作是不可以撤销的。", "警告", WindowX_MainView, MessageBoxButton.YesNo, new MessageBoxXConfigurations() { MessageBoxStyle = MessageBoxStyle.Classic, MessageBoxIcon = MessageBoxIcon.Warning, ButtonBrush = "#F1C825".ToColor().ToBrush()})==MessageBoxResult.Yes)
            {
                AccountMgrList.Children.Remove(index);
            }
        }
        private void GetItemIndex(String index,ImageSource src,object sender,RoutedEventArgs e)
        {
            Image_UserImg.ImageSource = src;
            Image_UserImg_back.ImageSource = src;
            TextBlock_UserName.Text = index;
            TreeViewItem_Home_Selected(sender, e);
        }

        private void TreeViewItem_Setting_Selected(object sender, RoutedEventArgs e)
        {
            TabItem_Setting.IsSelected = true;
            TreeViewItem_Setting.IsSelected = true;
            AnimationHelper.SetSlideInFromLeft(Grid_Setting, true);
        }

        private void TreeViewItem_Tools_Selected(object sender, RoutedEventArgs e)
        {
            TabItem_Tools.IsSelected = true;
            TreeViewItem_Tools.IsSelected = true;
            AnimationHelper.SetSlideInFromLeft(Grid_Tools, true);
        }

        private void Button_HomeSignOut_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem_Account_Selected(sender, e);
        }

        private void TabItem_LauncherSetting_Selected(object sender, RoutedEventArgs e)
        {
            try {TabItem_LauncherSetting.IsSelected = true; AnimationHelper.SetSlideInFromLeft(Grid_LauncherSetting, true);}catch (Exception){} 
        }

        private void TabItem_GameSetting_Selected(object sender, RoutedEventArgs e)
        {
            AnimationHelper.SetSlideInFromLeft(Grid_GameSetting, true);
            TabItem_GameSetting.IsSelected = true;
        }

        private void WindowX_MainView_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 30; i++)
            {
                MenuItem menu = new MenuItem()
                {
                    Header = "Item_" + i.ToString()
                };
                menu.Click += (Object, RoutedEventArgs) => ContextMenuItem_VerserSelector_Clicked(menu.Header.ToString(), sender, e);
                ContextMenu_VersionsSelector.Items.Add(menu);
            }
            EnableBlur();
        }

        private void MenuItem_DeleteAllAccount_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxX.Show("这是一个不可撤销的操作！\r\n您的添加的所有Minecraft账户将被永久删除！\r\n您要继续吗?", "Warning", WindowX_MainView, MessageBoxButton.YesNo, new MessageBoxXConfigurations() { MessageBoxStyle = MessageBoxStyle.Classic, MessageBoxIcon = MessageBoxIcon.Warning, ButtonBrush = "#F1C825".ToColor().ToBrush(), }) == MessageBoxResult.Yes)
            {
                AccountMgrList.Children.Clear();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Res/1041929.jpg")) };
        }

        private void MenuItem_AddAccount_Click(object sender, RoutedEventArgs e)
        {
            LoginView lgview = new LoginView();
            lgview.Show();
            //Button bt = new Button()
            //    {
            //        Height = 80,
            //        Width = 80,
            //        Content = "AHpxChina",
            //        Foreground = Colors.Transparent.ToBrush(),
            //        Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Res/Steve.jpg")) },
            //    };
            //bt.Click += (Object, RoutedEventArgs) => GetItemIndex(bt.Content.ToString(), new BitmapImage(new Uri("pack://application:,,,/Res/Steve.jpg")), sender, e);
            //ButtonHelper.SetButtonStyle(bt, ButtonStyle.Hollow);
            //ButtonHelper.SetHoverBrush(bt, Color.FromArgb(100, 255, 255, 255).ToBrush());
            //ContextMenu menu = new ContextMenu();
            //MenuHelper.SetSubmenuCornerRadius(menu, new CornerRadius(5));
            //MenuItem menuItem = new MenuItem()
            //{
            //    Header = "删除此账号",
            //    Icon = new Image()
            //    {
            //        Source = new BitmapImage(new Uri("pack://application:,,,/Res/Icons/icon_delete.png"))
            //    },
            //};
            //menuItem.Click += (Object, RoutedEventArgs) => MenuItemClicked(bt, sender, e);
            //menu.Items.Add(menuItem);
            //bt.ContextMenu = menu;
            //AccountMgrList.Children.Add(bt);
            //AnimationHelper.SetFadeIn(bt,true);
        }

        private void WindowX_MainView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //DragMove();
        }

        private void ContextMenuItem_VerserSelector_Clicked(String index,object sender, RoutedEventArgs e)
        {
            Display_VeriosnSelect.Text = index;
        }
    }
    class Blur
    {
        internal enum AccentState
        {
            ACCENT_DISABLED = 1,
            ACCENT_ENABLE_GRADIENT = 0,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19
            // ...
        }
    }
}
