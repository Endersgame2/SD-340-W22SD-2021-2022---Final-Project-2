using Microsoft.EntityFrameworkCore;
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

            AccountBusinessLogic = new AccountBusinessLogic(new ProjectRepository(mockContext.Object));

        }

        [DataRow(new string[] {"2813712"})]
        [DataRow(new string[] {"23182722"})]
        [TestMethod]
        public async Task CreateProjectAsync_ArrayOfStringIds_Async(string[] id)
        {
            //Act
            await AccountBusinessLogic.CreateProjectAsync(id);
            AccountBusinessLogic.

            //Assert





           
            

        }
    }
}