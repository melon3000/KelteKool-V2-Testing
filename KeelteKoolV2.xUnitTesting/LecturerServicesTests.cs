using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using Xunit;

namespace KeelteKoolV2.xUnitTesting;

public class LecturerServicesTests : TestBase
{
    /// <summary>
    /// Kontrollib, et Lecturer luuakse korrektselt ja kõik vajalikud väljad salvestatakse õigete väärtustega.
    /// </summary>
    [Fact]
    public async Task Should_CreateObject_WhenResultIsReturned()
    {
        var dto = CreateDefaultDTO();
        var result = await Svc<ILecturerServices>().Create(dto);
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(dto.FirstName, result.FirstName);
        Assert.Equal(dto.LastName, result.LastName);
        Assert.Equal(dto.Qualifications, result.Qualifications);
    }

    /// <summary>
    /// Kontrollib, et Lecturerri andmed leitakse õige ID järgi ja tagastatud andmed on õiged.
    /// </summary>
    [Fact]
    public async Task Should_GetLecturerDetails_WhenIdIsNotNull()
    {
        var dto = CreateDefaultDTO();
        var created = await Svc<ILecturerServices>().Create(dto);
        var result = await Svc<ILecturerServices>().DetailAsync(created.Id);
        Assert.NotNull(result);
        Assert.Equal(created.Id, result.Id);
        Assert.Equal(created.FirstName, result.FirstName);
        Assert.Equal(created.LastName, result.LastName);
        Assert.Equal(created.Qualifications, result.Qualifications);
    }

    /// <summary>
    /// Kontrollib, et Lecturerri andmeid saab uuendada ning muudatused salvestatakse õigesti.
    /// </summary>
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
        Assert.Equal(created.Id, result.Id);
        Assert.Equal("Uusnimi", result.FirstName);
        Assert.NotEqual(originalName, result.FirstName);
        Assert.Equal(created.LastName, result.LastName);
        Assert.Equal(created.Qualifications, result.Qualifications);
    }

    /// <summary>
    /// Kontrollib, et olemasoleva Lecturerri saab kustutada ning pärast kustutamist teda enam andmebaasist ei leita.
    /// </summary>
    [Fact]
    public async Task Should_DeleteLecturer_WhenValidIdIsGiven()
    {
        var dto = CreateDefaultDTO();
        var created = await Svc<ILecturerServices>().Create(dto);
        var deletedLecturer = await Svc<ILecturerServices>().Delete(created.Id);
        var result = await Svc<ILecturerServices>().DetailAsync(created.Id);
        Assert.NotNull(deletedLecturer);
        Assert.Equal(created.Id, deletedLecturer.Id);
        Assert.Equal(created.FirstName, deletedLecturer.FirstName);
        Assert.Equal(created.LastName, deletedLecturer.LastName);
        Assert.Null(result);
    }

    /// <summary>
    /// Kontrollib, et olematu ID korral Lecturer andmeid ei tagastata.
    /// </summary>
    [Fact]
    public async Task Should_ReturnNull_WhenLecturerIdDoesNotExist()
    {
        var result = await Svc<ILecturerServices>().DetailAsync(Guid.NewGuid());
        Assert.Null(result);
    }

    /// <summary>
    /// Kontrollib, et olematu Lecturer ID uuendamisel tagastatakse null.
    /// </summary>
    [Fact]
    public async Task Should_ReturnNull_WhenUpdatingNonExistingLecturer()
    {
        var dto = CreateDefaultDTO();
        dto.Id = Guid.NewGuid();
        var result = await Svc<ILecturerServices>().Update(dto);
        Assert.Null(result);
    }

    /// <summary>
    /// Kontrollib, et olematu Lecturer ID kustutamisel tagastatakse null.
    /// </summary>
    [Fact]
    public async Task Should_ReturnNull_WhenDeletingNonExistingLecturer()
    {
        var result = await Svc<ILecturerServices>().Delete(Guid.NewGuid());
        Assert.Null(result);
    }

    /// <summary>
    /// Kontrollib, et tühja eesnime korral määratakse Lecturer eesnimeks vaikimisi John.
    /// </summary>
    [Fact]
    public async Task Should_SetDefaultFirstName_WhenFirstNameIsEmpty()
    {
        var dto = CreateDefaultDTO();
        dto.FirstName = "";
        var result = await Svc<ILecturerServices>().Create(dto);
        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
    }

    /// <summary>
    /// Kontrollib, et tühja perekonnanime korral määratakse Lecturer perekonnanimeks vaikimisi Doe.
    /// </summary>
    [Fact]
    public async Task Should_SetDefaultLastName_WhenLastNameIsEmpty()
    {
        var dto = CreateDefaultDTO();
        dto.LastName = "";
        var result = await Svc<ILecturerServices>().Create(dto);
        Assert.NotNull(result);
        Assert.Equal("Doe", result.LastName);
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
