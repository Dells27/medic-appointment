using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Interfaces
{
    public interface IEmailService
    {

        Task SendEmailAsync(string ToEmail, string Subject, string HtmlBody);
    }

    //Contract for notification history
    public interface INotificationRepository
    {
        Task AddAsync(Notification Notification);
        Task SaveChangeAsync();
    }
}
