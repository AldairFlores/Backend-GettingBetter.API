using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenter.API.Learning.Resources;

[SwaggerSchema(Required = new []{"FirstName","LastName","SelectedGame","NickName","Email","Password"})]

public class SaveCoachResource
{
    [SwaggerSchema("Coach FirstName")]
    [Required]
    public string FirstName { get; set; }
    
    [SwaggerSchema("Coach LastName")]
    [Required]
    public string LastName { get; set; }
    
    [SwaggerSchema("Coach SelectedGame")]
    [Required]
    public string SelectedGame { get; set; }
    
    [SwaggerSchema("Coach NickName")]
    [Required]
    public string NickName { get; set; }
    
    [SwaggerSchema("Coach Email")]
    [Required]
    public string Email { get; set; }
    
    [SwaggerSchema("Coach Password")]
    [Required]
    public string Password { get; set; }
}