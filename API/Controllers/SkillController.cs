using BL.Skills;
using BL.Skills.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Commons;
using SharedKernel.Extensions;

namespace API.Controllers;

[Route(DevContants.BASE_ENDPOINT)]
[ApiController]
public class SkillController(SkillServices _service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) =>
       Ok(await _service.GetAsync(id).ToResponseAsync(message: Messages.RECORD_FETCHED));
    
    [HttpGet("{developerId:guid}/developer")]
    public async Task<IActionResult> GetbyDeveloper(Guid developerId) =>
       Ok(await _service.GetbyDeveloperAsync(developerId).ToResponseAsync(message: Messages.RECORD_FETCHED));
    
    [HttpPost]
    public async Task<IActionResult> Post(AddSkillDto request) =>
       Ok(await _service.AddAsync(request).ToResponseAsync(message: Messages.RECORD_INSERTED));
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, UpdateSkillDto request) =>
       Ok(await _service.UpdateAsync(id, request).ToResponseAsync(message: Messages.RECORD_UPDATED));    
    
    [HttpPatch("{id:guid}/assign/{developerId:guid}")]
    public async Task<IActionResult> Patch(Guid id, Guid developerId) =>
       Ok(await _service.UpdateAssigneeAsync(id, developerId).ToResponseAsync(message: Messages.RECORD_UPDATED));
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) =>
       Ok(await _service.DeleteAsync(id).ToResponseAsync(message: Messages.RECORD_DELETED));

    [HttpDelete("{id:guid}/assign/{developerId:guid}")]
    public async Task<IActionResult> Delet(Guid id, Guid developerId) =>
       Ok(await _service.DeleteAssigneeAsync(id, developerId).ToResponseAsync(message: Messages.RECORD_UPDATED));
}
