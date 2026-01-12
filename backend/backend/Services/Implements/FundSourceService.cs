using AutoMapper;
using backend.DTOs.FundSource.Request;
using backend.DTOs.FundSource.Response;
using backend.Exceptions;
using backend.Models.Finance;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;

namespace backend.Services.Implements
{
    public class FundSourceService : IFundSourceService
    {
        private readonly IFundSourceRepository _repo;
        private readonly IMapper _mapper;

        public FundSourceService(IFundSourceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PageVO<FundSourceResponse>> GetAll(EntityFilter<FundSource> filter, EntitySort<FundSource> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<FundSourceResponse>>(pagedResult.Content);

            return new PageVO<FundSourceResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<FundSourceResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<FundSourceResponse>(entity);
        }

        public async Task<FundSourceResponse> Create(CreateFundSourceRequest request)
        {
            var entity = _mapper.Map<FundSource>(request);

            await _repo.AddAsync(entity);
            return _mapper.Map<FundSourceResponse>(entity);
        }

        public async Task<FundSourceResponse> Update(string id, UpdateFundSourceRequest request)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No funding source found with ID: {id}");

            _mapper.Map(request, entity);

            await _repo.UpdateAsync(entity);
            return _mapper.Map<FundSourceResponse>(entity);
        }

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No funding source found with ID: {id}");

            // Có thể thêm logic kiểm tra ràng buộc khóa ngoại trước khi xóa
            // Ví dụ: Check xem nguồn tiền này đã dùng trong ImportVoucher nào chưa?

            await _repo.DeleteAsync(entity);
        }
    }
}