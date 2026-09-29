using Moq;
using OrganisationSystem.Data;
using OrganisationSystem.Models;
using OrganisationSystem.Validator;
using OrganisationSystem.Services;
using OrganisationSystem.Models.Enums;
using Microsoft.EntityFrameworkCore.Update.Internal;
using OrganisationSystem.Models.DTOs;
namespace OrganisationSystem.Tests

{
    public class VolunteerServerTests
    {


        Mock<IGenericRepo<Volunteer>> volunteerRepoMock = new ();
        Mock<IGenericRepo<Organisations>> organisationRepoMock = new();
        VolunteerValidator volunteerValidation = new ();
        VolunteerService sut;

        public VolunteerServerTests()
        {
              sut = new VolunteerService(
                volunteerRepoMock.Object,       
                organisationRepoMock.Object,    
                volunteerValidation);           
        }

        [Fact]
        public void UpdateVolunteer_ValidateVolunteerData_ReturnDtoAndUpdate()
        {
            //Arange

            var volunteer = new Volunteer 
            { 
                Id = 1,
                Name = "Test",
                Email = "Test@gmail.com",
                ReferenceId = "700000",
                OrganisationId = 5,
                PhoneNumber = "01000832333",
                DateOfBirth = new DateOnly(1995,4,3),
                MaritalStatus = MaritalStatus.Single

            };

            volunteerRepoMock.Setup(r => r.GetById(1)).Returns(volunteer);
           
            var updateVolunteerDto = new UpdateVolunteerDto
            {
                Name = "sama",
                Email = "sama@gmail.com",
                PhoneNumber = "01574839265",
                DateOfBirth= new DateOnly(2002,4,3),
                OrganisationId=5,
                Status = MaritalStatus.Single
            };

            organisationRepoMock.Setup(r => r.Exists(5)).Returns(true);


            //Act
            var result = sut.UpdateVolunteer(1, updateVolunteerDto);

            //Assert

            Assert.Equal("sama", result.Name);
            Assert.Equal(1, result.Id);
            volunteerRepoMock.Verify(r => r.SaveChanges(), Times.Once);

        }

        [Fact]
        public void UpdateVolunteer_VolunteerNotFound_ThrowExpection()
        {
            volunteerRepoMock.Setup(r => r.GetById(99)).Returns((Volunteer?)null);
            var dto = new UpdateVolunteerDto();

            // Act + Assert
            Assert.Throws<ArgumentException>(() => sut.UpdateVolunteer(99, dto));

            volunteerRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }

        [Fact]
        public void UpdateVolunteer_OrganisationNotFound_ThrowsArgumentException()
        {
            // Arrange
            var existingVolunteer = new Volunteer { Id = 1 };
            volunteerRepoMock.Setup(r => r.GetById(1)).Returns(existingVolunteer);
            organisationRepoMock.Setup(r => r.Exists(999)).Returns(false);

            var dto = new UpdateVolunteerDto
            {
                DateOfBirth = new DateOnly(1995, 1, 1),
                OrganisationId = 999
            };

            // Act + Assert
            Assert.Throws<ArgumentException>(() => sut.UpdateVolunteer(1, dto));

            volunteerRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }

        [Fact]
        public void UpdateVolunteer_InvalidDateOfBirth_ThrowsAndDoesNotSave()
        {
            // Arrange
            var existingVolunteer = new Volunteer { Id = 1 };
            volunteerRepoMock.Setup(r => r.GetById(1)).Returns(existingVolunteer);

            var dto = new UpdateVolunteerDto
            {
                DateOfBirth = new DateOnly(2030, 1, 1)   
            };

            // Act + Assert
            Assert.ThrowsAny<Exception>(() => sut.UpdateVolunteer(1, dto));

            volunteerRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }

