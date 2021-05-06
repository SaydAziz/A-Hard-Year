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
using System.Windows.Threading;
using System.Threading;
using System.Media;

namespace VirtualEcosystem
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        SoundPlayer woodChop = new SoundPlayer(@"Sounds\woodChop.wav");
        SoundPlayer buttonClick = new SoundPlayer(@"Sounds\buttonClick.wav");
        SoundPlayer shopRing = new SoundPlayer(@"Sounds\shopRing.wav");
        SoundPlayer trapSound = new SoundPlayer(@"Sounds\trapSound.wav");
        SoundPlayer sellSound = new SoundPlayer(@"Sounds\sellSound.wav");
        SoundPlayer buySound = new SoundPlayer(@"Sounds\buySound.wav");
        SoundPlayer seedSound = new SoundPlayer(@"Sounds\seedSound.wav");
        SoundPlayer doorClose = new SoundPlayer(@"Sounds\doorClose.wav");
        SoundPlayer axeBreak = new SoundPlayer(@"Sounds\axeBreak.wav");



        MediaPlayer backgroundMusic = new MediaPlayer();




        World Desert = new World();
        DispatcherTimer timer;
        TimeSpan timeSpan;
        private Random SeedRng = new Random();

        

        private int HBselection = 0;
        int tempSeeds = 0;
        private bool isDay = true;
        private List<TextBlock> InvHotbarTXT = new List<TextBlock>();
        private List<Image> InvHotbarIMG = new List<Image>();
        private List<Image> TextureList = new List<Image>();

        public MainPage()
        {
            
            Utility.ShowLog = PrintLogInformation;
            InitializeComponent();

            backgroundMusic.Open(new Uri(@"Sounds\backgroundMusic.wav", UriKind.Relative)); 
            backgroundMusic.Play();
            backgroundMusic.MediaEnded += new EventHandler(Media_Ended);

            ShopGrid.RenderTransform = new TranslateTransform(1000, 0);
            Desert.SetUpWorld();
            ShowAllOrganisms();
            InitialDisplay();
            Counter();

        }

        private void Counter()
        {
            //DispatchTimer by kmatyaszek (https://stackoverflow.com/users/1410998/kmatyaszek)
            timeSpan = TimeSpan.FromSeconds(15);

            timer = new DispatcherTimer(
                new TimeSpan(0, 0, 1),
                DispatcherPriority.Normal,
                delegate
                {
                    txtTimer.Text = timeSpan.ToString("ss");
                    if (timeSpan == TimeSpan.Zero)
                    {
                        timeSpan = TimeSpan.FromSeconds(16);

                        if (isDay) //BEGINNING OF NIGHT
                        {
                            PopulationCheck();
                            crclOuter.Fill = new SolidColorBrush(Color.FromRgb(158, 151, 151));
                            crclInner.Fill = new SolidColorBrush(Color.FromRgb(197, 190, 190));
                            txtTOD.Text = "NIGHT";
                            ShowAllOrganisms();
                            txtLogging.Text = PrintLogInformation(Desert.consoleLog);
                            AddToHotbar();
                            Desert.NewNight();
                            ShowAllOrganisms();
                            txtLogging.Text = PrintLogInformation(Desert.consoleLog);
                            isDay = false;
                        }
                        else //BEGINNING OF DAY
                        {
                            PopulationCheck();
                            Desert.NewDay();
                            WoodHarden();
                            crclOuter.Fill = new SolidColorBrush(Color.FromRgb(212, 212, 51));
                            crclInner.Fill = new SolidColorBrush(Color.FromRgb(255, 237, 53));
                            ShowAllOrganisms();
                            txtLogging.Text = PrintLogInformation(Desert.consoleLog);
                            DisplayDayInfo();
                            AddToHotbar();
                            ShowSeeds();
                            Desert.ResetViability();
                            isDay = true;

                        }

                    }
                    timeSpan = timeSpan.Add(TimeSpan.FromSeconds(-1));
                },
                Application.Current.Dispatcher);

            timer.Start();
        }



        

        //General functions
        private string PrintLogInformation(List<string> consoleLog)
        {
            return Utility.PrintLog(consoleLog);
        }

        private void ShowAllOrganisms()
        {
            int totalPlant = 0;
            int totalInsect = 0;
            string[] orgList = new string[Desert.organisms.Count()];
            int i = 0;
            foreach (Organism item in Desert.organisms)
            {
                orgList[i++] = item.Name;
            }

            foreach (Organism item in Desert.organisms)
            {
                if (item is Plant)
                {
                    totalPlant++;
                }
                else if (item is Insect)
                {
                    totalInsect++;
                }
            }

            string orgListString = string.Join("\n", orgList);

            txtTotalInsects.Text = totalInsect.ToString();
            txtTotalPlants.Text = totalPlant.ToString();
            txtOrganismList.Text = orgListString;
        }
        private void DisplayDayInfo()
        {
            DaySystem.ShowDay();
            txtTotalDays.Text = DaySystem.totalDay.ToString();
            txtTOD.Text = "  DAY";

            string tom = DaySystem.ShowMonth();
            if (tom == "FINISHED")
            {
                this.NavigationService.Navigate(new Uri("WinScreen.xaml", UriKind.Relative));
            }
            txtTOM.Text = tom;


            int cTemp = TemperatureSystem.CalculateTemp();
            if (cTemp < 16)
            {
                MainGrid.Background = new SolidColorBrush(Color.FromArgb(100, 0, 17, 131));
            }
            else if (cTemp > 31)
            {
                MainGrid.Background = new SolidColorBrush(Color.FromArgb(100, 255, 17, 0));
            }
            else
            {
                MainGrid.Background = new SolidColorBrush(Color.FromArgb(100, 192, 172, 0));
            }

            txtTemperature.Text = cTemp.ToString();





            if (DaySystem.Migration)
            {
                txtMigration.Content = "MIGRATION SEASON";
            }
            else
            {
                txtMigration.Content = "";
            }

        }
        private void InitialDisplay()
        {
            TemperatureSystem.currentTemp = 18;
            DaySystem.ShowDay();
            txtTotalDays.Text = DaySystem.totalDay.ToString();
            txtTOD.Text = "  DAY";
            txtTOM.Text = DaySystem.ShowMonth();
            txtTemperature.Text = TemperatureSystem.currentTemp.ToString();
            txtFireWood.Text = Desert.player.FireWood.ToString();


            InvHotbarTXT.Add(txtInv1);
            InvHotbarTXT.Add(txtInv2);
            InvHotbarTXT.Add(txtInv3);
            InvHotbarTXT.Add(txtInv4);
            InvHotbarTXT.Add(txtInv5);
            InvHotbarTXT.Add(txtInv6);
            InvHotbarTXT.Add(txtInv7);
            InvHotbarTXT.Add(txtInv8);
            InvHotbarTXT.Add(txtInv9);

            InvHotbarIMG.Add(imgInv1);
            InvHotbarIMG.Add(imgInv2);
            InvHotbarIMG.Add(imgInv3);
            InvHotbarIMG.Add(imgInv4);
            InvHotbarIMG.Add(imgInv5);
            InvHotbarIMG.Add(imgInv6);
            InvHotbarIMG.Add(imgInv7);
            InvHotbarIMG.Add(imgInv8);
            InvHotbarIMG.Add(imgInv9);

            TextureList.Add(imgSeed);
            TextureList.Add(imgSparrow);
            TextureList.Add(imgBird);
            TextureList.Add(imgArmadillo);


        }
        private void ShowSeeds()
        {
            
            if (Desert.Seeds > 0)
            {
                tempSeeds += Desert.Seeds;
                while (0 < Desert.Seeds)
                {
                    ImageBrush brush = new ImageBrush(imgSeed.Source);


                    var btnCollect = new Button {  BorderBrush = null,  Background =  brush, Margin = new Thickness(SeedRng.Next(-420, 420), SeedRng.Next(-260, 220), 0, 0), Height = 40, Width = 40 };
                    btnCollect.Click += Seed_Click;
                    Spawner.Children.Add(btnCollect);
                    Desert.Seeds--;                   
                }
                
            }


        }
        private void ShowHotbar()
        {
            if (Desert.player.Hotbar != null)
            {
                int i = 0;
                foreach (Item item in Desert.player.Hotbar)
                {
                    Desert.player.Inventory.TryGetValue(item, out int amount);
                    InvHotbarTXT[i].Text = amount.ToString();
                    InvHotbarIMG[i].Source = TextureList[(item.ItemID) - 1].Source;
                    i++;
                }
                do
                {
                    InvHotbarTXT[i].Text = "";
                    InvHotbarIMG[i].Source = null;
                    i++;
                } while (i != InvHotbarTXT.Count);
            }
            
        }
        private void AddToHotbar()
        {
            Desert.player.InvToHBcheck();
            ShowHotbar();
        }
        private void PopulationCheck()
        {
            int x = 0;
            foreach (Organism item in Desert.organisms)
            {
                
                if (item is Plant || item is Insect)
                {
                    x++;
                }
               
            }
            if (x < 1)
            {
                this.NavigationService.Navigate(new Uri("GameOver.xaml", UriKind.Relative));
            }
            
        }
        public void WoodHarden()
        {
            if (DaySystem.totalDay == 9 || DaySystem.totalDay == 21)
            {
                if (DaySystem.totalDay == 9)
                {
                    THELOG.Opacity = 0;
                    Desert.treeDensity++;
                    THELOG2.Opacity = 100;
                }
                else
                {
                    THELOG2.Opacity = 0;
                    Desert.treeDensity++;
                    THELOG3.Opacity = 100;
                }
            }
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            backgroundMusic.Position = TimeSpan.Zero;
            backgroundMusic.Play();
        }


        //Button click events
        private void btnAddTemp_Click(object sender, RoutedEventArgs e)
        {
            if (isDay)
            {
                buttonClick.Play();
                WeatherModifier.Heat(Desert);
                txtTemperature.Text = TemperatureSystem.currentTemp.ToString();             
                txtFireWood.Text = Desert.player.FireWood.ToString();
                AnimateLeftButton();
            }
        }
        private void btnRemoveTemp_Click_1(object sender, RoutedEventArgs e)
        {
            if (isDay)
            {
                buttonClick.Play();
                WeatherModifier.Cool(Desert);
                txtTemperature.Text = TemperatureSystem.currentTemp.ToString();
                txtFireWood.Text = Desert.player.FireWood.ToString();
                AnimateRightButton();
            }

        }
        private void btnChop_Click(object sender, RoutedEventArgs e)
        {
            if (isDay)
            {
                if (Desert.player.Chop(Desert.treeDensity))
                {
                    AnimatePlus1();
                    
                }
                else if (Desert.player.broke >0)
                {
                    imgAxe.Opacity = 0;
                    btnChop.IsEnabled = false;
                    axeBreak.Play();

                }
                txtFireWood.Text = Desert.player.FireWood.ToString();

                
                if (btnChop.IsEnabled)
                {
                    imgAxe.Opacity = 0;
                    imgAxe_Rotated.Opacity = 100;
                    woodChop.Play();
                    AnimateAxe();
                }
                
            }
        }
        private void txtShop_Click(object sender, RoutedEventArgs e)
        {
            ShopGrid.RenderTransform = new TranslateTransform(0, 0);
            shopRing.Play();
        }
        private void Seed_Click(object sender, RoutedEventArgs e) 
        {
            Desert.player.Inventory[Desert.player.Seed]++;
            
            
            List<UIElement> removeable = new List<UIElement>();
            foreach (UIElement children in Spawner.Children)
            {
                removeable.Add(children);
            }
            Spawner.Children.Remove(removeable[0]);
            AddToHotbar();
            tempSeeds--;
            seedSound.Play();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ShopGrid.RenderTransform = new TranslateTransform(1000, 0);
            doorClose.Play();
        }


        //Image animations
        public async void AnimateAxe()
        { 
            await Task.Delay(80);
            imgAxe.Opacity = 100;
            imgAxe_Rotated.Opacity = 0;
        }
        public async void AnimatePlus1()
        {
            lblPlus1.Opacity = 100;
            imgFireWood.Opacity = 100;
            await Task.Delay(100);
            lblPlus1.Opacity = 0;
            imgFireWood.Opacity = 0;
        }
        public async void AnimateLeftButton()
        {
            imgTAM_Left.Opacity = 100;
            await Task.Delay(100);
            imgTAM_Left.Opacity = 0;
        }
        public async void AnimateRightButton()
        {
            imgTAM_Right.Opacity = 100;
            await Task.Delay(100);
            imgTAM_Right.Opacity = 0;
        }


        //Store functionality
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            int i = Desert.Store.SelectedItem;

            if ((i == 1 || i == 2 || i == 6) && Desert.player.Coin >= 0)
            {
                if (i == 1 && Desert.player.Coin - 100 >= 0)
                {
                    Desert.player.Coin -= 100; 
                    shopAxe.IsEnabled = false;
                    lblCoin.Content = Desert.player.Coin;
                    imgAxe.Source = imgAXE.Source;
                    imgAxe_Rotated.Source = imgAXE.Source;
                    imgAxe.Opacity = 100;
                    Desert.player.broke = 0;
                    Desert.player.axeLVL++;
                    btnChop.IsEnabled = true;
                    buySound.Play();
                }
                else if (i == 2 && Desert.player.Coin - 150 >= 0 && Desert.player.axeLVL == 1)
                {
                    Desert.player.Coin -= 150;
                    shopAxe2.IsEnabled = false;
                    lblCoin.Content = Desert.player.Coin;
                    imgAxe.Source = imgAXE2.Source;
                    imgAxe_Rotated.Source = imgAXE2.Source;
                    imgAxe.Opacity = 100;
                    Desert.player.broke = 0;
                    Desert.player.axeLVL++;
                    btnChop.IsEnabled = true;
                    buySound.Play();
                }
                else if (i==6 && Desert.player.Coin - 5 >= 0)
                {
                    Desert.player.FireWood++;
                    txtFireWood.Text = Desert.player.FireWood.ToString();
                    Desert.player.Coin -= 5;
                    lblCoin.Content = Desert.player.Coin;
                    buySound.Play();
                }
                
                
            }
            else
            {
                try
                {
                    if (Desert.player.Coin >= Desert.Store.ShopContent[Desert.Store.SelectedItem].Price)
                    {
                        buySound.Play();
                    }
                }
                catch
                {

                }
                Desert.Store.Buy();
                lblCoin.Content = Desert.player.Coin;
                AddToHotbar();
                
            }       
        }
        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            if (Desert.Store.SelectedItem == 6 && Desert.player.FireWood > 0)
            {
                Desert.player.FireWood--;
                txtFireWood.Text = Desert.player.FireWood.ToString();
                Desert.player.Coin += 5;
                lblCoin.Content = Desert.player.Coin;
                sellSound.Play();
            }
            else
            {
                try
                {
                    Desert.player.Inventory.TryGetValue(Desert.Store.ShopContent[Desert.Store.SelectedItem], out int amount);

                    if (amount > 0)
                    {
                        sellSound.Play();
                    }
                }
                catch
                {

                }
                Desert.Store.Sell();
                lblCoin.Content = Desert.player.Coin;  
                AddToHotbar();
            }
            
        }
        private void shopAxe_Click(object sender, RoutedEventArgs e)
        {
            Desert.Store.SelectedItem = 1;

            lblSelection.Content = "Stone Axe";
            txtShopDescription.Text = "An upgrade to your current axe! The wood gets denser as the year goes, and your old axe just wont cut it anymore. Literally! haha...";
            txtItemPricing.Text = Desert.player.lvl2Axe.Price.ToString();
            coinycoincin.Opacity = 100;
        }
        private void shopAxe2_Click(object sender, RoutedEventArgs e)
        {
            Desert.Store.SelectedItem = 2;

            lblSelection.Content = "Metal Axe";
            txtShopDescription.Text = "* Stone Axe Upgrade Required *\n\nThe best Axe there is! No wood can stand in its way.";
            txtItemPricing.Text = Desert.player.lvl3Axe.Price.ToString();
            coinycoincin.Opacity = 100;
        }
        private void shopSparrow_Click(object sender, RoutedEventArgs e)
        {
            Desert.Store.SelectedItem = 3;

            lblSelection.Content = "Sparrow Trap";
            txtShopDescription.Text = "A trap specifically designed to combat those pesky Sparrows. Gotta keep those plants safe!";
            txtItemPricing.Text = Desert.player.SparrowTrap.Price.ToString();
            coinycoincin.Opacity = 100;
        }
        private void shopBird_Click(object sender, RoutedEventArgs e)
        {
            Desert.Store.SelectedItem = 4;

            lblSelection.Content = "Hummingbird\nTrap";
            txtShopDescription.Text = "A trap specifically designed to combat those pesky HummingBirds. Gotta keep those plants and insects safe!";
            txtItemPricing.Text = Desert.player.BirdTrap.Price.ToString();
            coinycoincin.Opacity = 100;
        }
        private void shopArmadillo_Click(object sender, RoutedEventArgs e)
        {
            Desert.Store.SelectedItem = 5;

            lblSelection.Content = "Armadillo Trap";
            txtShopDescription.Text = "A trap specifically designed to combat those pesky Armadillos. Gotta keep those insects safe!";
            txtItemPricing.Text = Desert.player.ArmadilloTrap.Price.ToString();
            coinycoincin.Opacity = 100;
            
        }
        private void shopWood_Click(object sender, RoutedEventArgs e)
        {
            Desert.Store.SelectedItem = 6;

            lblSelection.Content = "Fire Wood";
            txtShopDescription.Text = "Used as fuel for the Temperature Alteration Machine (T.A.M). Obtained from the weird magic infinite log in the corner of your screen.";
            txtItemPricing.Text = "5";
            coinycoincin.Opacity = 100;
        }
        private void shopSeed_Click(object sender, RoutedEventArgs e)
        {
            Desert.Store.SelectedItem = 0;

            lblSelection.Content = "Seed";
            txtShopDescription.Text = "Yucca Plants have a 50% chance to drop this item. Collect it, and sell it here to get coin in order to buy tools or upgrades!";
            txtItemPricing.Text = Desert.player.Seed.Price.ToString();
            coinycoincin.Opacity = 100;
        }


        //Hotbar item usage
        private async void HB1_Click(object sender, RoutedEventArgs e)
        {
            HBselection = 1;
            btnUse.Opacity = 100;
            btnUse.IsEnabled = true;
            await Task.Delay(1500);
            btnUse.IsEnabled = false;
            btnUse.Opacity = 0;
        }
        private async void HB3_Click(object sender, RoutedEventArgs e)
        {
            HBselection = 3;
            btnUse.Opacity = 100;
            btnUse.IsEnabled = true;
            await Task.Delay(1500);
            btnUse.IsEnabled = false;
            btnUse.Opacity = 0;
        }
        private async void HB4_Click(object sender, RoutedEventArgs e)
        {
            HBselection = 4;
            btnUse.Opacity = 100;
            btnUse.IsEnabled = true;
            await Task.Delay(1500);
            btnUse.IsEnabled = false;
            btnUse.Opacity = 0;
        }
        private async void HB2_Click(object sender, RoutedEventArgs e)
        {
            HBselection = 2;
            btnUse.Opacity = 100;
            btnUse.IsEnabled = true;
            await Task.Delay(1500);
            btnUse.IsEnabled = false;
            btnUse.Opacity = 0;
        }
        private async void HB5_Click(object sender, RoutedEventArgs e)
        {
            HBselection = 5;
            btnUse.Opacity = 100;
            btnUse.IsEnabled = true;
            await Task.Delay(1500);
            btnUse.IsEnabled = false;
            btnUse.Opacity = 0;
        }
        private async void HB6_Click(object sender, RoutedEventArgs e)
        {
            HBselection = 6;
            btnUse.Opacity = 100;
            btnUse.IsEnabled = true;
            await Task.Delay(1500);
            btnUse.IsEnabled = false;
            btnUse.Opacity = 0;
        }
        private async void HB7_Click(object sender, RoutedEventArgs e)
        {
            HBselection = 7;
            btnUse.Opacity = 100;
            btnUse.IsEnabled = true;
            await Task.Delay(1500);
            btnUse.IsEnabled = false;
            btnUse.Opacity = 0;
        }
        private async void HB8_Click(object sender, RoutedEventArgs e)
        {
            HBselection = 8;
            btnUse.Opacity = 100;
            btnUse.IsEnabled = true;
            await Task.Delay(1500);
            btnUse.IsEnabled = false;
            btnUse.Opacity = 0;
        }
        private async void HB9_Click(object sender, RoutedEventArgs e)
        {
            HBselection = 9;
            btnUse.Opacity = 100;
            btnUse.IsEnabled = true;
            await Task.Delay(1500);
            btnUse.IsEnabled = false;
            btnUse.Opacity = 0;
        }

        private void btnUse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = Desert.player.Hotbar[HBselection - 1].ItemID;
                Item selectedItem = Desert.player.Hotbar[HBselection - 1];
                switch (ID)
                {
                    case 1:
                        Desert.SeedPlant(selectedItem);
                        seedSound.Play();
                        ShowAllOrganisms();
                        txtLogging.Text = PrintLogInformation(Desert.consoleLog);
                        break;
                    case 2:
                        Desert.TrapSparrow(selectedItem);
                        trapSound.Play();
                        Desert.ClearCorpse();
                        ShowAllOrganisms();
                        txtLogging.Text = PrintLogInformation(Desert.consoleLog);
                        break;
                    case 3:
                        Desert.TrapBird(selectedItem);
                        trapSound.Play();
                        Desert.ClearCorpse();
                        ShowAllOrganisms();
                        txtLogging.Text = PrintLogInformation(Desert.consoleLog);
                        break;
                    case 4:
                        Desert.TrapArmadillo(selectedItem);
                        trapSound.Play();
                        Desert.ClearCorpse();
                        ShowAllOrganisms();
                        txtLogging.Text = PrintLogInformation(Desert.consoleLog);
                        break;
                    case 5:
                        break;

                }
                AddToHotbar();
                btnUse.IsEnabled = false;
                btnUse.Opacity = 0;
            }
            catch 
            {
                
            }

            
        }

        
    }
}
