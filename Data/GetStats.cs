using System.Collections.Generic;

namespace BF1RecordQuery.Data
{
    class GetStats
    {
        public string avatar { get; set; }
        public string userName { get; set; }
        public int rank { get; set; }
        public string rankImg { get; set; }
        public float skill { get; set; }
        public float scorePerMinute { get; set; }
        public float killsPerMinute { get; set; }
        public string winPercent { get; set; }
        public string bestClass { get; set; }
        public string Accuracy { get; set; }
        public string headshots { get; set; }
        public string timePlayed { get; set; }
        public float killDeath { get; set; }
        public float infantryKillDeath { get; set; }
        public float infantryKillsPerMinute { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int wins { get; set; }
        public int loses { get; set; }
        public float longestHeadShot { get; set; }
        public float revives { get; set; }
        public int dogtagsTaken { get; set; }
        public int highestKillStreak { get; set; }
        public int roundsPlayed { get; set; }
        public double awardScore { get; set; }
        public double bonusScore { get; set; }
        public double squadScore { get; set; }
        public double currentRankProgress { get; set; }
        public double totalRankProgress { get; set; }
        public int avengerKills { get; set; }
        public int saviorKills { get; set; }
        public int headShots { get; set; }
        public float heals { get; set; }
        public float repairs { get; set; }
        public float killAssists { get; set; }
        public long id { get; set; }

        public List<WeaponsItem> weapons { get; set; }
        public List<VehiclesItem> vehicles { get; set; }

        public bool cache { get; set; }
    }

    public class WeaponsItem
    {
        public string weaponName { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public int kills { get; set; }
        public double killsPerMinute { get; set; }
        public string accuracy { get; set; }
        public string headshots { get; set; }
        public double hitVKills { get; set; }
    }

    public class VehiclesItem
    {
        public string vehicleName { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public int kills { get; set; }
        public double killsPerMinute { get; set; }
        public int destroyed { get; set; }
    }

    public class ListBoxWeaponData
    {
        public string WeaponImage { get; set; }
        public string WeaponName { get; set; }
        public string WeaponKill { get; set; }
        public string WeaponKPM { get; set; }
        public string WeaponAccuracy { get; set; }
        public string WeaponHeadshots { get; set; }
        public string WeaponHitVKills { get; set; }

        public string WeaponStar { get; set; }
    }

    public class ListBoxVehicleData
    {
        public string VehicleImage { get; set; }
        public string VehicleName { get; set; }
        public string VehicleKill { get; set; }
        public string VehicleKPM { get; set; }
        public string VehicleDestroyede { get; set; }

        public string VehicleStar { get; set; }
    }
}
