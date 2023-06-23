using ATBM_Seminar.Models;
using ATBM_Seminar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATBM_Seminar.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModels;
        public UpdateViewCommand(MainViewModel viewModels)
        {
            this.viewModels = viewModels;
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            //Của Trưởng Phòng
            if (parameter == null)
            {
                viewModels.SelectedViewModel = new EmployeeViewModel();
            }
            else if (parameter.ToString() == "Agents")
            {
                viewModels.SelectedViewModel = new EmployeeViewModel();
            }
            else if (parameter.ToString() == "Rooms")
            {
                viewModels.SelectedViewModel = new RoomViewModel();
            }
            else if (parameter.ToString() == "DeAn")
            {
                viewModels.SelectedViewModel = new DeAnViewModel();
            }
            else if (parameter.ToString() == "PhanCong")
            {
                viewModels.SelectedViewModel = new PhanCongViewModel();
            }
            //Của trưởng đề án
            else if (parameter.ToString() == "Infor")
            {
                viewModels.SelectedViewModel = new InformationViewModel();
            }
            else if (parameter.ToString() == "TrDeAn")
            {
                viewModels.SelectedViewModel = new TrDeAnViewModel();
            }
        }
    }
}
