using System.ComponentModel.DataAnnotations;

namespace GameStore.Business.Enum;

public enum BanDuration
{
    [Display(Name = "1 hour")]
    OneHour,

    [Display(Name = "1 day")]
    OneDay,

    [Display(Name = "1 week")]
    OneWeek,

    [Display(Name = "1 month")]
    OneMonth,

    [Display(Name = "Permanent")]
    Permanent
}