using System.ComponentModel.DataAnnotations;

namespace GymLogger.Common.Enums
{
    public enum ExerciseLogType
    {
        [Display(Name = "")]
        Unknown = 0,

        [Display(Name = "Vježba s utezima")]
        Weight = 1,

        [Display(Name = "Vježba uz asistenciju")]
        NegativeWeight = 2,

        [Display(Name = "Vrijeme")]
        Time = 3,
    }
}
