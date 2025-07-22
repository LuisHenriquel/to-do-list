using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    /// <summary>
    /// Represents the data received in the HTTP request body to create a new Todo item.
    /// </summary>
    public class CreateTodoViewModel
    {
        /// <summary>
        /// Title of the task. This field is required and must be provided in the request body.
        /// If missing, the API will return a validation error.
        /// </summary>
        [Required]
        public string Title { set; get; }
    }
}