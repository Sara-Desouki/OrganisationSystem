using Moq;
using OrganisationSystem.ApplicationLayer.DTOs;
using OrganisationSystem.ApplicationLayer.Interfaces;
using OrganisationSystem.ApplicationLayer.Services;
using OrganisationSystem.Domain_Layer.Enums;
using OrganisationSystem.Domain_Layer.Models;

using OrganisationSystem.Models;
using OrganisationSystem.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrganisationSystem.Tests
{
    public class VolunteerOpportunityServiceTest
    {
        Mock<IGenericRepo<VolunteerOpportunity>> opportunityRepoMock = new();
        Mock<IGenericRepo<Organisations>> organisationRepoMock = new();

        VolunteerOpportunityService sut;

        public VolunteerOpportunityServiceTest() {
            sut = new VolunteerOpportunityService(opportunityRepoMock.Object, 
                organisationRepoMock.Object);

        }
        [Fact]
        public void Add_OrganisationNotFound_ThrowException()
        {
            //Arrang
            organisationRepoMock.Setup(r => r.Exists(4)).Returns(false);

            var dto = new VolunteerOpportunityDto()
            {
                OrganisationId = 4,
                Title = "Test"
            };
            //Act + Assert
              Assert.Throws<ArgumentException>(() => sut.Add(dto));
           
        }

        [Fact]

        public void Add_StartDateNotValid_ThrowExpcetion()
        {
            //Arrange

            var result = new VolunteerOpportunityDto()
            {
                OrganisationId = 1,
                StartDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-1)
            };

            organisationRepoMock.Setup(r => r.Exists(1)).Returns(true);

           

            //Act + Assert

            Assert.Throws<ArgumentException>(() => sut.Add(result));

        }
        [Fact]
        public void Add_EndDateNotValid_ThrowExpcetion() 
        {
            var result = new VolunteerOpportunityDto()
            {
                OrganisationId = 1,
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-1)
            };

            organisationRepoMock.Setup(r => r.Exists(1)).Returns(true);

            //Act + Assert

            Assert.Throws<ArgumentException>(() => sut.Add(result));
        }

        [Fact]
        public void Add_OrganisationIdFound_Adding()
        {
            var result = new VolunteerOpportunityDto()
            {
                OrganisationId = 1,
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today).AddDays(1),
                RequiredVolunteers = 4
            };

            organisationRepoMock.Setup(r => r.Exists(1)).Returns(true);

            //Act 
            var opportunity = sut.Add(result);
            //assert
            Assert.Equal(1, opportunity.OrganisationId);
            
            opportunityRepoMock.Verify(r => r.Add(It.IsAny<VolunteerOpportunity>()), Times.Once());
        }

        [Fact]
        public void GetByPageSize_PublishedOpportunities_ReturnsItemsAndCount()
        {
            var result =new List<object> { new { id = 1 }, new { id = 2 } }.AsQueryable();

            opportunityRepoMock.Setup(r => r.GetOpportuntyByPageSize(10, 1,
                It.IsAny<Expression<Func<VolunteerOpportunity , bool>>>() ,
                It.IsAny<Expression<Func<VolunteerOpportunity , object >>>() ))
                .Returns(result);

            var publishedOpportunities = new List<VolunteerOpportunity>
            {
               CreateOpportunity.CreatePublishedOpportunity("A"),
               CreateOpportunity.CreatePublishedOpportunity("B"),
               CreateOpportunity.CreatePublishedOpportunity("C")
            }.AsQueryable();


            opportunityRepoMock.Setup(r => r.GetOppertuntyCount(It.IsAny<Expression<Func<VolunteerOpportunity, bool>>>()))
                .Returns(publishedOpportunities);

            var ( items , count ) = sut.GetByPageSize(10, 1);

            Assert.Equal(2 ,items. Count() );
            Assert.Equal( 3 ,count );
    
        }

        [Fact]
        public void GetByPageSize_CallsRepoWithCorrectPaging()
        {
            // Arrange
            opportunityRepoMock
                .Setup(r => r.GetOpportuntyByPageSize(
                    It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<Expression<Func<VolunteerOpportunity, bool>>>(),
                    It.IsAny<Expression<Func<VolunteerOpportunity, object>>>()))
                .Returns(new List<object>().AsQueryable());

            opportunityRepoMock
                .Setup(r => r.GetOppertuntyCount(It.IsAny<Expression<Func<VolunteerOpportunity, bool>>>()))
                .Returns(new List<VolunteerOpportunity>().AsQueryable());

            // Act
            sut.GetByPageSize(pageSize: 5, pageNumber: 3);

            // Assert
            opportunityRepoMock.Verify(r => r.GetOpportuntyByPageSize(
                5, 3,
                It.IsAny<Expression<Func<VolunteerOpportunity, bool>>>(),
                It.IsAny<Expression<Func<VolunteerOpportunity, object>>>()),
                Times.Once());
        }

        [Fact]
        public void GetById_OpportunityNotFound_ThrowsKeyNotFoundException()
        {
            opportunityRepoMock
                .Setup(r => r.GetSpecificById(
                    99,
                    It.IsAny<Expression<Func<VolunteerOpportunity, OpportunityDetailsDto>>>()))
                .Returns((OpportunityDetailsDto)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => sut.GetById(99));
            Assert.Contains("99", ex.Message);
        }

      
        



    }
}
