namespace API.Models
{
    /// <summary>
    /// Represents a task model that can be stored, updated, and retrieved from the database.  
    /// </summary>
    public class Todo
    {
        /// <summary>
        /// Unique identifier for the task.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title or description of the task.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Indicates whether the task is completed.
        /// </summary>
        public bool Done { get; set; }

        /// <summary>
        /// Date and time the task was created.
        /// </summary>
        public DateTime Date { get; set; } = DateTime.Now;
    }
}