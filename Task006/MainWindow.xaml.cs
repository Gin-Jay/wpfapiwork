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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task006
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly string apiKey = "248726EA-D21C-8C46-A4F3-7E82EA505D63B6DB8BBD-51D3-45F3-B92B-7AC19CF47307";

        private async void ButtonAccountInfo_Click(object sender, RoutedEventArgs e)
        {
            var accInfo = await Wrapper.GetAccInfo(apiKey);
            TextName.Text = accInfo.Name;

            var span = TimeSpan.FromSeconds(accInfo.Age);
            TextAge.Text = $"{span.Days} Days, {span.Hours} Hours\n{span.Minutes} Minutes, {span.Seconds} Seconds";

            TextId.Text = accInfo.Id;
            TextCreated.Text = accInfo.Created.ToString();

            CheckBoxCommander.IsChecked = accInfo.Commander;

            ComboBoxAccess.ItemsSource = accInfo.Access;
            ComboBoxAccess.SelectedIndex = accInfo.Access.Count > 0 ? 0 : -1;

            var worldInfo = await Wrapper.GetWorldInfo(accInfo.World);
            TextWorld.Text = $"{worldInfo[0].Name} ({worldInfo[0].Id})";

            var guilds = await Wrapper.GetGuildInfo(accInfo.Guilds);
            ComboBoxGuilds.ItemsSource = guilds;
            ComboBoxGuilds.SelectedIndex = guilds.Count > 0 ? 0 : -1;
        }
    }
}
