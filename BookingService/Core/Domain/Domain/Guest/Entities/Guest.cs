﻿using Domain.Guest.Exceptions;
using Domain.Guest.Ports;
using Domain.Guest.ValueObjects;

namespace Domain.Guest.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }

        public bool IsValid()
        {
            try
            {
                this.ValidateState();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void ValidateState()
        {
            if (DocumentId == null 
                || string.IsNullOrEmpty(DocumentId.IdNumber) 
                || DocumentId.IdNumber.Length <= 3 
                || DocumentId.DocumentType == 0)
            {
                throw new InvalidPersonDocumentIdException();
            }

            if (string.IsNullOrEmpty(Name) 
                || string.IsNullOrEmpty(Surname) 
                || string.IsNullOrEmpty(Email))
            {
                throw new MissingRequiredInformationExcpetion();
            }

            if (!Utils.ValidateEmail(Email))
            { 
                throw new InvalidEmailException();
            }

        }

        public async Task Save(IGuestRepository guestRepository)
        {
            ValidateState();

            if (Id == 0)
            {
                Id = await guestRepository.Create(this);
            }
            else 
            {
                // await guestRepository.Update(this);
            }
        }
    }
}
