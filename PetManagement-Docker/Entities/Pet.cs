using System;

namespace PetManager.Api.Entities
{
    public class Pet
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public string OwnerName{get;set;}
        public string Species{get;set;}
        public char Gender{get;set;}
        public DateTime? Birthdate{get;set;}
        public DateTime? Deathdate{get;set;}
    }
}