using Application;
using Application.Guest.DTO;
using Application.Guest.Request;
using Domain.Entities;
using Domain.Ports;
using Moq;
using NuGet.Frameworks;

namespace ApplicationTests
{
    public class Tests
    {
        GuestManager guestManager;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task HappyPath()
        {
            var guestDto = new GuestDTO
            { Name = "Fulano", Surname = "Cilano", Email = "abc@email.com", IdNumber = "2223", IdTypeCode = 1 };

            var expectedId = 222;

            var request = new CreateGuestRequest() 
            {
                Data = guestDto,
            };

            //
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())
            ).Returns(Task.FromResult(expectedId));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);
            Assert.AreEqual(res.Data.Name, guestDto.Name);

        }

        [TestCase("")]
#pragma warning disable NUnit1001 // The individual arguments provided by a TestCaseAttribute must match the type of the corresponding parameter of the method
        [TestCase(null)]
#pragma warning restore NUnit1001 // The individual arguments provided by a TestCaseAttribute must match the type of the corresponding parameter of the method
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var guestDto = new GuestDTO
            { Name = "Fulano", Surname = "Cilano", Email = "abc@email.com", IdNumber = docNumber, IdTypeCode = 1 };

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            //
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())
            ).Returns(Task.FromResult(2222));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.AreEqual(res.ErrorCodes, ErrorCodes.INVALID_PERSON_ID);
            Assert.AreEqual(res.Message, "The ID passed is not valid");
        }

        [TestCase("", "surnametest", "test@test.com")]
        [TestCase(null, "surnametest", "test@test.com")]
        [TestCase("fulanotest", "", "test@test.com")]
        [TestCase("fulanotest", null, "test@test.com")]
        [TestCase("fulanotest", "surnametest", "")]
        [TestCase("fulanotest", "surnametest", null)]
        public async Task Should_Return_MissingRequiredInformation_WhenDocsAreInvalid(string name, string surname, string email)
        {
            var guestDto = new GuestDTO
            { Name = name, Surname = surname, Email = email, IdNumber = "12345", IdTypeCode = 1 };

            var request = new CreateGuestRequest()
            {
                Data = guestDto,
            };

            //
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())
            ).Returns(Task.FromResult(2222));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.AreEqual(res.ErrorCodes, ErrorCodes.MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing Required Information passed is not valid");
        }
    }
}