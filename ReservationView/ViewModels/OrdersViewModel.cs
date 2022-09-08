using Caliburn.Micro;
using ReservationDesktop.Commands;
using ReservationDesktop.Models;
using ReservationDesktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReservationDesktop.ViewModels
{
    public class OrdersViewModel : Conductor<object>
    {
        public ICommand Search { get; set; }

        public OrdersViewModel()
        {
            Orders.Add(new Order {  Username = "Ahmadjon", FloorId = 1, RoomId = 1, StartDate = DateTime.Today.AddDays(-1), EndDate = DateTime.Today.AddDays(2)});
            Orders.Add(new Order { Username = "Asadbek", FloorId = 1, RoomId = 4, StartDate = DateTime.Today.AddDays(-1), EndDate = DateTime.Today.AddDays(2) });
            Orders.Add(new Order { Username = "Ruslan", FloorId = 3, RoomId = 1, StartDate = DateTime.Today.AddDays(-1), EndDate = DateTime.Today.AddDays(2) });
            ForSearchOrders = Orders.ToList();
            Search = new CommandBase(ExecuteSearch, CanExecuteSearch);

        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; 
                NotifyOfPropertyChange(() => SearchText);
            }
        }


        public bool CanExecuteSearch(object parameter)
        {
            return true;
        }

        public void ExecuteSearch(object parameter)
        {
            TextBox? textbox = parameter as TextBox;

            string txt = textbox.Text;

            if (txt?.Length > 2)
            {
                ForSearchOrders = Orders.Where(obj => obj.Username.Contains(txt)).ToList();
            }
            else
            {
                ForSearchOrders = Orders.ToList();
            }
        }

        private BindableCollection<Order> _orders = new BindableCollection<Order>();

        public BindableCollection<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; 
                NotifyOfPropertyChange(() => Orders);
            }
        }

        private List<Order> _forSearchOrders = new List<Order>();

        public List<Order> ForSearchOrders
        {
            get { return _forSearchOrders; }
            set { _forSearchOrders = value;
                NotifyOfPropertyChange(() => ForSearchOrders);
            }
        }


        public void MakeReservation()
        {
            MakeReservationView window = new MakeReservationView();
            window.DataContext = new MakeReservationViewModel(this);
            window.Show();
            
           
        }

        

    }
}
