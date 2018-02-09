using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using UiTestApp.Factory;
using Type = UiTestApp.Factory.Type;

namespace UiTestApp
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Public Fields

        public List<string> RecalculatePropertyList = new List<string>
        {
            nameof(WeaponVariant),
            nameof(Variant.Scaler),
            nameof(Variant.Rpm),
            nameof(Variant.Mag),
            nameof(Variant.TimeToRelaod),
            nameof(BaseBulletDamage),
            nameof(Firearms),
            nameof(HeadshotDamageString),
            nameof(ReloadSpeed),
            nameof(CriticalHitChance),
            nameof(CriticalHitDamage),
            nameof(MagazineSizeBonus)
        };

        #endregion Public Fields

        #region Private Fields

        private double _baseBulletDamage;
        private string _baseBulletDamageString;
        private double _criticalHitChance;
        private string _criticalHitChanceString;
        private double _criticalHitDamage;
        private string _criticalHitDamageString;
        private int _firearms;
        private double _firepower;
        private string _headshotDamageString;
        private double _magazineSizeBonus;
        private string _magazineSizeBonusString;
        private double _reloadSpeed;
        private string _reloadSpeedString;
        private string _timeToReloadString;
        private Weapon _weapon;
        private int _weaponDamage;
        private string _weaponScalerString;
        private Type _weaponType;
        private Variant _weaponVariant;
        private double _x;
        private string _xString;
        private float decimals = 1000000;

        #endregion Private Fields

        #region Public Constructors

        public MainWindowViewModel()
        {
            Weapon = new Weapon();

            var typesTableAdapter = new Database1DataSetTableAdapters.TypesTableAdapter();
            TypesDataTable = typesTableAdapter.GetData();

            WeaponTypes = new ObservableCollection<Type>(TypesDataTable.Select(typesRow => new Type(typesRow)));

            var variantsTableAdapter = new Database1DataSetTableAdapters.VariantsTableAdapter();
            VariantsDataTable = variantsTableAdapter.GetData();

            var weaponsTableAdapter = new Database1DataSetTableAdapters.WeaponsTableAdapter();
            WeaponsDataTable = weaponsTableAdapter.GetData();

            //var weapons = WeaponsDataTable.Select(weaponsRow => new Weapon(weaponsRow)).ToList();

            BaseBulletDamageString = "100";
            XString = "0,6";
            Firearms = 100;
            ReloadSpeedString = "0,5";
            HeadshotDamageString = "1,5";
            CriticalHitChanceString = "0,5";
            CriticalHitDamageString = "0,5";
            MagazineSizeBonusString = "0,5";

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

        public ICommand CalculateButtonCommand => new CommandHandler(Recalculate);

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

        public double CycleLength => DumpTime + ReloadTime;

        public int DisplayedFirepower => (int)Firepower;

        public int DisplayedWeaponDamage => WeaponDamage;

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

        public string HeadshotDamageString
        {
            get
            {
                return _headshotDamageString;
            }
            set
            {
                _headshotDamageString = value;
                OnPropertyChanged(nameof(HeadshotDamageString));

                double number;
                var canParse = double.TryParse(value, out number);

                if (!canParse) return;

                if (Math.Abs(Weapon.Variant.HeadshotMultiplier - (1 + number)) > Tolerance)
                    Weapon.Variant.HeadshotMultiplier = 1 + number;
            }
        }

        public double MagazineSizeBonus
        {
            get { return _magazineSizeBonus; }
            set
            {
                _magazineSizeBonus = value;
                MagazineSizeBonusString = _magazineSizeBonus.ToString(CultureInfo.CurrentCulture);
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

        public int ModifiedMagazineSize => (int)(Weapon.Variant.Mag * (1 + MagazineSizeBonus));

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

        public double ReloadTime => Weapon.Variant.TimeToRelaod / 1000.0 / (1 + ReloadSpeed);

        public double Rps => Weapon.Variant.Rpm / 60.0;

        public string TimeToReloadString
        {
            get
            {
                return _timeToReloadString;
            }
            set
            {
                _timeToReloadString = value;
                OnPropertyChanged(nameof(TimeToReloadString));

                double number;
                var canParse = double.TryParse(value, out number);

                if (!canParse) return;

                if (Math.Abs(Weapon.Variant.TimeToRelaod - number * 1000) > Tolerance)
                    Weapon.Variant.TimeToRelaod = (int)(number * 1000);
            }
        }

        public double Tolerance => 0.001;

        public Weapon Weapon
        {
            get { return _weapon; }
            set
            {
                _weapon = value;

                WeaponScalerString = _weapon.Variant.Scaler.ToString(CultureInfo.CurrentCulture);
                TimeToReloadString = (_weapon.Variant.TimeToRelaod / 1000.0).ToString(CultureInfo.CurrentCulture);
                HeadshotDamageString = (_weapon.Variant.HeadshotMultiplier - 1).ToString(CultureInfo.CurrentCulture);

                OnPropertyChanged(nameof(Weapon));
            }
        }

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

        public string WeaponScalerString
        {
            get
            {
                return _weaponScalerString;
            }
            set
            {
                _weaponScalerString = value;
                OnPropertyChanged(nameof(WeaponScalerString));

                double number;
                var canParse = double.TryParse(value, out number);

                if (!canParse) return;

                if (Math.Abs(Weapon.Variant.Scaler - number) > Tolerance)
                    Weapon.Variant.Scaler = number;
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
            BaseBulletDamage = Convert.ToInt32(Math.Round(Dmg - Firearms * Weapon.Variant.Scaler));
            Console.WriteLine($@"Base Bullet Damage Calculated: {BaseBulletDamage}");
        }

        private void CalculateFirepower()
        {
            var critDamagePart = CriticalHitChance * CriticalHitDamage;
            var headshotDamagePart = (Weapon.Variant.HeadshotMultiplier - 1) * X;

            var scaledCycleDmg = ModifiedMagazineSize * (WeaponDamage * (1 + headshotDamagePart + critDamagePart));
            var roundendCycleLength = (int)(CycleLength * decimals) / decimals;
            Firepower = scaledCycleDmg / roundendCycleLength;
        }

        private void CalculateWeaponDamage()
        {
            var scaledFireams = Firearms * Weapon.Variant.Scaler;
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

                    Weapon = new Weapon(WeaponVariant.Clone());

                    Recalculate();

                    break;
            }
        }

        private void Recalculate()
        {
            if (Math.Abs(BaseBulletDamage) < 0.01)
                CalculateBaseBulletDamage();

            CalculateWeaponDamage();
            CalculateFirepower();
        }

        #endregion Private Methods

    }
}