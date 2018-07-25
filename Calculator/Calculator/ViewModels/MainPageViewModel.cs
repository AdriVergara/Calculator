using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _entryNumToShow { get; set; }
        public int EntryNumToShow
        {
            get
            {
                return _entryNumToShow;
            }
            set
            {
                if (_entryNumToShow == value) return;
                _entryNumToShow = value;
                OnPropertyChanged(nameof(EntryNumToShow));
            }
        }

        public int FirstNum { get; set; }
        public bool Calculated { get; set; }
        public int OrderClicked { get; set; } //0 = Aggregate, 1 = Minus, 2 = Multi, 3 = Div
        public bool OperDone { get; set; }
        public int Result { get; set; }

        public ICommand AddNum { get; set; }
        public ICommand AddNumP { get; set; }
        public ICommand AC { get; set; }

        public ICommand Aggregate { get; set; }
        public ICommand Minus { get; set; }
        public ICommand Multi { get; set; }
        public ICommand Div { get; set; }
        public ICommand Calculate { get; set; }

        public MainPageViewModel()
        {
            Calculated = false;
            OperDone = false;
            FirstNum = 0;
            Result = 0;

            AddNum = new Command(async () => await ExecuteAddNum());
            AddNumP = new Command(async (Param) => await ExecuteAddNumP(Param));

            AC = new Command(async () => await ExecuteAC());

            Aggregate = new Command(async () => await ExecuteAggregate());
            Minus = new Command(async () => await ExecuteMinus());
            Multi = new Command(async () => await ExecuteMulti());
            Div = new Command(async () => await ExecuteDiv());

            Calculate = new Command(async () => await ExecuteCalculate());
        }

        private async Task ExecuteAC()
        {
            EntryNumToShow = 0;
            Result = 0;
        }

        private async Task ExecuteDiv()
        {
            FirstNum = EntryNumToShow;
            OrderClicked = 3;
            EntryNumToShow = 0;
        }

        private async Task ExecuteMulti()
        {
            FirstNum = EntryNumToShow;
            OrderClicked = 2;
            EntryNumToShow = 0;
        }

        private async Task ExecuteMinus()
        {
            FirstNum = EntryNumToShow;
            OrderClicked = 1;
            EntryNumToShow = 0;
        }

        private async Task ExecuteAggregate()
        {
            //if (!OperDone)
            //{
                //Result = FirstNum + EntryNumToShow;
                //EntryNumToShow = Result;
                
                FirstNum = EntryNumToShow;
                OrderClicked = 0;
                EntryNumToShow = 0;
                
            //    OperDone = true;
            //}
            //else
            //{
            //    FirstNum = EntryNumToShow;
            //    EntryNumToShow = 0;
            //}
        }

        private async Task ExecuteCalculate()
        {
            Calculated = true;
            OperDone = false;

            switch (OrderClicked)
            {
                case 0: //Aggregate
                    Result = FirstNum + EntryNumToShow;
                    EntryNumToShow = Result;
                    break;
                case 1: //Minus
                    EntryNumToShow = FirstNum - EntryNumToShow;
                    Result = EntryNumToShow;
                    break;
                case 2: //Multi
                    EntryNumToShow *= FirstNum;
                    Result = EntryNumToShow;
                    break;
                case 3: //Div
                    EntryNumToShow = FirstNum / EntryNumToShow;
                    Result = EntryNumToShow;
                    break;
            }
        }

        private async Task ExecuteAddNumP(object Param)
        {
            if (OperDone)
            {
                if (EntryNumToShow == 0)
                {
                    EntryNumToShow = Convert.ToInt32(Param);
                }
                else
                {
                    EntryNumToShow = (EntryNumToShow * 10) + Convert.ToInt32(Param);
                }
                return;
            }
            if (!Calculated)
            {
                if (EntryNumToShow == 0)
                {
                    EntryNumToShow = Convert.ToInt32(Param);
                }
                else
                {
                    EntryNumToShow = (EntryNumToShow * 10) + Convert.ToInt32(Param);
                }
                //OperDone = true;
            }
            else
            {
                EntryNumToShow = Convert.ToInt32(Param);

                OperDone = false;
                Calculated = false;
            }
        }

        private async Task ExecuteAddNum()
        {
            EntryNumToShow += 1;
        }
    }
}