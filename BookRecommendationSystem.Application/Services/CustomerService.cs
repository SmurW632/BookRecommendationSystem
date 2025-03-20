using AutoMapper;
using BookRecommendationSystem.Application.Abstractions;
using BookRecommendationSystem.Application.DTOs;
using BookRecommendationSystem.Application.Helpers;
using BookRecommendationSystem.Domain.Entities;
using BookRecommendationSystem.Domain.ExceptionMessageConsts;
using BookRecommendationSystem.Domain.Repositories;

namespace BookRecommendationSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            Guard.AgainstNull(customer, ExMesConsts.CUSTOMER_NOT_FOUND);

            await _customerRepository.AddAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            Guard.AgainstNull(customer, ExMesConsts.CUSTOMER_NOT_FOUND);

            await _customerRepository.DeleteAsync(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            Guard.AgainstNull(customers, ExMesConsts.CUSTOMER_NOT_FOUND);

            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            Guard.AgainstNull(customer, ExMesConsts.CUSTOMER_NOT_FOUND);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateCustomerAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            Guard.AgainstNull(customer, ExMesConsts.CUSTOMER_NOT_FOUND);

            await _customerRepository.UpdateAsync(customer);
        }
    }
}
