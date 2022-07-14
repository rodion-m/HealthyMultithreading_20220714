using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NewThreadSyncContext;

namespace WpfAsyncThreading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            //https://source.dot.net/#System.Private.CoreLib/PortableThreadPool.Blocking.cs,32
            //https://source.dot.net/#System.Private.CoreLib/Task.cs,3018
            var current = SynchronizationContext.Current;
            MessageBox.Show(Environment.CurrentManagedThreadId.ToString());
            Task.Run(async () =>
            {
                Task.Delay(1000).GetAwaiter().GetResult();
                //SynchronizationContext.SetSynchronizationContext(current);
                MessageBox.Show(Environment.CurrentManagedThreadId.ToString());
                SynchronizationContext.SetSynchronizationContext(new NewThreadSynchronizationContext());
                await Task.Yield();
                Task.Delay(1000).Wait();
                MessageBox.Show(Environment.CurrentManagedThreadId.ToString());
                MessageBox.Show(Thread.CurrentThread.IsThreadPoolThread.ToString());
            });
        }
    }
}