        [Fact]
        public void GetVolunteerByPageSize_HasData_ReturnsCorrectPagedResult()
        {
            // Arrange
            var allVolunteers = new List<Volunteer>
        {
            new() { Id = 1, Name = "Ali" },
            new() { Id = 2, Name = "Sama" },
            new() { Id = 3, Name = "Omar" }
        };

            var pagedResponse = new List<VolunteerResponseDto>
        {
            new() { Id = 1, Name = "Ali" },
            new() { Id = 2, Name = "Sama" }
        };

            volunteerRepoMock.Setup(r => r.GetAll())
                .Returns(allVolunteers.AsQueryable());

            volunteerRepoMock.Setup(r => r.GetVolunteerByPageSize(2, 1))
                .Returns(pagedResponse.AsQueryable());

            // Act
            var result = sut.GetVolunteerByPageSize(2, 1);

            // Assert
            Assert.Equal(3, result.totalCount);
            Assert.Equal(2, result.pageSize);
            Assert.Equal(1, result.pageNumber);
           
        }
        [Fact]
        public void GetVolunteerByPageSize_NoData_ReturnsEmptyResult()
        {
            // Arrange
            volunteerRepoMock.Setup(r => r.GetAll())
                .Returns(new List<Volunteer>().AsQueryable());

            volunteerRepoMock.Setup(r => r.GetVolunteerByPageSize(10, 1))
                .Returns(new List<VolunteerResponseDto>().AsQueryable());

            // Act
            var result = sut.GetVolunteerByPageSize(10, 1);

            // Assert
            Assert.Equal(0, result.totalCount);
            Assert.Empty(result.item);
        }
        [Fact]
        public void Add_ValidData_CreatesAndReturnsVolunteer()
        {
            // Arrange
            var dto = new VolunteerDto
            {
                Name = "Ali",
                Email = "ali@mail.com",
                PhoneNumber = "0100000000",
                DateOfBirth = new DateOnly(1995, 1, 1),
                OrgnisationId = 5,
                MaritalStatus = MaritalStatus.Single
            };

            organisationRepoMock.Setup(r => r.Exists(5)).Returns(true);

            // Act
            var result = sut.Add(dto);

            // Assert
            Assert.Equal("Ali", result.Name);
            Assert.Equal("ali@mail.com", result.Email);
            Assert.Equal(5, result.OrganisationId);
            Assert.Equal(new DateOnly(1995, 1, 1), result.DateOfBirth);

        }

        [Fact]
        public void Add_OrganisationNotFound_ThrowsAndDoesNotAdd()
        {
            // Arrange
            var dto = new VolunteerDto
            {
                Name = "Ali",
                DateOfBirth = new DateOnly(1995, 1, 1),
                OrgnisationId = 999
            };

            organisationRepoMock.Setup(r => r.Exists(999)).Returns(false);

            // Act + Assert
            Assert.Throws<ArgumentException>(() => sut.Add(dto));
        }

        [Fact]
        public void Add_InvalidDateOfBirth_ThrowsAndDoesNotAdd()
        {
            // Arrange
            var dto = new VolunteerDto
            {
                Name = "Ali",
                DateOfBirth = new DateOnly(2030, 1, 1),   
                OrgnisationId = 5
            };

            // Act + Assert
            Assert.Throws<ArgumentException>(() => sut.Add(dto));
        }

        [Fact]
        public void GetById_VolunteerExists_ReturnsVolunteerDto()
        {
            // Arrange
            var volunteerDto = new VolunteerResponseDto
            {
                Id = 1,
                Name = "Ali",
                Email = "ali@mail.com"
            };

            volunteerRepoMock.Setup(r => r.GetVolunteerById(1)).Returns(volunteerDto);

            // Act
            var result = sut.GetById(1);

            // Assert
            Assert.Equal(1, result.Id);
            Assert.Equal("Ali", result.Name);
            Assert.Equal("ali@mail.com", result.Email);
        }

        [Fact]
        public void GetById_VolunteerNotFound_ThrowsArgumentException()
        {
            // Arrange
            volunteerRepoMock.Setup(r => r.GetVolunteerById(99)).Returns((VolunteerResponseDto?)null);

            // Act + Assert
            Assert.Throws<ArgumentException>(() => sut.GetById(99));
        }
    }
}

    


    
