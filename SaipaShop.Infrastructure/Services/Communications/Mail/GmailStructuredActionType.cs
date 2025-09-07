namespace SaipaShop.Infrastructure.Services.Communications.Mail;

public enum GmailStructuredActionType
{
    /// <summary>نمایش یک صفحه یا سند (فاکتور، سفارش، مقاله و...)</summary>
    ViewAction,

    /// <summary>تأیید ایمیل، عضویت یا هر درخواست تأییدی</summary>
    ConfirmAction,

    /// <summary>رهگیری وضعیت سفارش یا ارسال</summary>
    TrackAction,

    /// <summary>ثبت حضور یا پاسخ به دعوت‌نامه</summary>
    RsvpAction,

    /// <summary>ثبت امتیاز یا بازخورد</summary>
    ReviewAction,

    /// <summary>افزودن یک ایونت یا آیتم به لیست کاربر (مثلاً ذخیره پرواز)</summary>
    SaveAction,

    /// <summary>ایجاد پاسخ ایمیلی سریع</summary>
    SendEmailAction,

    /// <summary>مشاهده اطلاعات رزرو (پرواز، هتل و...)</summary>
    ViewReservationAction
}