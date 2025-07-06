using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Model.Queries;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;
using Restock.Platform.API.Profiles.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Restock.Platform.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile Endpoints.")]
public class ProfilesController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService)
: ControllerBase
{
    [HttpGet("{profileId:int}")]
    [SwaggerOperation("Get Profile by Id", "Get a profile by its unique identifier.", OperationId = "GetProfileById")]
    [SwaggerResponse(200, "The profile was found and returned.", typeof(ProfileResource))]
    [SwaggerResponse(404, "The profile was not found.")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if (profile is null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
    
    [HttpGet]
    [SwaggerOperation("Get All Profiles", "Get all profiles.", OperationId = "GetAllProfiles")]
    [SwaggerResponse(200, "The profiles were found and returned.", typeof(IEnumerable<ProfileResource>))]
    [SwaggerResponse(404, "The profiles were not found.")]
    public async Task<IActionResult> GetAllProfiles()
    {
        var getAllProfilesQuery = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(getAllProfilesQuery);
        var profileResources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(profileResources);
    }

    
    //Commands

    [HttpPost]
    [SwaggerOperation("Create Profile", "Create a new profile.", OperationId = "CreateProfile")]
    [SwaggerResponse(201, "The profile was created.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile was not created.")]
    public async Task<IActionResult> CreateProfile(CreateProfileResource resource)
    {
        var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var profile = await profileCommandService.Handle(createProfileCommand);
        if (profile is null) return BadRequest();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return CreatedAtAction(nameof(GetProfileById), new { profileId = profile.Id }, profileResource);
    }
     
    [HttpDelete("{profileId:int}")]
    [SwaggerOperation(
        Summary     = "Delete a profile",
        Description = "Deletes the profile with the given id.",
        OperationId = "DeleteProfile")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Profile deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> DeleteProfile(int profileId)
    {
        await profileCommandService.Handle(new DeleteProfileCommand(profileId));
        return NoContent();
    }
 
    [HttpPut("{profileId:int}")]
    [SwaggerOperation(
        Summary     = "Update an profile",
        Description = "Updates the profile's details.",
        OperationId = "UpdateProfile")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Profile updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> UpdateProfile(
        int profileId,
        [FromBody] UpdateProfileResource resource)
    {
        var cmd = UpdateProfileCommandFromResourceAssembler
            .ToCommandFromResource(profileId, resource);
        await profileCommandService.Handle(cmd);
        return NoContent();
    } 
}