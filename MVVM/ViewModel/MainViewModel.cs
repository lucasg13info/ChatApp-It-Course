﻿using ChatClient.MVVM.Core;
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
        public ObservableCollection<string> Messages { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }

        public string Username { get; set; }
        public string Message { get; set; }
        public object Aplication { get; private set; }

        private Server server;
        public MainViewModel() {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();

            server = new Server();
            server.connectedEvent += UserConnected;
            server.msgReceivedEvent += MessageReceived;
            server.userDisconnectedEvent += RemoveUser; ;
            ConnectToServerCommand = new RelayCommand(o => server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));

            SendMessageCommand = new RelayCommand(o => server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));

        }

        private void RemoveUser()
        {
            var uid = server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }

        private void MessageReceived()
        {
            var msg = server.PacketReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));
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
