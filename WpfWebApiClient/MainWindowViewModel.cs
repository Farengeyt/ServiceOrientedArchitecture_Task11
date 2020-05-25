﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfWebApiClient.Helpers;
using WpfWebApiClient.Models;

namespace WpfWebApiClient
{
    public class MainWindowViewModel : Observable
    {
        private ICommand graterClickCommand;
        private ICommand lowerClickCommand;
        private ICommand inRangeClickCommand;

        private string getStudentsGraterThanInput = "0";
        private string getStudentsLowerThanInput = "5";
        private string getStudentsInRangeMin = "0";
        private string getStudentsInRangeMax = "5";

        public ObservableCollection<Student> GraterStudents { get; } = new ObservableCollection<Student>();
        public ObservableCollection<Student> LowerStudents { get; } = new ObservableCollection<Student>();
        public ObservableCollection<Student> InRangeStudents { get; } = new ObservableCollection<Student>();

        public string GetStudentsGraterThanInput
        {
            get { return getStudentsGraterThanInput; }
            set { Set(ref getStudentsGraterThanInput, value); }
        }

        public string GetStudentsLowerThanInput
        {
            get { return getStudentsLowerThanInput; }
            set { Set(ref getStudentsLowerThanInput, value); }
        }
        public string GetStudentsInRangeMin
        {
            get { return getStudentsInRangeMin; }
            set { Set(ref getStudentsInRangeMin, value); }
        }
        public string GetStudentsInRangeMax
        {
            get { return getStudentsInRangeMax; }
            set { Set(ref getStudentsInRangeMax, value); }
        }

        public ICommand GraterClickCommand => graterClickCommand ?? (graterClickCommand = new RelayCommand(GraterSubmitClick));

        public ICommand LowerClickCommand => lowerClickCommand ?? (lowerClickCommand = new RelayCommand(LowerSubmitClick));

        public ICommand InRangeClickCommand => inRangeClickCommand ?? (inRangeClickCommand = new RelayCommand(InRangeSubmitClick));



        private async void GraterSubmitClick()
        {
            float mark;
            if (Single.TryParse(GetStudentsGraterThanInput, out mark))
            {
                var temp = await ServiceProvider.GetStudentGratherThan(mark);
                GraterStudents.Clear();
                foreach (var item in temp)
                {
                    GraterStudents.Add(item);
                }
            }
        }

        private async void LowerSubmitClick()
        {
            float mark;
            if (Single.TryParse(GetStudentsLowerThanInput, out mark))
            {
                var temp = await ServiceProvider.GetStudentLowerThan(mark);
                LowerStudents.Clear();
                foreach (var item in temp)
                {
                    LowerStudents.Add(item);
                }
            }
        }

        private async void InRangeSubmitClick()
        {
            float markMin;
            float markMax;
            if (Single.TryParse(GetStudentsInRangeMin, out markMin) && Single.TryParse(GetStudentsInRangeMax, out markMax))
            {
                var temp = await ServiceProvider.GetStudentInRange(markMin, markMax);
                InRangeStudents.Clear();
                foreach (var item in temp)
                {
                    InRangeStudents.Add(item);
                }
            }
        }


    }
}
