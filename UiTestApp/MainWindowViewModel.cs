using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace UiTestApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
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
        private double _reloadSpeed;
        private string _reloadSpeedString;
        private int _rpm;

        private double _timeToReload;

        private string _timeToReloadString;
        private int _weaponDamage;

        private double _weaponScaler;
        private string _weaponScalerString;

        private float decimals = 1000000;
        private int _mag;

        #endregion Private Fields

        #region Public Constructors

        public MainWindowViewModel()
        {
            BaseBulletDamageString = "55076";

            TimeToReloadString = "5";
            Mag = 8;
            Rpm = 150;
            WeaponScalerString = "8";
            Firearms = 3178;
            HeadshotDamageString = "0,6";
            ReloadSpeedString = "0";
            CriticalHitChanceString = "0";
            CriticalHitDamageString = "0,4";
        }

        #endregion Public Constructors

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        public double BaseBulletDamage
        {
            get { return _baseBulletDamage; }
            set
            {
                _baseBulletDamage = value;
                Recalculate();
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

        public double CriticalHitChance
        {
            get { return _criticalHitChance; }
            set
            {
                _criticalHitChance = value;
                Recalculate();
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
                Recalculate();
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

        public int DisplayedWeaponDamage => (int)WeaponDamage;

        public double Dmg => 0;

        public double DumpTime => Mag / Rps;

        public int Firearms
        {
            get { return _firearms; }
            set
            {
                _firearms = value;
                Recalculate();
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
                Recalculate();
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

        public double ReloadSpeed
        {
            get { return _reloadSpeed; }
            set
            {
                _reloadSpeed = value;
                Recalculate();
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
                Recalculate();
                OnPropertyChanged(nameof(Rpm));
            }
        }

        public int Mag
        {
            get { return _mag; }
            set
            {
                _mag = value;
                Recalculate();
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
                Recalculate();
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
                Recalculate();
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

        public ICommand CalculateFirepowerButtonCommand => new CommandHandler(CalculateFirepower);

        #endregion Public Properties

        #region Protected Methods

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Protected Methods

        #region Private Methods

        private void CalculateBaseBulletDamage()
        {
            BaseBulletDamage = Convert.ToInt32(Math.Round(Dmg - Firearms * WeaponScaler));
            Console.WriteLine($@"Base Bullet Damage Calculated: {BaseBulletDamage}");
        }

        private void CalculateFirepower()
        {
            var critDamagePart = WeaponDamage * CriticalHitChance * CriticalHitDamage;
            var headshotDamagePart = WeaponDamage * (HeadshotDamage * 0.5);

            var scaledCycleDmg = Mag * (WeaponDamage + headshotDamagePart + critDamagePart);
            var roundendCycleLength = (int)(CycleLength * decimals) / decimals;
            Firepower = scaledCycleDmg / roundendCycleLength;
        }

        private void CalculateWeaponDamage()
        {
            var scaledFireams = Firearms * WeaponScaler;
            WeaponDamage = (int)(BaseBulletDamage + scaledFireams);
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