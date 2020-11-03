using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace WaymarkLibrarian
{
    public partial class WaymarkPreview : UserControl, INotifyPropertyChanged
    {
        public WaymarkPreview()
        {
            this.InitializeComponent();
        }

        public NotifyPoint A { get; } = new NotifyPoint();
        public NotifyPoint B { get; } = new NotifyPoint();
        public NotifyPoint C { get; } = new NotifyPoint();
        public NotifyPoint D { get; } = new NotifyPoint();

        public NotifyPoint One { get; } = new NotifyPoint();
        public NotifyPoint Two { get; } = new NotifyPoint();
        public NotifyPoint Three { get; } = new NotifyPoint();
        public NotifyPoint Four { get; } = new NotifyPoint();

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

        public System.Windows.Point Point => new System.Windows.Point(this.X, this.Z);

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
