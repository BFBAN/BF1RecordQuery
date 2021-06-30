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
using System.Text.Json;
using System.ComponentModel;

using BF1RecordQuery.Data;
using BF1RecordQuery.Core;
using System.Net;

namespace BF1RecordQuery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int ImageCount = 2;
        private BackgroundWorker MainWorker;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Main_Loaded(object sender, RoutedEventArgs e)
        {
            Title = MyUtils.WindowTitle + MyUtils.LocalVersionInfo;

            MainWorker = new BackgroundWorker();
            MainWorker.DoWork += MainWorker_DoWork;
            MainWorker.RunWorkerAsync();
        }

        private void MainWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 刷新DNS缓存
            MyUtils.CMD_Code("ipconfig /flushdns");

            WebClient webClient = new WebClient();
            webClient.Proxy = null;
            webClient.Encoding = Encoding.UTF8;

            // 检查版本更新
            string version = webClient.DownloadString(MyUtils.CheckUpdateAdress);
            Version WebVersion = new Version(version);
            Version LocalVersion = new Version(MyUtils.LocalVersionInfo);

            if (WebVersion != LocalVersion)
            {
                string path = MyUtils.CurrentDirectory + MyUtils.WindowTitle + WebVersion + ".exe";

                // 下载更新
                MyUtils.HttpDownloadFile(MyUtils.DownloadUpdateAddress, path);

                MessageBox.Show("新版本下载完成，你下次启动软件可以选择当前目录下的最新版本使用。", "提示",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }

            webClient.Dispose();
        }


        private async void QueryRecord()
        {
            try
            {
                string nameStr = TextBox_PlayerName.Text.Trim();

                ListBox_GameTools_Stats.Items.Clear();
                ListBox_GameTools_Weapons.Items.Clear();
                ListBox_GameTools_Vehicles.Items.Clear();

                ListBox_GameTools_Stats.Items.Add("正在查詢中...");
                ListBox_GameTools_Weapons.Items.Add(new ListBoxWeaponData
                {
                    WeaponName = "正在查詢中..."
                });
                ListBox_GameTools_Vehicles.Items.Add(new ListBoxVehicleData
                {
                    VehicleName = "正在查詢中..."
                });
                TextBlock_UserName.Text = "Loading...";
                TextBlock_Rank.Text = "Loading...";
                TextBlock_PlayedTime.Text = "Loading...";
                Image_UserAvatar.Source = null;
                ImageToolTip_UserAvatar.Source = null;
                Image_Rank.Source = null;

                /*******************************************************************************/

                string result = await GameToolsApi.GetAllData(nameStr);
                if (!result.Contains("error"))
                {
                    // 按照指定类反序列化，然后排序
                    GetStats getStats = JsonSerializer.Deserialize<GetStats>(result);
                    getStats.weapons.Sort((a, b) => b.kills.CompareTo(a.kills));
                    getStats.vehicles.Sort((a, b) => b.kills.CompareTo(a.kills));

                    /*******************************************************************************/

                    ListBox_GameTools_Stats.Items.Clear();

                    Image_UserAvatar.Source = new BitmapImage(new Uri(getStats.avatar));
                    ImageToolTip_UserAvatar.Source = Image_UserAvatar.Source;

                    TextBlock_UserName.Text = getStats.userName;
                    Image_Rank.Source = new BitmapImage(new Uri(getStats.rankImg));
                    TextBlock_Rank.Text = getStats.rank.ToString();
                    TextBlock_PlayedTime.Text = getStats.timePlayed;

                    //ListBox_GameTools_Stats.Items.Add($"玩家ID : {getStats.userName}");
                    //ListBox_GameTools_Stats.Items.Add($"等级 : {getStats.rank}");

                    ListBox_GameTools_Stats.Items.Add($"K/D : {getStats.killDeath}");
                    ListBox_GameTools_Stats.Items.Add($"KPM : {getStats.killsPerMinute}");
                    ListBox_GameTools_Stats.Items.Add($"SPM : {getStats.scorePerMinute}");

                    ListBox_GameTools_Stats.Items.Add($"命中率 : {getStats.Accuracy}");
                    ListBox_GameTools_Stats.Items.Add($"爆頭率 : {getStats.headshots}");
                    ListBox_GameTools_Stats.Items.Add($"爆頭數 : {getStats.headShots}");

                    ListBox_GameTools_Stats.Items.Add($"最高連續擊殺數 : {getStats.highestKillStreak}");
                    ListBox_GameTools_Stats.Items.Add($"最遠爆頭距離 : {getStats.longestHeadShot}");

                    ListBox_GameTools_Stats.Items.Add("");

                    ListBox_GameTools_Stats.Items.Add($"擊殺 : {getStats.kills}");
                    ListBox_GameTools_Stats.Items.Add($"死亡 : {getStats.deaths}");
                    ListBox_GameTools_Stats.Items.Add($"協助擊殺數 : {getStats.killAssists}");

                    ListBox_GameTools_Stats.Items.Add($"勝率 : {getStats.winPercent}");
                    ListBox_GameTools_Stats.Items.Add($"技巧值 : {getStats.skill}");

                    ListBox_GameTools_Stats.Items.Add($"步兵KD : {getStats.infantryKillDeath}");
                    ListBox_GameTools_Stats.Items.Add($"步兵KPM : {getStats.infantryKillsPerMinute}");
                    ListBox_GameTools_Stats.Items.Add($"最佳兵種 : {getStats.bestClass}");

                    ListBox_GameTools_Stats.Items.Add("");

                    ListBox_GameTools_Stats.Items.Add($"仇敵擊殺數 : {getStats.avengerKills}");
                    ListBox_GameTools_Stats.Items.Add($"救星擊殺數 : {getStats.saviorKills}");
                    ListBox_GameTools_Stats.Items.Add($"急救數 : {getStats.revives}");
                    ListBox_GameTools_Stats.Items.Add($"治療分 : {getStats.heals}");
                    ListBox_GameTools_Stats.Items.Add($"修理分 : {getStats.repairs}");

                    //ListBox_GameTools_Stats.Items.Add($"遊戲時間 : {getStats.timePlayed}");
                    ListBox_GameTools_Stats.Items.Add($"取得狗牌數 : {getStats.dogtagsTaken}");
                    ListBox_GameTools_Stats.Items.Add($"勝利場數 : {getStats.wins}");
                    ListBox_GameTools_Stats.Items.Add($"戰敗場數 : {getStats.loses}");
                    ListBox_GameTools_Stats.Items.Add($"遊戲總局數 : {getStats.roundsPlayed}");

                    ListBox_GameTools_Stats.Items.Add("");

                    ListBox_GameTools_Stats.Items.Add($"小隊分數 : {getStats.squadScore}");
                    ListBox_GameTools_Stats.Items.Add($"獎勵分數 : {getStats.awardScore}");
                    ListBox_GameTools_Stats.Items.Add($"加成分數 : {getStats.bonusScore}");

                    ListBox_GameTools_Stats.Items.Add($"當前等級進度 : {getStats.currentRankProgress}");
                    ListBox_GameTools_Stats.Items.Add($"總計等級進度 : {getStats.totalRankProgress}");

                    ListBox_GameTools_Stats.Items.Add($"數字ID : {getStats.id}");

                    /*******************************************************************************/

                    ListBox_GameTools_Weapons.Items.Clear();

                    foreach (var item in getStats.weapons)
                    {
                        if (item.kills == 0)
                        {
                            break;
                        }

                        ListBox_GameTools_Weapons.Items.Add(new ListBoxWeaponData
                        {
                            WeaponImage = item.image,
                            WeaponName = item.weaponName,
                            WeaponStar = GetKillStar(item.kills),
                            WeaponKill = $"擊殺 : {item.kills}",
                            WeaponKPM = $"KPM : {item.killsPerMinute}",
                            WeaponAccuracy = $"命中率 : {item.accuracy}",
                            WeaponHeadshots = $"爆頭率 : {item.headshots}",
                            WeaponHitVKills = $"效率 : {item.hitVKills}"
                        });
                    }

                    /*******************************************************************************/

                    ListBox_GameTools_Vehicles.Items.Clear();

                    foreach (var item in getStats.vehicles)
                    {
                        if (item.kills == 0)
                        {
                            break;
                        }

                        ListBox_GameTools_Vehicles.Items.Add(new ListBoxVehicleData
                        {
                            VehicleImage = item.image,
                            VehicleName = item.vehicleName,
                            VehicleStar = GetKillStar(item.kills),
                            VehicleKill = $"擊殺 : {item.kills}",
                            VehicleKPM = $"KPM : {item.killsPerMinute}",
                            VehicleDestroyede = $"摧毀 : {item.destroyed}"
                        });
                    }
                }
                else
                {
                    TextBlock_UserName.Text = "未知";
                    TextBlock_Rank.Text = "0";
                    TextBlock_PlayedTime.Text = "0";

                    ListBox_GameTools_Stats.Items.Clear();
                    ListBox_GameTools_Weapons.Items.Clear();
                    ListBox_GameTools_Vehicles.Items.Clear();

                    MessageBox.Show($"查询玩家（{nameStr}）战绩失败 {result}", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了未知错误\n\n {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private async void SearchServers()
        {
            try
            {
                string nameStr = TextBox_SearchServers.Text.Trim();

                ListBox_GameTools_Servers.Items.Clear();
                ListBox_GameTools_Servers.Items.Add(new ListBoxServerInfo
                {
                    ServerName = "正在搜索服务器中..."
                });

                string result = await GameToolsApi.GetServers(nameStr);
                if (!result.Contains("\"servers\":[],"))
                {
                    GetServers getServers = JsonSerializer.Deserialize<GetServers>(result);

                    ListBox_GameTools_Servers.Items.Clear();

                    Random rd = new Random();

                    foreach (var item in getServers.servers)
                    {
                        ListBox_GameTools_Servers.Items.Add(new ListBoxServerInfo()
                        {
                            ServerImage = item.url,
                            ServerName = item.prefix,
                            ServerMode = $"{item.mode} - {item.currentMap} - 60HZ",
                            ServerPlayerCount = $"{item.serverInfo} [{item.inQue}]",
                            ServerPing = $"{rd.Next(35, 65)}"
                        });
                    }
                }
                else
                {
                    ListBox_GameTools_Servers.Items.Clear();

                    MessageBox.Show($"搜索（{nameStr}）服务器失败 {result}", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生了未知错误\n\n {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private string GetKillStar(int kills)
        {
            if (kills < 100)
            {
                return "";
            }
            else
            {
                int count = kills / 100;

                return $"★ {count}";
            }
        }

        private void UpdateBackground()
        {
            ImageBrush b = new ImageBrush();
            if (ImageCount < 3)
            {
                ImageCount++;
            }
            else
            {
                ImageCount = 0;
            }

            switch (ImageCount)
            {
                case 0:
                    b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/Backgrounds/MP_Beachhead_BGLoop-4f04c02e.jpg"));
                    b.Stretch = Stretch.Fill;
                    break;
                case 1:
                    b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/Backgrounds/MP_Harbor_BGLoop-70b9c5f4.jpg"));
                    b.Stretch = Stretch.Fill;
                    break;
                case 2:
                    b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/Backgrounds/MP_Naval_BGLoop-dd6e9a89.jpg"));
                    b.Stretch = Stretch.Fill;
                    break;
                case 3:
                    b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Image/Backgrounds/MP_Ridge_BGLoop-22333a03.jpg"));
                    b.Stretch = Stretch.Fill;
                    break;
            }

            Window_Main.Background = null;
            Window_Main.Background = b;
        }

        private void Button_UpdateImage_Click(object sender, RoutedEventArgs e)
        {
            UpdateBackground();
        }

        private void Button_QueryRecord_Click(object sender, RoutedEventArgs e)
        {
            QueryRecord();
        }

        private void Button_SearchServers_Click(object sender, RoutedEventArgs e)
        {
            SearchServers();
        }

        private void TextBox_PlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                QueryRecord();
            }
        }

        private void TextBox_SearchServers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchServers();
            }
        }
    }
}
