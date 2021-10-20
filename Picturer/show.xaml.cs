using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Picturer
{
    /// <summary>
    /// show.xaml 的交互逻辑
    /// </summary>
    public partial class Show : Window
    {
        public Show(DirTree tree, ChangeMode picChangeMode, string filename = null)
        {
            InitializeComponent();

            package = new Package(tree, picChangeMode, this.Dispatcher, filename);

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            randomMode.IsChecked = package.picChangeMode == ChangeMode.Random;
            changePic(0);

            //leftClickTimer.Tick += (o, evt) => { changePic(1); leftClickTimer.Stop(); };
            //leftClickTimer.Interval = new TimeSpan(TimeSpan.TicksPerSecond / 10 * 2);

            //rightClickTimer.Tick += (o, evt) => { changePic(-1); rightClickTimer.Stop(); };
            //rightClickTimer.Interval = new TimeSpan(TimeSpan.TicksPerSecond / 10 * 2);

        }
        //System.Windows.Threading.DispatcherTimer leftClickTimer = new System.Windows.Threading.DispatcherTimer();
        //System.Windows.Threading.DispatcherTimer rightClickTimer = new System.Windows.Threading.DispatcherTimer();

        private Package package;
        private int rateNum = 1;
        private double[] rates = new double[4];
        private double workWidth, workHeight;
        private void fixWindowSize()
        {
            Window.Width = Math.Min(SystemParameters.WorkArea.Width, (tbShow.Source as BitmapSource).PixelWidth + SystemParameters.ResizeFrameVerticalBorderWidth * 2);
            Window.Height = Math.Min(SystemParameters.WorkArea.Height, (tbShow.Source as BitmapSource).PixelHeight + SystemParameters.ResizeFrameHorizontalBorderHeight * 2 + SystemParameters.WindowCaptionHeight);
            workWidth = Window.Width - SystemParameters.ResizeFrameVerticalBorderWidth * 2;
            workHeight = Window.Height - SystemParameters.ResizeFrameHorizontalBorderHeight * 2 - SystemParameters.WindowCaptionHeight;
            Window.Left = (SystemParameters.WorkArea.Width - Window.Width) / 2;
            Window.Top = (SystemParameters.WorkArea.Height - Window.Height) / 2;
        }
        private void fixSize(int rateNum)
        {
            double uniformRate = Math.Min(workWidth / (tbShow.Source as BitmapSource).PixelWidth, workHeight / (tbShow.Source as BitmapSource).PixelHeight);
            double fillRate = Math.Max(workWidth / (tbShow.Source as BitmapSource).PixelWidth, workHeight / (tbShow.Source as BitmapSource).PixelHeight);
            rates[2] = 1;
            rates[1] = Math.Min(fillRate, 1);
            rates[0] = Math.Min(uniformRate, rates[1]);
            rates[3] = rates[this.rateNum = rateNum] < rates[0] ? rates[0] : rates[rateNum];
            tbShow.Width = (tbShow.Source as BitmapSource).PixelWidth * rates[3];
            tbShow.Height = (tbShow.Source as BitmapSource).PixelHeight * rates[3];
        }
        private void resetScroll()
        {
            switch (package.picChangeMode)
            {
                case ChangeMode.Sequence:
                    moveScrollTo(tbShow.Width - workWidth, 0);
                    break;
                case ChangeMode.Random:
                    moveScrollTo((tbShow.Width - workWidth) / 2, 0);
                    break;
            }
        }
        private void imgSizeChange(int change)
        {
            if (rateNum + change >= 0 && rateNum + change < 3)
            {
                rateNum = rateNum + change;
            }
            fixSize(this.rateNum);
        }
        private void imgSizeChange(double change)
        {
            if (change >= 1 || change <= -1) return;
            rates[3] = rates[3] * (1 + change);
            this.rateNum = rates[3] < rates[0] ? 0 : rates[3] > rates[2] ? 2 : Math.Abs((rates[3] - rates[1]) / rates[1]) < 0.01 ? 1 : 3;
            rates[3] = rates[this.rateNum];
            tbShow.Width = (tbShow.Source as BitmapSource).PixelWidth * rates[3];
            tbShow.Height = (tbShow.Source as BitmapSource).PixelHeight * rates[3];
        }
        private void changePic(int picNumChange)
        {
            try
            {
                tbShow.StopAnimate();
                if (autoModeTimer != null && autoModeTimer.IsEnabled)
                {
                    autoModeTimer.Stop();
                    autoModeTimer.Start();
                }
                package.ChangePicNum(picNumChange, PicMode.AllSupport);
                if (Tools.IsPicImage(package.FilePath, PicMode.OnlyGif))
                {
                    tbShow.AnimatedImageControl(package.FilePath);
                }
                else
                {
                    tbShow.Source = package.SetAndGetPic();
                }
                fixWindowSize();
                fixSize(this.rateNum);
                resetScroll();
                this.Title = package.FilePath;
            }
            catch (Exception e)
            {
                this.Title = package.FilePath + "切换失败:" + e.Message;
            }
        }

        private System.Windows.Point mousePosition;
        private Vector speed;
        private void tbShow_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    leftClickTimer.Stop();
            //}
            var lastMousePosition = mousePosition;
            mousePosition = e.GetPosition(Window);
            speed = (lastMousePosition - mousePosition) / 15;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                moveScrollTo(imageScrollViewer.HorizontalOffset + lastMousePosition.X - mousePosition.X, imageScrollViewer.VerticalOffset + lastMousePosition.Y - mousePosition.Y);
            }
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double pastRate = rates[3];
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                e.Handled = true;
                imgSizeChange(e.Delta > 0 ? 0.1 : -0.1);
                double x = e.GetPosition(tbShow).X * rates[3] / pastRate - workWidth / 2;
                double y = e.GetPosition(tbShow).Y * rates[3] / pastRate - workHeight / 2;
                moveScrollTo(x, y);
            }
            else if(Keyboard.IsKeyDown(Key.LeftAlt))
            {
                e.Handled = true;
                autoSlider.Value += e.Delta > 0 ? 1 : -1;
            }
            else if (e.Delta > 0)
            {
                if (tbShow.Height > workHeight && imageScrollViewer.VerticalOffset > 0)
                {
                    moveScrollTo(imageScrollViewer.HorizontalOffset, imageScrollViewer.VerticalOffset - 50);
                }
                else if (tbShow.Width > workWidth && imageScrollViewer.HorizontalOffset > 0)
                {
                    moveScrollTo(imageScrollViewer.HorizontalOffset - 50, imageScrollViewer.VerticalOffset);
                }
            }
            else
            {
                if (tbShow.Height > workHeight && imageScrollViewer.VerticalOffset < tbShow.Height - workHeight)
                {
                    moveScrollTo(imageScrollViewer.HorizontalOffset, imageScrollViewer.VerticalOffset + 50);
                }
                else if (tbShow.Width > workWidth && imageScrollViewer.HorizontalOffset < tbShow.Width - workWidth)
                {
                    moveScrollTo(imageScrollViewer.HorizontalOffset + 50, imageScrollViewer.VerticalOffset);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    imgSizeChange(0.1);
                    break;
                case Key.Down:
                    imgSizeChange(-0.1);
                    break;
                case Key.Home:
                case Key.Q:
                case Key.Left:
                    changePic(-1);
                    break;
                case Key.End:
                    if (tbShow.Width - workWidth - imageScrollViewer.HorizontalOffset > 100)
                        moveScrollTo(imageScrollViewer.HorizontalOffset + workWidth, imageScrollViewer.VerticalOffset);
                    else if (tbShow.Height - workHeight - imageScrollViewer.VerticalOffset > 100)
                        moveScrollTo(imageScrollViewer.HorizontalOffset, imageScrollViewer.VerticalOffset + workHeight);
                    else changePic(1);
                    break;
                case Key.E:
                case Key.Right:
                    changePic(1);
                    break;
                case Key.W:
                    moveScrollTo(imageScrollViewer.HorizontalOffset, imageScrollViewer.VerticalOffset - 50);
                    break;
                case Key.S:
                    moveScrollTo(imageScrollViewer.HorizontalOffset, imageScrollViewer.VerticalOffset + 50);
                    break;
                case Key.A:
                    moveScrollTo(imageScrollViewer.HorizontalOffset - 50, imageScrollViewer.VerticalOffset);
                    break;
                case Key.D:
                    moveScrollTo(imageScrollViewer.HorizontalOffset + 50, imageScrollViewer.VerticalOffset);
                    break;
                case Key.Delete:
                    var result = MessageBox.Show("是否删除" + package.FilePath + "?", "是否删除", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        package.DeleteCurrent();
                        changePic(0);
                    }
                    break;
                case Key.PageUp:
                    if (tbShow.Height > workHeight && imageScrollViewer.VerticalOffset > 0)
                    {
                        moveScrollTo(imageScrollViewer.HorizontalOffset, imageScrollViewer.VerticalOffset - workHeight);
                    }
                    else if (tbShow.Width > workWidth && imageScrollViewer.HorizontalOffset > 0)
                    {
                        moveScrollTo(imageScrollViewer.HorizontalOffset - workWidth, imageScrollViewer.VerticalOffset);
                    }
                    else
                    {
                        changePic(-1);
                    }
                    break;
                case Key.PageDown:
                    if (tbShow.Height > workHeight && imageScrollViewer.VerticalOffset < tbShow.Height - workHeight)
                    {
                        moveScrollTo(imageScrollViewer.HorizontalOffset, imageScrollViewer.VerticalOffset + workHeight);
                    }
                    else if(tbShow.Width > workWidth && imageScrollViewer.HorizontalOffset < tbShow.Width - workWidth)
                    {
                        moveScrollTo(imageScrollViewer.HorizontalOffset + workWidth, imageScrollViewer.VerticalOffset);
                    }
                    else
                    {
                        changePic(1);
                    }
                    break;
                case Key.Escape:
                    this.Close();
                    break;
            }
            e.Handled = true;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
        }

        private void tbShow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            changePic(-1);
            //rightClickTimer.Start();
        }

        //private void imageScrollViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //   if (e.ChangedButton == MouseButton.Left)
        //    {
        //        leftClickTimer.Stop();
        //        if (rateNum == 2)
        //        {
        //            imgSizeChange(-2);
        //        }
        //        else
        //        {
        //            imgSizeChange(1);
        //        }
        //    }
        //}

        private void tbShow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            changePic(1);
            //leftClickTimer.Start();
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Middle)
            {
                //autoMode.IsChecked = !autoMode.IsChecked;
            }
        }

        private void tbShow_Unloaded(object sender, RoutedEventArgs e)
        {
            tbShow.StopAnimate();
        }

    }
}
