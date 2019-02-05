using System.Windows.Media.Imaging;

namespace Task006.Models
{
    public class WalletInfoTable
    {
        public WalletInfoTable(BitmapImage icon, string name, int amount)
        {
            Icon = icon;
            Name = name;
            Amount = amount;
        }
        public BitmapImage Icon { get; private set; }
        public string Name { get; private set; }
        public int Amount { get; private set; }
    }
}
