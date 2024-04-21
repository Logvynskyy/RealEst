using RealEst.Core.Constants;
using RealEst.Core.Models;
using RealEst.Core.Models.EnumModels;

namespace RealEst.DataAccess
{
    public class EntitiesSeeder
    {
        public static void EnsureSeeded(ApplicationContext context)
        {
            var units = new List<Unit>()
                {
                    new Unit("unit1", "address1", (int)UnitTypes.Room, 15),
                    new Unit("unit2", "address2", UnitTypes.Apartment, 60),
                    new Unit("unit3", "address3", UnitTypes.House, 100, new List<Defect>()
                    {
                        new Defect(0, "def1", "desc1", DefectTypes.InnerCosmetic)
                    }
                    )
                };

            var tennant = new Tennant("name1", "surname1", "email1");

            if (!context.Units.Any())
            {
                //var units = new List<Unit>()
                //{
                //    new Unit("unit1", "address1", (int)UnitTypes.Room, 15),
                //    new Unit("unit2", "address2", UnitTypes.Apartment, 60),
                //    new Unit("unit3", "address3", UnitTypes.House, 100, new List<Defect>()
                //    {
                //        new Defect(0, "def1", "desc1", DefectTypes.InnerCosmetic)
                //    }
                //    )
                //};

                context.Units.AddRange(units);
                context.SaveChanges();
            }
            
            if(!context.Tennants.Any())
            {
                context.Tennants.Add(tennant);
                context.SaveChanges();
            }

            if(!context.Contracts.Any())
            {
                var contracts = new List<Contract>()
                {
                    new Contract("contract1", units[0], "abcd1", tennant, 1000, new DateTime(), new DateTime()),
                    new Contract("contract2", units[1], "abcd2", tennant, 1500, new DateTime(), new DateTime()),
                    new Contract("contract3", units[2], "abcd3", tennant, 2000, new DateTime(), new DateTime())
                };

                context.Contracts.AddRange(contracts);
                context.SaveChanges();
            }

            if (!context.Contacts.Any())
            {
                var contacts = new List<Contact>()
                {
                    new Contact("contact1", "lastname1", "email1", "123", ContactTypes.Private, 2),
                    new Contact("contact2", "lastname2", "email2", "456", ContactTypes.Company, 2),
                    new Contact("contact3", "lastname3", "email3", "789", ContactTypes.RentingWebsite, 1)
                };

                context.Contacts.AddRange(contacts);
                context.SaveChanges();
            }

            if(!context.UnitTypes.Any())
            {
                var unitTypes = new List<UnitType>()
                {
                    new UnitType(UnitTypes.Room),
                    new UnitType(UnitTypes.Apartment),
                    new UnitType(UnitTypes.House),
                    new UnitType(UnitTypes.Garage),
                    new UnitType(UnitTypes.Storage)
                };

                context.UnitTypes.AddRange(unitTypes);
                context.SaveChanges();
            }

            if(!context.ContactTypes.Any())
            {
                var contactTypes = new List<ContactType>()
                {
                    new ContactType(ContactTypes.Private),
                    new ContactType(ContactTypes.Company),
                    new ContactType(ContactTypes.RentingWebsite)
                };

                context.ContactTypes.AddRange(contactTypes);
                context.SaveChanges();
            }

            if (!context.DefectTypes.Any())
            {
                var defectTypes = new List<DefectType>()
                {
                    new DefectType(DefectTypes.InnerCosmetic),
                    new DefectType(DefectTypes.InnerInfrastructural),
                    new DefectType(DefectTypes.OuterCosmetic),
                    new DefectType(DefectTypes.OuterInfrastructural)
                };

                context.DefectTypes.AddRange(defectTypes);
                context.SaveChanges();
            }
        }
    }
}
