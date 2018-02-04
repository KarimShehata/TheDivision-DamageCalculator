using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace UiTestApp
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Private Fields

        private double _baseBulletDamage;

        private string _baseBulletDamageString;
        private double _criticalHitChance;
        private string _criticalHitChanceString;
        private double _criticalHitDamage;
        private string _criticalHitDamageString;

        private int _firearms;

        private double _firepower;

        private double _headshotDamage;
        private string _headshotDamageString;
        private int _baseMagazineSize;
        private double _reloadSpeed;
        private string _reloadSpeedString;
        private int _rpm;

        private double _timeToReload;

        private string _timeToReloadString;
        private int _weaponDamage;

        private double _weaponScaler;
        private string _weaponScalerString;

        private Type _weaponType;
        private Variant _weaponVariant;
        private double _x;
        private string _xString;
        private float decimals = 1000000;
        private double _magazineSizeBonus;
        private string _magazineSizeBonusString;

        #endregion Private Fields

        #region Public Constructors

        public MainWindowViewModel()
        {

            var typesTableAdapter = new Database1DataSetTableAdapters.TypesTableAdapter();
            TypesDataTable = typesTableAdapter.GetData();

            WeaponTypes = new ObservableCollection<Type>(TypesDataTable.Select(typesRow => new Type(typesRow)));

            var variantsTableAdapter = new Database1DataSetTableAdapters.VariantsTableAdapter();
            VariantsDataTable = variantsTableAdapter.GetData();

            var weaponsTableAdapter = new Database1DataSetTableAdapters.WeaponsTableAdapter();
            WeaponsDataTable = weaponsTableAdapter.GetData();

            var weapons = WeaponsDataTable.Select(weaponsRow => new Weapon(weaponsRow)).ToList();

            BaseBulletDamageString = "10";
            XString = "0,6";
            Firearms = 1000;
            ReloadSpeedString = "0";
            HeadshotDamageString = "0";
            CriticalHitChanceString = "0,0";
            CriticalHitDamageString = "0,0";
            MagazineSizeBonusString = "0,0";

            PropertyChanged += OnPropertyChanged;
        }

        #endregion Public Constructors

        #region Public Properties

        public double BaseBulletDamage
        {
            get { return _baseBulletDamage; }
            set
            {
                _baseBulletDamage = value;
                OnPropertyChanged(nameof(BaseBulletDamage));
            }
        }

        public string BaseBulletDamageString
        {
            get { return _baseBulletDamageString; }
            set
            {
                _baseBulletDamageString = value;
                OnPropertyChanged(nameof(BaseBulletDamageString));

                double number;
                if (!double.TryParse(value, out number)) return;

                if (Math.Abs(BaseBulletDamage - number) > Tolerance)
                    BaseBulletDamage = number;
            }
        }

        public ICommand CalculateFirepowerButtonCommand => new CommandHandler(Recalculate);

        public double CriticalHitChance
        {
            get { return _criticalHitChance; }
            set
            {
                _criticalHitChance = value;
                OnPropertyChanged(nameof(CriticalHitChance));
            }
        }

        public string CriticalHitChanceString
        {
            get { return _criticalHitChanceString; }
            set
            {
                _criticalHitChanceString = value;
                OnPropertyChanged(nameof(CriticalHitChanceString));

                double number;
                if (!double.TryParse(value, out number)) return;

                if (Math.Abs(CriticalHitChance - number) > Tolerance)
                    CriticalHitChance = number;
            }
        }

        public double CriticalHitDamage
        {
            get { return _criticalHitDamage; }
            set
            {
                _criticalHitDamage = value;
                OnPropertyChanged(nameof(CriticalHitDamage));
            }
        }

        public string CriticalHitDamageString
        {
            get { return _criticalHitDamageString; }
            set
            {
                _criticalHitDamageString = value;
                OnPropertyChanged(nameof(CriticalHitDamageString));

                double number;
                if (!double.TryParse(value, out number)) return;

                if (Math.Abs(CriticalHitDamage - number) > Tolerance)
                    CriticalHitDamage = number;
            }
        }

        public double MagazineSizeBonus
        {
            get { return _magazineSizeBonus; }
            set
            {
                _magazineSizeBonus = value;
                MagazineSizeBonusString = _magazineSizeBonus.ToString();
                OnPropertyChanged(nameof(MagazineSizeBonus));
            }
        }

        public string MagazineSizeBonusString
        {
            get { return _magazineSizeBonusString; }
            set
            {
                _magazineSizeBonusString = value;
                OnPropertyChanged(nameof(MagazineSizeBonusString));

                double number;
                if (!double.TryParse(value, out number)) return;

                if (Math.Abs(MagazineSizeBonus - number) > Tolerance)
                    MagazineSizeBonus = number;
            }
        }

        public double CycleLength => DumpTime + ReloadTime;

        public int DisplayedFirepower => (int)Firepower;

        public int DisplayedWeaponDamage => (int)WeaponDamage;

        public double Dmg => 0;

        public double DumpTime => ModifiedMagazineSize / Rps;

        public int Firearms
        {
            get { return _firearms; }
            set
            {
                _firearms = value;
                OnPropertyChanged(nameof(Firearms));
            }
        }

        public double Firepower
        {
            get { return _firepower; }
            set
            {
                _firepower = value;
                OnPropertyChanged(nameof(Firepower));
                OnPropertyChanged(nameof(DisplayedFirepower));
            }
        }

        public double HeadshotDamage
        {
            get { return _headshotDamage; }
            set
            {
                _headshotDamage = value;
                HeadshotDamageString = _headshotDamage.ToString();
                OnPropertyChanged(nameof(HeadshotDamage));
            }
        }

        public string HeadshotDamageString
        {
            get { return _headshotDamageString; }
            set
            {
                _headshotDamageString = value;
                OnPropertyChanged(nameof(HeadshotDamageString));

                double number;
                if (!double.TryParse(value, out number)) return;

                if (Math.Abs(HeadshotDamage - number) > Tolerance)
                    HeadshotDamage = number;
            }
        }

        public int BaseMagazineSize
        {
            get { return _baseMagazineSize; }
            set
            {
                _baseMagazineSize = value;
                OnPropertyChanged(nameof(BaseMagazineSize));
            }
        }

        public int ModifiedMagazineSize => (int)(BaseMagazineSize * (1 + MagazineSizeBonus));

        public double ReloadSpeed
        {
            get { return _reloadSpeed; }
            set
            {
                _reloadSpeed = value;
                OnPropertyChanged(nameof(ReloadSpeed));
            }
        }

        public string ReloadSpeedString
        {
            get { return _reloadSpeedString; }
            set
            {
                _reloadSpeedString = value;
                OnPropertyChanged(nameof(ReloadSpeedString));

                double number;
                if (!double.TryParse(value, out number)) return;

                if (Math.Abs(ReloadSpeed - number) > Tolerance)
                    ReloadSpeed = number;
            }
        }

        public double ReloadTime => TimeToReload / (1 + ReloadSpeed);

        public int Rpm
        {
            get { return _rpm; }
            set
            {
                _rpm = value;
                OnPropertyChanged(nameof(Rpm));
            }
        }

        public double Rps => Rpm / 60.0;

        public double TimeToReload
        {
            get { return _timeToReload; }
            set
            {
                _timeToReload = value;
                TimeToReloadString = _timeToReload.ToString();
                OnPropertyChanged(nameof(TimeToReload));
            }
        }

        public string TimeToReloadString
        {
            get { return _timeToReloadString; }
            set
            {
                _timeToReloadString = value;
                OnPropertyChanged(nameof(TimeToReloadString));

                double number;
                if (!double.TryParse(value, out number)) return;

                if (Math.Abs(TimeToReload - number) > Tolerance)
                    TimeToReload = number;
            }
        }

        public double Tolerance => 0.001;

        public int WeaponDamage
        {
            get { return _weaponDamage; }
            set
            {
                _weaponDamage = value;
                OnPropertyChanged(nameof(WeaponDamage));
                OnPropertyChanged(nameof(DisplayedWeaponDamage));
            }
        }

        public double WeaponScaler
        {
            get { return _weaponScaler; }
            set
            {
                _weaponScaler = value;
                WeaponScalerString = _weaponScaler.ToString();
                OnPropertyChanged(nameof(WeaponScaler));
            }
        }

        public string WeaponScalerString
        {
            get { return _weaponScalerString; }
            set
            {
                _weaponScalerString = value;
                OnPropertyChanged(nameof(WeaponScalerString));

                double number;
                if (!double.TryParse(value, out number)) return;

                if (Math.Abs(WeaponScaler - number) > Tolerance)
                    WeaponScaler = number;
            }
        }

        public Type WeaponType
        {
            get { return _weaponType; }
            set
            {
                _weaponType = value;
                OnPropertyChanged(nameof(WeaponType));
            }
        }

        public ObservableCollection<Type> WeaponTypes { get; set; }

        public Variant WeaponVariant
        {
            get { return _weaponVariant; }
            set
            {
                _weaponVariant = value;
                OnPropertyChanged(nameof(WeaponVariant));
            }
        }

        public ObservableCollection<Variant> WeaponVariants { get; set; }

        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public string XString
        {
            get { return _xString; }
            set
            {
                _xString = value;
                OnPropertyChanged(nameof(XString));

                double number;
                if (!double.TryParse(value, out number)) return;

                if (Math.Abs(X - number) > Tolerance)
                    X = number;
            }
        }

        public Database1DataSet.TypesDataTable TypesDataTable { get; set; }
        public Database1DataSet.VariantsDataTable VariantsDataTable { get; set; }
        public Database1DataSet.WeaponsDataTable WeaponsDataTable { get; set; }

        #endregion Public Properties

        #region Private Methods

        private void CalculateBaseBulletDamage()
        {
            BaseBulletDamage = Convert.ToInt32(Math.Round(Dmg - Firearms * WeaponScaler));
            Console.WriteLine($@"Base Bullet Damage Calculated: {BaseBulletDamage}");
        }

        private void CalculateFirepower()
        {
            var critDamagePart = WeaponDamage * CriticalHitChance * CriticalHitDamage;
            var headshotDamagePart = WeaponDamage * (HeadshotDamage * X);

            var scaledCycleDmg = ModifiedMagazineSize * (WeaponDamage + headshotDamagePart + critDamagePart);
            var roundendCycleLength = (int)(CycleLength * decimals) / decimals;
            Firepower = scaledCycleDmg / roundendCycleLength;
        }

        private void CalculateWeaponDamage()
        {
            var scaledFireams = Firearms * WeaponScaler;
            WeaponDamage = (int)(BaseBulletDamage + scaledFireams);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var propertyName = propertyChangedEventArgs.PropertyName;

            switch (propertyName)
            {
                case nameof(WeaponType):
                    WeaponVariants = new ObservableCollection<Variant>(from variantsRow in VariantsDataTable
                                                                       where variantsRow.Type == WeaponType.Name
                                                                       select new Variant(variantsRow));
                    OnPropertyChanged(nameof(WeaponVariants));
                    break;
                case nameof(WeaponVariant):

                    if (WeaponVariant == null) return;

                    TimeToReload = WeaponVariant.TimeToRelaod / 1000.0;
                    Rpm = WeaponVariant.Rpm;
                    BaseMagazineSize = WeaponVariant.Mag;
                    WeaponScaler = WeaponVariant.Scaler;
                    HeadshotDamage = WeaponVariant.HeadshotMultiplier - 1;

                    break;
                default:

                    break;
            }
        }

        private void Recalculate()
        {
            if (BaseBulletDamage == 0)
                CalculateBaseBulletDamage();

            CalculateWeaponDamage();
            CalculateFirepower();
        }

        #endregion Private Methods

    }
}