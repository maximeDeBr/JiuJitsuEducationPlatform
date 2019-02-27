﻿using Taijitan_Yoshin_Ryu_vzw.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taijitan_Yoshin_Ryu_vzw.Models.Domain {
    public class Lesgroep {
        #region Fields
        private string _groepsnaam;
        #endregion

        #region Properties
        public string Groepsnaam {
            get => _groepsnaam;
            set {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 45)
                    throw new ArgumentException("Groepsnaam kan niet leeg zijn of meer dan 45 caracters bevatten");
                _groepsnaam = value;
            }
        }
        #endregion

        #region Collections
        public ICollection<ApplicationUserLesgroep> ApplicationUserLesgroepen { get; private set; }
        public IEnumerable<ApplicationUser> ApplicationUsers => ApplicationUserLesgroepen.Select(l => l.ApplicationUser);

        public ICollection<SessieLesgroep> SessieLesgroepen { get; private set; }
        public IEnumerable<Sessie> Sessies => SessieLesgroepen.Select(s => s.Sessie);
        #endregion

        #region Constructors
        protected Lesgroep() {
            ApplicationUserLesgroepen = new HashSet<ApplicationUserLesgroep>();
            SessieLesgroepen = new HashSet<SessieLesgroep>();
        }

        public Lesgroep(string groepsnaam) : this() {
            Groepsnaam = groepsnaam;
        }
        #endregion

        #region Methods
        public void AddApplicationUser(ApplicationUser l) {
            ApplicationUserLesgroepen.Add(new ApplicationUserLesgroep(l, this));
        }
        #endregion
    }
}
