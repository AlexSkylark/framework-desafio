using FrameworkLeads.Data;
using FrameworkLeads.Models;
using FrameworkLeads.Testing.Helpers;
using Moq;
using System.Data.SqlClient;

namespace FrameworkLeads.Testing.Controller
{
    public class LeadsRepositoryTests
    {

        Mock<ILeadsRepository> _leadsRepositoryMock = new Mock<ILeadsRepository>();

        LeadsRepository _repository;
        private readonly Mock<IDapperBase> _mockDapperBase = new Mock<IDapperBase>();


        [SetUp]
        public void Setup()
        {
            _repository = new LeadsRepository(_mockDapperBase.Object);
        }

        [Test]
        public async Task get_all_leads_successfully()
        {
            // arrange
            var timesCalled = 0;
            var expectedList = ObjectMocks.GetLeadCollection();
            _mockDapperBase.Setup(o => o.QueryAsync<Lead>(It.IsAny<SqlConnection>(), It.IsAny<string>())).ReturnsAsync(expectedList)
                .Callback(() => ++timesCalled);

            // act
            var result = await _repository.QueryAll();

            // assert
            Assert.That(expectedList, Is.SameAs(result));
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task get_leads_by_id_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            var expected = ObjectMocks.GetLead();
            var returnValue = new List<Lead>();
            returnValue.Add(expected);

            _mockDapperBase.Setup(o => o.QueryAsync<Lead>(It.IsAny<SqlConnection>(), It.IsAny<string>(), It.IsAny<object>())).ReturnsAsync(returnValue)
                .Callback(() => ++timesCalled);

            // act
            var result = await _repository.QueryById(id);

            // assert
            Assert.That(expected, Is.SameAs(result));
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        public async Task create_leads_successfully()
        {
            // arrange
            var timesCalled = 0;
            var lead = ObjectMocks.GetLead();
            _mockDapperBase.Setup(o => o.QuerySingleAsync<int>(It.IsAny<SqlConnection>(), It.IsAny<string>(), It.IsAny<object>())).ReturnsAsync(1)
                .Callback(() => ++timesCalled);

            // act
            await _repository.CreateNew(lead);

            // assert
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        public async Task update_leads_successfully()
        {
            // arrange
            var timesCalled = 0;
            var lead = ObjectMocks.GetLead();
            _mockDapperBase.Setup(o => o.ExecuteAsync(It.IsAny<SqlConnection>(), It.IsAny<string>(), It.Is<Lead>(p => p.Id == lead.Id))).ReturnsAsync(1)
                .Callback(() => ++timesCalled);

            // act
            await _repository.Update(lead);

            // assert
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task delete_leads_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            var lead = ObjectMocks.GetLead(id);
            _mockDapperBase.Setup(o => o.ExecuteAsync(It.IsAny<SqlConnection>(), It.IsAny<string>(), It.IsAny<object>())).ReturnsAsync(1)
                .Callback(() => ++timesCalled);

            // act
            await _repository.Delete(id);

            // assert
            Assert.That(timesCalled, Is.EqualTo(1));
        }
    }
}