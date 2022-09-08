using Caliburn.Micro;
using ReservationDesktop.Commands;
using ReservationDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReservationDesktop.ViewModels
{
    public class MakeReservationViewModel : Screen
    {


        public ICommand Cancel { get; set; }
        public ICommand Submit { get; set; }

        private OrdersViewModel _ordersView;
        
        public MakeReservationViewModel(OrdersViewModel ordersView)
        {
            Cancel = new CommandBase(ExecuteCancel, CanExecuteCancel);
            Submit = new CommandBase(ExecuteSubmit, CanExecuteSubmit);
            _ordersView = ordersView;
        }

        private string _username;
        private int _floorId;
        private int _roomId;
        private DateTime _startDate;
        private DateTime _endDate;



        public string Username
        {
            get { return _username; }
            set { _username = value; 
                NotifyOfPropertyChange(() => Username);
            }
        }

        

        public int FloorId
        {
            get { return _floorId; }
            set { _floorId = value; 
                NotifyOfPropertyChange(() =>FloorId);
            }
        }

        
        public int RoomId
        {
            get { return _roomId; }
            set { _roomId = value; 
                NotifyOfPropertyChange(() => RoomId);
            }
        }

        

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value;
                NotifyOfPropertyChange(() => StartDate);
            }
        }

        

        public DateTime EndDate 
        {
            get { return _endDate; }
            set { _endDate = value; 
                NotifyOfPropertyChange(() => EndDate);
            }
        }



        public bool CanExecuteCancel(object parameter)
        {
            return true;
        }

        public void ExecuteCancel(object parameter)
        {
            Username = "";
            FloorId = 0;
            RoomId = 0;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
        }

        public bool CanExecuteSubmit(object parameter)
        {
            return true;
        }

        public void ExecuteSubmit(object parameter)
        {
            Order order = new Order{ Username = Username, FloorId=FloorId, RoomId=RoomId, StartDate=StartDate, EndDate=EndDate};

            if (order.StartDate > order.EndDate)
            {
                MessageBox.Show("Conflict with date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StartDate = DateTime.MinValue;
                EndDate = DateTime.MinValue;
                return;
            }
            
            foreach(Order item in _ordersView.Orders)
            {
                if(item.RoomId == order.RoomId && item.FloorId == order.FloorId)
                {
                    MessageBox.Show("The room is occupied", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    FloorId = 0;
                    RoomId = 0;
                    return;

                }
            }

            _ordersView.Orders.Add(order);
            _ordersView.ForSearchOrders = _ordersView.Orders.ToList();

            Window wnd = parameter as Window;
            wnd?.Close();
        }
    }
}
