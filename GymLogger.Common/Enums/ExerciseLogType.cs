using System.ComponentModel.DataAnnotations;

namespace GymLogger.Common.Enums
{
    public enum ExerciseLogType
    {
        [Display(Name = "")]
        Unknown = 0,

        [Display(Name = "Weight")]
        Weight = 1,

        [Display(Name = "Negative weight")]
        NegativeWeight = 2,

        [Display(Name = "Time")]
        Time = 3,
    }
}
