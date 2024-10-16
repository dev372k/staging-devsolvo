using Microsoft.AspNetCore.Mvc;
using BL.Developer;
using BL.Developer.DTOs.Request;
using SharedKernel.Commons;
using SharedKernel.Extensions;

namespace API.Controllers;

[Route(DevContants.BASE_ENDPOINT)]
[ApiController]
public class DeveloperController(DeveloperServices _service) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id) =>
       Ok(await _service.GetAsync(id).ToResponseAsync(message: Messages.RECORD_FETCHED));

    [HttpGet]
    public async Task<IActionResult> Get(int pageSize = 10, int pageNo = 1) =>
       Ok(await _service.GetAsync(pageSize, pageNo).ToResponseAsync(message: Messages.RECORD_FETCHED));

    [HttpPost]
    public async Task<IActionResult> Post(AddDeveloperDto request) =>
       Ok(await _service.AddAsync(request).ToResponseAsync(message: Messages.RECORD_INSERTED));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Post(Guid id, UpdateDeveloperDto request) =>
       Ok(await _service.UpdateAsync(id, request).ToResponseAsync(message: Messages.RECORD_UPDATED));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Post(Guid id) =>
       Ok(await _service.DeleteAsync(id).ToResponseAsync(message: Messages.RECORD_DELETED));
}
