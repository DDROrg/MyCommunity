using System;
using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;

using MyCommunity.API.Controllers;
using MyCommunity.DAL;
using MyCommunity.DAL.DTO;
using MyCommunity.DAL.Models;
using MyCommunity.Utility.Extensions;

namespace MyCommunity.APITest
{
    [Collection("Sequential")]
    public class AppartmentControllerTest
    {
        //private readonly IFixture _fixture;
        private Mock<ILogger<AppartmentController>> _mockLogger;
        private Mock<ICommunityRepository> _mockRepo;
        private ICommunityRepository _repo;
        private IMapper _mapper;
        private AppartmentController _controller; 

        public AppartmentControllerTest(){
            //_fixture = new Fixture();
            _mockLogger = new Mock<ILogger<AppartmentController>>();
            _mockRepo = new Mock<ICommunityRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
           

            if(new AppConfigration().IsRealMongoDbUsed()){
                var dbConfig = new AppConfigration().GetMongoDBConfig();
                var dbContext = new CommunityContext(dbConfig);
                _repo = new CommunityRepository(dbContext);
                var dbSeeder = new CommunitySeeder(_repo);
                dbSeeder.Seed();

                _controller = new AppartmentController(_mockLogger.Object, _repo, _mapper);
            }else{
                _controller = new AppartmentController(_mockLogger.Object, _mockRepo.Object, _mapper);
            }
        }

        [Fact(DisplayName = "Get Appartments")]
        public async Task AppartmentGet()
        {   
            // Setup 
            if(! new AppConfigration().IsRealMongoDbUsed()){
                _mockRepo.Setup(repo => repo.GetAllAppartments())
                    .ReturnsAsync(GetAllTestAppartments())
                    .Verifiable();
            }

            // Act        
            var result = await _controller.Get();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<AppartmentDTO>>>(result);
            var objResult = Assert.IsType<ObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<AppartmentDTO>>(objResult.Value);
            Assert.Single(returnValue);
        }

        [Fact(DisplayName = "Get Appartment by ID [with valid ID]")]
        public async Task AppartmentGetById_ValidID()
        {               
            // Setup 
            string aptId = "d14ed311-6ea5-4d0c-8e89-b7f7fe10b9ff";
            if(! new AppConfigration().IsRealMongoDbUsed()){
                _mockRepo.Setup(repo => repo.GetAppartment(aptId))
                    .ReturnsAsync(GetTestAppartment(aptId))
                    .Verifiable();
            }

            // Act        
            var result = await _controller.Get(aptId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<AppartmentDTO>>(result);
            var objResult = Assert.IsType<ObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<AppartmentDTO>(objResult.Value);
            Assert.Equal("d14ed311-6ea5-4d0c-8e89-b7f7fe10b9ff", returnValue.id);
        }

        [Fact(DisplayName = "Get Appartment by ID [with invalid ID]")]
        public async Task AppartmentGetById_InvalidID()
        {   
            // Setup 
            string aptId = "d14ed311-6ea5-4d0c-8e89-b7f7fe10b9fe";
            if(! new AppConfigration().IsRealMongoDbUsed()){
                _mockRepo.Setup(repo => repo.GetAppartment(aptId))
                    .ReturnsAsync(GetTestAppartment(aptId))
                    .Verifiable();
            }

            // Act        
            var result = await _controller.Get(aptId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<AppartmentDTO>>(result);
            var objResult = Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact(DisplayName = "Create Appartment [with valid object]")]
        public async Task AppartmentCreate_Valid()
        {   
            // Setup 
            AppartmentDTO testApt = new AppartmentDTO(){
                name = "Test 1",
                address = new AddressDTO(){
                    buildingNo = "142A", 
                    street = "Test Road"
                }
            };
            Appartment apt = _mapper.Map<Appartment>(testApt);
            if(! new AppConfigration().IsRealMongoDbUsed()){
                _mockRepo.Setup(repo => repo.CreateAppartment(apt))
                    .Verifiable();
            }

            // Act        
            var result = await _controller.Post(testApt);

            // Assert
            var actionResult = Assert.IsType<ActionResult<AppartmentDTO>>(result);
            var okObjResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<AppartmentDTO>(okObjResult.Value);
            Assert.Equal("ACTIVE", returnValue.status);
        }

        [Fact(DisplayName = "Create Appartment [with invalid object]")]
        public async Task AppartmentCreate_Invalid()
        {   
            // Setup 
            AppartmentDTO testApt = new AppartmentDTO(){
                address = new AddressDTO(){
                    buildingNo = "142A", 
                    street = "Test Road"
                }
            };
            Appartment apt = _mapper.Map<Appartment>(testApt);
            if(! new AppConfigration().IsRealMongoDbUsed()){
                _mockRepo.Setup(repo => repo.CreateAppartment(apt))
                    .Verifiable();
            }

            // Act        
            var result = await _controller.Post(testApt);

            // Assert
            var actionResult = Assert.IsType<ActionResult<AppartmentDTO>>(result);
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<AppartmentDTO>(badRequestObjectResult.Value);
        }

        //=========================================================
        private IEnumerable<Appartment> GetAllTestAppartments()
        {
            var retVal = new List<Appartment>();
            retVal.Add(new Appartment()
            {
                id = "d14ed311-6ea5-4d0c-8e89-b7f7fe10b9ff",
                customerId = "3VZX-FNRT-GWYA6",
                name = "SUPPORT",
                status = "ACTIVE",
                createdOn = DateTime.Now,
                modifiedOn = DateTime.Now,
                address = new Address() { buildingNo = "90/A/2", street = "Gouranga Sarani" }
            });
            return retVal;
        }

        private Appartment GetTestAppartment(string id)
        {
            return GetAllTestAppartments().Where(i => i.id == id).FirstOrDefault();
        }


    }
}
