using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackathonCardGenerator.Models;

namespace HackathonCardGenerator.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Participant> Participants { get; set; }

        public MainWindowViewModel()
        {
            Participants = new ObservableCollection<Participant>(new List<Participant>
            {
                new Participant{Name = "Athanasios", Surname = "Bonis", Type = "Board"}
            });
        }
    }
}
