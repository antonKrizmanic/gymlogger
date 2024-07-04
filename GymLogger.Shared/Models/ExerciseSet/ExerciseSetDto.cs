namespace GymLogger.Shared.Models.ExerciseSet
{
    public class ExerciseSetDto
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Reps { get; set; }
        public decimal? Time { get; set; }
        public string? Note { get; set; }
    }
}