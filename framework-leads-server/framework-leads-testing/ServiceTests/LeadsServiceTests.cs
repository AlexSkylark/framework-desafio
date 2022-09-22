using FrameworkLeads.Data;
using FrameworkLeads.Helpers;
using FrameworkLeads.Models;
using FrameworkLeads.Services;
using FrameworkLeads.Testing.Helpers;
using Moq;

namespace FrameworkLeads.Testing.Controller
{
    public class LeadsServiceTests
    {

        Mock<ILeadsRepository> _leadsRepositoryMock = new Mock<ILeadsRepository>();

        LeadsService _service;


        [SetUp]
        public void Setup()
        {
            _service = new LeadsService(_leadsRepositoryMock.Object);
        }

        [Test]
        public async Task get_all_leads_successfully()
        {
            // arrange
            var timesCalled = 0;
            _leadsRepositoryMock.Setup(m => m.QueryAll()).ReturnsAsync(ObjectMocks.GetLeadCollection())
                .Callback(() => ++timesCalled);

            // act
            var result = await _service.GetLeads();

            // assert
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task get_lead_by_id_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsRepositoryMock.Setup(m => m.QueryById(It.IsAny<int>())).ReturnsAsync(ObjectMocks.GetLead(id))
                .Callback(() => ++timesCalled);

            // act
            var result = await _service.GetLeadByID(id);

            // assert
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        public async Task create_lead_successfully()
        {
            // arrange
            var timesCalled = 0;
            _leadsRepositoryMock.Setup(o => o.CreateNew(It.IsAny<Lead>())).ReturnsAsync(1)
                .Callback(() => ++timesCalled);

            // act
            var result = await _service.CreateNewLead(ObjectMocks.GetLead());

            // assert
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task update_lead_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsRepositoryMock.Setup(o => o.Update(It.IsAny<Lead>()))
                .Callback(() => ++timesCalled);

            // act
            await _service.UpdateLead(ObjectMocks.GetLead());

            // assert
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task delete_lead_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsRepositoryMock.Setup(o => o.Delete(It.IsAny<int>()))
                .Callback(() => ++timesCalled);

            // act + assert
            await _service.DeleteLead(id);

            // assert called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task accept_lead_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsRepositoryMock.Setup(o => o.QueryById(It.IsAny<int>())).ReturnsAsync(ObjectMocks.GetLead())
                .Callback(() => ++timesCalled);

            // act + assert no errors
            Assert.DoesNotThrowAsync(async () => await _service.UpdateLeadStatus(id, LeadStatus.ACCEPTED));

            // assert called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task reject_lead_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsRepositoryMock.Setup(o => o.QueryById(It.IsAny<int>())).ReturnsAsync(ObjectMocks.GetLead())
                .Callback(() => ++timesCalled);

            // act + assert no errors
            Assert.DoesNotThrowAsync(async () => await _service.UpdateLeadStatus(id, LeadStatus.REJECTED));

            // assert called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task accept_lead_throws_keynotfound_on_invalid_status(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsRepositoryMock.Setup(o => o.QueryById(It.IsAny<int>())).ReturnsAsync(ObjectMocks.GetLead(id, LeadStatus.ACCEPTED));

            _leadsRepositoryMock.Setup(o => o.Update(It.IsAny<Lead>()))
                .Callback(() => ++timesCalled);

            AsyncTestDelegate act = () => _service.UpdateLeadStatus(id, LeadStatus.ACCEPTED);

            // assert not called method
            Assert.That(act, Throws.TypeOf<MalformedDataException>());
            Assert.That(timesCalled, Is.EqualTo(0));
        }

        [Test]
        [TestCase(1)]
        public async Task reject_lead_throws_keynotfound_on_invalid_status(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsRepositoryMock.Setup(o => o.QueryById(It.IsAny<int>())).ReturnsAsync(ObjectMocks.GetLead(id, LeadStatus.REJECTED));

            _leadsRepositoryMock.Setup(o => o.Update(It.IsAny<Lead>()))
                .Callback(() => ++timesCalled);

            AsyncTestDelegate act = () => _service.UpdateLeadStatus(id, LeadStatus.REJECTED);

            // assert not called method
            Assert.That(act, Throws.TypeOf<MalformedDataException>());
            Assert.That(timesCalled, Is.EqualTo(0));
        }
    }
}