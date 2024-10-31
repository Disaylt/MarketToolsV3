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
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(5)]
        public void ClearNotifications_AddNotifications_Expected0(int quantity)
        {
            IEventRepository eventRepository = new EventRepository();

            for (int i = 0; i < quantity; i++)
            {
                INotification notification = new Mock<INotification>().Object;
                eventRepository.AddNotification(notification);
            }

            eventRepository.ClearNotifications();

            Assert.That(eventRepository.Notifications.Count, Is.EqualTo(0));
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(5)]
        public void AddNotifications_AddQuantity_ExpectedQuantityFromCase(int quantity)
        {
            IEventRepository eventRepository = new EventRepository();

            for (int i = 0; i < quantity; i++)
            {
                INotification notification = new Mock<INotification>().Object;
                eventRepository.AddNotification(notification);
            }

            Assert.That(eventRepository.Notifications.Count, Is.EqualTo(quantity));
        }

        [TestCaseSource(nameof(CreateNotificationForRemoveNotification))]
        public void RemoveNotification_AddNotification_ExpectedNotContainsCase(INotification notification)
        {
            IEventRepository eventRepository = new EventRepository();
            eventRepository.AddNotification(notification);
            eventRepository.RemoveNotification(notification);

            Assert.IsFalse(eventRepository.Notifications.Contains(notification));
        }

        private static IEnumerable<TestCaseData> CreateNotificationForRemoveNotification()
        {
            yield return new TestCaseData(new Mock<INotification>().Object);
        }
    }
}
