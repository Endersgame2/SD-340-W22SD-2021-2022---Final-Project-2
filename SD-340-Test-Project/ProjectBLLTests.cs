using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SD_340_W22SD_2021_2022___Final_Project_2.Data;
using SD_340_W22SD_2021_2022___Final_Project_2.Data.BLL;
using SD_340_W22SD_2021_2022___Final_Project_2.Data.DAL;
using SD_340_W22SD_2021_2022___Final_Project_2.Models;
using System.Runtime.CompilerServices;

namespace SD_340_Test_Project
{
    [TestClass]
    public class ProjectBLLTests
    {
        private AccountBusinessLogic AccountBusinessLogic;
        
        public ProjectBLLTests()
        {
            var dbContext = new Mock<ApplicationDbContext>();
            var MockProjRepo = new Mock<ProjectRepository>();
            var userManager = new Mock<FakeUserManager>();

            var testData = new List<Project>
            {
                new Project{Id = 1, Name = "Uloma"},
                new Project{Id = 2, Name = "Adaeze"},
                new Project{Id = 3, Name = "Ugomma"},
                new Project{Id = 4, Name = "Mmachukwu"}

            }.AsQueryable();

            var mockEmployeeSet = new Mock<DbSet<Project>>();

            mockEmployeeSet.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockEmployeeSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockEmployeeSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            mockEmployeeSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Project).Returns(mockEmployeeSet.Object);

            AccountBusinessLogic = new AccountBusinessLogic(MockProjRepo.Object, dbContext.Object, userManager.Object);



        }



        [DataRow(new string[] {"2813712"})]
        [DataRow(new string[] {"23182722"})]
        [TestMethod]
        public async Task CreateProjectAsync_ArrayOfStringIds_Async_newProject(string[] id)
        {      

            //Act
            await AccountBusinessLogic.CreateProjectAsync(id);
            int projectCount = AccountBusinessLogic.repo.GetAll().Count();          
            

            //Assert
            Assert.AreEqual(projectCount, id.Length);         
            

        }

        [DataRow(1)]
        [DataRow(2)]
        [TestMethod]
        public async Task ProjectDetail_ProjectId_projectData(int id)
        {
            Project project = AccountBusinessLogic.ProjectDetails(id);

            Assert.IsNotNull(project);


        } 



        public class FakeUserManager : UserManager<ApplicationUser>
        {
            public FakeUserManager()
                : base(new Mock<IUserStore<ApplicationUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<ApplicationUser>>().Object,
                    new IUserValidator<ApplicationUser>[0],
                    new IPasswordValidator<ApplicationUser>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
            { }
        }
    }
}