using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace WaymarkLibrarian
{
    public partial class WaymarkPreview : UserControl, INotifyPropertyChanged
    {
        public static readonly double DefaultCenter = 100;

        public static WaymarkPreview Current { get; private set; }

        public WaymarkPreview()
        {
            Current = this;

            this.A = new NotifyPoint(this);
            this.B = new NotifyPoint(this);
            this.C = new NotifyPoint(this);
            this.D = new NotifyPoint(this);
            this.One = new NotifyPoint(this);
            this.Two = new NotifyPoint(this);
            this.Three = new NotifyPoint(this);
            this.Four = new NotifyPoint(this);

            this.InitializeComponent();
        }

        public NotifyPoint A { get; }
        public NotifyPoint B { get; }
        public NotifyPoint C { get; }
        public NotifyPoint D { get; }

        public NotifyPoint One { get; }
        public NotifyPoint Two { get; }
        public NotifyPoint Three { get; }
        public NotifyPoint Four { get; }

        private double centerX = DefaultCenter;

        public double CenterX
        {
            get => this.centerX;
            set => this.SetProperty(ref this.centerX, value);
        }

        private double centerZ = DefaultCenter;

        public double CenterZ
        {
            get => this.centerZ;
            set => this.SetProperty(ref this.centerZ, value);
        }

        public bool IsAvailable
        {
            get => this.A.IsVisible && this.B.IsVisible && this.C.IsVisible && this.D.IsVisible && this.One.IsVisible && this.Two.IsVisible && this.Three.IsVisible && this.Four.IsVisible;
            set
            {
                this.A.IsVisible = value;
                this.B.IsVisible = value;
                this.C.IsVisible = value;
                this.D.IsVisible = value;
                this.One.IsVisible = value;
                this.Two.IsVisible = value;
                this.Three.IsVisible = value;
                this.Four.IsVisible = value;
            }
        }

        #region INotifyPropertyChanged

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            this.PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));

            return true;
        }

        #endregion INotifyPropertyChanged
    }

    public class NotifyPoint : INotifyPropertyChanged
    {
        public WaymarkPreview Parent { get; private set; }

        public NotifyPoint(
            WaymarkPreview parent)
        {
            this.Parent = parent;

            parent.PropertyChanged += (_, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(WaymarkPreview.CenterX):
                    case nameof(WaymarkPreview.CenterZ):
                        this.RaisePropertyChanged(nameof(this.Point));
                        break;
                }
            };
        }

        private double x;

        public double X
        {
            get => this.x;
            set
            {
                if (this.SetProperty(ref this.x, value))
                {
                    this.RaisePropertyChanged(nameof(this.Point));
                }
            }
        }

        private double z;

        public double Z
        {
            get => this.z;
            set
            {
                if (this.SetProperty(ref this.z, value))
                {
                    this.RaisePropertyChanged(nameof(this.Point));
                }
            }
        }

        public System.Windows.Point Point => new System.Windows.Point(
            this.X + (WaymarkPreview.DefaultCenter - this.Parent.CenterX),
            this.Z + (WaymarkPreview.DefaultCenter - this.Parent.CenterZ));

        private bool isVisible;

        public bool IsVisible
        {
            get => this.isVisible;
            set => this.SetProperty(ref this.isVisible, value);
        }

        #region INotifyPropertyChanged

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            this.PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));

            return true;
        }

        #endregion INotifyPropertyChanged
    }
}
