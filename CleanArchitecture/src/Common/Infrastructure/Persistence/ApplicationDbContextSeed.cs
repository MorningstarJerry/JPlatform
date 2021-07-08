using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            var administratorRole = new IdentityRole("Administrator");
            var jabilAdminRole = new IdentityRole("admin");
            var jabilEditorRole = new IdentityRole("editor");
            var jabilApproverRole = new IdentityRole("approver");
            var jabilUserRole = new IdentityRole("user");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            if (roleManager.Roles.All(r => r.Name != jabilAdminRole.Name))
            {
                await roleManager.CreateAsync(jabilAdminRole);
            }

            if (roleManager.Roles.All(r => r.Name != jabilEditorRole.Name))
            {
                await roleManager.CreateAsync(jabilEditorRole);
            }

            if (roleManager.Roles.All(r => r.Name != jabilApproverRole.Name))
            {
                await roleManager.CreateAsync(jabilApproverRole);
            }

            if (roleManager.Roles.All(r => r.Name != jabilUserRole.Name))
            {
                await roleManager.CreateAsync(jabilUserRole);
            }

            //var defaultUser = new ApplicationUser { UserName = "iayti", Email = "test@test.com" };

            //if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            //{
            //    await userManager.CreateAsync(defaultUser, "Matech_1850");
            //    await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
            //}

            /*Add a Test User****************
            var newUser = new ApplicationUser
            {
                UserName = "Jerry",
                Email = "Jerry_Liu4765@Jabil.com",
                sap_no = "2294765",
                wc = "IE",
                factory_location = "IE",
                phone = "18588850203",
                PhoneNumber = "18588850203",
                LastName = "Liu"
            };

            var task = await userManager.CreateAsync(newUser, "Test_123456");
            if (task.Succeeded)
            {
                userManager.AddToRolesAsync(newUser, new[] { jabilUserRole.Name }).Wait();
            }
            *******************************/

            /******** sync users one time excute
            foreach (var model in context.facilityUsers.ToList())
            { 
                var newUser = new ApplicationUser { UserName = model.sap_no, Email = model.sap_no, 
                    sap_no= model.sap_no,
                    wc = model.wc, 
                    factory_location = model.factory_location,
                    phone = model.phone,
                    PhoneNumber = model.phone,
                    LastName = model.name};

                var task = await userManager.CreateAsync(newUser, "Test_123456");
                if (task.Succeeded)
                {
                    userManager.AddToRolesAsync(newUser, new[] { jabilUserRole.Name }).Wait();
                }
            }
            ***********/
        }

        public static async Task SeedSampleCityDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Cities.Any())
            {
                context.Cities.Add(new City
                {
                    Name = "Bursa",
                    Districts =
                    {
                        new District{Name = "Osmangazi"},
                        new District{Name = "Nilüfer"},
                        new District{Name = "Büyükorhan"},
                        new District{Name = "Yıldırım"},
                        new District{Name = "Gemlik"},
                        new District{Name = "Gürsu"},
                        new District{Name = "Harmancık"},
                        new District{Name = "İnegöl"},
                        new District{Name = "İznik"},
                        new District
                        {
                            Name = "Karacabey",
                            Villages =
                            {
                                new Village{Name = "Akçakoyun"},
                                new Village{Name = "Akçasusurluk"},
                                new Village{Name = "Akhisar"},
                                new Village{Name = "Arız"},
                                new Village{Name = "Bakırköy"},
                                new Village{Name = "Ballıkaya"},
                                new Village{Name = "Bayramdere"},
                                new Village{Name = "Beylik"},
                                new Village{Name = "Boğazköy"},
                                new Village{Name = "Çamlıca"},
                                new Village{Name = "Canbaz"},
                                new Village{Name = "Çarık"},
                                new Village{Name = "Çavuşköy"},
                                new Village{Name = "Çeşnigir"},
                                new Village{Name = "Dağesemen"},
                                new Village{Name = "Dağkadı"},
                                new Village{Name = "Danişment"},
                                new Village{Name = "Doğla"},
                                new Village{Name = "Ekinli"},
                                new Village{Name = "Ekmekçi"},
                                new Village{Name = "Eskikaraağaç"},
                                new Village{Name = "Eskisarıbey"},
                                new Village{Name = "Fevzipaşa"},
                                new Village{Name = "Gölecik"},
                                new Village{Name = "Gölkıyı"},
                                new Village{Name = "Gönü"},
                                new Village{Name = "Güngörmez"},
                                new Village{Name = "Hamidiye"},
                                new Village{Name = "Harmanlı"},
                                new Village{Name = "Hayırlar"},
                                new Village{Name = "Hotanlı"},
                                new Village{Name = "Hürriyet"},
                                new Village{Name = "İkizce"},
                                new Village{Name = "İnkaya"},
                                new Village{Name = "İsmetpaşa"},
                                new Village{Name = "Karakoca"},
                                new Village{Name = "Karasu"},
                                new Village{Name = "Kedikaya"},
                                new Village{Name = "Keşlik"},
                                new Village{Name = "Kıranlar"},
                                new Village{Name = "Küçükkaraağaç"},
                                new Village{Name = "Kulakpınar"},
                                new Village{Name = "Kurşunlu"},
                                new Village{Name = "Muratlı"},
                                new Village{Name = "Okçular"},
                                new Village{Name = "Örencik"},
                                new Village{Name = "Orhaniye"},
                                new Village{Name = "Ortasarıbey"},
                                new Village{Name = "Ovaesemen"},
                                new Village{Name = "Şahinköy"},
                                new Village{Name = "Şahmelek"},
                                new Village{Name = "Sazlıca"},
                                new Village{Name = "Seyran"},
                                new Village{Name = "Subaşı"},
                                new Village{Name = "Sultaniye"},
                                new Village{Name = "Taşlık"},
                                new Village{Name = "Taşpınar"},
                                new Village{Name = "Tophisar"},
                                new Village{Name = "Uluabat"},
                                new Village{Name = "Yarış"},
                                new Village{Name = "Yeni"},
                                new Village{Name = "Yenikaraağaç"},
                                new Village{Name = "Yenisarıbey"},
                                new Village{Name = "Yeşildere"},
                                new Village{Name = "Yolağzı"}
                            }
                        },
                        new District{Name = "Keles"},
                        new District{Name = "Kestel"},
                        new District{Name = "Mudanya"},
                        new District{Name = "Mustafakemalpaşa"},
                        new District{Name = "Orhaneli"},
                        new District{Name = "Orhangazi"},
                        new District{Name = "Yenişehir"}
                    }
                });

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedCompetencyDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Competency_Process.Any())
            {
                var myJsonString = File.ReadAllText("competency/HeatStake.json");
                var myJsonObject = JsonConvert.DeserializeObject<Competency.Competency>(myJsonString);

                //NewMethod(myJsonObject);

                //var myJObject = JObject.Parse(myJsonString);

                context.Competency_Process.Add(new Competency_Process
                {
                    name = "Heat Stake",
                    Categorys =
                    {
                        GetCategorys(myJsonObject,"CTQ/CTP"),
                        GetCategorys(myJsonObject,"Data Traceability"),
                        GetCategorys(myJsonObject,"Visualization"),
                        GetCategorys(myJsonObject,"Smartness")
                    },
                    Dimensions =
                    {
                        GetSmeDetails(myJsonObject,"Attitude"),
                        GetSmeDetails(myJsonObject,"Skill"),
                        GetSmeDetails(myJsonObject,"Knowledge"),
                    }
                });

                await context.SaveChangesAsync();
            }
        }

        private static Competency_Category GetCategorys(Competency.Competency myJsonObject, string categoryKey)
        {
            Competency_Category competencyCategory1 = new Competency_Category();
            competencyCategory1.name = categoryKey;
            var category1 = myJsonObject.datas.Where(x => x.category == categoryKey).ToList();
            foreach (var item in category1)
            {
                competencyCategory1.Critiras.Add(new Competency_Critira()
                {
                    name = item.critira,
                    description = item.critira,
                    gradea = item.gradea,
                    gradea_url = item.gradea_url,
                    gradeb = item.gradeb,
                    gradeb_url = item.gradeb_url,
                    gradec = item.gradec,
                    gradec_url = item.gradec_url,
                    graded = item.graded,
                    graded_url = item.graded_url,
                    current = string.IsNullOrEmpty(item.current) ? "N/A" : item.current,
                    score = item.score
                });
            }

            return competencyCategory1;
        }

        private static Competency_SmeDimension GetSmeDetails(Competency.Competency myJsonObject, string categoryKey)
        {
            Competency_SmeDimension competency_SmeDimension = new Competency_SmeDimension();
            competency_SmeDimension.name = categoryKey;
            competency_SmeDimension.code = categoryKey;
            var category1 = myJsonObject.sme.Where(x => x.Dimension == categoryKey).ToList();
            foreach (var item in category1)
            {
                competency_SmeDimension.smeDetails.Add(new Competency_SmeDetails()
                {
                    name = item.Competency,
                    code = item.Code,
                    definition = item.Dimension,
                    gradea = item.Level0,
                    gradeb = item.Level1,
                    gradec = item.Level2,
                    graded = item.Level3,
                    weight = item.Weight,
                    score = item.Score
                });
            }

            return competency_SmeDimension;
        }

        public static async Task SeedSamplesSitesDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Sites.Any())
            {
                context.Sites.AddRange(new List<Site>() { new Site("HUA"),
                    new Site("HuangPu"),
                    new Site("Wuxi"),
                    new Site("WuHan"),
                    new Site("WeiHai"),
                    new Site("ShenZhen")});

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSamplesSectorsDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Sectors.Any())
            {
                context.Sectors.AddRange(new List<Sector>() {
                    new Sector("All"),
                    new Sector("P&R"),
                    new Sector("I&E"),
                    new Sector("A&T")
                    });

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSamplesPermissionsDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Permissions.Any())
            {
                context.Permissions.AddRange(new List<Permission>() {
                    new Permission("All"),
                    new Permission("ADD"),
                    new Permission("VIEW"),
                    new Permission("DELETE"),
                    new Permission("EXPORT")
                    });

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSamplesDepartmentsDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(new List<Department>() {
                new Department("DEAULT"),
                new Department("IT"),
                new Department("Facility"),
                new Department("Finance"),
                new Department("IE"),
                new Department("LEAN"),
                new Department("MFG"),
                new Department("Material"),
                new Department("Planing"),
                new Department("TE"),
                new Department("IC"),
                new Department("SQE"),
                new Department("HR"),
                new Department("PUR"),
                new Department("ME"),
                new Department("Shipping"),
                new Department("PMO"),
                new Department("SCM"),
                new Department("CMRO"),
                new Department("EHS") });

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSamplesWcsDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Workcells.Any())
            {
                context.Workcells.AddRange(new List<Workcell>() {
                new Workcell("DEAULT"),
                new Workcell("HP"),
                new Workcell("IROBOT"),
                new Workcell("CPS"),
                new Workcell("HPLSJ"),
                new Workcell("MAKERBOT"),
                new Workcell("ZEBRA"),
                new Workcell("ELECTROLX"),
                new Workcell("EMERSON"),
                new Workcell("FPA"),
                new Workcell("GE"),
                new Workcell("INCUBATOR"),
                new Workcell("IROBOT"),
                new Workcell("LOREAL"),
                new Workcell("SOLAREDGE"),
                new Workcell("TRAEGER"),
                new Workcell("WHIRLPOOL"),
                new Workcell("XEROX"),
                new Workcell("DANAHER"),
                new Workcell("FORD"),
                new Workcell("JE"),
                new Workcell("OSRAM"),
                new Workcell("ROADTRACK"),
                new Workcell("SHARP"),
                new Workcell("TESLA"),
                new Workcell("VALEO"),
                new Workcell("UAT"),
                new Workcell("LITTLEBITS"),
                new Workcell("TTEC"),
                new Workcell("SUMUP"),
                new Workcell("FUNCTION"),

                });

                await context.SaveChangesAsync();
            }
        }


    }
}