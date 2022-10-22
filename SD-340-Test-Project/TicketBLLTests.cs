using Microsoft.EntityFrameworkCore;
using Moq;
using SD_340_W22SD_2021_2022___Final_Project_2.Data;
using SD_340_W22SD_2021_2022___Final_Project_2.Data.BLL;
using SD_340_W22SD_2021_2022___Final_Project_2.Data.DAL;
using SD_340_W22SD_2021_2022___Final_Project_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD_340_Test_Project.ProjectBLLTests;

namespace SD_340_Test_Project
{
    [TestClass]
    internal class TicketBLLTests
    {
        private TicketBusinessLogic TicketBusinessLogic;

        public TicketBLLTests()
        {
            var dbContext = new Mock<ApplicationDbContext>();
            var MockTicketRepo = new Mock<TicketRepository>();
            var MockProjectRepo = new Mock<ProjectRepository>();
            var userManager = new Mock<FakeUserManager>();


            var testData = new List<Ticket>
            {
                new Ticket{Id = 1, Name = "Uloma", Comment = {new Comment(), new Comment()}},
                new Ticket{Id = 2, Name = "Adaeze"},
                new Ticket{Id = 3, Name = "Ugomma"},
                new Ticket{Id = 4, Name = "Mmachukwu"}

            }.AsQueryable();

            var mockTicketSet = new Mock<DbSet<Project>>();

            mockTicketSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockTicketSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockTicketSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            mockTicketSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Project).Returns(mockTicketSet.Object);

            TicketBusinessLogic = new TicketBusinessLogic(MockTicketRepo.Object, MockProjectRepo.Object, userManager.Object);

        }

        [DataRow(1)]
        [TestMethod]
        public async Task CommentListInTask_taskId_Async_taskCommentData(int id)
        {
            List<Comment> comment = TicketBusinessLogic.CommentsOnTask(id).ToList();


            Assert.AreEqual(2, comment.Count);
        }
    }
}
