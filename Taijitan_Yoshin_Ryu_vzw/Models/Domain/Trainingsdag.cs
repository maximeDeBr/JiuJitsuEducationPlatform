﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan_Yoshin_Ryu_vzw.Models.Domain {
    public class Trainingsdag {
        #region Properties
        public int Id { get; set; } //Autogenerated
        public int DagNummer { get; set; }
        public string DagNaam { get; set; }
        public string BeginUur { get; set; }
        public string EindUur { get; set; }
        #endregion

        #region Collections
        public virtual ICollection<FormuleTrainingsdag> FormuleTrainingsdagen{ get; private set; }
        public IEnumerable<Formule> Formules => FormuleTrainingsdagen.Select(f => f.Formule);
        #endregion

        #region Constructors
        public Trainingsdag() {
            FormuleTrainingsdagen = new HashSet<FormuleTrainingsdag>();
        }

        public Trainingsdag(int dagNummer, string dagNaam, string beginUur, string eindUur) : this() {
            DagNummer = dagNummer;
            BeginUur = beginUur;
            EindUur = eindUur;
            DagNaam = dagNaam;
        }
        #endregion

        #region Methods
        //public void AddFormule(Formule f) {
        //    FormuleTrainingsdagen.Add(new FormuleTrainingsdag(f, this));
        //}

        
        public DateTime geefDatumBeginUur(){
            return geefDatumUur(BeginUur);
        }
        public DateTime geefDatumEindUur()
        {
            return geefDatumUur(EindUur);
        }
        private DateTime geefDatumUur(string uur)
        {
            return DateTime.ParseExact(uur, "H:mm", null, System.Globalization.DateTimeStyles.None);
        }
        #endregion
    }
}
