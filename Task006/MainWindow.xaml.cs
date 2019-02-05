using System;
using System.Collections.Generic;
using System.Data;
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
using Task006.Models;

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

        Dictionary<string, CharactersCoreModel> dictionaryCacheCharInfo = new Dictionary<string, CharactersCoreModel>();

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

        private async void ButtonCharacterInfo_Click(object sender, RoutedEventArgs e)
        {
            listBoxName.ItemsSource = await Wrapper.GetCharInfo(apiKey);
        }

        private async void ListBoxName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsEnabled = false;
            //clear info
            textBlockAge.Text = string.Empty;
            textBlockCreated.Text = string.Empty;
            textBlockDeaths.Text = string.Empty;
            textBlockGender.Text = string.Empty;
            textBlockLevel.Text = string.Empty;
            textBlockProfession.Text = string.Empty;
            textBlockRace.Text = string.Empty;
            textBlockTitle.Text = string.Empty;
            
            //request new info
            var listBox = sender as ListBox;
            var name = listBox.SelectedItem as string;
            var nameEncoded = Uri.EscapeUriString(name);

            //check if we have info in 'cache'
            if (!dictionaryCacheCharInfo.ContainsKey(nameEncoded))
            {
                dictionaryCacheCharInfo.Add(nameEncoded, await Wrapper.GetCharacterInfo(nameEncoded, apiKey));
            }
            
            var characterCoreInfo = dictionaryCacheCharInfo[nameEncoded];

            //fill new info
            try
            {
                var titleInfo = await Wrapper.GetTitleInfo(characterCoreInfo.Title);
                textBlockTitle.Text = $"{titleInfo.Name} ({titleInfo.Id})";
            }
            catch
            {
                textBlockTitle.Text = "None";
            }

            textBlockRace.Text = characterCoreInfo.Race;
            textBlockGender.Text = characterCoreInfo.Gender;
            textBlockProfession.Text = characterCoreInfo.Profession;
            var span = TimeSpan.FromSeconds(characterCoreInfo.Age);
            textBlockAge.Text = $"{span.Days} Days, {span.Hours} Hours\n{span.Minutes} Minutes, {span.Seconds} Seconds";
            textBlockCreated.Text = characterCoreInfo.Created.ToString();
            textBlockDeaths.Text = characterCoreInfo.Deaths.ToString();
            textBlockLevel.Text = characterCoreInfo.Level.ToString();
          

            IsEnabled = true;
        }

        private async void ButtonGetWalletInfo_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;


            //request wallet
            var walletInfo = await Wrapper.GetWalletInfo(apiKey);

            //request currencies
            var currencyList = new List<int>();
            foreach (var currency in walletInfo)
            {
                currencyList.Add(currency.Id);
            }

            var currencyInfo = await Wrapper.GetCurrencyInfo(currencyList);

            //load currency img
            var currencyImageList = new List<BitmapImage>();
            foreach (var currency in currencyInfo)
            {
                currencyImageList.Add(await Request.GetImageAsync(currency.Icon));
            }

            //create view data structure
            //var dt = new DataTable();
            //List<CurrencyModelFull> dt = new List<CurrencyModelFull>();
            //dt.Columns.Add("Icon", typeof(BitmapImage));
            //dt.Columns.Add("Name", typeof(string));
            //dt.Columns.Add("Amount", typeof(int));

            var walletList = new List<WalletInfoTable>();
            for (var i = 0; i < currencyInfo.Count; i++)
            {
                var obj = new WalletInfoTable(currencyImageList[i], currencyInfo[i].Name, walletInfo[i].Value);
                walletList.Add(obj);
            }


            dataGridWallet.ItemsSource = walletList;
        

            // fill in currency data
            //dataGridViewWallet.DataSource = dt;
           
            

            //adjust icon view
            //var iconColumn = dataGridViewWallet.Columns["Icon"] as DataGridViewImageColumn;
            //iconColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            IsEnabled = true;
        }
    }
}
