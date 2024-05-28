using ChatClient.MVVM.Core;
using ChatClient.MVVM.Model;
using ChatClient.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }

        public string Username { get; set; }
        public object Aplication { get; private set; }

        private Server server;
        public MainViewModel() {
            Users = new ObservableCollection<UserModel>();
            server = new Server();
            server.connectedEvent += UserConnected;
            ConnectToServerCommand = new RelayCommand(o => server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
        
        }

        private void UserConnected()
        {
            /*throw new NotImplementedException();*/
            var user = new UserModel
            {
                Username = server.PacketReader.ReadMessage(),
                UID = server.PacketReader.ReadMessage(),
            };

            if(!Users.Any(x=> x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }
    }
}
