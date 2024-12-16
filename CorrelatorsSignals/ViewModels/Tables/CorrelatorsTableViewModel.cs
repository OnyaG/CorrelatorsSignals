using Database;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace CorrelatorsSignals.ViewModels.Tables
{
    public class CorrelatorsTableViewModel : NotifyPropertyChanged, ITableViewModel
    {
        private List<Correlator> items = new List<Correlator>();
        public ObservableCollection<Correlator> Items { get; } = new ObservableCollection<Correlator>();
        
        private List<Correlator> addedCorrelators = new List<Correlator>();

        private List<Correlator> editedCorrelators = new List<Correlator>();

        private List<Correlator> deletedCorrelators = new List<Correlator>();

        public CorrelatorsTableViewModel()
        {
            Refresh();
        }

        Correlator selectedItem = new Correlator();

        string ipString = @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$";

        public Correlator SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem != value && value!=null)
                {
                    selectedItem = value;
                    editedCorrelators.Add(selectedItem);
                }
            }
        }
        public void Create()
        {
            var newCorrelator = new Correlator();
            addedCorrelators.Add(newCorrelator);
            Items.Add(newCorrelator);
        }

        public void Refresh()
        {
            using (SignalsCorrelatorsContext db = new SignalsCorrelatorsContext())
            {
                Items.Clear();
                foreach (var vm in db.Correlators)
                {
                    Items.Add(vm);
                }

                SelectedItem = Items.FirstOrDefault();
            }
        }

        public void Save()
        {
            using (SignalsCorrelatorsContext db = new SignalsCorrelatorsContext())
            {

                var useless = addedCorrelators.Intersect(deletedCorrelators).ToArray();

                foreach (var entry in useless)
                {
                    deletedCorrelators.Remove(entry);
                    editedCorrelators.Remove(entry);
                    addedCorrelators.Remove(entry);
                }

                useless = addedCorrelators.Intersect(editedCorrelators).ToArray();

                foreach (var entity in useless)
                {
                    editedCorrelators.Remove(entity);
                }

                useless = editedCorrelators.Intersect(deletedCorrelators).ToArray();

                foreach (var entity in useless)
                {
                    editedCorrelators.Remove(entity);
                }


                foreach (var added in addedCorrelators)
                {
                    if (IsValidValue(added))
                        db.Correlators.Add(added);
                }
                foreach (var edited in editedCorrelators)
                {
                    if (IsValidValue(edited))
                        db.Correlators.Update(edited);
                }

                db.Correlators.RemoveRange(deletedCorrelators);
                db.SaveChanges();

            }

            addedCorrelators.Clear();
            editedCorrelators.Clear();
            deletedCorrelators.Clear();
            Refresh();
        }

        public void Delete()
        {
            deletedCorrelators.Add(SelectedItem);
            Items.Remove(SelectedItem);
        }


        private bool IsValidValue(Correlator correlator)
        {
            if (correlator.ComputingSpeed >= 0 &&
                        correlator.CopySpeed >= 0 &&
                        Regex.IsMatch(correlator.IpAddress, ipString))
                return true;
            
            return false;
        }
    }
}
