using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HackathonCardGenerator.Annotations;
using HackathonCardGenerator.Commands;
using HackathonCardGenerator.Helpers;
using HackathonCardGenerator.Models;

namespace HackathonCardGenerator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Participant _newParticipant;
        private ObservableCollection<Participant> _participants;

        public Participant NewParticipant
        {
            get => _newParticipant;
            set
            {
                _newParticipant = value;
                OnPropertyChanged(nameof(NewParticipant));
            }
        }

        public ObservableCollection<Participant> Participants
        {
            get => _participants;
            set
            {
                _participants = value;
                OnPropertyChanged(nameof(Participants));
            }
        }

        public ICommand AddParticipantCommand { get; set; }
        public ICommand AddParticipantsCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public MainWindowViewModel()
        {
            NewParticipant = new Participant();
            AddParticipantCommand = new CommandViewModel(AddParticipant);
            AddParticipantsCommand = new CommandViewModel(GenerateParticipants);
            ClearCommand = new CommandViewModel(ClearList);
            Participants = new ObservableCollection<Participant>();
        }

        private void ClearList()
        {
            Participants = new ObservableCollection<Participant>();
        }

        private void GenerateParticipants()
        {
            CardGenerator.Generator(Participants, "C:/cards/");
        }

        private void AddParticipant()
        {
            Participants.Add(NewParticipant);
            NewParticipant = new Participant();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
