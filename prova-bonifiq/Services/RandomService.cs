using ProvaPub.IRepository;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaPub.Services
{
    public class RandomService : IRandomService
    {
        private readonly INumberRepository _numberRepository;
        private readonly Random _random = new Random();

        public RandomService(INumberRepository numberRepository)
        {
            _numberRepository = numberRepository ?? throw new ArgumentNullException(nameof(numberRepository));
        }

        public async Task<int> GetRandomAsync()
        {
            var existingNumbers = await _numberRepository.GetAllNumbersAsync();

            var availableNumbers = Enumerable.Range(0, 100)
                                             .Except(existingNumbers)
                                             .ToList();

            if (!availableNumbers.Any())
                throw new InvalidOperationException("Todos os números já foram gerados.");

            var number = availableNumbers[_random.Next(availableNumbers.Count)];

            await _numberRepository.AddAsync(new RandomNumber { Number = number });

            return number;
        }
    }
}
