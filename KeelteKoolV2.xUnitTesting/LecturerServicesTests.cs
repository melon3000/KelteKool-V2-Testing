using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using Xunit;

namespace KeelteKoolV2.xUnitTesting;

public class LecturerServicesTests : TestBase
{
    [Fact]
    public async Task Should_CreateObject_WhenResultIsReturned()
    {
        var dto = CreateDefaultDTO();
        var result = await Svc<ILecturerServices>().Create(dto);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_GetLecturerDetails_WhenIdIsNotNull()
    {
        var dto = CreateDefaultDTO();
        var created = await Svc<ILecturerServices>().Create(dto);
        var result = await Svc<ILecturerServices>().DetailAsync(created.Id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_UpdateFirstNameWithNewData_WhenDataIsDifferentFromDB()
    {
        var dto = CreateDefaultDTO();
        var created = await Svc<ILecturerServices>().Create(dto);
        var originalName = created.FirstName;
        var newDto = new LecturerDTO
        {
            Id = created.Id,
            FirstName = "Uusnimi",
            LastName = created.LastName,
            Qualifications = created.Qualifications,
            UserID = created.UserID
        };
        var result = await Svc<ILecturerServices>().Update(newDto);
        Assert.NotNull(result);
        Assert.NotEqual(originalName, result.FirstName);
    }

    [Fact]
    public async Task Should_DeleteLecturer_WhenValidIdIsGiven()
    {
        var dto = CreateDefaultDTO();
        var created = await Svc<ILecturerServices>().Create(dto);
        var deletedLecturer = await Svc<ILecturerServices>().Delete(created.Id);
        var result = await Svc<ILecturerServices>().DetailAsync(created.Id);
        Assert.Null(result);
    }

    private LecturerDTO CreateDefaultDTO()
    {
        return new LecturerDTO
        {
            FirstName = "Test",
            LastName = "Test",
            Qualifications = "Test"
        };
    }
}
