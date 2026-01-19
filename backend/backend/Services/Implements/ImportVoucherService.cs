using AutoMapper;
using backend.DTOs.Import.Request;
using backend.DTOs.Import.Response;
using backend.Enums;
using backend.Exceptions;
using backend.Models;
using backend.Models.Import;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using backend.vo;
using Plainquire.Filter;
using Plainquire.Sort;
using backend.Utils;
using backend.Models.EquipmentInfo;

namespace backend.Services.Implements
{
    public class ImportVoucherService : IImportVoucherService
    {
        private readonly IImportVoucherRepository _repo;
        private readonly IEquipmentRepository _equipmentRepo;
        private readonly IMapper _mapper;
        private readonly JwtUtils _jwtUtils;

        public ImportVoucherService(IImportVoucherRepository repo, IEquipmentRepository equipmentRepository, IMapper mapper, JwtUtils jwt)
        {
            _repo = repo;
            _equipmentRepo = equipmentRepository;
            _mapper = mapper;
            _jwtUtils = jwt;
        }

        public async Task<PageVO<ImportVoucherResponse>> GetAll(EntityFilter<ImportVoucher> filter, EntitySort<ImportVoucher> sort, int page, int size)
        {
            var pagedResult = await _repo.GetPagedAsync(filter, sort, page, size);

            var dtoList = _mapper.Map<List<ImportVoucherResponse>>(pagedResult.Content);

            return new PageVO<ImportVoucherResponse>(
                pagedResult.Page,
                pagedResult.Size,
                pagedResult.TotalElements,
                dtoList
            );
        }

        public async Task<ImportVoucherResponse?> GetById(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<ImportVoucherResponse>(entity);
        }

        public async Task<ImportVoucherResponse> Create(CreateImportVoucherRequest request)
        {
            var allEquipments = new List<Equipment>();

            var entity = _mapper.Map<ImportVoucher>(request);

            entity.CreatedBy = _jwtUtils.GetCurrentUserId();

            await _repo.AddAsync(entity);

            foreach (var detail in request.Details)
            {
                for (int i = 0; i < detail.Quantity; i++)
                {
                    allEquipments.Add(new Equipment
                    {
                        EquipmentName = detail.EquipmentName,
                        Note = detail.Note,
                    });
                }
            }

            if (allEquipments.Any())
            {
                await _equipmentRepo.AddRangeAsync(allEquipments);
            }

            return _mapper.Map<ImportVoucherResponse>(entity);
        }

        //public async Task<ImportVoucherResponse> Update(string id, UpdateImportVoucherRequest request)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) throw new NotFoundException($"No import voucher found with ID: {id}");

        //    _mapper.Map(request, entity);

        //    await _repo.UpdateAsync(entity);
        //    return _mapper.Map<ImportVoucherResponse>(entity);
        //}

        public async Task Delete(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"No import voucher found with ID: {id}");

            await _repo.DeleteAsync(entity);
        }
    }
}