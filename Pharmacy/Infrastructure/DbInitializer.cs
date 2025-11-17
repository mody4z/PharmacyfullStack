using Microsoft.AspNetCore.Identity;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Infrastructure
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(PharmacyDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Check if database already has data
            if (context.Clients.Any())
            {
                return; // Database has been seeded
            }

            // Seed Clients
            var clients = new[]
            {
                new Client
                {
                    Name = "Ahmed Ali",
                    PhoneNumber = "01012345678",
                    Email = "ahmed.ali@gmail.com",
                    Address = "123 Cairo Street, Cairo",
                    CreatedDate = DateTime.Now.AddDays(-30)
                },
                new Client
                {
                    Name = "Sara Mohamed",
                    PhoneNumber = "01098765432",
                    Email = "sara.m@yahoo.com",
                    Address = "456 Alexandria Road, Alexandria",
                    CreatedDate = DateTime.Now.AddDays(-20)
                },
                new Client
                {
                    Name = "Omar Hassan",
                    PhoneNumber = "01155667788",
                    Email = "omar.hassan@hotmail.com",
                    Address = "789 Giza Avenue, Giza",
                    CreatedDate = DateTime.Now.AddDays(-15)
                },
                new Client
                {
                    Name = "Mona Samir",
                    PhoneNumber = "01223344556",
                    Email = "mona.samir@outlook.com",
                    Address = "321 Mansoura Street, Mansoura",
                    CreatedDate = DateTime.Now.AddDays(-10)
                },
                new Client
                {
                    Name = "Khaled Ibrahim",
                    PhoneNumber = "01087654321",
                    Email = "khaled.ib@gmail.com",
                    Address = "654 Aswan Road, Aswan",
                    CreatedDate = DateTime.Now.AddDays(-5)
                }
            };
            context.Clients.AddRange(clients);
            context.SaveChanges();

            // Seed Employees
            var employees = new[]
            {
                new Employee
                {
                    Name = "Dr. Heba Mahmoud",
                    Position = "Chief Pharmacist",
                    PhoneNumber = "01011122233",
                    Email = "heba.m@pharmacy.com",
                    Salary = 15000.00m,
                    HireDate = DateTime.Now.AddYears(-3)
                },
                new Employee
                {
                    Name = "Mohamed Youssef",
                    Position = "Senior Pharmacist",
                    PhoneNumber = "01099988877",
                    Email = "mohamed.y@pharmacy.com",
                    Salary = 12000.00m,
                    HireDate = DateTime.Now.AddYears(-2)
                },
                new Employee
                {
                    Name = "Fatma Ahmed",
                    Position = "Pharmacist",
                    PhoneNumber = "01066677788",
                    Email = "fatma.a@pharmacy.com",
                    Salary = 9000.00m,
                    HireDate = DateTime.Now.AddYears(-1)
                },
                new Employee
                {
                    Name = "Tarek Fahmy",
                    Position = "Pharmacy Assistant",
                    PhoneNumber = "01044455566",
                    Email = "tarek.f@pharmacy.com",
                    Salary = 6000.00m,
                    HireDate = DateTime.Now.AddMonths(-6)
                },
                new Employee
                {
                    Name = "Nour El-Din",
                    Position = "Inventory Manager",
                    PhoneNumber = "01033344455",
                    Email = "nour.eldin@pharmacy.com",
                    Salary = 10000.00m,
                    HireDate = DateTime.Now.AddMonths(-8)
                }
            };
            context.Employees.AddRange(employees);
            context.SaveChanges();

            // Seed Medicines
            var medicines = new[]
            {
                new Medicine
                {
                    Name = "Paracetamol 500mg",
                    Description = "Pain reliever and fever reducer",
                    Category = "Pain Relief",
                    Price = 15.50m,
                    StockQuantity = 500,
                    ExpiryDate = DateTime.Now.AddYears(2),
                    Manufacturer = "Pharco Pharmaceuticals"
                },
                new Medicine
                {
                    Name = "Amoxicillin 500mg",
                    Description = "Antibiotic for bacterial infections",
                    Category = "Antibiotics",
                    Price = 45.00m,
                    StockQuantity = 200,
                    ExpiryDate = DateTime.Now.AddYears(1).AddMonths(6),
                    Manufacturer = "EIPICO"
                },
                new Medicine
                {
                    Name = "Omeprazole 20mg",
                    Description = "Treats stomach acid and ulcers",
                    Category = "Gastrointestinal",
                    Price = 35.75m,
                    StockQuantity = 150,
                    ExpiryDate = DateTime.Now.AddYears(1).AddMonths(8),
                    Manufacturer = "Hikma Pharma"
                },
                new Medicine
                {
                    Name = "Aspirin 100mg",
                    Description = "Blood thinner and pain relief",
                    Category = "Cardiovascular",
                    Price = 12.00m,
                    StockQuantity = 300,
                    ExpiryDate = DateTime.Now.AddYears(2).AddMonths(3),
                    Manufacturer = "Sanofi Egypt"
                },
                new Medicine
                {
                    Name = "Insulin Glargine",
                    Description = "Long-acting insulin for diabetes",
                    Category = "Diabetes",
                    Price = 250.00m,
                    StockQuantity = 50,
                    ExpiryDate = DateTime.Now.AddMonths(10),
                    Manufacturer = "Novo Nordisk"
                },
                new Medicine
                {
                    Name = "Vitamin D3 1000 IU",
                    Description = "Vitamin D supplement",
                    Category = "Vitamins",
                    Price = 25.00m,
                    StockQuantity = 400,
                    ExpiryDate = DateTime.Now.AddYears(3),
                    Manufacturer = "Eva Pharma"
                },
                new Medicine
                {
                    Name = "Loratadine 10mg",
                    Description = "Antihistamine for allergies",
                    Category = "Allergy",
                    Price = 18.50m,
                    StockQuantity = 250,
                    ExpiryDate = DateTime.Now.AddYears(1).AddMonths(9),
                    Manufacturer = "Memphis"
                },
                new Medicine
                {
                    Name = "Metformin 500mg",
                    Description = "Type 2 diabetes medication",
                    Category = "Diabetes",
                    Price = 20.00m,
                    StockQuantity = 180,
                    ExpiryDate = DateTime.Now.AddYears(1).AddMonths(7),
                    Manufacturer = "Amoun Pharmaceutical"
                },
                new Medicine
                {
                    Name = "Cough Syrup",
                    Description = "Relieves cough symptoms",
                    Category = "Respiratory",
                    Price = 28.00m,
                    StockQuantity = 120,
                    ExpiryDate = DateTime.Now.AddYears(1),
                    Manufacturer = "Delta Pharma"
                },
                new Medicine
                {
                    Name = "Ibuprofen 400mg",
                    Description = "Anti-inflammatory pain reliever",
                    Category = "Pain Relief",
                    Price = 22.00m,
                    StockQuantity = 8, // Low stock
                    ExpiryDate = DateTime.Now.AddYears(1).AddMonths(5),
                    Manufacturer = "GlaxoSmithKline"
                }
            };
            context.Medicines.AddRange(medicines);
            context.SaveChanges();

            // Seed InOut Transactions
            var transactions = new[]
            {
                // Stock IN transactions
                new InOut
                {
                    MedicineId = 1, // Paracetamol
                    EmployeeId = 5, // Nour El-Din (Inventory Manager)
                    TransactionType = "In",
                    Quantity = 200,
                    TransactionDate = DateTime.Now.AddDays(-25),
                    Notes = "Initial stock purchase"
                },
                new InOut
                {
                    MedicineId = 2, // Amoxicillin
                    EmployeeId = 5,
                    TransactionType = "In",
                    Quantity = 100,
                    TransactionDate = DateTime.Now.AddDays(-20),
                    Notes = "Monthly restock"
                },
                new InOut
                {
                    MedicineId = 5, // Insulin
                    EmployeeId = 5,
                    TransactionType = "In",
                    Quantity = 30,
                    TransactionDate = DateTime.Now.AddDays(-15),
                    Notes = "Special order"
                },
                
                // Stock OUT transactions (Sales)
                new InOut
                {
                    MedicineId = 1, // Paracetamol
                    EmployeeId = 2, // Mohamed Youssef
                    ClientId = 1, // Ahmed Ali
                    TransactionType = "Out",
                    Quantity = 2,
                    TransactionDate = DateTime.Now.AddDays(-12),
                    Notes = "Customer purchase"
                },
                new InOut
                {
                    MedicineId = 3, // Omeprazole
                    EmployeeId = 1, // Dr. Heba
                    ClientId = 2, // Sara Mohamed
                    TransactionType = "Out",
                    Quantity = 1,
                    TransactionDate = DateTime.Now.AddDays(-10),
                    Notes = "Prescription sale"
                },
                new InOut
                {
                    MedicineId = 2, // Amoxicillin
                    EmployeeId = 3, // Fatma Ahmed
                    ClientId = 3, // Omar Hassan
                    TransactionType = "Out",
                    Quantity = 3,
                    TransactionDate = DateTime.Now.AddDays(-8),
                    Notes = "Prescription sale"
                },
                new InOut
                {
                    MedicineId = 6, // Vitamin D3
                    EmployeeId = 4, // Tarek Fahmy
                    ClientId = 4, // Mona Samir
                    TransactionType = "Out",
                    Quantity = 2,
                    TransactionDate = DateTime.Now.AddDays(-5),
                    Notes = "Customer purchase"
                },
                new InOut
                {
                    MedicineId = 4, // Aspirin
                    EmployeeId = 2,
                    ClientId = 5, // Khaled Ibrahim
                    TransactionType = "Out",
                    Quantity = 1,
                    TransactionDate = DateTime.Now.AddDays(-3),
                    Notes = "Customer purchase"
                },
                new InOut
                {
                    MedicineId = 7, // Loratadine
                    EmployeeId = 1,
                    ClientId = 1,
                    TransactionType = "Out",
                    Quantity = 1,
                    TransactionDate = DateTime.Now.AddDays(-2),
                    Notes = "Allergy medication"
                },
                new InOut
                {
                    MedicineId = 10, // Ibuprofen (low stock)
                    EmployeeId = 3,
                    ClientId = 2,
                    TransactionType = "Out",
                    Quantity = 2,
                    TransactionDate = DateTime.Now.AddDays(-1),
                    Notes = "Customer purchase"
                }
            };
            context.InOuts.AddRange(transactions);
            context.SaveChanges();

            // Seed default users for employees
            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "heba.m@pharmacy.com",
                EmployeeId = employees[0].Id // Dr. Heba Mahmoud
            };
            await userManager.CreateAsync(admin, "Admin@123");

            var pharmacist1 = new ApplicationUser
            {
                UserName = "mohamed.y",
                Email = "mohamed.y@pharmacy.com",
                EmployeeId = employees[1].Id // Mohamed Youssef
            };
            await userManager.CreateAsync(pharmacist1, "Pharma@123");

            var pharmacist2 = new ApplicationUser
            {
                UserName = "fatma.a",
                Email = "fatma.a@pharmacy.com",
                EmployeeId = employees[2].Id // Fatma Ahmed
            };
            await userManager.CreateAsync(pharmacist2, "Pharma@123");
        }
    }
}
