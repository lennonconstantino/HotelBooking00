using Application;
using Application.Guest;
using Application.Guest.Dtos;
using Application.Guest.Request;
using Domain.Guest.Entities;
using Domain.Guest.Enums;
using Domain.Guest.Ports;
using Moq;
using NuGet.Frameworks;

namespace ApplicationTests
{
    public class Tests
    {
        GuestManager _guestManager;

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

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.CreateGuest(request);
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

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.AreEqual(res.ErrorCodes, ErrorCodes.INVALID_PERSON_ID);
            Assert.AreEqual(res.Message, "The ID passed is not valid");
        }

        [TestCase("", "surnametest", "test@test.com")]
#pragma warning disable NUnit1001
        [TestCase(null, "surnametest", "test@test.com")]
        [TestCase("fulanotest", "", "test@test.com")]
        [TestCase("fulanotest", null, "test@test.com")]
        [TestCase("fulanotest", "surnametest", "")]
        [TestCase("fulanotest", "surnametest", null)]
#pragma warning restore NUnit1001
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

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.AreEqual(res.ErrorCodes, ErrorCodes.MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing Required Information passed is not valid");
        }

        [Test]
        public async Task Should_Return_GuestNotFound_When_GuestDoesntExist()
        { 
            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest?>(null));

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.GetGuest(333);

            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.AreEqual(res.ErrorCodes, ErrorCodes.GUEST_NOT_FOUND);
            Assert.AreEqual(res.Message, "No Guest record was found with the given Id");
        }

        [Test]
        public async Task Should_Return_Guest_Success()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            var fakeGuest = new Guest
            {
                Id = 333,
                Name = "Test",
                DocumentId = new Domain.Guest.ValueObjects.PersonId
                {
                    DocumentType = DocumentType.DriveLicence,
                    IdNumber = "123"
                }
            };

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult((Guest?)fakeGuest));

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.GetGuest(333);

            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.AreEqual(res.Data.Id, fakeGuest.Id);
            Assert.AreEqual(res.Data.Name, fakeGuest.Name);

        }
    }
}