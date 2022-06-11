using AutoMapper;
using LearningCenter.API.GettingBetter_System.Resources;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Resources;
using LearningCenter.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LearningCenter.API.GettingBetter_System.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    public StudentsController(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<StudentResource>> GetAllAsync()
    {
        var students = await _studentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

        return resources;

    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveStudentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var student = _mapper.Map<SaveStudentResource, Student>(resource);

        var result = await _studentService.SaveAsync(student);

        if (!result.Success)
            return BadRequest(result.Message);

        var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);

        return Ok(studentResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveStudentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var student = _mapper.Map<SaveStudentResource, Student>(resource);

        var result = await _studentService.UpdateAsync(id, student);

        if (!result.Success)
            return BadRequest(result.Message);

        var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);

        return Ok(studentResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _studentService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);

        return Ok(studentResource);
    }
    
}