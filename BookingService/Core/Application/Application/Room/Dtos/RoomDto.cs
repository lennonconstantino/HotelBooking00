﻿using Domain.Enums;

namespace Application.Room.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public decimal Price { get; set; }
        public AcceptedCurrencies Currency { get; set; }

        public static Domain.Room.Entities.Room MapToEntity(RoomDto dto)
        {
            return new Domain.Room.Entities.Room
            {
                Id = dto.Id,
                Name = dto.Name,
                Level = dto.Level,
                InMaintenance = dto.InMaintenance,
                Price = new Domain.Guest.ValueObjects.Price { Currency = dto.Currency, Value = dto.Price }
            };
        }
    }
}
