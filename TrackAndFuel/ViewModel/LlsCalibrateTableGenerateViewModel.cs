using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackAndFuel.ViewModel
{
    public class LlsCalibrateTableGenerateViewModel : ViewModelBase
    {
        private int _liters = 0;
        private int _steps = 0;
        private bool _isReadyForGenerate = false;

        public int Liters
        {
            get => _liters;
            set
            {
                _liters = value;
                IsReadyForGenerate = ((_liters != 0) && (_steps != 0) && (_steps <= 30));
                OnPropertyChanged();
            }
        }
        public int Steps
        {
            get => _steps;
            set
            {
                _steps = value;
                IsReadyForGenerate = ((_liters != 0) && (_steps != 0) && (_steps <= 30));
                OnPropertyChanged();
            }
        }
        public bool IsReadyForGenerate
        {
            get => _isReadyForGenerate; 
            set
            {
                _isReadyForGenerate = value;
                OnPropertyChanged();
            }
        }
    }
}
