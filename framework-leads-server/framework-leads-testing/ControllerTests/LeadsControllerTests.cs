using FrameworkLeads.Controllers;
using FrameworkLeads.Helpers;
using FrameworkLeads.Models;
using FrameworkLeads.Services;
using FrameworkLeads.Testing.Helpers;
using Moq;

namespace FrameworkLeads.Testing.Controller
{
    public class LeadsControllerTests
    {

        Mock<ILeadsService> _leadsServiceMock = new Mock<ILeadsService>();

        LeadController _controller;


        [SetUp]
        public void Setup()
        {
            _controller = new LeadController(_leadsServiceMock.Object);
        }

        [Test]
        public async Task get_all_leads_successfully()
        {
            // arrange
            var timesCalled = 0;
            _leadsServiceMock.Setup(m => m.GetLeads()).ReturnsAsync(ObjectMocks.GetLeadCollection())
                .Callback(() => ++timesCalled);

            // act
            var result = await _controller.Get();

            // assert called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task get_lead_by_id_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsServiceMock.Setup(m => m.GetLeadByID(It.IsAny<int>())).ReturnsAsync(ObjectMocks.GetLead(id))
                .Callback(() => ++timesCalled);

            // act
            var result = await _controller.GetById(id);

            // assert called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        public async Task create_lead_successfully()
        {
            // arrange
            var timesCalled = 0;
            _leadsServiceMock.Setup(m => m.CreateNewLead(It.IsAny<Lead>())).ReturnsAsync(1)
                .Callback(() => ++timesCalled);

            // act
            var result = await _controller.Create(ObjectMocks.GetLead());

            // assert called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task update_lead_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsServiceMock.Setup(m => m.UpdateLead(It.IsAny<Lead>()))
                .Callback(() => ++timesCalled);

            // act
            await _controller.Update(id, ObjectMocks.GetLead());

            // assert called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task delete_lead_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsServiceMock.Setup(m => m.DeleteLead(It.IsAny<int>()))
                .Callback(() => ++timesCalled);

            // act
            Assert.DoesNotThrowAsync(async () => await _controller.Delete(id));

            // assert called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task accept_lead_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsServiceMock.Setup(m => m.UpdateLeadStatus(It.IsAny<int>(), It.Is<LeadStatus>(p => p == LeadStatus.ACCEPTED)))
                .Callback(() => ++timesCalled);

            // act
            await _controller.acceptLead(id);

            // assert parameters and called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task reject_lead_successfully(int id)
        {
            // arrange
            var timesCalled = 0;
            _leadsServiceMock.Setup(m => m.UpdateLeadStatus(It.IsAny<int>(), It.Is<LeadStatus>(p => p == LeadStatus.REJECTED)))
                .Callback(() => ++timesCalled);

            // act
            await _controller.rejectLead(id);

            // assert parameters and called method
            Assert.That(timesCalled, Is.EqualTo(1));
        }
    }
}