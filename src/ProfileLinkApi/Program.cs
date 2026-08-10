using System.Text.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string DataFile = "profiles.json";

// Helper Function: Read data from JSON
List<UserProfile> GetProfiles()
{
    if (!File.Exists(DataFile)) return new List<UserProfile>();
    var json = File.ReadAllText(DataFile);
    return JsonSerializer.Deserialize<List<UserProfile>>(json) ?? new List<UserProfile>();
}

// Helper Function: Write data to JSON
void SaveProfiles(List<UserProfile> profiles)
{
    var json = JsonSerializer.Serialize(profiles, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(DataFile, json);
}

// Helper Function: Generate 6-character short ID
string GenerateShortId()
{
    return Guid.NewGuid().ToString("N").Substring(0, 6);
}

// Helper Function: Safe Text (XSS Protection and Trim)
string Sanitize(string? input)
{
    if (string.IsNullOrWhiteSpace(input)) return string.Empty;
    return WebUtility.HtmlEncode(input.Trim());
}

// ENDPOINT 1: Create Profile (POST)
app.MapPost("/api/profiles", (UserProfile inputProfile) =>
{
    // Validation: Name is strictly required
    if (string.IsNullOrWhiteSpace(inputProfile.Name))
    {
        return Results.BadRequest(new { error = "Name is strictly required.", code = 400 });
    }

    // Validation: Name max length
    if (inputProfile.Name.Length > 50)
    {
        return Results.BadRequest(new { error = "Name cannot exceed 50 characters.", code = 400 });
    }

    // Validation: Name Regex (Letters and spaces only)
    if (!Regex.IsMatch(inputProfile.Name, @"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$"))
    {
        return Results.BadRequest(new { error = "Name can only contain letters and spaces.", code = 400 });
    }

    // Validation: Headline max length
    if (inputProfile.Headline != null && inputProfile.Headline.Length > 100)
    {
        return Results.BadRequest(new { error = "Headline cannot exceed 100 characters.", code = 400 });
    }

    // Data Sanitization
    inputProfile.Name = Sanitize(inputProfile.Name);
    inputProfile.Headline = Sanitize(inputProfile.Headline);

    // Clean Skills array (remove empty, sanitize, and remove duplicates)
    if (inputProfile.Skills != null)
    {
        inputProfile.Skills = inputProfile.Skills
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(Sanitize)
            .Distinct()
            .ToList();
    }
    else 
    {
        inputProfile.Skills = new List<string>();
    }

    var profiles = GetProfiles();

    // Duplicate Profile Check (Prevents exact same profile from being created again)
    bool isDuplicate = profiles.Any(p => 
        p.Name == inputProfile.Name && 
        p.Headline == inputProfile.Headline && 
        p.Skills.SequenceEqual(inputProfile.Skills));

    if (isDuplicate)
    {
        return Results.Conflict(new { error = "A profile with these exact details already exists.", code = 409 });
    }

    inputProfile.Id = GenerateShortId();
    profiles.Add(inputProfile);
    SaveProfiles(profiles);

    var shareableLink = $"/api/profiles/{inputProfile.Id}";
    
    return Results.Created(shareableLink, new { 
        Message = "Profile created successfully.", 
        Id = inputProfile.Id,
        Link = shareableLink 
    });
});

// ENDPOINT 2: View Profile (GET)
app.MapGet("/api/profiles/{id}", (string id) =>
{
    var profiles = GetProfiles();
    var profile = profiles.FirstOrDefault(p => p.Id == id);

    if (profile == null)
    {
        return Results.NotFound(new { error = "Profile not found.", code = 404 });
    }

    return Results.Ok(profile);
});

// ENDPOINT 3: Update Profile (PUT)
app.MapPut("/api/profiles/{id}", (string id, UserProfile updatedProfile) =>
{
    var profiles = GetProfiles();
    var existingProfile = profiles.FirstOrDefault(p => p.Id == id);

    if (existingProfile == null)
    {
        return Results.NotFound(new { error = "Profile not found.", code = 404 });
    }

    // Validation: Name is strictly required
    if (string.IsNullOrWhiteSpace(updatedProfile.Name))
    {
        return Results.BadRequest(new { error = "Name is strictly required.", code = 400 });
    }

    // Validation: Name max length
    if (updatedProfile.Name.Length > 50)
    {
        return Results.BadRequest(new { error = "Name cannot exceed 50 characters.", code = 400 });
    }

    // Validation: Name Regex
    if (!Regex.IsMatch(updatedProfile.Name, @"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$"))
    {
        return Results.BadRequest(new { error = "Name can only contain letters and spaces.", code = 400 });
    }

    // Validation: Headline max length
    if (updatedProfile.Headline != null && updatedProfile.Headline.Length > 100)
    {
        return Results.BadRequest(new { error = "Headline cannot exceed 100 characters.", code = 400 });
    }

    // Sanitization and Assignment
    existingProfile.Name = Sanitize(updatedProfile.Name);
    existingProfile.Headline = Sanitize(updatedProfile.Headline);
    
    if (updatedProfile.Skills != null)
    {
        existingProfile.Skills = updatedProfile.Skills
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(Sanitize)
            .Distinct()
            .ToList();
    }
    else
    {
        existingProfile.Skills = new List<string>();
    }

    SaveProfiles(profiles);

    return Results.Ok(new { Message = "Profile updated successfully.", Profile = existingProfile });
});

// ENDPOINT 4: Delete Profile (DELETE)
app.MapDelete("/api/profiles/{id}", (string id) =>
{
    var profiles = GetProfiles();
    var profileToRemove = profiles.FirstOrDefault(p => p.Id == id);

    if (profileToRemove == null)
    {
        return Results.NotFound(new { error = "Profile not found.", code = 404 });
    }

    profiles.Remove(profileToRemove);
    SaveProfiles(profiles);

    return Results.Ok(new { Message = "Profile deleted successfully." });
});

app.Run();

// Data Model (Must be placed at the bottom for top-level statements structure)
public class UserProfile
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Headline { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
}