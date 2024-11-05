using Identity.Domain.Seed;
using Identity.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;

namespace MarketToolsV3.Users.UnitTests.Tests.Infrastructure.Repositories
{
    public class EventRepositoryTests
    {
        private Mock<IMediator> _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(5)]
        public void ClearNotifications_AddNotifications_Return0Notifications(int quantity)
        {
            EventRepository eventRepository = new(_mediator.Object);

            for (int i = 0; i < quantity; i++)
            {
                INotification notification = new Mock<INotification>().Object;
                eventRepository.AddNotification(notification);
            }

            eventRepository.ClearNotifications();

            Assert.That(eventRepository.Notifications, Has.Count.EqualTo(0));
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(5)]
        public void AddNotifications_AddQuantity_ReturnQuantityNotifications(int quantity)
        {
            EventRepository eventRepository = new(_mediator.Object);

            for (int i = 0; i < quantity; i++)
            {
                INotification notification = new Mock<INotification>().Object;
                eventRepository.AddNotification(notification);
            }

            Assert.That(eventRepository.Notifications, Has.Count.EqualTo(quantity));
        }

        [TestCaseSource(nameof(CreateNotificationForRemoveNotification))]
        public void RemoveNotification_AddNotification_ExpectNotContains(INotification notification)
        {
            EventRepository eventRepository = new(_mediator.Object);
            eventRepository.AddNotification(notification);
            eventRepository.RemoveNotification(notification);

            Assert.That(eventRepository.Notifications, Does.Not.Contain(notification));
        }

        [TestCase(4)]
        [TestCase(8)]
        [TestCase(10)]
        public async Task PublishAllAsync_AddQuantityNotifications_CallPublishQuantityTimes(int quantity)
        {
            EventRepository eventRepository = new(_mediator.Object);

            for (int i = 0; i < quantity; i++)
            {
                INotification notification = new Mock<INotification>().Object;
                eventRepository.AddNotification(notification);
            }

            await eventRepository.PublishAllAsync();

            _mediator.Verify(m => m.Publish(It.IsAny<INotification>(), default), Times.Exactly(quantity));
        }

        [TestCase(4)]
        [TestCase(8)]
        [TestCase(10)]
        public async Task PublishAllAsync_AddQuantityNotifications_Expect0Notifications(int quantity)
        {
            EventRepository eventRepository = new(_mediator.Object);

            for (int i = 0; i < quantity; i++)
            {
                INotification notification = new Mock<INotification>().Object;
                eventRepository.AddNotification(notification);
            }

            await eventRepository.PublishAllAsync();

            Assert.That(eventRepository.Notifications, Has.Count.EqualTo(0));
        }

        private static IEnumerable<TestCaseData> CreateNotificationForRemoveNotification()
        {
            yield return new TestCaseData(new Mock<INotification>().Object);
        }
    }
}
