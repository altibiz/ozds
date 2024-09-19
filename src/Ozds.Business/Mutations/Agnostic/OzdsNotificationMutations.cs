using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using INotification = Ozds.Business.Models.Abstractions.INotification;

namespace Ozds.Business.Mutations.Agnostic;

public class OzdsNotificationMutations(
  DataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IMutations
{
  private readonly DataDbContext _context = context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public Task SaveChangesAsync()
  {
    return _context.SaveChangesAsync();
  }

  public void ClearChanges()
  {
    _context.ChangeTracker.Clear();
  }

  public async Task CreateAsync(
    SystemNotificationModel notification,
    IEnumerable<NotificationRecipientModel> recipients)
  {
    var validationResults = notification
      .Validate(new ValidationContext(this))
      .ToList();
    if (validationResults.Count is not 0)
    {
      throw new ValidationException(validationResults.First().ErrorMessage);
    }

    var notificationEntity = _modelEntityConverter.ToEntity<NotificationEntity>(notification);
    _context.Add(notificationEntity);
    await _context.SaveChangesAsync();

    _context.AddRange(recipients
      .Select(recipient =>
      {
        recipient.NotificationId = notificationEntity.Id;
        return recipient;
      })
      .Select(_modelEntityConverter.ToEntity));
    await _context.SaveChangesAsync();
  }

  public async Task MarkNotificationAsSeen(
    string notificationId,
    string representativeId
  )
  {
    var notification = await _context.NotificationRecipients
      .Where(_context.ForeignKeyEquals<NotificationRecipientEntity>(
        nameof(NotificationRecipientEntity.Notification),
        notificationId))
      .Where(_context.ForeignKeyEquals<NotificationRecipientEntity>(
        nameof(NotificationRecipientEntity.Representative),
        representativeId))
      .FirstOrDefaultAsync();
    if (notification is null || notification.SeenOn is not null)
    {
      return;
    }

    notification.SeenOn = DateTimeOffset.UtcNow;
    _context.Update(notification);
    await _context.SaveChangesAsync();
  }
}
