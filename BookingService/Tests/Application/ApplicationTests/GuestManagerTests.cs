using Application;
using Application.Guest.DTO;
using Application.Guest.Request;
using Domain.Entities;
using Domain.Ports;

namespace ApplicationTests
{
    class FakeRepo : IGuestRepository
    {
        public Task<int> Create(Guest guest)
        {
            return Task.FromResult(111);    
        }

        public Task<Guest> Get(int Id) 
        {
            throw new System.NotImplementedException();
        }
    }
    public class Tests
    {
        GuestManager guestManager;

        [SetUp]
        public void Setup()
        {
            var fakeRepo = new FakeRepo();
            guestManager = new GuestManager(fakeRepo);
        }

        [Test]
        public async Task Test1Async()
        {
            var guestDto = new GuestDTO
            { Name = "Fulano", Surname = "Cilano", Email = "abc@email.com", IdNumber = "abc", IdTypeCode = 1 };

            var request = new CreateGuestRequest() 
            {
                Data = guestDto,
            };
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);

        }
    }
}