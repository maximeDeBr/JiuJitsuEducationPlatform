﻿using Taijitan_Yoshin_Ryu_vzw.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Taijitan_Yoshin_Ryu_vzw.Data {
    public class DataInitializer {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager) {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData() {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated()) {
                //Trainingsdagen (van site gehaald)
                Trainingsdag Dinsdag = new Trainingsdag(2, "18:00", "20:00", "Dinsdag");
                Trainingsdag Woensdag = new Trainingsdag(3, "14:00", "15:30", "Woensdag");
                Trainingsdag Donderdag = new Trainingsdag(4, "18:00", "20:00", "Donderdag");
                Trainingsdag Zaterdag = new Trainingsdag(6, "10:00", "11:30", "Zaterdag");

                var trainingsdagen = new List<Trainingsdag> {
                    Dinsdag, Woensdag, Donderdag, Zaterdag
                };
                _dbContext.Trainingsdagen.AddRange(trainingsdagen);

                //Formules (van powerpoint gehaald)
                Formule DI_DO = new Formule("DI-DO");
                Formule DI_ZA = new Formule("DI_ZA");
                Formule WO_ZA = new Formule("WO_ZA");
                Formule WO = new Formule("WO");
                Formule ZA = new Formule("ZA");

                DI_DO.addTrainingsdag(Dinsdag);
                DI_DO.addTrainingsdag(Donderdag);

                DI_ZA.addTrainingsdag(Dinsdag);
                DI_ZA.addTrainingsdag(Zaterdag);

                WO_ZA.addTrainingsdag(Woensdag);
                WO_ZA.addTrainingsdag(Zaterdag);

                WO.addTrainingsdag(Woensdag);

                ZA.addTrainingsdag(Zaterdag);

                var formules = new List<Formule> {
                    DI_DO, DI_ZA, WO_ZA, WO, ZA
                };
                _dbContext.Formules.AddRange(formules);

                //Gebruikers en corresponderende ASPUsers
                //--Leden
                Gebruiker lid1 = new Lid("Username001", "maxime@gmail.com", "De Braekeleer", "Maxime", 'm',
                    new DateTime(1998, 02, 03), "België", "Gent", "Vossenlaan", 2, "Beerle", 9000,
                    091234583, 0471212121, 123456789, new DateTime(2019, 03, 05), "maxime@ouders.com", true, false, DI_ZA);
                await CreateUser(lid1.Username, lid1.Email, "P@ssword1", "Lid");

                Gebruiker lid2 = new Lid("Username002", "daan@gmail.com", "Van Vooren", "Daan", 'm',
                    new DateTime(1997, 01, 10), "België", "Gent", "Rijschootstraat", 32, "Ertvelde", 9040,
                    093447501, 0470067701, 321654789, new DateTime(2019, 03, 06), "daan@ouders.com", true, false, DI_ZA);
                await CreateUser(lid2.Username, lid2.Email, "P@ssword1", "Lid");

                Gebruiker lid3 = new Lid("Username003", "michael@gmail.com", "Vermassen", "Michael", 'm',
                    new DateTime(1997, 05, 14), "België", "Gent", "Laanstraat", 72, "Hasselt", 6547,
                    094782662, 0470477701, 324754747, new DateTime(2019, 03, 09), "Michael@ouders.com", true, true, WO);
                await CreateUser(lid3.Username, lid3.Email, "P@ssword1", "Lid");

                //--Lesgevers
                Gebruiker lesgever1 = new Lesgever("Username004", "hans@gmail.com", "Van Der Staak", "Hans", 'm',
                    new DateTime(1999, 08, 18), "België", "Gent", "Kerkstraat", 47, "Aalter", 1436,
                    0978956147, 0478945159, 159487263, new DateTime(2019, 03, 09), null, false, false);
                await CreateUser(lesgever1.Username, lesgever1.Email, "P@ssword1", "Lid");

                //--Toevoegen
                var gebruikers = new List<Gebruiker> {
                    lid1, lid2, lid3, lesgever1
                };
                _dbContext.Gebruikers.AddRange(gebruikers);

                //Alles opslaan
                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string userName, string email, string password, string role) {
            var user = new IdentityUser { UserName = userName, Email = email };
            await _userManager.CreateAsync(user, password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
        }
    }
}
